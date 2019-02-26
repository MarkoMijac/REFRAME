using ReframeCore.Exceptions;
using ReframeCore.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore.Nodes
{
    public class CollectionMethodNode : Node
    {
        private string _updateAllMethodName = "UpdateAll";

        #region Constructors

        public CollectionMethodNode(object collection, string methodName)
            : base(collection, methodName)
        {
            ValidateArguments(collection, methodName);
            UpdateMethod = Reflector.CreateAction(this, _updateAllMethodName);
        }

        #endregion

        #region Methods

        private void ValidateArguments(object ownerObject, string methodName)
        {
            if (methodName == "" || Reflector.IsMethod(ownerObject, methodName) == false)
            {
                throw new ReactiveNodeException("Unable to create reactive node! Provided update method is not a valid method!");
            }
        }

        private void UpdateAll()
        {
            IEnumerable collection = OwnerObject as IEnumerable;

            Action updateMethod;
            foreach (var obj in collection)
            {
                updateMethod = Reflector.CreateAction(obj, MemberName);
                updateMethod.Invoke();
            }
        }

        #endregion
    }
}
