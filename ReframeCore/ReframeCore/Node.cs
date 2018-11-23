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

        public int Identifier { get; private set; }

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
        /// <param name="memberName">The name of the class member reactive node represents.</param>
        /// <param name="ownerObject">Associated object which owns the member.</param>
        public Node(string memberName, object ownerObject, Action updateMethod)
        {
            MemberName = memberName;
            OwnerObject = ownerObject;
            UpdateMethod = updateMethod;

            Predecessors = new List<INode>();
            Successors = new List<INode>();

            Identifier = GetIdentifier();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets reactive node's unique identifier.
        /// </summary>
        /// <returns></returns>
        private int GetIdentifier()
        {
            return GetIdentifier(OwnerObject, MemberName);
        }

        /// <summary>
        /// Gets reactive node's unique identifier.
        /// </summary>
        /// <param name="o">Owner object.</param>
        /// <param name="m">Member name.</param>
        /// <returns>Reactive node's unique identifier.</returns>
        private int GetIdentifier(object o, string m)
        {
            return o.GetHashCode() + m.GetHashCode();
        }

        /// <summary>
        /// Checks if forwarded owner object and member name have the same identifier as this reactive node.
        /// </summary>
        /// <param name="ownerObject">Owner object to be compared.</param>
        /// <param name="memberName">Member name to be compared.</param>
        /// <returns>True if forwarded combination has the same identifier as this reactive node.</returns>
        public bool IsEqualIdentifier(object ownerObject, string memberName)
        {
            return GetIdentifier() == GetIdentifier(ownerObject, memberName);
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
        private bool IsPredecessor(INode predecessor)
        {
            return Predecessors.Contains(predecessor);
        }

        /// <summary>
        /// Checks if forwarded reactive node is a successor of this reactive node.
        /// </summary>
        /// <param name="predecessor">Reactive node that we check if it is a successor of this reactive node.</param>
        /// <returns>True if forwarded reactive node is a predecessor of this reactive node, otherwise False.</returns>
        private bool IsSuccessor(INode successor)
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

            if (!IsPredecessor(predecessor) && predecessor != this)
            {
                Predecessors.Add(predecessor);
                added = true;
            }

            return added;
        }

        /// <summary>
        /// Adds successor to this reactive node.
        /// </summary>
        /// <param name="successor">Reactive node which becomes sucessor.</param>
        /// <returns>True if successor is added, otherwise False.</returns>
        public bool AddSuccessor(INode successor)
        {
            bool added = false;

            if (!IsSuccessor(successor) && successor != this)
            {
                Successors.Add(successor);
                added = true;
            }

            return added;
        }

        public override string ToString()
        {
            return OwnerObject.GetType().ToString() + " -> " + MemberName;
        }

        #endregion
    }
}
