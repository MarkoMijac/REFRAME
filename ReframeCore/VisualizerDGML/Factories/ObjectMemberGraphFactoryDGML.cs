using ReframeAnalyzer.Graph;
using ReframeVisualizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualizerDGML.Graphs;

namespace VisualizerDGML.Factories
{
    public class ObjectMemberDGMLGraphFactory : IVisualGraphFactory
    {
        public IVisualGraph CreateGraph(string reactorIdentifier, IEnumerable<IAnalysisNode> analysisNodes)
        {
            return new ObjectMemberDGMLVisualGraph(reactorIdentifier, analysisNodes);
        }
    }
}
