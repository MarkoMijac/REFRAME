﻿using ReframeCore.Exceptions;
using ReframeCore.Helpers;
using ReframeCore.Nodes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore.Nodes
{
    public abstract class Node : INode, IUpdateInfoProvider
    {
        #region Properties

        public int Layer { get; set; }

        /// <summary>
        /// Node's unique identifier.
        /// </summary>
        public uint Identifier { get; set; }

        /// <summary>
        /// The name of the class member which reactive node represents.
        /// </summary>
        public string MemberName { get; set; }

        private WeakReference _weakOwnerObject;

        /// <summary>
        /// An associated object which owns the member.
        /// </summary>
        public object OwnerObject
        {
            get
            {
                if (_weakOwnerObject == null || _weakOwnerObject.IsAlive == false)
                {
                    return null;
                }
                return _weakOwnerObject.Target;
            }
        }

        /// <summary>
        /// List of reactive nodes that are predecessors to this reactive node.
        /// </summary>
        private IList<INode> _predecessors;

        /// <summary>
        /// List of reactive nodes that are successors to this reactive node.
        /// </summary>
        private IList<INode> _successors;

        /// <summary>
        /// Delegate to the update method.
        /// </summary>
        private Action UpdateMethod
        {
            get
            {
                return GetUpdateMethod();
            }
        }

        public IDependencyGraph Graph { get; set; }

        public NodeUpdateInfo UpdateInfo
        {
            get; private set;
        }

        #endregion

        #region Constructors

        public Node(object ownerObject, string memberName)
        {
            ValidateArguments(ownerObject, memberName);
            Initialize(ownerObject, memberName);
        }

        /// <summary>
        /// Initializes reactive node's properties.
        /// </summary>
        /// <param name="ownerObject">Associated object which owns the member.</param>
        /// <param name="memberName">The name of the class member reactive node represents.</param>
        /// <param name="updateMethod">Delegate to the update method.</param>
        protected virtual void Initialize(object ownerObject, string memberName)
        {
            _weakOwnerObject = new WeakReference(ownerObject);
            MemberName = memberName;

            _predecessors = new List<INode>();
            _successors = new List<INode>();

            Identifier = GenerateIdentifier();
            UpdateInfo = new NodeUpdateInfo();
        }

        /// <summary>
        /// Validates arguments passed in order to create reactive node.
        /// </summary>
        /// <param name="ownerObject">Associated object which owns the member.</param>
        /// <param name="memberName">The name of the class member reactive node represents.</param>
        protected virtual void ValidateArguments(object ownerObject, string memberName)
        {
            if (ownerObject == null)
            {
                throw new ReactiveNodeException("Unable to create reactive node! Provided object is not valid!");
            }

            string message = "";
            if (IsValidNode(ownerObject, memberName, out message) == false)
            {
                throw new ReactiveNodeException(message);
            }
        }

        protected abstract bool IsValidNode(object owner, string memberName, out string message);

        #endregion

        #region Methods

        public IReadOnlyList<INode> Predecessors
        {
            get
            {
                return (_predecessors as List<INode>).AsReadOnly();
            }
        }

        public IReadOnlyList<INode> Successors
        {
            get
            {
                return (_successors as List<INode>).AsReadOnly();
            }
        }

        /// <summary>
        /// Gets reactive node's unique identifier.
        /// </summary>
        /// <returns>Reactive node's unique identifier.</returns>
        private uint GenerateIdentifier()
        {
            return GenerateIdentifier(OwnerObject, MemberName);
        }

        /// <summary>
        /// Gets reactive node's unique identifier.
        /// </summary>
        /// <param name="owner">Associated object which owns the member.</param>
        /// <param name="member">The name of the class member reactive node represents.</param>
        /// <returns>Reactive node's unique identifier.</returns>
        private uint GenerateIdentifier(object owner, string member)
        {
            uint id = 0;

            if (owner != null && member != "")
            {
                id = (uint)(owner.GetHashCode() ^ member.GetHashCode());
            }

            return id;
        }

        /// <summary>
        /// Checks if specified reactive node has the same identifier as this reactive node.
        /// </summary>
        /// <param name="ownerObject">Associated object which owns the member.</param>
        /// <param name="memberName">Name of the member reactive node represents.</param>
        /// <returns>True if specified reactive node has the same identifier as this reactive node.</returns>
        public bool HasSameIdentifier(object ownerObject, string memberName)
        {
            return Identifier == GenerateIdentifier(ownerObject, memberName);
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
            UpdateInfo.StartMeasuring();
            UpdateMethod?.Invoke();
            UpdateInfo.EndMeasuring();

            SaveValues();
        }

        public virtual void SaveValues()
        {

        }

        /// <summary>
        /// Checks if forwarded reactive node is a predecessor of this reactive node.
        /// </summary>
        /// <param name="predecessor">Reactive node that we check if it is a predecessor of this reactive node.</param>
        /// <returns>True if forwarded reactive node is a predecessor of this reactive node, otherwise False.</returns>
        public bool HasPredecessor(INode predecessor)
        {
            return _predecessors.Contains(predecessor);
        }

        /// <summary>
        /// Checks if forwarded reactive node is a successor of this reactive node.
        /// </summary>
        /// <param name="predecessor">Reactive node that we check if it is a successor of this reactive node.</param>
        /// <returns>True if forwarded reactive node is a predecessor of this reactive node, otherwise False.</returns>
        public bool HasSuccessor(INode successor)
        {
            return _successors.Contains(successor);
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
                _predecessors.Add(predecessor);
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
            if (predecessor == null)
            {
                return false;
            }

            bool removed = _predecessors.Remove(predecessor);
            if (predecessor.HasSuccessor(this))
            {
                predecessor.RemoveSuccessor(this);
            }
            return removed;
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
                _successors.Add(successor);
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
            if (successor == null)
            {
                return false;
            }

            bool removed = _successors.Remove(successor);
            if (successor.HasPredecessor(this))
            {
                successor.RemovePredecessor(this);
            }
            return removed;
        }

        public virtual bool IsTriggered()
        {
            return true;
        }

        public int ClearPredecessors()
        {
            int numOfRemoved = 0;

            for (int i = _predecessors.Count - 1; i >= 0; i--)
            {
                RemovePredecessor(_predecessors[i]);
                numOfRemoved++;
            }

            return numOfRemoved;
        }

        public int ClearSuccessors()
        {
            int numOfRemoved = 0;

            for (int i = _successors.Count - 1; i >= 0; i--)
            {
                RemoveSuccessor(_successors[i]);
                numOfRemoved++;
            }

            return numOfRemoved;
        }

        protected abstract Action GetUpdateMethod();

        #endregion
    }
}
