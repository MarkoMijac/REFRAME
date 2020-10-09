using ReframeAnalyzer.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeVisualizer
{
    public abstract class VisualGraphFactory : IVisualGraphFactory
    {
        public abstract IVisualGraph CreateGraph(string reactorIdentifier, List<IAnalysisNode> analysisNodes);

        protected void Validate(string reactorIdentifier, List<IAnalysisNode> analysisNodes)
        {
            if (reactorIdentifier == "")
                throw new VisualizationException("Reactor identifier has to be specified!");
            if (analysisNodes == null)
                throw new VisualizationException("List of nodes cannot be null!");
        }
    }
}
