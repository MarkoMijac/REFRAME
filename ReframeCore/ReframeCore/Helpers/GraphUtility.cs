using ReframeCore.Nodes;
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
                if (node.Predecessors.Contains(updatePath[i]) == true)
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
    }
}
