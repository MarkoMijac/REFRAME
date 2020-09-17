using ReframeImporter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ReframeAnalyzer.Graph
{
    public class UpdateAnalysisGraphFactory : AnalysisGraphFactory
    {
        private Importer _importer = new Importer();

        private IAnalysisGraph _objectMemberAnalysisGraph;

        public UpdateAnalysisGraphFactory(IAnalysisGraph objectMemberAnalysisGraph)
        {
            _objectMemberAnalysisGraph = objectMemberAnalysisGraph;
        }

        protected override IAnalysisGraph CreateGraph()
        {
            var graph = new UpdateAnalysisGraph(_objectMemberAnalysisGraph.Identifier, AnalysisLevel.UpdateAnalysisLevel);
            InitializeGraph(graph);
            return graph;
        }

        private void InitializeGraph(UpdateAnalysisGraph graph)
        {
            if (graph != null)
            {
                XElement xGraph = _importer.GetGraph(XmlSource);
                XElement xUpdateProcess = XElement.Parse(XmlSource);
                IEnumerable<XElement> xNodes = _importer.GetNodes(xUpdateProcess);

                InitializeBasicData(graph);
                InitializeGraphNodes(graph, xNodes);
                InitializeGraphDependencies(graph, xNodes);
            }
        }

        private void InitializeBasicData(UpdateAnalysisGraph graph)
        {
            XElement xRoot = XElement.Parse(XmlSource);
            XElement xGraph = _importer.GetGraph(XmlSource);

            graph.TotalNodeCount = int.Parse(xGraph.Element("TotalNodeCount").Value);
            graph.UpdateSuccessful = bool.Parse(xRoot.Element("UpdateSuccessful").Value);
            graph.UpdateStartedAt = xRoot.Element("UpdateStartedAt").Value;
            graph.UpdateEndedAt = xRoot.Element("UpdateEndedAt").Value;
            graph.UpdateDuration = xRoot.Element("UpdateDuration").Value;

            XElement xError = xRoot.Element("UpdateError");
            if (xError != null)
            {
                XElement xFailedNode = xRoot.Element("FailedNode");
                if (xFailedNode != null)
                {
                    graph.FailedNodeIdentifier = uint.Parse(xFailedNode.Element("Identifier").Value);
                    graph.FailedNodeName = xFailedNode.Element("MemberName").Value;
                    graph.FailedNodeOwner = xFailedNode.Element("OwnerObject").Value;
                }

                graph.SourceException = xError.Element("SourceException").Value;
                graph.StackTrace = xError.Element("StackTrace").Value;
            }

            XElement xUpdateCause = xRoot.Element("UpdateCause");
            if (xUpdateCause != null)
            {
                graph.CauseMessage = xUpdateCause.Element("Message").Value;
                XElement xInitialNode = xUpdateCause.Element("InitialNode");
                if (xInitialNode != null)
                {
                    graph.InitialNodeIdentifier = uint.Parse(xInitialNode.Element("Identifier").Value);
                    graph.InitialNodeName = xInitialNode.Element("MemberName").Value;
                    graph.InitialNodeOwner = xInitialNode.Element("OwnerObject").Value;
                    graph.InitialNodeCurrentValue = xInitialNode.Element("CurrentValue").Value;
                    graph.InitialNodePreviousValue = xInitialNode.Element("PreviousValue").Value;
                }
            }
        }


        private void InitializeGraphNodes(UpdateAnalysisGraph graph, IEnumerable<XElement> xNodes)
        {
            var nodeFactory = new UpdateAnalysisNodeFactory();
            foreach (var xNode in xNodes)
            {
                uint nodeIdentifier = uint.Parse(xNode.Element("Identifier").Value);
                if (graph.ContainsNode(nodeIdentifier) == false)
                {
                    var objectMemberNode = _objectMemberAnalysisGraph.GetNode(nodeIdentifier);
                    var node = nodeFactory.CreateNode(xNode, objectMemberNode);
                    graph.AddNode(node);
                }
            }
        }

        private void InitializeGraphDependencies(UpdateAnalysisGraph graph, IEnumerable<XElement> xNodes)
        {
            foreach (var xNode in xNodes)
            {
                uint nodeIdentifier = uint.Parse(_importer.GetIdentifier(xNode));
                var analysisNode = graph.GetNode(nodeIdentifier);

                var xSuccessors = _importer.GetSuccessors(xNode);

                foreach (var xSuccessor in xSuccessors)
                {
                    uint successorIdentifier = uint.Parse(_importer.GetIdentifier(xSuccessor));
                    var successorAnalysisNode = graph.GetNode(successorIdentifier);
                    if (successorAnalysisNode != null)
                    {
                        analysisNode.AddSuccesor(successorAnalysisNode);
                    }
                }
            }
        }
    }
}
