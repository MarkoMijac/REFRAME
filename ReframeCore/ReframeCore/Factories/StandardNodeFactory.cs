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
    public class StandardNodeFactory : NodeFactory
    {
        #region Constructors

        public StandardNodeFactory()
            :base()
        {
            
        }

        #endregion

        #region Methods

        protected override NodeType DetermineNodeType(object ownerObject, string memberName, string updateMethodName)
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
                    if (Reflector.IsProperty(ownerObject, memberName) == true)
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

        protected override INode CreateNodeForType(object ownerObject, string memberName, string updateMethod, NodeType nodeType)
        {
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

        #region PropertyNode

        protected virtual PropertyNode CreatePropertyNode(object ownerObject, string propertyName, string updateMethod)
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

        protected virtual MethodNode CreateMethodNode(object ownerObject, string memberName)
        {
            return new MethodNode(ownerObject, memberName);
        }

        #endregion

        #region CollectionPropertyNode

        protected virtual CollectionPropertyNode CreateCollectionPropertyNode(object collection, string propertyName, string updateMethod)
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

        protected virtual CollectionMethodNode CreateCollectionMethodNode(object collection, string methodName)
        {
            return new CollectionMethodNode(collection, methodName);
        }

        #endregion

        #endregion
    }
}
