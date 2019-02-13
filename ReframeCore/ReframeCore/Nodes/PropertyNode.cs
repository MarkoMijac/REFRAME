using ReframeCore.Exceptions;
using ReframeCore.Helpers;
using ReframeCore.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore.Nodes
{
    /// <summary>
    /// Reactive node which encapsulates object's property that needs to be tracked.
    /// </summary>
    public class PropertyNode : INode
    {
        #region Properties

        private Node DefaultImplementation { get; set; }

        /// <summary>
        /// Node's unique identifier.
        /// </summary>
        public uint Identifier
        {
            get => DefaultImplementation.Identifier;
            private set => DefaultImplementation.Identifier = value;
        }

        /// <summary>
        /// The name of the class member (property or method) reactive node represents.
        /// </summary>
        public string MemberName
        {
            get => DefaultImplementation.MemberName;
            private set => DefaultImplementation.MemberName = value;
        }

        /// <summary>
        /// An associated object which owns the member.
        /// </summary>
        public object OwnerObject
        {
            get => DefaultImplementation.OwnerObject;
            private set => DefaultImplementation.OwnerObject = value;
        }

        /// <summary>
        /// Delegate to the update method.
        /// </summary>
        public Action UpdateMethod
        {
            get => DefaultImplementation.UpdateMethod;
            set => DefaultImplementation.UpdateMethod = value;
        }

        /// <summary>
        /// List of reactive nodes that are predecessors to this reactive node.
        /// </summary>
        public IList<INode> Predecessors
        {
            get => DefaultImplementation.Predecessors;
            private set => DefaultImplementation.Predecessors = value;
        }

        /// <summary>
        /// List of reactive nodes that are successors to this reactive node.
        /// </summary>
        public IList<INode> Successors
        {
            get => DefaultImplementation.Successors;
            private set => DefaultImplementation.Successors = value;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Instantiates new reactive node.
        /// </summary>
        /// <param name="ownerObject">Associated object which owns the member.</param>
        /// <param name="memberName">The name of the class member reactive node represents.</param>
        public PropertyNode(object ownerObject, string memberName)
        {
            DefaultImplementation = new Node(ownerObject, memberName);
        }

        /// <summary>
        /// Instantiates new reactive node.
        /// </summary>
        /// <param name="ownerObject">Associated object which owns the member.</param>
        /// <param name="memberName">The name of the class member reactive node represents.</param>
        /// <param name="updateMethodName">Update method name.</param>
        public PropertyNode(object ownerObject, string memberName, string updateMethodName)
        {
            DefaultImplementation = new Node(ownerObject, memberName, updateMethodName);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Checks if specified reactive node has the same identifier as this reactive node.
        /// </summary>
        /// <param name="ownerObject">Associated object which owns the member.</param>
        /// <param name="memberName">Name of the member reactive node represents.</param>
        /// <returns>True if specified reactive node has the same identifier as this reactive node.</returns>
        public bool HasSameIdentifier(object ownerObject, string memberName)
        {
            return DefaultImplementation.HasSameIdentifier(ownerObject, memberName);
        }

        /// <summary>
        /// Checks if specified reactive node has the same identifier as this reactive node.
        /// </summary>
        /// <param name="node">Reactive node which we want to compare.</param>
        /// <returns>True if specified reactive node has the same identifier as this reactive node.</returns>
        public bool HasSameIdentifier(INode node)
        {
            return DefaultImplementation.HasSameIdentifier(node);
        }

        /// <summary>
        /// A method responsible for updating reactive node.
        /// </summary>
        public void Update()
        {
            DefaultImplementation.Update();
        }

        /// <summary>
        /// Checks if forwarded reactive node is a predecessor of this reactive node.
        /// </summary>
        /// <param name="predecessor">Reactive node that we check if it is a predecessor of this reactive node.</param>
        /// <returns>True if forwarded reactive node is a predecessor of this reactive node, otherwise False.</returns>
        public bool HasPredecessor(INode predecessor)
        {
            return DefaultImplementation.HasPredecessor(predecessor);
        }

        /// <summary>
        /// Checks if forwarded reactive node is a successor of this reactive node.
        /// </summary>
        /// <param name="predecessor">Reactive node that we check if it is a successor of this reactive node.</param>
        /// <returns>True if forwarded reactive node is a predecessor of this reactive node, otherwise False.</returns>
        public bool HasSuccessor(INode successor)
        {
            return DefaultImplementation.HasSuccessor(successor);
        }

        /// <summary>
        /// Adds predecessor to this reactive node.
        /// </summary>
        /// <param name="predecessor">Reactive node which becomes predecessor.</param>
        /// <returns>True if predecessor is added, otherwise False.</returns>
        public bool AddPredecessor(INode predecessor)
        {
            return DefaultImplementation.AddPredecessor(predecessor, this);
        }

        /// <summary>
        /// Removes node's predecessor.
        /// </summary>
        /// <param name="predecessor">Predecessor reactive node which should be removed.</param>
        /// <returns>True if predecessor removed, otherwise false.</returns>
        public bool RemovePredecessor(INode predecessor)
        {
            return DefaultImplementation.RemovePredecessor(predecessor, this);
        }

        /// <summary>
        /// Adds successor to this reactive node.
        /// </summary>
        /// <param name="successor">Reactive node which becomes sucessor.</param>
        /// <returns>True if successor is added, otherwise False.</returns>
        public bool AddSuccessor(INode successor)
        {
            return DefaultImplementation.AddSuccessor(this, successor);
        }

        /// <summary>
        /// Removes node's successor.
        /// </summary>
        /// <param name="successor">Successor reactive node which should be removed.</param>
        /// <returns>True if successor removed, otherwise false.</returns>
        public bool RemoveSuccessor(INode successor)
        {
            return DefaultImplementation.RemoveSuccessor(this, successor);
        }

        #endregion
    }
}
