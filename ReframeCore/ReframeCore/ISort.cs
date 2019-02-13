using ReframeCore.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore
{
    public interface ISort
    {
        IList<INode> Sort(IEnumerable<INode> sourceGraph, Func<INode, IEnumerable<INode>> getDependents);
        IList<INode> Sort(IEnumerable<INode> sourceGraph, Func<INode, IEnumerable<INode>> getDependents, INode initialNode, bool omitInitialNode);
    }
}
