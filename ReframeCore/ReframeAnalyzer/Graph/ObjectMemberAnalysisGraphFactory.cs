using ReframeImporter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ReframeAnalyzer.Graph
{
    public class ObjectMemberAnalysisGraphFactory : AnalysisGraphFactory
    {
        private Importer _importer = new Importer();

        public ObjectMemberAnalysisGraphFactory()
        {
            NodeFactory = new ObjectMemberAnalysisNodeFactory();
        }

        protected override IAnalysisGraph CreateGraph()
        {
            var xGraph = _importer.GetGraph(XmlSource);
            string identifier = _importer.GetIdentifier(xGraph);

            var graph = new AnalysisGraph(identifier, AnalysisLevel.ObjectMemberLevel);
            InitializeGraph(graph, xGraph);

            return graph;
        }

        private void InitializeGraph(AnalysisGraph graph, XElement xGraph)
        {
            var xNodes = _importer.GetNodes(xGraph);
            InitializeNodes(graph, xNodes);
            InitializeDependencies(graph, xNodes);
        }

        private void InitializeNodes(AnalysisGraph graph, IEnumerable<XElement> xNodes)
        {
            foreach (var xNode in xNodes)
            {
                uint nodeId = uint.Parse(_importer.GetIdentifier(xNode));
                if (graph.ContainsNode(nodeId) == false)
                {
                    var node = NodeFactory.CreateNode(xNode);
                    graph.AddNode(node);
                }
            }
        }

        private void InitializeDependencies(AnalysisGraph graph, IEnumerable<XElement> xNodes)
        {
            foreach (var xNode in xNodes)
            {
                uint nodeId = uint.Parse(_importer.GetIdentifier(xNode));
                var node = graph.GetNode(nodeId);

                var xSuccessors = _importer.GetSuccessors(xNode);
                foreach (var xSuccessor in xSuccessors)
                {
                    uint successorId = uint.Parse(_importer.GetIdentifier(xSuccessor));
                    var successor = graph.GetNode(successorId);
                    if (successor != null)
                    {
                        node.AddSuccesor(successor);
                    }
                }
            }
        }
    }
}
