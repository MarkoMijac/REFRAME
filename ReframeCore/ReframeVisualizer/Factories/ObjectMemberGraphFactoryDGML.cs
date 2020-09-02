using ReframeAnalyzer.Graph;
using ReframeVisualizer.Graphs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeVisualizer
{
    public class ObjectMemberDGMLGraphFactory : IVisualGraphFactory
    {
        public IVisualGraph CreateGraph(IEnumerable<IAnalysisNode> analysisNodes)
        {
            return new ObjectMemberDGMLVisualGraph(analysisNodes);
        }
    }
}
