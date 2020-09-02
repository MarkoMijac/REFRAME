using Microsoft.VisualStudio.GraphModel;
using ReframeAnalyzer.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeVisualizer
{
    public class AssemblyDGMLGraphFactory : IVisualGraphFactory
    {
        public IVisualGraph CreateGraph(IEnumerable<IAnalysisNode> analysisNodes)
        {
            return new AssemblyVisualGraph(analysisNodes);
        }
    }
}
