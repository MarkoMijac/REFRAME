using ReframeCore.Exceptions;
using ReframeCore.Helpers;
using ReframeCore.Nodes;
using ReframeCore.ReactiveCollections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore.Factories
{
    internal enum NodeType { PropertyNode, MethodNode, CollectionPropertyNode, CollectionMethodNode, Unknown }

    public class NodeFactory
    {
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
            switch (nodeType)
            {
                case NodeType.PropertyNode: return CreatePropertyNode(ownerObject, memberName, updateMethod);
                case NodeType.MethodNode: return CreateMethodNode(ownerObject, memberName);
                case NodeType.CollectionPropertyNode: return CreateCollectionPropertyNode(ownerObject, memberName, updateMethod);
                case NodeType.CollectionMethodNode: return CreateCollectionMethodNode(ownerObject, memberName);
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
                    if (Reflector.IsProperty(ownerObject, memberName) == true)
                    {
                        nodeType = NodeType.CollectionPropertyNode;
                    }
                    else if (Reflector.IsMethod(ownerObject, memberName) == true)
                    {
                        nodeType = NodeType.CollectionMethodNode;
                    }
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

        /// <summary>
        /// Gets update method name generated from default prefix and property name.
        /// </summary>
        /// <param name="propertyName">Property name represented by reactive node.</param>
        /// <returns>Update method name generated from default prefix and property name.</returns>
        public string GenerateDefaultUpdateMethodName(string propertyName)
        {
            return UpdateMethodNamePrefix + propertyName;
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
            string updateMethod = GenerateDefaultUpdateMethodName(propertyName);
            return new PropertyNode(ownerObject, propertyName, updateMethod);
        }

        private bool ShouldDefaultUpdateMethodBeUsed(object ownerObject, string propertyName)
        {
            bool should = false;

            if (UseDefaultUpdateMethodNames == true)
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

        #region CollectionPropertyNode

        private CollectionPropertyNode CreateCollectionPropertyNode(object collection, string propertyName, string updateMethod)
        {
            CollectionPropertyNode collectionNode = null;

            if (updateMethod == "")
            {
                if (ShouldDefaultUpdateMethodBeUsed(collection, propertyName) == true)
                {
                    collectionNode = CreateCollectionPropertyNode_WithDefaultUpdateMethod(collection, propertyName);
                }
                else
                {
                    collectionNode = CreateCollectionPropertyNode_WithoutUpdateMethod(collection, propertyName);
                }
            }
            else
            {
                collectionNode = CreateCollectionPropertyNode_WithUpdateMethod(collection, propertyName, updateMethod);
            }

            return collectionNode;
        }

        private CollectionPropertyNode CreateCollectionPropertyNode_WithDefaultUpdateMethod(object ownerObject, string propertyName)
        {
            string updateMethod = GenerateDefaultUpdateMethodName(propertyName);
            return new CollectionPropertyNode(ownerObject, propertyName, updateMethod);
        }

        private CollectionPropertyNode CreateCollectionPropertyNode_WithoutUpdateMethod(object ownerObject, string propertyName)
        {
            return new CollectionPropertyNode(ownerObject, propertyName);
        }

        private CollectionPropertyNode CreateCollectionPropertyNode_WithUpdateMethod(object ownerObject, string propertyName, string updateMethod)
        {
            return new CollectionPropertyNode(ownerObject, propertyName, updateMethod);
        }

        #endregion

        #region CollectionMethodNode

        private INode CreateCollectionMethodNode(object collection, string methodName)
        {
            return new CollectionMethodNode(collection, methodName);
        }

        #endregion

        #endregion
    }
}
