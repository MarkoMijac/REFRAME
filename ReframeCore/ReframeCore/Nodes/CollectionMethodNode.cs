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
    public class CollectionMethodNode : CollectionNode
    {
        private string _updateAllMethodName = "UpdateAll";

        #region Constructors

        public CollectionMethodNode(object collection, string methodName)
            : base(collection, methodName)
        {

        }

        #endregion

        #region Methods

        protected override bool IsValidNode(object owner, string memberName, out string message)
        {
            message = "";
            if (memberName == "" || Reflector.IsMethod(owner, memberName) == false)
            {
                message = "Unable to create reactive node! Provided update method is not a valid method!";
                return false;
            }
            
            return true;
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
