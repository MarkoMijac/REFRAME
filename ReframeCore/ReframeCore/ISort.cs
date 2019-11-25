using ReframeCore.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore
{
    public interface ISorter
    {
        IList<INode> Sort(IEnumerable<INode> graphNodes);
        IList<INode> Sort(IEnumerable<INode> graphNodes, INode initialNode);
    }
}
