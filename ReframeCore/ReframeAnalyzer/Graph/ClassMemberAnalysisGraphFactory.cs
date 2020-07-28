using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ReframeAnalyzer.Graph
{
    public class ClassMemberAnalysisGraphFactory : AnalysisGraphAbstractFactory
    {
        protected override IAnalysisGraph CreateGraph()
        {
            var factory = new ObjectMemberAnalysisGraphFactory();
            var objectMemberAnalysisGraph = factory.CreateGraph(XmlSource) as ObjectMemberAnalysisGraph;

            string identifier = objectMemberAnalysisGraph.Identifier;
            var graph = new ClassMemberAnalysisGraph(identifier, AnalysisLevel.ClassMemberLevel);

            InitializeGraph(graph, objectMemberAnalysisGraph);
            return graph;
        }

        private void InitializeGraph(ClassMemberAnalysisGraph graph, ObjectMemberAnalysisGraph objectMemberAnalysisGraph)
        {
            if (graph != null && objectMemberAnalysisGraph != null)
            {
                InitializeGraphNodes(graph, objectMemberAnalysisGraph.Nodes);
                InitializeGraphDependencies(graph, objectMemberAnalysisGraph.Nodes);
            }
        }

        private void InitializeGraphNodes(ClassMemberAnalysisGraph graph, List<IAnalysisNode> nodes)
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

        private void InitializeGraphDependencies(ClassMemberAnalysisGraph graph, List<IAnalysisNode> nodes)
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
