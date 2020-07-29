using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeAnalyzer.Graph
{
    public class ClassAnalysisGraphFactory : AnalysisGraphAbstractFactory
    {
        protected override IAnalysisGraph CreateGraph()
        {
            var factory = new ObjectAnalysisGraphFactory();
            var objectAnalysisGraph = factory.CreateGraph(XmlSource);

            string identifier = objectAnalysisGraph.Identifier;
            var graph = new AnalysisGraph(identifier, AnalysisLevel.ClassLevel);

            InitializeGraph(graph, objectAnalysisGraph);
            return graph;
        }

        private void InitializeGraph(AnalysisGraph graph, IAnalysisGraph objectAnalysisGraph)
        {
            if (graph != null && objectAnalysisGraph != null)
            {
                InitializeGraphNodes(graph, objectAnalysisGraph.Nodes);
                InitializeGraphDependencies(graph, objectAnalysisGraph.Nodes);
            }
        }

        private void InitializeGraphNodes(AnalysisGraph graph, List<IAnalysisNode> nodes)
        {
            foreach (var objectNode in nodes)
            {
                if (graph.ContainsNode(objectNode.Parent.Identifier) == false)
                {
                    graph.AddNode(objectNode.Parent);
                }
            }
        }

        private void InitializeGraphDependencies(AnalysisGraph graph, List<IAnalysisNode> nodes)
        {
            foreach (var objectNode in nodes)
            {
                var classNode = graph.GetNode(objectNode.Parent.Identifier);

                foreach (var objectNodeSuccessor in objectNode.Successors)
                {
                    var successorClassNode = graph.GetNode(objectNodeSuccessor.Parent.Identifier);
                    if (successorClassNode != null)
                    {
                        classNode.AddSuccesor(successorClassNode);
                    }
                }
            }
        }
    }
}
