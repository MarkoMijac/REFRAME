using ReframeAnalyzer.Nodes;
using ReframeVisualizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualizerDGML.Graphs;

namespace VisualizerDGML.Factories
{
    public class UpdateGraphFactoryDGML : VisualGraphFactory
    {
        public override IVisualGraph CreateGraph(string reactorIdentifier, List<IAnalysisNode> analysisNodes)
        {
            Validate(reactorIdentifier, analysisNodes);
            return new UpdateVisualGraphDGML(reactorIdentifier, analysisNodes);
        }
    }
}
