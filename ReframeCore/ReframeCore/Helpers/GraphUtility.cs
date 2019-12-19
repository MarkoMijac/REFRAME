using ReframeCore.Exceptions;
using ReframeCore.Nodes;
using ReframeCore.ReactiveCollections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore.Helpers
{
    public static class GraphUtility
    {
        public static int GetIndexPositionOfNodeInUpdatePath(INode node, IList<INode> updatePath)
        {
            return updatePath.IndexOf(node);
        }

        public static IList<INode> GetNodeDirectPredecessorsFromUpdatePath(int nodeIndex, IList<INode> updatePath)
        {
            IList<INode> predecessors = new List<INode>();

            INode node = updatePath[nodeIndex];

            for (int i = nodeIndex - 1; i >= 0; i--)
            {
                if (node.GetPredecessors().Contains(updatePath[i]) == true)
                {
                    predecessors.Add(updatePath[i]);

                }
            }

            return predecessors;
        }

        public static bool IsChildOfCollectionNode(INode node, ICollectionNode collectionNode)
        {
            bool isChild = false;

            if (node.MemberName == (collectionNode as INode).MemberName)
            {
                foreach (var o in ((collectionNode as INode).OwnerObject as IEnumerable))
                {
                    if (node.OwnerObject == o)
                    {
                        isChild = true;
                        break;
                    }
                }
            }

            return isChild;
        }

        public static INode GetCollectionNode(ICollectionNodeItem ownerObject, string memberName)
        {
            if (ownerObject != null && memberName != "")
            {
                ReactiveCollectionItemEventArgs eArgs = new ReactiveCollectionItemEventArgs();
                eArgs.MemberName = memberName;

                Reflector.RaiseEvent(ownerObject, "UpdateTriggered", eArgs);

                INode collectionNode = eArgs.CollectionNode as INode;
                return collectionNode;
            }
            else
            {
                throw new NodeNullReferenceException();
            }
        }

        public static bool IsCollectionNodeTriggeredThroughItsChildPredecessors(ICollectionNode collectionNode, IList<INode> nodesToUpdate)
        {
            bool isTriggered = false;

            if (collectionNode.HasChildPredecessors())
            {
                int collectionNodeIndex = GetIndexPositionOfNodeInUpdatePath((INode)collectionNode, nodesToUpdate);
                IList<INode> predecessorsFromUpdatePath = GetNodeDirectPredecessorsFromUpdatePath(collectionNodeIndex, nodesToUpdate);

                foreach (var p in predecessorsFromUpdatePath)
                {
                    if (IsChildOfCollectionNode(p, collectionNode))
                    {
                        isTriggered = true;
                        break;
                    }
                }
            }

            return isTriggered;
        }

        public static INode GetNearestNonChildPredecessorInUpdatePath(ICollectionNode collectionNode, IList<INode> nodesToUpdate)
        {
            INode predecessor = null;

            int collectionNodeIndex = GetIndexPositionOfNodeInUpdatePath((INode)collectionNode, nodesToUpdate);
            IList<INode> predecessorsFromUpdatePath = GetNodeDirectPredecessorsFromUpdatePath(collectionNodeIndex, nodesToUpdate);

            if (predecessorsFromUpdatePath.Count > 0)
            {
                predecessor = predecessorsFromUpdatePath[0];
            }

            return predecessor;
        }
    }
}
