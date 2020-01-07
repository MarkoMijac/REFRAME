using ReframeCore.Exceptions;
using ReframeCore.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore.Nodes
{
    public abstract class CollectionNode : Node, ICollectionNode
    {
        public bool ContainsChildNode(INode node)
        {
            bool contains = false;

            if (node == null)
            {
                throw new NodeNullReferenceException();
            }

            Type nodeType = node.OwnerObject.GetType();
            Type collectionGenericType = Reflector.GetGenericArgumentType(OwnerObject);

            if (node.MemberName == MemberName && nodeType.Equals(collectionGenericType))
            {
                IEnumerable collection = OwnerObject as IEnumerable;
                foreach (var item in collection)
                {
                    if (item == node.OwnerObject)
                    {
                        contains = true;
                        break;
                    }
                }
            }

            return contains;
        }

        public bool HasChildPredecessors()
        {
            bool has = false;

            foreach (var p in Predecessors)
            {
                if (ContainsChildNode(p))
                {
                    has = true;
                    break;
                }
            }

            return has;
        }

        public CollectionNode(object ownerObject, string memberName)
            :base(ownerObject, memberName)
        {

        }
    }
}
