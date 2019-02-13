using ReframeCore.Exceptions;
using ReframeCore.Helpers;
using ReframeCore.Nodes;
using ReframeCore.ReactiveCollections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore.Nodes
{
    public class CollectionNode<T> : Node
    {
        #region Constructors

        /// <summary>
        /// Instantiates new reactive node.
        /// </summary>
        /// <param name="ownerObject">Associated object which owns the member.</param>
        /// <param name="memberName">The name of the class member reactive node represents.</param>
        public CollectionNode(ReactiveCollection<T> owner, string memberName)
            :base(owner, memberName)
        {
            
        }

        /// <summary>
        /// Initializes reactive node's properties.
        /// </summary>
        /// <param name="ownerObject">Associated object which owns the member.</param>
        /// <param name="memberName">The name of the class member reactive node represents.</param>
        /// <param name="updateMethod">Delegate to the update method.</param>
        protected override void Initialize(object owner, string memberName, Action updateMethod)
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

        protected override void ValidateArguments(object ownerObject, string memberName)
        {
            ReactiveCollection<T> collection = ownerObject as ReactiveCollection<T>;

            if (collection == null
                || Reflector.ContainsMember(collection, memberName) == false)
            {
                throw new ReactiveNodeException("Unable to create reactive node! Not all provided arguments were valid!");
            }
        }

        #endregion
    }
}
