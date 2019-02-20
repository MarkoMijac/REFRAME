using ReframeCore.Exceptions;
using ReframeCore.Helpers;
using ReframeCore.Nodes;
using ReframeCore.ReactiveCollections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore
{
    internal enum NodeType { PropertyNode, MethodNode, CollectionNode, Unknown }

    public class NodeFactory
    {
        #region Properties

        public NodeFactorySettings Settings { get; set; }

        #endregion

        #region Constructors

        public NodeFactory()
        {
            Settings = new NodeFactorySettings();
        }

        #endregion

        #region Methods

        public INode CreateNode(object ownerObject, string memberName)
        {
            return CreateNode(ownerObject, memberName, "");
        }

        public INode CreateNode(object ownerObject, string memberName, string updateMethod)
        {
            NodeType nodeType = DetermineNodeType(ownerObject, memberName);
            switch (nodeType)
            {
                case NodeType.PropertyNode: return CreatePropertyNode(ownerObject, memberName, updateMethod);
                case NodeType.MethodNode: return CreateMethodNode(ownerObject, memberName);
                case NodeType.CollectionNode: return CreateCollectionNode(ownerObject, memberName);
                case NodeType.Unknown: throw new ReactiveNodeException("Unable to determine node type!");
                default:
                    throw new ReactiveNodeException("Unable to create reactive node!");
            }
        }

        private NodeType DetermineNodeType(object ownerObject, string memberName)
        {
            NodeType nodeType = NodeType.Unknown;

            if (Reflector.IsProperty(ownerObject, memberName) == true)
            {
                nodeType = NodeType.PropertyNode;
            }
            else if (Reflector.IsMethod(ownerObject, memberName) == true)
            {
                nodeType = NodeType.MethodNode;
            }
            else if (Reflector.IsGenericCollection(ownerObject))
            {
                nodeType = NodeType.CollectionNode;
            }

            return nodeType;
        }

        #region PropertyNode

        /// <summary>
        /// Gets update method name generated from default prefix and property name.
        /// </summary>
        /// <param name="propertyName">Property name represented by reactive node.</param>
        /// <returns>Update method name generated from default prefix and property name.</returns>
        public string GenerateDefaultUpdateMethodName(string propertyName)
        {
            return Settings.UpdateMethodNamePrefix + propertyName;
        }

        private PropertyNode CreatePropertyNode(object ownerObject, string propertyName, string updateMethod)
        {
            PropertyNode propertyNode = null;

            if (updateMethod == "")
            {
                if (ShouldDefaultUpdateMethodBeUsed(ownerObject, propertyName) == true)
                {
                    propertyNode = CreatePropertyNode_WithDefaultUpdateMethod(ownerObject, propertyName);
                }
                else
                {
                    propertyNode = CreatePropertyNode_WithoutUpdateMethod(ownerObject, propertyName);
                }                
            }
            else
            {
                propertyNode = CreatePropertyNode_WithUpdateMethod(ownerObject, propertyName, updateMethod);
            }

            return propertyNode;
        }

        private PropertyNode CreatePropertyNode_WithUpdateMethod(object ownerObject, string propertyName, string updateMethod)
        {
            return new PropertyNode(ownerObject, propertyName, updateMethod);
        }

        private PropertyNode CreatePropertyNode_WithoutUpdateMethod(object ownerObject, string propertyName)
        {
            return new PropertyNode(ownerObject, propertyName);
        }

        private PropertyNode CreatePropertyNode_WithDefaultUpdateMethod(object ownerObject, string propertyName)
        {
            string updateMethod = GenerateDefaultUpdateMethodName(propertyName);
            return new PropertyNode(ownerObject, propertyName, updateMethod);
        }

        private bool ShouldDefaultUpdateMethodBeUsed(object ownerObject, string propertyName)
        {
            bool should = false;

            if (Settings.UseDefaultUpdateMethodNames == true)
            {
                string updateMethod = GenerateDefaultUpdateMethodName(propertyName);
                if (Reflector.IsMethod(ownerObject, updateMethod) == true)
                {
                    should = true;
                }
            }

            return should;
        }

        #endregion

        #region MethodNode

        private MethodNode CreateMethodNode(object ownerObject, string memberName)
        {
            return new MethodNode(ownerObject, memberName);
        }

        #endregion

        #region CollectionNode

        private CollectionNode CreateCollectionNode(object collection, string memberName)
        {
            return new CollectionNode(collection, memberName);
        }

        #endregion

        #endregion
    }
}
