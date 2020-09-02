using ReframeAnalyzer.Graph;
using ReframeVisualizer.Graphs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeVisualizer.Factories
{
    public class ObjectGraphFactoryDGML : IVisualGraphFactory
    {
        public IVisualGraph CreateGraph(IEnumerable<IAnalysisNode> analysisNodes)
        {
            return new ObjectVisualGraphDGML(analysisNodes);
        }
    }
}
