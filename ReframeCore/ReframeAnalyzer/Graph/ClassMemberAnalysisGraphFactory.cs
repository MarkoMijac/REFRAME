using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ReframeAnalyzer.Graph
{
    public class ClassMemberAnalysisGraphFactory : AnalysisGraphFactory
    {
        protected override IAnalysisGraph CreateGraph()
        {
            var factory = new ObjectMemberAnalysisGraphFactory();
            var objectMemberAnalysisGraph = factory.CreateGraph(XmlSource);

            string identifier = objectMemberAnalysisGraph.Identifier;
            var graph = new AnalysisGraph(identifier, AnalysisLevel.ClassMemberLevel);

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
            foreach (var objectMemberNode in nodes)
            {
                XElement xNode = XElement.Parse(objectMemberNode.Source);
                var classMemberNode = NodeFactory.CreateNode(xNode, AnalysisLevel.ClassMemberLevel);

                if (classMemberNode != null && graph.ContainsNode(classMemberNode.Identifier) == false)
                {
                    graph.AddNode(classMemberNode);
                }
            }
        }

        private void InitializeGraphDependencies(AnalysisGraph graph, List<IAnalysisNode> nodes)
        {
            foreach (var objectMemberNode in nodes)
            {
                XElement xNode = XElement.Parse(objectMemberNode.Source);
                var node = NodeFactory.CreateNode(xNode, AnalysisLevel.ClassMemberLevel);

                var classMemberNode = graph.GetNode(node.Identifier);

                foreach (var objectMemberNodeSuccessor in objectMemberNode.Successors)
                {
                    XElement xSNode = XElement.Parse(objectMemberNodeSuccessor.Source);
                    var sNode = NodeFactory.CreateNode(xSNode, AnalysisLevel.ClassMemberLevel);
                    var successorClassMemberNode = graph.GetNode(sNode.Identifier);
                    if (successorClassMemberNode != null)
                    {
                        classMemberNode.AddSuccesor(successorClassMemberNode);
                    }
                }
            }
        }
    }
}
