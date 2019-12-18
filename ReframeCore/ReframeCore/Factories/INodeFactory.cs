using ReframeCore.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore.Factories
{
    public interface INodeFactory
    {
        INode CreateNode(object ownerObject, string memberName);
        INode CreateNode(object ownerObject, string memberName, string updateMethod);
    }
}
