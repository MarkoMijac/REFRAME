using ReframeCore.Exceptions;
using ReframeCore.Helpers;
using ReframeCore.Nodes;
using ReframeCore.ReactiveCollections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore.Nodes
{
    public class CollectionNode : Node
    {
        private string _updateMethodName = "";

        #region Constructors

        /// <summary>
        /// Instantiates new reactive node.
        /// </summary>
        /// <param name="collection">Collection of objects associated with the reactive node.</param>
        /// <param name="memberName">The name of the class member reactive node represents.</param>
        public CollectionNode(object collection, string memberName)
            : base(collection, memberName)
        {

        }

        /// <summary>
        /// Instantiates new reactive node.
        /// </summary>
        /// <param name="collection">Collection of objects associated with the reactive node.</param>
        /// <param name="memberName">The name of the class member reactive node represents.</param>
        /// <param name="updateMethodName">Update method name.</param>
        public CollectionNode(object collection, string memberName, string updateMethodName)
            :base(collection, memberName, updateMethodName)
        {
            _updateMethodName = updateMethodName;
        }

        /// <summary>
        /// Initializes reactive node's properties.
        /// </summary>
        /// <param name="ownerObject">Associated object which owns the member.</param>
        /// <param name="memberName">The name of the class member reactive node represents.</param>
        /// <param name="updateMethod">Delegate to the update method.</param>
        protected override void Initialize(object collection, string memberName, Action updateMethod)
        {
            Predecessors = new List<INode>();
            Successors = new List<INode>();

            MemberName = memberName;
            OwnerObject = collection;

            Identifier = GetIdentifier();
        }

        #endregion

        #region Methods

        protected override void ValidateArguments(object ownerObject, string memberName, string updateMethodName)
        {
            if (ownerObject == null
                || Reflector.ContainsMember(ownerObject, memberName) == false)
            {
                throw new ReactiveNodeException("Unable to create reactive node! Not all provided arguments were valid!");
            }
        }

        public override void Update()
        {
            if (_updateMethodName != "")
            {
                IEnumerable collection = OwnerObject as IEnumerable;

                Action updateMethod;
                foreach (var obj in collection)
                {
                    updateMethod = Reflector.CreateAction(obj, _updateMethodName);
                    updateMethod.Invoke();
                }
            }
        }

        #endregion
    }
}
