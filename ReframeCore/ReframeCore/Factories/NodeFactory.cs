using ReframeCore.Exceptions;
using ReframeCore.Helpers;
using ReframeCore.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore.Factories
{
    public abstract class NodeFactory : INodeFactory
    {
        protected enum NodeType { PropertyNode, MethodNode, CollectionPropertyNode, CollectionMethodNode, Unknown }

        #region Constructors

        public NodeFactory()
        {
            
        }

        #endregion

        #region Methods

        public INode CreateNode(object ownerObject, string memberName)
        {
            return CreateNode(ownerObject, memberName, "");
        }

        public INode CreateNode(object ownerObject, string memberName, string updateMethod)
        {
            NodeType nodeType = DetermineNodeType(ownerObject, memberName, updateMethod);
            return CreateNodeForType(ownerObject, memberName, updateMethod, nodeType);
        }

        protected abstract NodeType DetermineNodeType(object ownerObject, string memberName, string updateMethodName);

        protected abstract INode CreateNodeForType(object ownerObject, string memberName, string updateMethod, NodeType nodeType);

        #endregion
    }
}
