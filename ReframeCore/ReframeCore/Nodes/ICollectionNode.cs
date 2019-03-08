using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore.Nodes
{
    public interface ICollectionNode
    {
        bool ContainsChildNode(INode node);
        bool HasChildPredecessors();
    }
}
