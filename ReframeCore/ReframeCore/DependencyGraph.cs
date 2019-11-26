using ReframeCore.Exceptions;
using ReframeCore.Helpers;
using ReframeCore.Nodes;
using ReframeCore.ReactiveCollections;
using ReframeCore.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReframeCore
{
    public enum DependencyGraphStatus { NotInitialized, Initialized, NotConsistent, Consistent}

    /// <summary>
    /// Dependency graph containing reactive nodes.
    /// </summary>
    public class DependencyGraph : IDependencyGraph
    {
        #region Properties

        public NodeFactory NodeFactory { get; private set; }

        public Updater Updater { get; private set; }

        public IScheduler Scheduler { get; private set; }

        /// <summary>
        /// Dependency graph unique identifier.
        /// </summary>
        public string Identifier { get; private set; }

        /// <summary>
        /// List of all reactive nodes in dependency graph.
        /// </summary>
        public IList<INode> Nodes { get; set; }

        /// <summary>
        /// Settings object for this dependency graph.
        /// </summary>
        public Settings Settings { get; private set; }

        public DependencyGraphStatus Status { get; private set; }

        public bool UpdateSuspended { get; set; } = false;

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

            NodeFactory = new StandardNodeFactory();
            Scheduler = new Scheduler(this, new DFS_Sorter());
            Updater = new Updater(this, Scheduler);
            Updater.UpdateCompleted += delegate { OnPerformUpdateCompleted(); };

            Status = DependencyGraphStatus.NotInitialized;
        }

        public void Initialize()
        {
            Status = DependencyGraphStatus.Initialized;
        }

        #endregion

        #region Methods

        #region AddNode

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
                RegisterGraphInNode(addedNode);
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
                nodeToAdd = NodeFactory.CreateNode(ownerObject, memberName, updateMethodName);
                Nodes.Add(nodeToAdd);
                RegisterGraphInNode(nodeToAdd);
            }

            return nodeToAdd;
        }

        private void RegisterGraphInNode(INode node)
        {
            node.Graph = this;
        }

        #endregion

        #region RemoveNode

        /// <summary>
        /// Removes reactive node from dependency graph if the node does not participate in any reactive dependencies (i.e. it doesn't have any predecessors or successors).
        /// </summary>
        /// <param name="node">Node which we want to remove from dependency graph.</param>
        /// <param name="forceRemoval">Indicates whether the node removal is forced, i.e. whether 
        /// it will remove node along with all its dependencies with other nodes.</param>
        public bool RemoveNode(INode node, bool forceRemoval = false)
        {
            ValidateNodeRemoval(node, forceRemoval);

            node.ClearPredecessors();
            node.ClearSuccessors();

            bool removed = Nodes.Remove(node);
            UnregisterGraphFromNode(node);

            return removed;

        }

        private void ValidateNodeRemoval(INode node, bool forceRemoval)
        {
            if (node == null)
            {
                throw new NodeNullReferenceException("Cannot remove reactive node which is null!");
            }

            if (ContainsNode(node) == false)
            {
                throw new NodeNullReferenceException("Cannot remove reactive node which is not part of the graph!");
            }

            if (forceRemoval == false)
            {
                if (node.Predecessors.Count > 0 || node.Successors.Count > 0)
                {
                    throw new ReactiveNodeException("Cannot remove reactive node which participates in reactive dependencies!");
                }
            }
        }

        /// <summary>
        /// Clears unused nodes, i.e. removes all nodes from the graph which do not participate in reactive dependencies.
        /// </summary>
        /// <returns>Number of nodes removed from graph.</returns>
        public int RemoveUnusedNodes()
        {
            int numberOfRemovedNodes = 0;

            for (int i = Nodes.Count - 1; i >= 0; i--)
            {
                if (Nodes[i].Predecessors.Count == 0 && Nodes[i].Successors.Count == 0)
                {
                    RemoveNode(Nodes[i]);
                    numberOfRemovedNodes++;
                }
            }

            return numberOfRemovedNodes;
        }

        private void UnregisterGraphFromNode(INode node)
        {
            if (node.Graph == this)
            {
                node.Graph = null;
            }
        }

        private int RemoveNodesOfNonexistantObjects()
        {
            GC.Collect();

            int numberOfRemovedNodes = 0;

            for (int i = Nodes.Count - 1; i >= 0; i--)
            {
                if (CheckIfNodeHasNonexistantOwnerObject(Nodes[i]))
                {
                    RemoveNode(Nodes[i], true);
                    numberOfRemovedNodes++;
                }
            }

            return numberOfRemovedNodes;
        }

        private bool CheckIfNodeHasNonexistantOwnerObject(INode node)
        {
            return node.OwnerObject == null;
        }

        #endregion

        #region ContainsNode

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

        #endregion

        #region GetNode

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

        private INode GetOrCreateNode(INode node)
        {
            INode n = GetNode(node);
            if (n == null)
            {
                n = AddNode(node);
            }

            return n;
        }

        #endregion

        #region AddDependency

        /// <summary>
        /// Constructs reactive dependency between two reactive nodes, where the first node acts as a predecessor and
        /// the second one as successor. 
        /// </summary>
        /// <param name="predecessor">Reactive node acting as a predecessor in reactive dependecy.</param>
        /// <param name="successor">Reactive node acting as a successor in reactive dependency.</param>
        public void AddDependency(INode predecessor, INode successor)
        {
            ValidateDependencyInputParameters(predecessor, successor);

            INode p = GetOrCreateNode(predecessor);
            INode s = GetOrCreateNode(successor);

            ValidateDuplicateDependency(p, s);
            ValidateDirectCycles(p, s);

            p.AddSuccessor(s);
        }

        private static void ValidateDependencyInputParameters(INode predecessor, INode successor)
        {
            if (predecessor == null || successor == null)
            {
                throw new NodeNullReferenceException("Reactive dependency could not be added! At least one of the involved nodes is null!");
            }
        }

        private static void ValidateDirectCycles(INode p, INode s)
        {
            if (p.HasPredecessor(s) && s.HasSuccessor(p))
            {
                throw new ReactiveDependencyException("No direct cycles in reactive dependencies are allowed between reactive nodes!");
            }
        }

        private static void ValidateDuplicateDependency(INode p, INode s)
        {
            if (p.HasSuccessor(s) && s.HasPredecessor(p))
            {
                throw new ReactiveDependencyException("Reactive dependency cannot be added twice!");
            }
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

        #endregion

        #region RemoveDependency

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

        #endregion

        #region PerformUpdate

        public Task PerformUpdate(object ownerObject, string memberName)
        {
            INode initialNode = GetNode(ownerObject, memberName);
            return Updater.PerformUpdate(initialNode);
        }

        public Task PerformUpdate(ICollectionNodeItem ownerObject, string memberName)
        {
            return Updater.PerformUpdate(ownerObject, memberName);
        }

        /// <summary>
        /// Performs update of all nodes in dependency graph. Proper order of update is handled by topologically sorting dependend nodes.
        /// </summary>
        public Task PerformUpdate()
        {
            return Updater.PerformUpdate();
        }

        #endregion

        public bool ContainsDependency(INode predecessor, INode successor)
        {
            if (predecessor == null || successor == null)
            {
                throw new NodeNullReferenceException();
            }

            bool contains = false;

            if (predecessor.HasSuccessor(successor)
                && successor.HasPredecessor(predecessor))
            {
                contains = true;
            }

            return contains;
        }

        public void Clean()
        {
            RemoveNodesOfNonexistantObjects();
        }

        #endregion

        #region Events

        public event EventHandler UpdateCompleted;

        private void OnPerformUpdateCompleted()
        {
            UpdateCompleted?.Invoke(this, null);
        }

        #endregion
    }
}
