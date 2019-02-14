using ReframeCore.Exceptions;
using ReframeCore.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore
{
    internal enum NodeType { PropertyNode, MethodNode, Unknown }

    public static class NodeFactory
    {
        public static INode CreateNode(object ownerObject, string memberName)
        {
            return CreateNode(ownerObject, memberName, "");
        }

        public static INode CreateNode(object ownerObject, string memberName, string updateMethod)
        {
            NodeType nodeType = DetermineNodeType(ownerObject, memberName);
            switch (nodeType)
            {
                case NodeType.PropertyNode: return CreatePropertyNode(ownerObject, memberName, updateMethod);
                default:
                    throw new ReactiveNodeException("Unable to create reactive node!");
            }
        }

        private static NodeType DetermineNodeType(object ownerObject, string memberName)
        {
            return NodeType.PropertyNode;
        }

        #region PropertyNode

        private static PropertyNode CreatePropertyNode(object ownerObject, string memberName)
        {
            return new PropertyNode(ownerObject, memberName);
        }

        private static PropertyNode CreatePropertyNode(object ownerObject, string memberName, string updateMethod)
        {
            return new PropertyNode(ownerObject, memberName, updateMethod);
        }

        #endregion
    }
}
