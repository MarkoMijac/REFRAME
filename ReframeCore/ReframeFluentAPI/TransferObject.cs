using ReframeCore;
using ReframeCore.Nodes;
using System.Collections.Generic;

namespace ReframeCoreFluentAPI
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
