using ReframeCore.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore.Helpers
{
    public class UpdateError
    {
        public Exception SourceException { get; private set; }
        public IDependencyGraph Graph { get; set; }
        public INode FailedNode { get; set; }

        public UpdateError(Exception sourceException, IDependencyGraph graph, INode failedNode)
        {
            SourceException = sourceException;
            Graph = graph;
            FailedNode = failedNode;
        }
    }
}
