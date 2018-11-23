using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore
{
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
            Nodes = new List<INode>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds reactive node to dependency graph if it is not already added.
        /// </summary>
        /// <param name="node">Reactive node which is to be added.</param>
        /// <returns>True if reactive node is added, otherwise False.</returns>
        //private bool AddNode(INode node)
        //{
        //    bool added = false;

        //    if (Nodes.Contains(node) == false)
        //    {
        //        Nodes.Add(node);
        //        added = true;
        //    }

        //    return added;
        //}


        private INode AddNode(INode node)
        {
            INode n = GetNode(node);
            if (n == null)
            {
                n = node;
                Nodes.Add(n);
            }

            return n;
        }

        /// <summary>
        /// Adds new node in dependency graph if such node does not exist.
        /// </summary>
        /// <param name="ownerObject">Owner object associated with reactive node.</param>
        /// <param name="memberName">Member name represented by reactive node.</param>
        /// <param name="updateMethod">Delegate of the update method.</param>
        /// <returns>Newly added or existing reactive node.</returns>
        private INode AddNode(object ownerObject, string memberName, Action updateMethod)
        {
            INode node = GetNode(ownerObject, memberName);
            if (node == null)
            {
                node = CreateNewNode(ownerObject, memberName, updateMethod);
                Nodes.Add(node);
            }

            return node;
        }

        /// <summary>
        /// Creates new reactive node.
        /// </summary>
        /// <param name="ownerObject">Owner object associated with reactive node.</param>
        /// <param name="memberName">Member name represented by reactive node.</param>
        /// <param name="updateMethod">Delegate of the update method.</param>
        /// <returns>New reactive node.</returns>
        private INode CreateNewNode(object ownerObject, string memberName, Action updateMethod)
        {
            return new Node(ownerObject, memberName, updateMethod);
        }

        /// <summary>
        /// Gets existing reactive node from dependency graph.
        /// </summary>
        /// <param name="ownerObject">Owner object associated with reactive node.</param>
        /// <param name="memberName">Member name represented by the reactive node.</param>
        /// <returns>Reactive node if exists in dependency graph, otherwise null.</returns>
        private INode GetNode(object ownerObject, string memberName)
        {
            return Nodes.FirstOrDefault(n => n.HasSameIdentifier(ownerObject, memberName));
        }

        /// <summary>
        /// Gets existing reactive node from dependency graph.
        /// </summary>
        /// <param name="node">Reactive node which we want to find.</param>
        /// <returns>Reactive node if such exists in dependency graph, otherwise null.</returns>
        private INode GetNode(INode node)
        {
            return GetNode(node.OwnerObject, node.MemberName);
        }

        /// <summary>
        /// Constructs reactive dependency between two reactive nodes, where the first node acts as a predecessor and
        /// the second one as successor. 
        /// </summary>
        /// <param name="predecessor">Reactive nodes acting as a predecessor in reactive dependecy.</param>
        /// <param name="successor">Reactive node acting as a successor in reactive dependency.</param>
        /// <returns></returns>
        public bool AddDependency(INode predecessor, INode successor)
        {
            bool added = false;

            if (predecessor != null && successor != null)
            {
                AddNode(predecessor);
                AddNode(successor);

                bool addedSuccessor = predecessor.AddSuccessor(successor);
                bool addedPredecessor = successor.AddPredecessor(predecessor);
                added = addedSuccessor & addedPredecessor;
            }

            return added;
        }

        #endregion
    }
}
