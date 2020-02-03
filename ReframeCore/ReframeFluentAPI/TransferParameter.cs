using ReframeCore;
using ReframeCore.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCoreFluentAPI
{
    public class TransferParameter
    {
        public List<INode> Successors { get; set; }
        public IReactor Reactor { get; set; }

        public TransferParameter(IReactor reactor, List<INode> successors)
        {
            Successors = successors;
            Reactor = reactor;
        }
    }
}
