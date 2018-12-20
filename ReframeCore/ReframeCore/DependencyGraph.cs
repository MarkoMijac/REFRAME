using ReframeCore.Exceptions;
using ReframeCore.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        private Settings Settings { get; set; }

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
            Status = DependencyGraphStatus.NotInitialized;
            Logger = new Logger();
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
        /// <returns>True if node is added to dependency graph, False if reactive node has already existed.</returns>
        public bool AddNode(INode node)
        {
            bool added = false;

            if (node == null)
            {
                throw new NodeNullReferenceException();
            }

            if (ContainsNode(node) == false)
            {
                Nodes.Add(node);
                added = true;
            }

            return added;
        }

        /// <summary>
        /// Adds node to dependency graph if such node does not already exist in dependency graph.
        /// </summary>
        /// <param name="ownerObject">Owner object associated with reactive node.</param>
        /// <param name="memberName">Member name represented by reactive node.</param>
        /// <returns>Newly added or already existing node.</returns>
        public INode AddNode(object ownerObject, string memberName)
        {
            string updateMethodName = "";
            if (Settings.UseDefaultUpdateMethodNames == true)
            {
                updateMethodName = GetDefaultUpdateMethodName(memberName);
            }

            return AddNode(ownerObject, memberName, updateMethodName);
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
            INode addedNode = GetNode(ownerObject, memberName);

            if (addedNode == null)
            {
                addedNode = CreateNewNode(ownerObject, memberName, updateMethodName);
                Nodes.Add(addedNode);
            }

            return addedNode;
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
        /// Creates new reactive node.
        /// </summary>
        /// <param name="ownerObject">Owner object associated with reactive node.</param>
        /// <param name="memberName">Member name represented by reactive node.</param>
        /// <param name="updateMethod">Delegate of the update method.</param>
        /// <returns>New reactive node.</returns>
        private INode CreateNewNode(object ownerObject, string memberName, string updateMethodName)
        {
            return new PropertyNode(ownerObject, memberName, updateMethodName);
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
        /// <param name="predecessor">Reactive nodes acting as a predecessor in reactive dependecy.</param>
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
        /// Gets update method name generated from default prefix and property name.
        /// </summary>
        /// <param name="memberName">Member name represented by reactive node.</param>
        /// <returns>Update method name generated from default prefix and property name.</returns>
        private string GetDefaultUpdateMethodName(string memberName)
        {
            return Settings.UpdateMethodNamePrefix + memberName;
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
        public void PerformUpdate(INode initialNode)
        {
            if (Status != DependencyGraphStatus.NotInitialized)
            {
                Status = DependencyGraphStatus.NotConsistent;

                IList<INode> nodesToUpdate = GetNodesToUpdate(initialNode);
                foreach (var node in nodesToUpdate)
                {
                    node.Update();
                }

                Status = DependencyGraphStatus.Consistent;
            }
            else
            {
                throw new DependencyGraphException("Dependency graph is not initialized!");
            }
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
            else
            {
                throw new DependencyGraphException("Dependency graph is not initialized!");
            }
        }

        /// <summary>
        /// Gets nodes from dependency graph that need to be updated, arranged in order they should be updated.
        /// </summary>
        /// <param name="node">Initial node that triggered the update.</param>
        /// <returns>List of nodes from dependency graph that need to be updated.</returns>
        private IList<INode> GetNodesToUpdate(INode node)
        {
            IList<INode> nodesToUpdate = null;

            INode initialNode = GetNode(node);

            if (initialNode == null)
            {
                throw new NodeNullReferenceException("Reactive node set as initial node of the update process is not part of the graph!");
            }

            ISort sortAlgorithm = new TopologicalSort();
            nodesToUpdate = sortAlgorithm.Sort(Nodes, n => n.Successors, initialNode, true);

            LogNodesToUpdate(nodesToUpdate);

            return nodesToUpdate;
        }

        /// <summary>
        /// Gets all nodes from dependency graph, arranged in order they should be updated.
        /// </summary>
        /// <returns>List of all nodes from dependency graph.</returns>
        private IList<INode> GetNodesToUpdate()
        {
            ISort sortAlgorithm = new TopologicalSort();
            IList<INode> nodesToUpdate = sortAlgorithm.Sort(Nodes, n => n.Successors);

            LogNodesToUpdate(nodesToUpdate);

            return nodesToUpdate;
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
