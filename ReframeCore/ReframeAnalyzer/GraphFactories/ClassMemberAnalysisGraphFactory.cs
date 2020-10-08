using ReframeAnalyzer.Graph;
using ReframeAnalyzer.NodeFactories;
using ReframeAnalyzer.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ReframeAnalyzer.GraphFactories
{
    public class ClassMemberAnalysisGraphFactory : AnalysisGraphFactory
    {
        public ClassMemberAnalysisGraphFactory()
        {
            NodeFactory = new ClassMemberAnalysisNodeFactory();
        }

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
                var classMemberNode = NodeFactory.CreateNode(xNode);

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
                var node = NodeFactory.CreateNode(xNode);

                var classMemberNode = graph.GetNode(node.Identifier);

                foreach (var objectMemberNodeSuccessor in objectMemberNode.Successors)
                {
                    XElement xSNode = XElement.Parse(objectMemberNodeSuccessor.Source);
                    var sNode = NodeFactory.CreateNode(xSNode);
                    var successorClassMemberNode = graph.GetNode(sNode.Identifier);
                    if (successorClassMemberNode != null)
                    {
                        classMemberNode.AddSuccessor(successorClassMemberNode);
                    }
                }
            }
        }
    }
}
