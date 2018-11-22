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
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets reactive node's unique identifier.
        /// </summary>
        /// <returns></returns>
        public int GetIdentifier()
        {
            return OwnerObject.GetHashCode() + MemberName.GetHashCode();
        }

        /// <summary>
        /// A method responsible for updating reactive node.
        /// </summary>
        public void Update()
        {
            UpdateMethod?.Invoke();
        }

        public override string ToString()
        {
            return OwnerObject.GetType().ToString() + " -> " + MemberName;
        }

        #endregion
    }
}
