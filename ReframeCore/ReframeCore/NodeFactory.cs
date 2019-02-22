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
            NodeType nodeType = DetermineNodeType(ownerObject, memberName, updateMethod);
            switch (nodeType)
            {
                case NodeType.PropertyNode: return CreatePropertyNode(ownerObject, memberName, updateMethod);
                case NodeType.MethodNode: return CreateMethodNode(ownerObject, memberName);
                case NodeType.CollectionNode: return CreateCollectionNode(ownerObject, memberName, updateMethod);
                case NodeType.Unknown: throw new ReactiveNodeException("Unable to determine node type!");
                default:
                    throw new ReactiveNodeException("Unable to create reactive node!");
            }
        }

        private NodeType DetermineNodeType(object ownerObject, string memberName, string updateMethodName)
        {
            NodeType nodeType = NodeType.Unknown;

            if (ownerObject != null)
            {
                if (Reflector.IsGenericCollection(ownerObject))
                {
                    nodeType = NodeType.CollectionNode;
                }
                else
                {
                    if (Reflector.IsProperty(ownerObject, memberName) == true
                    && (updateMethodName == "" || Reflector.IsMethod(ownerObject, updateMethodName) == true))
                    {
                        nodeType = NodeType.PropertyNode;
                    }
                    else if (Reflector.IsMethod(ownerObject, memberName) == true
                    && (updateMethodName == "" || updateMethodName == memberName))
                    {
                        nodeType = NodeType.MethodNode;
                    }
                }
            }

            return nodeType;
        }

        #region PropertyNode

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
            string updateMethod = Settings.GenerateDefaultUpdateMethodName(propertyName);
            return new PropertyNode(ownerObject, propertyName, updateMethod);
        }

        private bool ShouldDefaultUpdateMethodBeUsed(object ownerObject, string propertyName)
        {
            bool should = false;

            if (Settings.UseDefaultUpdateMethodNames == true)
            {
                string updateMethod = Settings.GenerateDefaultUpdateMethodName(propertyName);
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

        private CollectionNode CreateCollectionNode(object collection, string propertyName, string updateMethod)
        {
            CollectionNode collectionNode = null;

            if (updateMethod == "")
            {
                if (ShouldDefaultUpdateMethodBeUsed(collection, propertyName) == true)
                {
                    collectionNode = CreateCollectionNode_WithDefaultUpdateMethod(collection, propertyName);
                }
                else
                {
                    collectionNode = CreateCollectionNode_WithoutUpdateMethod(collection, propertyName);
                }
            }
            else
            {
                collectionNode = CreateCollectionNode_WithUpdateMethod(collection, propertyName, updateMethod);
            }

            return collectionNode;
        }

        private CollectionNode CreateCollectionNode_WithDefaultUpdateMethod(object ownerObject, string propertyName)
        {
            string updateMethod = Settings.GenerateDefaultUpdateMethodName(propertyName);
            return new CollectionNode(ownerObject, propertyName, updateMethod);
        }

        private CollectionNode CreateCollectionNode_WithoutUpdateMethod(object ownerObject, string propertyName)
        {
            return new CollectionNode(ownerObject, propertyName);
        }

        private CollectionNode CreateCollectionNode_WithUpdateMethod(object ownerObject, string propertyName, string updateMethod)
        {
            return new CollectionNode(ownerObject, propertyName, updateMethod);
        }

        #endregion

        #endregion
    }
}
