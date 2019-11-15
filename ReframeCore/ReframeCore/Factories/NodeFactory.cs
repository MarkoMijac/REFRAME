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
    public abstract class NodeFactory
    {
        protected enum NodeType { PropertyNode, MethodNode, CollectionPropertyNode, CollectionMethodNode, Unknown }

        #region Properties

        public string UpdateMethodNamePrefix { get; set; }
        public bool UseDefaultUpdateMethodNames { get; set; }

        #endregion

        #region Constructors

        public NodeFactory()
        {
            UpdateMethodNamePrefix = "Update_";
            UseDefaultUpdateMethodNames = true;
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

        /// <summary>
        /// Gets update method name generated from default prefix and property name.
        /// </summary>
        /// <param name="propertyName">Property name represented by reactive node.</param>
        /// <returns>Update method name generated from default prefix and property name.</returns>
        protected virtual string GenerateDefaultUpdateMethodName(string propertyName)
        {
            return UpdateMethodNamePrefix + propertyName;
        }

        #endregion
    }
}
