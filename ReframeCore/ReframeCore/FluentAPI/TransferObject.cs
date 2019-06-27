using ReframeCore.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore.FluentAPI
{
    public class TransferObject
    {
        public List<INode> Successors { get; set; }
        public IDependencyGraph Graph { get; set; }

        public TransferObject(IDependencyGraph graph, List<INode> nodes)
        {
            Successors = nodes;
            Graph = graph;
        }
    }
}
