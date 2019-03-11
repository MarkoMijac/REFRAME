using ReframeCore.Exceptions;
using ReframeCore.Nodes;
using ReframeCore.ReactiveCollections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore.Helpers
{
    public class UpdateScheduler
    {
        #region Properties

        private List<Tuple<INode, INode>> ChildCollectionDependenciesToAdd { get; set; }
        private List<Tuple<INode, INode>> RedirectionDependencesToRemove { get; set; }
        private List<Tuple<INode, INode>> RedirectionDependencesToAdd { get; set; }
        
        /// <summary>
        /// Algorithm for topological sorting.
        /// </summary>
        public ISort SortAlgorithm { get; set; }

        public IDependencyGraph DependencyGraph { get; set; }

        public UpdateLogger Logger { get; private set; }

        public bool EnableSkippingUpdateIfInitialNodeValueNotChanged { get; set; }

        #endregion

        #region Constructor

        public UpdateScheduler(IDependencyGraph graph)
        {
            ChildCollectionDependenciesToAdd = new List<Tuple<INode, INode>>();
            RedirectionDependencesToRemove = new List<Tuple<INode, INode>>();
            RedirectionDependencesToAdd = new List<Tuple<INode, INode>>();

            SortAlgorithm = new TopologicalSort2();
            Logger = new UpdateLogger();
            DependencyGraph = graph;

            EnableSkippingUpdateIfInitialNodeValueNotChanged = false;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Gets all nodes from dependency graph, arranged in order they should be updated.
        /// </summary>
        /// <returns>List of all nodes from dependency graph.</returns>
        public IList<INode> GetNodesToUpdate()
        {
            MakeTemporaryAdjustmentsToGraph(DependencyGraph.Nodes);
            IList<INode> nodesToUpdate = GetSortedGraph(DependencyGraph.Nodes);
            ResetGraphToInitialState();

            GraphValidator.ValidateGraph(nodesToUpdate);
            Logger.Log(nodesToUpdate);

            return nodesToUpdate;
        }

        /// <summary>
        /// Gets nodes from dependency graph that need to be updated, arranged in order they should be updated.
        /// </summary>
        /// <param name="node">Initial node that triggered the update.</param>
        /// <param name="skipInitialNode">Specifies whether the initial node will be skipped from updating.</param>
        /// <returns>List of nodes from dependency graph that need to be updated.</returns>
        public IList<INode> GetNodesToUpdate(INode node, bool skipInitialNode)
        {
            INode initialNode = DependencyGraph.GetNode(node);

            if (initialNode == null)
            {
                throw new NodeNullReferenceException("Reactive node set as initial node of the update process is not part of the graph!");
            }

            MakeTemporaryAdjustmentsToGraph(DependencyGraph.Nodes, node, skipInitialNode);
            IList<INode> nodesToUpdate = GetSortedGraph(DependencyGraph.Nodes, initialNode, skipInitialNode);
            ResetGraphToInitialState();

            GraphValidator.ValidateGraph(nodesToUpdate);
            Logger.Log(nodesToUpdate);

            return nodesToUpdate;
        }

        public void PerformUpdate(INode initialNode, bool skipInitialNode)
        {
            if (initialNode == null)
            {
                throw new NodeNullReferenceException("Reactive node set as initial node of the update process is not part of the graph!");
            }

            if (SkipUpdate(initialNode) == false)
            {
                IList<INode> nodesToUpdate = GetNodesToUpdate(initialNode, skipInitialNode);
                foreach (var node in nodesToUpdate)
                {
                    node.Update();
                }
            }
        }

        public void PerformUpdate(ICollectionNodeItem ownerObject, string memberName)
        {
            INode initialNode = DependencyGraph.GetNode(ownerObject, memberName);

            if (initialNode == null)
            {
                initialNode = GraphUtility.GetCollectionNode(ownerObject, memberName);
            }

            IList<INode> nodesToUpdate = GetNodesToUpdate(initialNode, true);
            foreach (var node in nodesToUpdate)
            {
                node.Update();
            }
        }

        public void PerformUpdate()
        {
            IList<INode> nodesToUpdate = GetNodesToUpdate();
            foreach (var node in nodesToUpdate)
            {
                node.Update();
            }
        }

        #endregion

        #region Private methods

        private IList<INode> GetSortedGraph(IList<INode> nodes, INode initialNode, bool skipInitialNode)
        {
            return SortAlgorithm.Sort(nodes, n => n.Successors, initialNode, skipInitialNode);
        }

        private IList<INode> GetSortedGraph(IList<INode> nodes)
        {
            return SortAlgorithm.Sort(nodes, n => n.Successors);
        }

        private void AddTemporaryDependenciesBetweenChildNodesAndCollectionNode(IList<INode> graph)
        {
            ChildCollectionDependenciesToAdd = DetermineVirtualChildVsCollectionNodeDependencies(graph);

            foreach (var item in ChildCollectionDependenciesToAdd)
            {
                DependencyGraph.AddDependency(item.Item1, item.Item2);
            }
        }

        private List<Tuple<INode, INode>> DetermineVirtualChildVsCollectionNodeDependencies(IList<INode> nodes)
        {
            List<Tuple<INode, INode>> tempDependencies = new List<Tuple<INode, INode>>();

            foreach (var node in nodes)
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

        private void ResetTemporaryDependenciesBetweenChildNodesAndCollectionNode()
        {
            foreach (var item in ChildCollectionDependenciesToAdd)
            {
                DependencyGraph.RemoveDependency(item.Item1, item.Item2);
            }

            ChildCollectionDependenciesToAdd.Clear();
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
                DependencyGraph.AddDependency(d.Item1, d.Item2);
            }

            foreach (var d in RedirectionDependencesToRemove)
            {
                DependencyGraph.RemoveDependency(d.Item1, d.Item2);
            }
        }

        private void ResetRedirectionDependencies()
        {
            foreach (var d in RedirectionDependencesToRemove)
            {
                DependencyGraph.AddDependency(d.Item1, d.Item2);
            }

            foreach (var d in RedirectionDependencesToAdd)
            {
                DependencyGraph.RemoveDependency(d.Item1, d.Item2);
            }

            RedirectionDependencesToRemove.Clear();
            RedirectionDependencesToAdd.Clear();
        }

        private void ResetGraphToInitialState()
        {
            ResetTemporaryDependenciesBetweenChildNodesAndCollectionNode();
            ResetRedirectionDependencies();
        }

        private void MakeTemporaryAdjustmentsToGraph(IList<INode> graph)
        {
            AddTemporaryDependenciesBetweenChildNodesAndCollectionNode(graph);
        }

        private void MakeTemporaryAdjustmentsToGraph(IList<INode> graph, INode initialNode, bool skipInitialNode)
        {
            AddTemporaryDependenciesBetweenChildNodesAndCollectionNode(graph);
            IList<INode> updatePath = GetSortedGraph(graph, initialNode, skipInitialNode);
            MakeNecessaryRedirectionsInUpdatePath(updatePath);
        }

        private bool SkipUpdate(INode node)
        {
            return EnableSkippingUpdateIfInitialNodeValueNotChanged == true && node.IsValueChanged() == false;
        }

        #endregion
    }
}
