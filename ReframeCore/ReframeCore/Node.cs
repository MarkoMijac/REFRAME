﻿using ReframeCore.Exceptions;
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
    public class Node : INode
    {
        #region Properties

        public uint Identifier { get; private set; }

        /// <summary>
        /// The name of the class member (property or method) reactive node represents.
        /// </summary>
        public string MemberName { get; private set; }

        /// <summary>
        /// An associated object which owns the member.
        /// </summary>
        public object OwnerObject { get; private set; }

        /// <summary>
        /// Delegate to the update method.
        /// </summary>
        private Action UpdateMethod { get; set; }

        /// <summary>
        /// List of reactive nodes that are predecessors to this reactive node.
        /// </summary>
        public IList<INode> Predecessors { get; private set; }

        /// <summary>
        /// List of reactive nodes that are successors to this reactive node.
        /// </summary>
        public IList<INode> Successors { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Instantiates new reactive node.
        /// </summary>
        /// <param name="ownerObject">Associated object which owns the member.</param>
        /// <param name="memberName">The name of the class member reactive node represents.</param>
        public Node(object ownerObject, string memberName)
        {
            Initialize(ownerObject, memberName, null);
        }

        /// <summary>
        /// Instantiates new reactive node.
        /// </summary>
        /// <param name="ownerObject">Associated object which owns the member.</param>
        /// <param name="memberName">The name of the class member reactive node represents.</param>
        /// <param name="updateMethodName">Update method name.</param>
        public Node(object ownerObject, string memberName, string updateMethodName)
        {
            Action action = Reflector.CreateAction(ownerObject, updateMethodName);
            Initialize(ownerObject, memberName, action);
        }

        /// <summary>
        /// Instantiates new reactive node.
        /// </summary>
        /// <param name="ownerObject">Associated object which owns the member.</param>
        /// <param name="memberName">The name of the class member reactive node represents.</param>
        /// <param name="updateMethod">Delegate to the update method.</param>
        public Node(object ownerObject, string memberName, Action updateMethod)
        {
            Initialize(ownerObject, memberName, updateMethod);
        }

        /// <summary>
        /// Initializes reactive node's properties.
        /// </summary>
        /// <param name="ownerObject">Associated object which owns the member.</param>
        /// <param name="memberName">The name of the class member reactive node represents.</param>
        /// <param name="updateMethod">Delegate to the update method.</param>
        private void Initialize(object ownerObject, string memberName, Action updateMethod)
        {
            ValidateArguments(ownerObject, memberName);

            OwnerObject = ownerObject;
            MemberName = memberName;
            UpdateMethod = updateMethod;

            Predecessors = new List<INode>();
            Successors = new List<INode>();

            Identifier = GetIdentifier();
        }

        /// <summary>
        /// Validates arguments passed in order to create reactive node.
        /// </summary>
        /// <param name="ownerObject">Associated object which owns the member.</param>
        /// <param name="memberName">The name of the class member reactive node represents.</param>
        private void ValidateArguments(object ownerObject, string memberName)
        {
            if (ownerObject == null 
                || Reflector.ContainsMember(ownerObject, memberName) == false)
            {
                throw new ReactiveNodeException("Unable to create reactive node! Not all provided arguments were valid!");
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets reactive node's unique identifier.
        /// </summary>
        /// <returns>Reactive node's unique identifier.</returns>
        private uint GetIdentifier()
        {
            return GetIdentifier(OwnerObject, MemberName);
        }

        /// <summary>
        /// Gets reactive node's unique identifier.
        /// </summary>
        /// <param name="ownerObject">Associated object which owns the member.</param>
        /// <param name="memberName">The name of the class member reactive node represents.</param>
        /// <returns>Reactive node's unique identifier.</returns>
        private uint GetIdentifier(object ownerObject, string memberName)
        {
            return (uint)(ownerObject.GetHashCode() + memberName.GetHashCode());
        }

        /// <summary>
        /// Checks if specified reactive node has the same identifier as this reactive node.
        /// </summary>
        /// <param name="ownerObject">Associated object which owns the member.</param>
        /// <param name="memberName">Name of the member reactive node represents.</param>
        /// <returns>True if specified reactive node has the same identifier as this reactive node.</returns>
        public bool HasSameIdentifier(object ownerObject, string memberName)
        {
            return Identifier == GetIdentifier(ownerObject, memberName);
        }

        /// <summary>
        /// Checks if specified reactive node has the same identifier as this reactive node.
        /// </summary>
        /// <param name="node">Reactive node which we want to compare.</param>
        /// <returns>True if specified reactive node has the same identifier as this reactive node.</returns>
        public bool HasSameIdentifier(INode node)
        {
            return Identifier == node.Identifier;
        }

        /// <summary>
        /// A method responsible for updating reactive node.
        /// </summary>
        public void Update()
        {
            UpdateMethod?.Invoke();
        }

        /// <summary>
        /// Checks if forwarded reactive node is a predecessor of this reactive node.
        /// </summary>
        /// <param name="predecessor">Reactive node that we check if it is a predecessor of this reactive node.</param>
        /// <returns>True if forwarded reactive node is a predecessor of this reactive node, otherwise False.</returns>
        public bool HasPredecessor(INode predecessor)
        {
            return Predecessors.Contains(predecessor);
        }

        /// <summary>
        /// Checks if forwarded reactive node is a successor of this reactive node.
        /// </summary>
        /// <param name="predecessor">Reactive node that we check if it is a successor of this reactive node.</param>
        /// <returns>True if forwarded reactive node is a predecessor of this reactive node, otherwise False.</returns>
        public bool HasSuccessor(INode successor)
        {
            return Successors.Contains(successor);
        }

        /// <summary>
        /// Adds predecessor to this reactive node.
        /// </summary>
        /// <param name="predecessor">Reactive node which becomes predecessor.</param>
        /// <returns>True if predecessor is added, otherwise False.</returns>
        public bool AddPredecessor(INode predecessor)
        {
            bool added = false;

            if (predecessor == null)
            {
                throw new ReactiveNodeException("Cannot add null object as a predecessor");
            }

            if (HasSameIdentifier(predecessor))
            {
                throw new ReactiveNodeException("Reactive node cannot be both predecessor and successor!");
            }

            if (!HasPredecessor(predecessor))
            {
                Predecessors.Add(predecessor);
                predecessor.AddSuccessor(this);
                added = true;
            }

            return added;
        }

        /// <summary>
        /// Removes node's predecessor.
        /// </summary>
        /// <param name="predecessor">Predecessor reactive node which should be removed.</param>
        /// <returns>True if predecessor removed, otherwise false.</returns>
        public bool RemovePredecessor(INode predecessor)
        {
            return Predecessors.Remove(predecessor) && predecessor.Successors.Remove(this);
        }

        /// <summary>
        /// Adds successor to this reactive node.
        /// </summary>
        /// <param name="successor">Reactive node which becomes sucessor.</param>
        /// <returns>True if successor is added, otherwise False.</returns>
        public bool AddSuccessor(INode successor)
        {
            bool added = false;

            if (successor == null)
            {
                throw new ReactiveNodeException("Cannot add null object as a successor!");
            }

            if (HasSameIdentifier(successor))
            {
                throw new ReactiveNodeException("Reactive node cannot be both predecessor and successor!");
            }

            if (!HasSuccessor(successor))
            {
                Successors.Add(successor);
                successor.AddPredecessor(this);
                added = true;
            }

            return added;
        }

        /// <summary>
        /// Removes node's successor.
        /// </summary>
        /// <param name="successor">Successor reactive node which should be removed.</param>
        /// <returns>True if successor removed, otherwise false.</returns>
        public bool RemoveSuccessor(INode successor)
        {
            return Successors.Remove(successor) && successor.Predecessors.Remove(this);
        }

        public override string ToString()
        {
            return Identifier + "; " + OwnerObject.GetType().ToString() + ";" + MemberName;
        }

        #endregion
    }
}
