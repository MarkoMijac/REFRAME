using ReframeCore.Exceptions;
using ReframeCore.Helpers;
using ReframeCore.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore.Nodes
{
    public class MethodNode : Node
    {
        #region Constructors

        public MethodNode(object ownerObject, string memberName)
            : base(ownerObject, memberName)
        {
            
        }

        #endregion

        #region Methods

        protected override Action GetUpdateMethod()
        {
            Action action = null;

            if (OwnerObject != null)
            {
                action = Reflector.CreateAction(OwnerObject, MemberName);
            }

            return action;
        }

        protected override bool IsValidNode(object owner, string memberName, out string message)
        {
            message = "";
            if (Reflector.IsMethod(owner, memberName) == false)
            {
                message = "Unable to create reactive node! Provided member is not a valid method!";
                return false;
            }
            return true;
        }

        #endregion
    }
}
