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
            ValidateArguments(ownerObject, memberName);
            UpdateMethod = Reflector.CreateAction(ownerObject, memberName);
        }

        #endregion

        #region Methods

        private void ValidateArguments(object ownerObject, string memberName)
        {
            if (Reflector.IsMethod(ownerObject, memberName) == false)
            {
                throw new ReactiveNodeException("Unable to create reactive node! Provided member is not a valid method!");
            }
        }

        #endregion
    }
}
