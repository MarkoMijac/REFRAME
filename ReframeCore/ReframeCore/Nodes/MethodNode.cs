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
    }
}
