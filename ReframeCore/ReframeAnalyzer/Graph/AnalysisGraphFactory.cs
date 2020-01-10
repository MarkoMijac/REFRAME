using ReframeAnalyzer.Exceptions;
using ReframeCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeAnalyzer.Graph
{
    public enum GraphType { ClassGraph }

    public class AnalysisGraphFactory
    {
        public IAnalysisGraph GetGraph(IDependencyGraph graph, GraphType type)
        {
            ValidateGraph(graph);

            IAnalysisGraph analysisGraph;
            switch (type)
            {
                case GraphType.ClassGraph:
                    analysisGraph = new ClassAnalysisGraph();
                    analysisGraph.InitializeGraph(graph);
                    break;
                default:
                    analysisGraph = null;
                    break;
            }

            return analysisGraph;
        }

        private void ValidateGraph(IDependencyGraph graph)
        {
            if (graph == null)
            {
                throw new AnalyzerException("Provided dependency graph is null!");
            }
        }
    }
}
