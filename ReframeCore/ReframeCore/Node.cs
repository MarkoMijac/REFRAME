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
        /// <summary>
        /// The class member (property or method) reactive node represents.
        /// </summary>
        public string MemberName { get; private set; }

        /// <summary>
        /// An associated object which owns the member.
        /// </summary>
        public object OwnerObject { get; private set; }

        public Node(string memberName, object ownerObject)
        {
            MemberName = memberName;
            OwnerObject = ownerObject;
        }

        /// <summary>
        /// A method responsible for updating reactive node.
        /// </summary>
        public void Update()
        {

        }
    }
}
