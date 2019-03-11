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
    internal enum UpdateProcessStatus
    {
        NotSet,
        Started,
        Ended
    }

    public class UpdateScheduler
    {
        #region Properties

        private List<Tuple<INode, INode>> ChildCollectionDependenciesToAdd { get; set; }
        private List<Tuple<INode, INode>> RedirectionDependencesToRemove { get; set; }
        private List<Tuple<INode, INode>> RedirectionDependencesToAdd { get; set; }

        private UpdateProcessStatus Status { get; set; }

        /// <summary>
        /// Algorithm for topological sorting.
        /// </summary>
        public ISort SortAlgorithm { get; set; }

        public IDependencyGraph DependencyGraph { get; set; }

        public UpdateLogger LoggerNodesForUpdate { get; private set; }
        public UpdateLogger LoggerUpdatedNodes { get; private set; }

        public bool EnableSkippingUpdateIfInitialNodeValueNotChanged { get; set; }

        #endregion

        #region Constructor

        public UpdateScheduler(IDependencyGraph graph)
        {
            ChildCollectionDependenciesToAdd = new List<Tuple<INode, INode>>();
            RedirectionDependencesToRemove = new List<Tuple<INode, INode>>();
            RedirectionDependencesToAdd = new List<Tuple<INode, INode>>();

            SortAlgorithm = new TopologicalSort2();
            LoggerNodesForUpdate = new UpdateLogger();
            LoggerUpdatedNodes = new UpdateLogger();
            DependencyGraph = graph;

            EnableSkippingUpdateIfInitialNodeValueNotChanged = false;
            Status = UpdateProcessStatus.NotSet;
        }

        #endregion

        #region Public methods

        public void PerformUpdate()
        {
            if (UpdateIsAllowed())
            {
                MarkUpdateStart();

                IList<INode> nodesForUpdate = GetNodesForUpdate();
                Update(nodesForUpdate);

                MarkUpdateEnd();
            }
        }

        /// <summary>
        /// Gets all nodes from dependency graph, arranged in order they should be updated.
        /// </summary>
        /// <returns>List of all nodes from dependency graph.</returns>
        public IList<INode> GetNodesForUpdate()
        {
            MakeTemporaryAdjustmentsToGraph(DependencyGraph.Nodes);
            IList<INode> nodesForUpdate = GetSortedGraph(DependencyGraph.Nodes);
            ResetGraphToInitialState();

            GraphValidator.ValidateGraph(nodesForUpdate);
            if (DependencyGraph.Settings.EnableLogging == true)
            {
                LoggerNodesForUpdate.ClearLog();
                LoggerNodesForUpdate.Log(nodesForUpdate);
            }

            return nodesForUpdate;
        }

        /// <summary>
        /// Gets nodes from dependency graph that need to be updated, arranged in order they should be updated.
        /// </summary>
        /// <param name="node">Initial node that triggered the update.</param>
        /// <param name="skipInitialNode">Specifies whether the initial node will be skipped from updating.</param>
        /// <returns>List of nodes from dependency graph that need to be updated.</returns>
        public IList<INode> GetNodesForUpdate(INode node, bool skipInitialNode)
        {
            IList<INode> nodesForUpdate = null;
            INode initialNode = DependencyGraph.GetNode(node);

            if (initialNode == null)
            {
                throw new NodeNullReferenceException("Reactive node set as initial node of the update process is not part of the graph!");
            }

            if (SkipUpdate(initialNode) == false)
            {
                MakeTemporaryAdjustmentsToGraph(DependencyGraph.Nodes, node, skipInitialNode);
                nodesForUpdate = GetSortedGraph(DependencyGraph.Nodes, initialNode, skipInitialNode);
                ResetGraphToInitialState();
            }

            GraphValidator.ValidateGraph(nodesForUpdate);
            if (DependencyGraph.Settings.EnableLogging == true)
            {
                LoggerNodesForUpdate.ClearLog();
                LoggerNodesForUpdate.Log(nodesForUpdate);
            }

            return nodesForUpdate;
        }

        public void PerformUpdate(INode initialNode, bool skipInitialNode)
        {
            if (UpdateIsAllowed())
            {
                MarkUpdateStart();

                ValidateInitialNode(initialNode);
                IList<INode> nodesForUpdate = GetNodesForUpdate(initialNode, skipInitialNode);
                Update(nodesForUpdate);

                MarkUpdateEnd();
            }
        }

        public void PerformUpdate(ICollectionNodeItem ownerObject, string memberName)
        {
            if (UpdateIsAllowed())
            {
                MarkUpdateStart();

                INode initialNode = DependencyGraph.GetNode(ownerObject, memberName);

                if (initialNode == null)
                {
                    initialNode = GraphUtility.GetCollectionNode(ownerObject, memberName);
                }

                IList<INode> nodesForUpdate = GetNodesForUpdate(initialNode, true);
                Update(nodesForUpdate);

                MarkUpdateEnd();
            }
        }

        #endregion

        #region Private methods

        private bool UpdateIsAllowed()
        {
            return Status == UpdateProcessStatus.NotSet || Status == UpdateProcessStatus.Ended;
        }

        private void MarkUpdateStart()
        {
            Status = UpdateProcessStatus.Started;
        }

        private void MarkUpdateEnd()
        {
            Status = UpdateProcessStatus.Ended;
        }

        private void Update(IList<INode> nodesForUpdate)
        {
            LoggerUpdatedNodes.ClearLog();

            foreach (var node in nodesForUpdate)
            {
                node.Update();

                if (DependencyGraph.Settings.EnableLogging == true)
                {
                    LoggerUpdatedNodes.Log(node);
                }
            }
        }

        private void ValidateInitialNode(INode initialNode)
        {
            if (initialNode == null)
            {
                throw new NodeNullReferenceException("Reactive node set as initial node of the update process is not part of the graph!");
            }
        }

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
