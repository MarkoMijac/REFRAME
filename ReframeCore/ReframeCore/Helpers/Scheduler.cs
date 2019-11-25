using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReframeCore.Nodes;
using ReframeCore.ReactiveCollections;
using ReframeCore.Exceptions;

namespace ReframeCore.Helpers
{
    public class Scheduler : IScheduler, ILoggable
    {
        private List<Tuple<INode, INode>> ChildCollectionDependenciesToAdd { get; set; } = new List<Tuple<INode, INode>>();
        private List<Tuple<INode, INode>> RedirectionDependencesToRemove { get; set; } = new List<Tuple<INode, INode>>();
        private List<Tuple<INode, INode>> RedirectionDependencesToAdd { get; set; } = new List<Tuple<INode, INode>>();
        public UpdateLogger NodeLog { get; private set; } = new UpdateLogger();

        public bool EnableSkippingUpdateIfInitialNodeValueNotChanged { get; set; } = false;

        private IDependencyGraph Graph { get; set; }
        private ISorter Sorter { get; set; }

        public Scheduler(IDependencyGraph graph, ISorter sorter)
        {
            Graph = graph;
            Sorter = sorter;
        }

        #region Methods

        public IList<INode> GetNodesForUpdate()
        {
            MakeTemporaryAdjustmentsToGraph();
            IList<INode> nodesForUpdate = GetTopologicallySortedGraph();
            SetNodeLevels(nodesForUpdate);
            ResetGraphToInitialState();

            LogSchedule(nodesForUpdate);

            return nodesForUpdate;
        }

        private void LogSchedule(IList<INode> nodesForUpdate)
        {
            if (Graph.Settings.EnableLogging == true)
            {
                NodeLog.ClearLog();
                NodeLog.Log(nodesForUpdate);
            }
        }

        private void ResetGraphToInitialState()
        {
            ResetTemporaryDependenciesBetweenChildNodesAndCollectionNode();
            ResetRedirectionDependencies();
        }

        private void ResetRedirectionDependencies()
        {
            foreach (var d in RedirectionDependencesToRemove)
            {
                Graph.AddDependency(d.Item1, d.Item2);
            }

            foreach (var d in RedirectionDependencesToAdd)
            {
                Graph.RemoveDependency(d.Item1, d.Item2);
            }

            RedirectionDependencesToRemove.Clear();
            RedirectionDependencesToAdd.Clear();
        }

        private void ResetTemporaryDependenciesBetweenChildNodesAndCollectionNode()
        {
            foreach (var item in ChildCollectionDependenciesToAdd)
            {
                Graph.RemoveDependency(item.Item1, item.Item2);
            }

            ChildCollectionDependenciesToAdd.Clear();
        }

        private void MakeTemporaryAdjustmentsToGraph()
        {
            AddTemporaryDependenciesBetweenChildNodesAndCollectionNode();
        }

        private void AddTemporaryDependenciesBetweenChildNodesAndCollectionNode()
        {
            ChildCollectionDependenciesToAdd = DetermineVirtualChildVsCollectionNodeDependencies();

            foreach (var item in ChildCollectionDependenciesToAdd)
            {
                Graph.AddDependency(item.Item1, item.Item2);
            }
        }

        private List<Tuple<INode, INode>> DetermineVirtualChildVsCollectionNodeDependencies()
        {
            List<Tuple<INode, INode>> tempDependencies = new List<Tuple<INode, INode>>();

            foreach (var node in Graph.Nodes)
            {
                if (node.OwnerObject is ICollectionNodeItem)
                {
                    INode collectionNode = GraphUtility.GetCollectionNode((ICollectionNodeItem)node.OwnerObject, node.MemberName);

                    if (collectionNode != null)
                    {
                        tempDependencies.Add(new Tuple<INode, INode>(node, collectionNode));
                    }
                }
            }

            return tempDependencies;
        }

        private IList<INode> GetTopologicallySortedGraph()
        {
            return Sorter.Sort(Graph.Nodes);
        }

        private IList<INode> GetTopologicallySortedGraph(INode initialNode, bool omitInitialNode)
        {
            IList<INode> sortedGraph = Sorter.Sort(Graph.Nodes, initialNode);
            if (omitInitialNode == true)
            {
                sortedGraph.Remove(initialNode);
            }

            return sortedGraph;
        }

        private void SetNodeLevels(IList<INode> nodes)
        {
            for (int i = nodes.Count - 1; i >= 0; i--)
            {
                nodes[i].DetermineLevel();
            }
        }

        public IList<INode> GetNodesForUpdate(INode initialNode, bool omitInitialNode)
        {
            IList<INode> nodesForUpdate = null;
            INode initial = Graph.GetNode(initialNode);

            if (initial == null)
            {
                throw new NodeNullReferenceException("Reactive node set as initial node of the update process is not part of the graph!");
            }

            if (SkipUpdate(initial) == false)
            {
                MakeTemporaryAdjustmentsToGraph(initialNode, omitInitialNode);
                nodesForUpdate = GetTopologicallySortedGraph(initial, omitInitialNode);
                SetNodeLevels(nodesForUpdate);
                ResetGraphToInitialState();
            }

            LogSchedule(nodesForUpdate);

            return nodesForUpdate;
        }

        private bool SkipUpdate(INode node)
        {
            return EnableSkippingUpdateIfInitialNodeValueNotChanged == true && node.IsValueChanged() == false;
        }

        private void MakeTemporaryAdjustmentsToGraph(INode initialNode, bool skipInitialNode)
        {
            AddTemporaryDependenciesBetweenChildNodesAndCollectionNode();
            IList<INode> updatePath = GetTopologicallySortedGraph(initialNode, skipInitialNode);
            MakeNecessaryRedirectionsInUpdatePath(updatePath);
        }

        private void MakeNecessaryRedirectionsInUpdatePath(IList<INode> updatePath)
        {
            RedirectionDependencesToAdd.Clear();
            RedirectionDependencesToRemove.Clear();

            foreach (INode collectionNode in updatePath)
            {
                if (collectionNode is ICollectionNode)
                {
                    if (GraphUtility.IsCollectionNodeTriggeredThroughItsChildPredecessors((ICollectionNode)collectionNode, updatePath) == false)
                    {
                        INode nonChildPredecessor = GraphUtility.GetNearestNonChildPredecessorInUpdatePath((ICollectionNode)collectionNode, updatePath);
                        if (nonChildPredecessor != null)
                        {
                            DetermineRedirectionDependencies(nonChildPredecessor, (ICollectionNode)collectionNode);
                        }
                    }
                }
            }

            foreach (var d in RedirectionDependencesToAdd)
            {
                Graph.AddDependency(d.Item1, d.Item2);
            }

            foreach (var d in RedirectionDependencesToRemove)
            {
                Graph.RemoveDependency(d.Item1, d.Item2);
            }
        }

        private void DetermineRedirectionDependencies(INode predecessor, ICollectionNode collectionNode)
        {
            RedirectionDependencesToRemove.Add(new Tuple<INode, INode>(predecessor, (INode)collectionNode));

            foreach (var p in (collectionNode as INode).Predecessors)
            {
                if ((collectionNode as ICollectionNode).ContainsChildNode(p))
                {
                    RedirectionDependencesToAdd.Add(new Tuple<INode, INode>(predecessor, p));
                }
            }
        }

        #endregion
    }
}
