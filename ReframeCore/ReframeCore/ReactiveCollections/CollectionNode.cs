using ReframeCore.Exceptions;
using ReframeCore.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore.ReactiveCollections
{
    public class CollectionNode<T> : INode
    {
        #region Properties

        private Node DefaultImplementation { get; set; }

        /// <summary>
        /// Node's unique identifier.
        /// </summary>
        public uint Identifier
        {
            get; set;
        }

        /// <summary>
        /// The name of the class member (property or method) reactive node represents.
        /// </summary>
        public string MemberName
        {
            get; set;
        }

        /// <summary>
        /// An associated object which owns the member.
        /// </summary>
        public object OwnerObject
        {
            get; set;
        }

        /// <summary>
        /// List of reactive nodes that are predecessors to this reactive node.
        /// </summary>
        public IList<INode> Predecessors
        {
            get; set;
        }

        /// <summary>
        /// List of reactive nodes that are successors to this reactive node.
        /// </summary>
        public IList<INode> Successors
        {
            get; set;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Instantiates new reactive node.
        /// </summary>
        /// <param name="ownerObject">Associated object which owns the member.</param>
        /// <param name="memberName">The name of the class member reactive node represents.</param>
        public CollectionNode(ReactiveCollection<T> owner, string memberName)
        {
            Initialize(owner, memberName);
        }


        /// <summary>
        /// Initializes reactive node's properties.
        /// </summary>
        /// <param name="ownerObject">Associated object which owns the member.</param>
        /// <param name="memberName">The name of the class member reactive node represents.</param>
        /// <param name="updateMethod">Delegate to the update method.</param>
        private void Initialize(ReactiveCollection<T> owner, string memberName)
        {
            ValidateArguments(owner, memberName);

            Predecessors = new List<INode>();
            Successors = new List<INode>();

            MemberName = memberName;
            OwnerObject = owner;

            Identifier = GetIdentifier();
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

        /// <summary>
        /// Validates arguments passed in order to create reactive node.
        /// </summary>
        /// <param name="ownerObject">Associated object which owns the member.</param>
        /// <param name="memberName">The name of the class member reactive node represents.</param>
        private void ValidateArguments(ReactiveCollection<T> ownerObject, string memberName)
        {
            if (ownerObject == null
                || Reflector.ContainsMember(ownerObject, memberName) == false)
            {
                throw new ReactiveNodeException("Unable to create reactive node! Not all provided arguments were valid!");
            }
        }

        #endregion
    }
}
