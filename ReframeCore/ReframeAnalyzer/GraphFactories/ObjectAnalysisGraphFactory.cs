using ReframeAnalyzer.Graph;
using ReframeAnalyzer.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeAnalyzer.GraphFactories
{
    public class ObjectAnalysisGraphFactory : AnalysisGraphFactory
    {
        protected override IAnalysisGraph CreateGraph()
        {
            var factory = new ObjectMemberAnalysisGraphFactory();
            var objectMemberAnalysisGraph = factory.CreateGraph(XmlSource);

            string identifier = objectMemberAnalysisGraph.Identifier;
            var graph = new AnalysisGraph(identifier, AnalysisLevel.ObjectLevel);

            InitializeGraph(graph, objectMemberAnalysisGraph);
            return graph;
        }

        private void InitializeGraph(AnalysisGraph graph, IAnalysisGraph objectMemberAnalysisGraph)
        {
            if (graph != null && objectMemberAnalysisGraph != null)
            {
                InitializeGraphNodes(graph, objectMemberAnalysisGraph.Nodes);
                InitializeGraphDependencies(graph, objectMemberAnalysisGraph.Nodes);
            }
        }

        private void InitializeGraphNodes(AnalysisGraph graph, List<IAnalysisNode> nodes)
        {
            foreach (var memberNode in nodes)
            {
                if (graph.ContainsNode(memberNode.Parent.Identifier) == false)
                {
                    graph.AddNode(memberNode.Parent);
                }
            }
        }

        private void InitializeGraphDependencies(AnalysisGraph graph, List<IAnalysisNode> nodes)
        {
            foreach (var memberNode in nodes)
            {
                var objectNode = graph.GetNode(memberNode.Parent.Identifier);

                foreach (var memberNodeSuccessor in memberNode.Successors)
                {
                    var successorMemberNode = graph.GetNode(memberNodeSuccessor.Parent.Identifier);
                    if (successorMemberNode != null)
                    {
                        objectNode.AddSuccessor(successorMemberNode);
                    }
                }
            }
        }
    }
}
