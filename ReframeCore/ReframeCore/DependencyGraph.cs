using ReframeCore.Exceptions;
using ReframeCore.Helpers;
using ReframeCore.Nodes;
using ReframeCore.ReactiveCollections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReframeCore
{
    public enum DependencyGraphStatus { NotInitialized, Initialized, NotConsistent, Consistent}

    /// <summary>
    /// Dependency graph containing reactive nodes.
    /// </summary>
    public class DependencyGraph : IDependencyGraph
    {
        #region Private Fields

        private List<Tuple<INode, INode>> childCollectionDependenciesToAdd = new List<Tuple<INode, INode>>();
        private List<Tuple<INode, INode>> redirectionDependencesToAdd = new List<Tuple<INode, INode>>();
        private List<Tuple<INode, INode>> redirectionDependencesToRemove = new List<Tuple<INode, INode>>();

        #endregion

        #region Properties

        public NodeFactory DefaultNodeFactory { get; private set; }

        /// <summary>
        /// Dependency graph unique identifier.
        /// </summary>
        public string Identifier { get; private set; }

        /// <summary>
        /// List of all reactive nodes in dependency graph.
        /// </summary>
        private IList<INode> Nodes { get; set; }

        /// <summary>
        /// Settings object for this dependency graph.
        /// </summary>
        public Settings Settings { get; private set; }

        /// <summary>
        /// Algorithm for topological sorting.
        /// </summary>
        public ISort SortAlgorithm { get; set; }

        public DependencyGraphStatus Status { get; private set; }

        public Logger Logger { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Instantiates new dependency graph.
        /// </summary>
        /// <param name="identifier">Dependency graph unique identifier.</param>
        public DependencyGraph(string identifier)
        {
            Identifier = identifier;
            Settings = new Settings();
            Nodes = new List<INode>();
            SortAlgorithm = new TopologicalSort2();
            Logger = new Logger();
            DefaultNodeFactory = new NodeFactory();
            Status = DependencyGraphStatus.NotInitialized;
        }

        public void Initialize()
        {
            Status = DependencyGraphStatus.Initialized;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds new node to dependency graph if such node does not already exist in dependency graph.
        /// </summary>
        /// <param name="node">Reactive node which we want to add.</param>
        /// <returns>Node which is added to dependency graph, NULL if reactive node has already existed in dependency graph.</returns>
        public INode AddNode(INode node)
        {
            INode addedNode = null;

            if (node == null)
            {
                throw new NodeNullReferenceException();
            }

            if (ContainsNode(node) == false)
            {
                Nodes.Add(node);
                addedNode = node;
            }

            return addedNode;
        }

        /// <summary>
        /// Adds node to dependency graph if such node does not already exist in dependency graph.
        /// </summary>
        /// <param name="ownerObject">Owner object associated with reactive node.</param>
        /// <param name="memberName">Member name represented by reactive node.</param>
        /// <returns>Newly added or already existing node.</returns>
        public INode AddNode(object ownerObject, string memberName)
        {
            return AddNode(ownerObject, memberName, "");
        }

        /// <summary>
        /// Adds node to dependency graph if such node does not already exist in dependency graph.
        /// </summary>
        /// <param name="ownerObject">Owner object associated with reactive node.</param>
        /// <param name="memberName">Member name represented by reactive node.</param>
        /// <param name="updateMethodName">Update method name.</param>
        /// <returns>Newly added or already existing node.</returns>
        public INode AddNode(object ownerObject, string memberName, string updateMethodName)
        {
            INode nodeToAdd = GetNode(ownerObject, memberName);

            if (nodeToAdd == null)
            {
                nodeToAdd = DefaultNodeFactory.CreateNode(ownerObject, memberName, updateMethodName);
                Nodes.Add(nodeToAdd);
            }

            return nodeToAdd;
        }

        /// <summary>
        /// Checks if dependency graph contains provided reactive node.
        /// </summary>
        /// <param name="node">Reactive node for which we are checking.</param>
        /// <returns>True if dependency graph contains reactive node, otherwise False.</returns>
        public bool ContainsNode(INode node)
        {
            return GetNode(node) != null;
        }

        /// <summary>
        /// Checks if dependency graph contains provided reactive node.
        /// </summary>
        /// <param name="ownerObject">Owner object associated with reactive node.</param>
        /// <param name="memberName">Member name represented by reactive node.</param>
        /// <returns>True if dependency graph contains reactive node, otherwise False.</returns>
        public bool ContainsNode(object ownerObject, string memberName)
        {
            return GetNode(ownerObject, memberName) != null;
        }

        /// <summary>
        /// Gets reactive node from dependency graph if exists, otherwise returns null.
        /// </summary>
        /// <param name="ownerObject">Owner object associated with reactive node.</param>
        /// <param name="memberName">Member name represented by the reactive node.</param>
        /// <returns>Reactive node if exists in dependency graph, otherwise null.</returns>
        public INode GetNode(object ownerObject, string memberName)
        {
            return Nodes.FirstOrDefault(n => n.HasSameIdentifier(ownerObject, memberName));
        }

        /// <summary>
        /// Gets existing reactive node from dependency graph, otherwise returns null.
        /// </summary>
        /// <param name="node">Reactive node which we want to find.</param>
        /// <returns>Reactive node if such exists in dependency graph, otherwise null.</returns>
        public INode GetNode(INode node)
        {
            INode n;

            if (node == null)
            {
                throw new NodeNullReferenceException();
            }

            if (Nodes.Contains(node))
            {
                n = node;
            }
            else
            {
                n = GetNode(node.OwnerObject, node.MemberName);
            }

            return n;
        }

        /// <summary>
        /// Constructs reactive dependency between two reactive nodes, where the first node acts as a predecessor and
        /// the second one as successor. 
        /// </summary>
        /// <param name="predecessor">Reactive node acting as a predecessor in reactive dependecy.</param>
        /// <param name="successor">Reactive node acting as a successor in reactive dependency.</param>
        public void AddDependency(INode predecessor, INode successor)
        {
            if (predecessor == null || successor == null)
            {
                throw new NodeNullReferenceException("Reactive dependency could not be added! At least one of the involved nodes is null!");
            }

            INode p = GetNode(predecessor);
            INode s = GetNode(successor);

            if (p == null)
            {
                AddNode(predecessor);
                p = predecessor;
            }

            if (s == null)
            {
                AddNode(successor);
                s = successor;
            }

            p.AddSuccessor(s);
        }

        /// <summary>
        /// Constructs reactive dependencies between predecessor node and multiple successor nodes.
        /// </summary>
        /// <param name="predecessor">Reactive node acting as a predecessor in reactive dependency.</param>
        /// <param name="successors">List of reactive nodes acting as a successors in reactive dependencies.</param>
        public void AddDependency(INode predecessor, List<INode> successors)
        {
            if (successors != null)
            {
                foreach (INode successor in successors)
                {
                    AddDependency(predecessor, successor);
                }
            }
            else
            {
                throw new NodeNullReferenceException();
            }
        }

        /// <summary>
        /// Constructs reactive dependencies between multiple predecessor nodes and a successor node.
        /// </summary>
        /// <param name="predecessors">List of reactive nodes acting as a predecessors in reactive dependencies.</param>
        /// <param name="successor">Reactive node acting as a successor in reactive dependencies.</param>
        public void AddDependency(List<INode> predecessors, INode successor)
        {
            if (predecessors != null)
            {
                foreach (INode predecessor in predecessors)
                {
                    AddDependency(predecessor, successor);
                }
            }
            else
            {
                throw new NodeNullReferenceException();
            }
        }

        /// <summary>
        /// Constructs reactive dependency between two reactive nodes, where the first node acts as a predecessor and
        /// the second one as successor.
        /// </summary>
        /// <param name="predecessorObject">Owner object associated with predecessor reactive node.</param>
        /// <param name="predecessorMember">Member name represented by the predecessor reactive node.</param>
        /// <param name="successorObject">Owner object associated with successor reactive node.</param>
        /// <param name="successorMember">Member name represented by the successor reactive node</param>
        public void AddDependency(object predecessorObject, string predecessorMember, object successorObject, string successorMember)
        {
            if (predecessorObject == null || predecessorMember == "" || successorObject == null || successorMember == "")
            {
                throw new NodeNullReferenceException("Reactive dependency could not be added! At least one of the involved nodes is null!");
            }

            INode predecessor = AddNode(predecessorObject, predecessorMember);
            INode successor = AddNode(successorObject, successorMember);

            AddDependency(predecessor, successor);
        }

        /// <summary>
        /// Removes reactive node from dependency graph if the node does not participate in any reactive dependencies (i.e. it doesn't have any predecessors or successors).
        /// </summary>
        /// <param name="node">Node which we want to remove from dependency graph.</param>
        public bool RemoveNode(INode node)
        {
            if (node == null)
            {
                throw new NodeNullReferenceException("Cannot remove reactive node which is null!");
            }

            if (node.Predecessors.Count > 0 || node.Successors.Count > 0)
            {
                throw new ReactiveNodeException("Cannot remove reactive node which participates in reactive dependencies!");
            }

            return Nodes.Remove(node);
        }

        /// <summary>
        /// Removes reactive dependency between two nodes if it exists. The first node stops to be predecessor of the second node,
        /// and the second node stops to be successor on the first node.
        /// </summary>
        /// <param name="predecessor">Reactive nodes acting as a predecessor in reactive dependecy.</param>
        /// <param name="successor">Reactive node acting as a successor in reactive dependency.</param>
        public bool RemoveDependency(INode predecessor, INode successor)
        {
            INode p = GetNode(predecessor);
            INode s = GetNode(successor);

            if (p == null || s == null)
            {
                throw new ReactiveDependencyException("Reactive dependency could not be removed! Specified reactive nodes are not part of the graph!");
            }

            return p.RemoveSuccessor(s);
        }

        /// <summary>
        /// Clears unused nodes, i.e. removes all nodes from the graph which do not participate in reactive dependencies.
        /// </summary>
        /// <returns>Number of nodes removed from graph.</returns>
        public int RemoveUnusedNodes()
        {
            int numberOfRemovedNodes = 0;

            for (int i = Nodes.Count-1; i >=0; i--)
            {
                if (Nodes[i].Predecessors.Count == 0 && Nodes[i].Successors.Count == 0)
                {
                    RemoveNode(Nodes[i]);
                    numberOfRemovedNodes++;
                }
            }

            return numberOfRemovedNodes;
        }

        /// <summary>
        /// Performs update of all nodes that depend on provided initial node.
        /// Proper order of update is handled by topologically sorting dependent nodes.
        /// </summary>
        /// <param name="initialNode">Initial node that triggered the update.</param>
        /// <param name="skipInitialNode">Specifies whether the initial node will be skipped from updating.l</param>
        public void PerformUpdate(INode initialNode, bool skipInitialNode)
        {
            if (Status != DependencyGraphStatus.NotInitialized)
            {
                Status = DependencyGraphStatus.NotConsistent;

                IList<INode> nodesToUpdate = GetNodesToUpdate(initialNode, skipInitialNode);
                foreach (var node in nodesToUpdate)
                {
                    node.Update();
                }

                Status = DependencyGraphStatus.Consistent;
            }
        }

        public void PerformUpdate(object ownerObject, string memberName)
        {
            INode initialNode = GetNode(ownerObject, memberName);
            PerformUpdate(initialNode);
        }

        public void PerformUpdate(ICollectionNodeItem ownerObject, string memberName)
        {
            if (Status != DependencyGraphStatus.NotInitialized)
            {
                Status = DependencyGraphStatus.NotConsistent;

                INode initialNode = GetNode(ownerObject, memberName);

                if (initialNode == null)
                {
                    initialNode = GetCollectionNode(ownerObject, memberName);
                }

                IList<INode> nodesToUpdate = GetNodesToUpdate(initialNode, true);
                foreach (var node in nodesToUpdate)
                {
                    node.Update();
                }

                Status = DependencyGraphStatus.Consistent;
            }
        }

        private INode GetCollectionNode(ICollectionNodeItem ownerObject, string memberName)
        {
            if (ownerObject != null && memberName != "")
            {
                ReactiveCollectionItemEventArgs eArgs = new ReactiveCollectionItemEventArgs();
                eArgs.MemberName = memberName;

                Reflector.RaiseEvent(ownerObject, "UpdateTriggered", eArgs);

                INode collectionNode = eArgs.CollectionNode as INode;
                return collectionNode;
            }
            else
            {
                throw new NodeNullReferenceException();
            }
        }

        /// <summary>
        /// Performs update of all nodes that depend on provided initial node.
        /// Proper order of update is handled by topologically sorting dependent nodes.
        /// </summary>
        /// <param name="initialNode">Initial node that triggered the update.</param>
        /// <param name="skipInitialNode">Specifies whether the initial node will be skipped from updating.l</param>
        public void PerformUpdate(INode initialNode)
        {
            PerformUpdate(initialNode, true);
        }

        /// <summary>
        /// Performs update of all nodes in dependency graph. Proper order of update is handled by topologically sorting dependend nodes.
        /// </summary>
        public void PerformUpdate()
        {
            if (Status != DependencyGraphStatus.NotInitialized)
            {
                Status = DependencyGraphStatus.NotConsistent;

                IList<INode> nodesToUpdate = GetNodesToUpdate();
                foreach (var node in nodesToUpdate)
                {
                    node.Update();
                }

                Status = DependencyGraphStatus.Consistent;
            }
        }

        /// <summary>
        /// Gets nodes from dependency graph that need to be updated, arranged in order they should be updated.
        /// </summary>
        /// <param name="node">Initial node that triggered the update.</param>
        /// <param name="skipInitialNode">Specifies whether the initial node will be skipped from updating.</param>
        /// <returns>List of nodes from dependency graph that need to be updated.</returns>
        private IList<INode> GetNodesToUpdate(INode node, bool skipInitialNode)
        {
            INode initialNode = GetNode(node);

            if (initialNode == null)
            {
                throw new NodeNullReferenceException("Reactive node set as initial node of the update process is not part of the graph!");
            }

            MakeTemporaryAdjustmentsToGraph(Nodes, node, skipInitialNode);
            IList<INode> nodesToUpdate = GetSortedGraph(Nodes, initialNode, skipInitialNode);
            ResetGraphToInitialState();

            ValidateGraph(nodesToUpdate);
            LogNodesToUpdate(nodesToUpdate);

            return nodesToUpdate;
        }

        /// <summary>
        /// Gets all nodes from dependency graph, arranged in order they should be updated.
        /// </summary>
        /// <returns>List of all nodes from dependency graph.</returns>
        private IList<INode> GetNodesToUpdate()
        {
            MakeTemporaryAdjustmentsToGraph(Nodes);
            IList<INode> nodesToUpdate = GetSortedGraph(Nodes);
            ResetGraphToInitialState();

            ValidateGraph(nodesToUpdate);
            LogNodesToUpdate(nodesToUpdate);

            return nodesToUpdate;
        }

        private IList<INode> GetSortedGraph(IList<INode> nodes)
        {
            return SortAlgorithm.Sort(nodes, n => n.Successors);
        }

        private IList<INode> GetSortedGraph(IList<INode> nodes, INode initialNode, bool skipInitialNode)
        {
            return SortAlgorithm.Sort(nodes, n => n.Successors, initialNode, skipInitialNode);
        }

        private void MakeTemporaryAdjustmentsToGraph(IList<INode> graph)
        {
            AddTemporaryDependenciesBetweenChildNodesAndCollectionNode(graph);
        }

        private void MakeTemporaryAdjustmentsToGraph(IList<INode> graph, INode initialNode, bool skipInitialNode)
        {
            MakeTemporaryAdjustmentsToGraph(graph);
            IList<INode> updatePath = GetSortedGraph(graph, initialNode, skipInitialNode);
            MakeNecessaryRedirectionsInUpdatePath(updatePath);
        }

        private void AddTemporaryDependenciesBetweenChildNodesAndCollectionNode(IList<INode> graph)
        {
            childCollectionDependenciesToAdd = DetermineTemporaryDependenciesBetweenChildNodesAndCollectionNode(graph);

            foreach (var item in childCollectionDependenciesToAdd)
            {
                AddDependency(item.Item1, item.Item2);
            }
        }

        private List<Tuple<INode, INode>> DetermineTemporaryDependenciesBetweenChildNodesAndCollectionNode(IList<INode> nodes)
        {
            List<Tuple<INode, INode>> tempDependencies = new List<Tuple<INode, INode>>();

            foreach (var node in nodes)
            {
                if (node.OwnerObject is ICollectionNodeItem)
                {
                    INode collectionNode = GetCollectionNode((ICollectionNodeItem)node.OwnerObject, node.MemberName);

                    if (collectionNode != null)
                    {
                            tempDependencies.Add(new Tuple<INode, INode>(node, collectionNode));
                    }
                }
            }

            return tempDependencies;
        }

        private void ResetGraphToInitialState()
        {
            ResetTemporaryDependenciesBetweenChildNodesAndCollectionNode();
            ResetRedirectionDependencies();
        }

        private void ResetTemporaryDependenciesBetweenChildNodesAndCollectionNode()
        {
            foreach (var item in childCollectionDependenciesToAdd)
            {
                RemoveDependency(item.Item1, item.Item2);
            }

            childCollectionDependenciesToAdd.Clear();
        }

        private void ResetRedirectionDependencies()
        {
            foreach (var d in redirectionDependencesToRemove)
            {
                AddDependency(d.Item1, d.Item2);
            }

            foreach (var d in redirectionDependencesToAdd)
            {
                RemoveDependency(d.Item1, d.Item2);
            }

            redirectionDependencesToRemove.Clear();
            redirectionDependencesToAdd.Clear();
        }

        private void MakeNecessaryRedirectionsInUpdatePath(IList<INode> updatePath)
        {
            redirectionDependencesToAdd.Clear();
            redirectionDependencesToRemove.Clear();

            foreach (INode collectionNode in updatePath)
            {
                if (collectionNode is ICollectionNode)
                {
                    if (IsCollectionNodeTriggeredThroughItsChildPredecessors((ICollectionNode)collectionNode, updatePath) == false)
                    {
                        INode nonChildPredecessor = GetNearestNonChildPredecessorInUpdatePath((ICollectionNode)collectionNode, updatePath);
                        if (nonChildPredecessor != null)
                        {
                            DetermineRedirectionDependencies(nonChildPredecessor, (ICollectionNode)collectionNode);
                        }
                    }
                }
                
            }

            foreach (var d in redirectionDependencesToAdd)
            {
                AddDependency(d.Item1, d.Item2);
            }

            foreach (var d in redirectionDependencesToRemove)
            {
                RemoveDependency(d.Item1, d.Item2);
            }
        }

        private Dictionary<INode, INode> DetermineDependenciesToBeRemovedForRedirection(INode predecessor, INode collectionNode)
        {
            Dictionary<INode, INode> dependenciesForRemoval = new Dictionary<INode, INode>();

            dependenciesForRemoval.Add(predecessor, collectionNode);

            return dependenciesForRemoval;
        }

        private Dictionary<INode, INode> DetermineDependenciesToBeAddedForRedirection(INode predecessor, INode collectionNode)
        {
            Dictionary<INode, INode> dependenciesForAddition = new Dictionary<INode, INode>();

            foreach (var p in collectionNode.Predecessors)
            {
                if ((collectionNode as ICollectionNode).ContainsChildNode(p))
                {
                    dependenciesForAddition.Add(predecessor, p);
                }
            }

            return dependenciesForAddition;
        }

        private void DetermineRedirectionDependencies(INode predecessor, ICollectionNode collectionNode)
        {
            redirectionDependencesToRemove.Add(new Tuple<INode, INode>(predecessor, (INode)collectionNode));

            foreach (var p in (collectionNode as INode).Predecessors)
            {
                if ((collectionNode as ICollectionNode).ContainsChildNode(p))
                {
                    redirectionDependencesToAdd.Add(new Tuple<INode, INode>(predecessor, p));
                }
            }

        }

        private bool HasChildPredecessors(ICollectionNode collectionNode)
        {
            bool has = false;

            foreach (var p in (collectionNode as INode).Predecessors)
            {
                if (collectionNode.ContainsChildNode(p))
                {
                    has = true;
                    break;
                }
            }

            return has;
        }

        private bool IsCollectionNodeTriggeredThroughItsChildPredecessors(ICollectionNode collectionNode, IList<INode> nodesToUpdate)
        {
            bool isTriggered = false;

            if (HasChildPredecessors(collectionNode))
            {
                int collectionNodeIndex = GraphUtility.GetIndexPositionOfNodeInUpdatePath((INode)collectionNode, nodesToUpdate);
                IList<INode> predecessorsFromUpdatePath = GraphUtility.GetNodeDirectPredecessorsFromUpdatePath(collectionNodeIndex, nodesToUpdate);

                foreach (var p in predecessorsFromUpdatePath)
                {
                    if (GraphUtility.IsChildOfCollectionNode(p, collectionNode))
                    {
                        isTriggered = true;
                        break;
                    }
                }
            }

            return isTriggered;
        }

        private INode GetNearestNonChildPredecessorInUpdatePath(ICollectionNode collectionNode, IList<INode> nodesToUpdate)
        {
            INode predecessor = null;

            int collectionNodeIndex = GraphUtility.GetIndexPositionOfNodeInUpdatePath((INode)collectionNode, nodesToUpdate);
            IList<INode> predecessorsFromUpdatePath = GraphUtility.GetNodeDirectPredecessorsFromUpdatePath(collectionNodeIndex, nodesToUpdate);

            if (predecessorsFromUpdatePath.Count > 0)
            {
                predecessor = predecessorsFromUpdatePath[0];
            }

            return predecessor;
        }

        private void ValidateGraph(IList<INode> nodes)
        {

        }

        /// <summary>
        /// Logs nodes to be updated.
        /// </summary>
        /// <param name="nodesToUpdate">Nodes to be updated.</param>
        private void LogNodesToUpdate(IList<INode> nodesToUpdate)
        {
            if (Settings.LogUpdates == true)
            {
                Logger.ClearNodesToUpdate();
                foreach (var n in nodesToUpdate)
                {
                    Logger.LogNodeToUpdate(n);
                }
            }
        }

        #endregion
    }
}
