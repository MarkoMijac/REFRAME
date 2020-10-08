using ReframeAnalyzer.Graph;
using ReframeAnalyzer.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeAnalyzer.GraphFactories
{
    public class AssemblyAnalysisGraphFactory : AnalysisGraphFactory
    {
        protected override IAnalysisGraph CreateGraph()
        {
            var factory = new ClassAnalysisGraphFactory();
            var classAnalysisGraph = factory.CreateGraph(XmlSource);

            var graph = new AnalysisGraph(classAnalysisGraph.Identifier, AnalysisLevel.AssemblyLevel);
            InitializeGraph(graph, classAnalysisGraph);
            return graph;
        }

        private void InitializeGraph(IAnalysisGraph graph, IAnalysisGraph classAnalysisGraph)
        {
            if (graph != null && classAnalysisGraph != null)
            {
                InitializeGraphNodes(graph, classAnalysisGraph.Nodes);
                InitializeGraphDependencies(graph, classAnalysisGraph.Nodes);
            }
        }

        private void InitializeGraphNodes(IAnalysisGraph graph, List<IAnalysisNode> nodes)
        {
            foreach (var classNode in nodes)
            {
                if (graph.ContainsNode(classNode.Parent2.Identifier) == false)
                {
                    graph.AddNode(classNode.Parent2);
                }
            }
        }

        private void InitializeGraphDependencies(IAnalysisGraph graph, List<IAnalysisNode> nodes)
        {
            foreach (var classNode in nodes)
            {
                var assemblyNode = graph.GetNode(classNode.Parent2.Identifier);

                foreach (var classNodeSuccessor in classNode.Successors)
                {
                    var successorAssemblyNode = graph.GetNode(classNodeSuccessor.Parent2.Identifier);
                    if (successorAssemblyNode != null)
                    {
                        assemblyNode.AddSuccessor(successorAssemblyNode);
                    }
                }
            }
        }
    }
}
