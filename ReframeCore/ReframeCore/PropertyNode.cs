using ReframeCore.Exceptions;
using ReframeCore.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore
{
    /// <summary>
    /// Reactive node which encapsulates object's property that needs to be tracked.
    /// </summary>
    public class PropertyNode : INode
    {
        #region Properties

        private Node DefaultNodeImplementation { get; set; }

        /// <summary>
        /// Node's unique identifier.
        /// </summary>
        public uint Identifier
        {
            get => DefaultNodeImplementation.Identifier;
            private set => DefaultNodeImplementation.Identifier = value;
        }

        /// <summary>
        /// The name of the class member (property or method) reactive node represents.
        /// </summary>
        public string MemberName
        {
            get => DefaultNodeImplementation.MemberName;
            private set => DefaultNodeImplementation.MemberName = value;
        }

        /// <summary>
        /// An associated object which owns the member.
        /// </summary>
        public object OwnerObject
        {
            get => DefaultNodeImplementation.OwnerObject;
            private set => DefaultNodeImplementation.OwnerObject = value;
        }

        /// <summary>
        /// Delegate to the update method.
        /// </summary>
        private Action UpdateMethod
        {
            get => DefaultNodeImplementation.UpdateMethod;
            set => DefaultNodeImplementation.UpdateMethod = value;
        }

        /// <summary>
        /// List of reactive nodes that are predecessors to this reactive node.
        /// </summary>
        public IList<INode> Predecessors
        {
            get => DefaultNodeImplementation.Predecessors;
            private set => DefaultNodeImplementation.Predecessors = value;
        }

        /// <summary>
        /// List of reactive nodes that are successors to this reactive node.
        /// </summary>
        public IList<INode> Successors
        {
            get => DefaultNodeImplementation.Successors;
            private set => DefaultNodeImplementation.Successors = value;
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
            DefaultNodeImplementation = new Node(ownerObject, memberName);
        }

        /// <summary>
        /// Instantiates new reactive node.
        /// </summary>
        /// <param name="ownerObject">Associated object which owns the member.</param>
        /// <param name="memberName">The name of the class member reactive node represents.</param>
        /// <param name="updateMethodName">Update method name.</param>
        public PropertyNode(object ownerObject, string memberName, string updateMethodName)
        {
            DefaultNodeImplementation = new Node(ownerObject, memberName, updateMethodName);
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
            return DefaultNodeImplementation.HasSameIdentifier(ownerObject, memberName);
        }

        /// <summary>
        /// Checks if specified reactive node has the same identifier as this reactive node.
        /// </summary>
        /// <param name="node">Reactive node which we want to compare.</param>
        /// <returns>True if specified reactive node has the same identifier as this reactive node.</returns>
        public bool HasSameIdentifier(INode node)
        {
            return DefaultNodeImplementation.HasSameIdentifier(node);
        }

        /// <summary>
        /// A method responsible for updating reactive node.
        /// </summary>
        public void Update()
        {
            DefaultNodeImplementation.Update();
        }

        /// <summary>
        /// Checks if forwarded reactive node is a predecessor of this reactive node.
        /// </summary>
        /// <param name="predecessor">Reactive node that we check if it is a predecessor of this reactive node.</param>
        /// <returns>True if forwarded reactive node is a predecessor of this reactive node, otherwise False.</returns>
        public bool HasPredecessor(INode predecessor)
        {
            return DefaultNodeImplementation.HasPredecessor(predecessor);
        }

        /// <summary>
        /// Checks if forwarded reactive node is a successor of this reactive node.
        /// </summary>
        /// <param name="predecessor">Reactive node that we check if it is a successor of this reactive node.</param>
        /// <returns>True if forwarded reactive node is a predecessor of this reactive node, otherwise False.</returns>
        public bool HasSuccessor(INode successor)
        {
            return DefaultNodeImplementation.HasSuccessor(successor);
        }

        /// <summary>
        /// Adds predecessor to this reactive node.
        /// </summary>
        /// <param name="predecessor">Reactive node which becomes predecessor.</param>
        /// <returns>True if predecessor is added, otherwise False.</returns>
        public bool AddPredecessor(INode predecessor)
        {
            return DefaultNodeImplementation.AddPredecessor(predecessor, this);
        }

        /// <summary>
        /// Removes node's predecessor.
        /// </summary>
        /// <param name="predecessor">Predecessor reactive node which should be removed.</param>
        /// <returns>True if predecessor removed, otherwise false.</returns>
        public bool RemovePredecessor(INode predecessor)
        {
            return DefaultNodeImplementation.RemovePredecessor(predecessor, this);
        }

        /// <summary>
        /// Adds successor to this reactive node.
        /// </summary>
        /// <param name="successor">Reactive node which becomes sucessor.</param>
        /// <returns>True if successor is added, otherwise False.</returns>
        public bool AddSuccessor(INode successor)
        {
            return DefaultNodeImplementation.AddSuccessor(this, successor);
        }

        /// <summary>
        /// Removes node's successor.
        /// </summary>
        /// <param name="successor">Successor reactive node which should be removed.</param>
        /// <returns>True if successor removed, otherwise false.</returns>
        public bool RemoveSuccessor(INode successor)
        {
            return DefaultNodeImplementation.RemoveSuccessor(this, successor);
        }

        #endregion
    }
}
