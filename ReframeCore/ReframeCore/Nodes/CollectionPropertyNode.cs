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
    public class CollectionPropertyNode : CollectionNode
    {
        private string _updateAllMethodName = "UpdateAll";

        public string UpdateMethodName { get; private set; }

        #region Constructors

        /// <summary>
        /// Instantiates new reactive node.
        /// </summary>
        /// <param name="collection">Collection of objects associated with the reactive node.</param>
        /// <param name="memberName">The name of the class member reactive node represents.</param>
        public CollectionPropertyNode(object collection, string memberName)
            : base(collection, memberName)
        {
            ValidateArguments(collection, memberName);
            (OwnerObject as IReactiveCollection).UpdateTriggered += CollectionPropertyNode_UpdateTriggered;
            (OwnerObject as IReactiveCollection).CollectionNode = this;
        }

        /// <summary>
        /// Instantiates new reactive node.
        /// </summary>
        /// <param name="collection">Collection of objects associated with the reactive node.</param>
        /// <param name="memberName">The name of the class member reactive node represents.</param>
        /// <param name="updateMethodName">Update method name.</param>
        public CollectionPropertyNode(object collection, string memberName, string updateMethodName)
            :base(collection, memberName)
        {
            ValidateArguments(collection, memberName, updateMethodName);
            UpdateMethodName = updateMethodName;

            (OwnerObject as IReactiveCollection).UpdateTriggered += CollectionPropertyNode_UpdateTriggered;
            (OwnerObject as IReactiveCollection).CollectionNode = this;
        }

        #endregion

        #region Methods

        private void ValidateArguments(object ownerObject, string memberName)
        {
            if (Reflector.ContainsMember(ownerObject, memberName) == false)
            {
                throw new ReactiveNodeException("Unable to create reactive node! Not all provided arguments were valid!");
            }
        }

        protected void ValidateArguments(object ownerObject, string memberName, string updateMethodName)
        {
            ValidateArguments(ownerObject, memberName);

            if (updateMethodName != "" && Reflector.IsMethod(ownerObject, updateMethodName) == false)
            {
                throw new ReactiveNodeException("Unable to create reactive node! Provided update method is not a valid method!");
            }
        }

        private void UpdateAll()
        {
            if (UpdateMethodName != "")
            {
                IEnumerable collection = OwnerObject as IEnumerable;

                Action updateMethod;
                foreach (var obj in collection)
                {
                    updateMethod = Reflector.CreateAction(obj, UpdateMethodName);
                    updateMethod.Invoke();
                }
            }
        }

        private void CollectionPropertyNode_UpdateTriggered(object sender, EventArgs e)
        {
            var eArgs = e as ReactiveCollectionItemEventArgs;
            if (eArgs.MemberName == MemberName)
            {
                eArgs.CollectionNode = this;
            }
        }

        protected override Action GetUpdateMethod()
        {
            Action action = null;

            if (OwnerObject != null)
            {
                action = Reflector.CreateAction(this, _updateAllMethodName);
            }

            return action;
        }

        #endregion
    }
}
