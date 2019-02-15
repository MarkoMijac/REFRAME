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
            : base(ownerObject, memberName, memberName)
        {
           
        }

        #endregion

        #region Methods

        protected override void ValidateArguments(object ownerObject, string memberName, string updateMethodName)
        {
            if (ownerObject == null || Reflector.IsMethod(ownerObject, memberName) == false)
            {
                throw new ReactiveNodeException("Unable to create reactive node! Not all provided arguments were valid!");
            }
        }

        #endregion
    }
}
