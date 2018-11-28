using ReframeCore.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore
{
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

        #endregion

        #region Constructors

        /// <summary>
        /// Instantiates new dependency graph.
        /// </summary>
        public DependencyGraph() : this("")
        {

        }

        /// <summary>
        /// Instantiates new dependency graph.
        /// </summary>
        /// <param name="identifier">Dependency graph unique identifier.</param>
        public DependencyGraph(string identifier)
        {
            Identifier = identifier;
            Settings = new Settings();
            Nodes = new List<INode>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds new node to dependency graph if such node does not already exist in dependency graph.
        /// </summary>
        /// <param name="node">Reactive node which we want to add.</param>
        /// <returns>True if node is added to dependency graph, False if reactive node has already existed.</returns>
        private bool AddNode(INode node)
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
        /// <returns>True if node is added to dependency graph, False if reactive node has already existed.</returns>
        private bool AddNode(object ownerObject, string memberName)
        {
            string updateMethodName = GetDefaultUpdateMethodName(memberName);
            return AddNode(ownerObject, memberName, "");
        }

        /// <summary>
        /// Adds node to dependency graph if such node does not already exist in dependency graph.
        /// </summary>
        /// <param name="ownerObject">Owner object associated with reactive node.</param>
        /// <param name="memberName">Member name represented by reactive node.</param>
        /// <param name="updateMethodName">Update method name.</param>
        /// <returns>True if node is added to dependency graph, False if reactive node has already existed.</returns>
        private bool AddNode(object ownerObject, string memberName, string updateMethodName)
        {
            bool added = false;

            if (ContainsNode(ownerObject, memberName) == false)
            {
                Nodes.Add(CreateNewNode(ownerObject, memberName, updateMethodName));
                added = true;
            }

            return added;
        }

        /// <summary>
        /// Checks if dependency graph contains provided reactive node.
        /// </summary>
        /// <param name="node">Reactive node for which we are checking.</param>
        /// <returns>True if dependency graph contains reactive node, otherwise False.</returns>
        private bool ContainsNode(INode node)
        {
            return GetNode(node) != null;
        }

        /// <summary>
        /// Checks if dependency graph contains provided reactive node.
        /// </summary>
        /// <param name="ownerObject">Owner object associated with reactive node.</param>
        /// <param name="memberName">Member name represented by reactive node.</param>
        /// <returns>True if dependency graph contains reactive node, otherwise False.</returns>
        private bool ContainsNode(object ownerObject, string memberName)
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
            return new Node(ownerObject, memberName, updateMethodName);
        }

        /// <summary>
        /// Gets reactive node from dependency graph if exists, otherwise returns null.
        /// </summary>
        /// <param name="ownerObject">Owner object associated with reactive node.</param>
        /// <param name="memberName">Member name represented by the reactive node.</param>
        /// <returns>Reactive node if exists in dependency graph, otherwise null.</returns>
        private INode GetNode(object ownerObject, string memberName)
        {
            return Nodes.FirstOrDefault(n => n.HasSameIdentifier(ownerObject, memberName));
        }

        /// <summary>
        /// Gets existing reactive node from dependency graph, otherwise returns null.
        /// </summary>
        /// <param name="node">Reactive node which we want to find.</param>
        /// <returns>Reactive node if such exists in dependency graph, otherwise null.</returns>
        private INode GetNode(INode node)
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
            INode p = GetNode(predecessor);
            INode s = GetNode(successor);

            if (p == null || s == null)
            {
                throw new ReactiveDependencyException("Reactive dependency could not be added! Specified reactive nodes do not exist!");
            }

            p.AddSuccessor(s);
            s.AddPredecessor(p);
        }

        /// <summary>
        /// Removes reactive node from dependency graph if the node does not participate in any reactive dependencies (i.e. it doesn't have any predecessors or successors).
        /// </summary>
        /// <param name="node">Node which we want to remove from dependency graph.</param>
        private void RemoveNode(INode node)
        {
            if (node == null)
            {
                throw new NodeNullReferenceException("Cannot remove reactive node which is null!");
            }

            if (node.Predecessors.Count > 0 || node.Successors.Count > 0)
            {
                throw new ReactiveNodeException("Cannot remove reactive node which participates in reactive dependencies!");
            }

            Nodes.Remove(node);
        }

        /// <summary>
        /// Removes reactive dependency between two nodes if it exists. The first node stops to be predecessor of the second node,
        /// and the second node stops to be successor on the first node.
        /// </summary>
        /// <param name="predecessor">Reactive nodes acting as a predecessor in reactive dependecy.</param>
        /// <param name="successor">Reactive node acting as a successor in reactive dependency.</param>
        private void RemoveDependency(INode predecessor, INode successor)
        {
            INode p = GetNode(predecessor);
            INode s = GetNode(successor);

            if (p == null || s == null)
            {
                throw new ReactiveDependencyException("Reactive dependency could not be removed! Specified reactive nodes do not exist!");
            }

            p.Successors.Remove(s);
            s.Predecessors.Remove(p);

            //Removing nodes if they are not participating in reactive dependencies anymore (i.e. they do not have any predecessors or dependents).
            if (p.Predecessors.Count == 0 && p.Successors.Count == 0)
            {
                RemoveNode(p);
            }

            if (s.Predecessors.Count == 0 && s.Successors.Count == 0)
            {
                RemoveNode(s);
            }
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

        #endregion
    }
}
