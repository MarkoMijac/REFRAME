using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ReframeAnalyzer.Graph
{
    public class UpdateAnalysisGraph : AnalysisGraph
    {
        public bool UpdateSuccessful { get; set; }
        public string UpdateStartedAt { get; set; }
        public string UpdateEndedAt { get; set; }
        public string UpdateDuration { get; set; }
        public int TotalNodeCount { get; set; }
        public uint FailedNodeIdentifier { get; set; }
        public string FailedNodeName { get; set; }
        public string FailedNodeOwner { get; set; }
        public string SourceException { get; set; }
        public string StackTrace { get; set; }
        public string CauseMessage { get; set; }
        public uint InitialNodeIdentifier { get; set; }
        public string InitialNodeName { get; set; }
        public string InitialNodeOwner { get; set; }
        public string InitialNodeCurrentValue { get; set; }
        public string InitialNodePreviousValue { get; set; }

        public ObjectMemberAnalysisGraph ObjectMemberGraph { get; set; }

        public UpdateAnalysisGraph(string source, ObjectMemberAnalysisGraph objectMemberGraph)
        {
            ObjectMemberGraph = objectMemberGraph;
            AnalysisLevel = AnalysisLevel.UpdateAnalysisLevel;
            Source = source;

            XElement xRoot = XElement.Parse(source);
            XElement xReactor = xRoot.Element("Reactor");
            XElement xGraph = xReactor.Element("Graph");

            XElement xUpdatedNodes = xRoot.Element("Nodes");

            InitializeBasicData(xRoot);
            InitializeGraphBasicData(xGraph);
            IEnumerable<XElement> xNodes = FetchNodes(xUpdatedNodes);
            if (xNodes.Count() > 0)
            {
                InitializeGraphNodes(xNodes);
                InitializeGraphDependencies(xNodes);
            }
        }

        private void InitializeBasicData(XElement xRoot)
        {
            UpdateSuccessful = bool.Parse(xRoot.Element("UpdateSuccessful").Value);
            UpdateStartedAt = xRoot.Element("UpdateStartedAt").Value;
            UpdateEndedAt = xRoot.Element("UpdateEndedAt").Value;
            UpdateDuration = xRoot.Element("UpdateDuration").Value;

            XElement xError = xRoot.Element("UpdateError");
            if (xError != null)
            {
                XElement xFailedNode = xRoot.Element("FailedNode");
                if (xFailedNode != null)
                {
                    FailedNodeIdentifier = uint.Parse(xFailedNode.Element("Identifier").Value);
                    FailedNodeName = xFailedNode.Element("MemberName").Value;
                    FailedNodeOwner = xFailedNode.Element("OwnerObject").Value;
                }

                SourceException = xError.Element("SourceException").Value;
                StackTrace = xError.Element("StackTrace").Value;
            }

            XElement xUpdateCause = xRoot.Element("UpdateCause");
            if (xUpdateCause != null)
            {
                CauseMessage = xUpdateCause.Element("Message").Value;
                XElement xInitialNode = xUpdateCause.Element("InitialNode");
                if (xInitialNode != null)
                {
                    InitialNodeIdentifier = uint.Parse(xInitialNode.Element("Identifier").Value);
                    InitialNodeName = xInitialNode.Element("MemberName").Value;
                    InitialNodeOwner = xInitialNode.Element("OwnerObject").Value;
                    InitialNodeCurrentValue = xInitialNode.Element("CurrentValue").Value;
                    InitialNodePreviousValue = xInitialNode.Element("PreviousValue").Value;
                }
            }
        }

        private IEnumerable<XElement> FetchNodes(XElement xUpdatedNodes)
        {
            IEnumerable<XElement> xNodes = null;

            if (xUpdatedNodes != null)
            {
                xNodes = xUpdatedNodes.Descendants("Node");
            }

            return xNodes;
        }

        private void InitializeGraphBasicData(XElement xGraph)
        {
            Identifier = xGraph.Element("Identifier").Value;
            TotalNodeCount = int.Parse(xGraph.Element("TotalNodeCount").Value);
        }

        private void InitializeGraphNodes(IEnumerable<XElement> xNodes)
        {
            foreach (var xNode in xNodes)
            {
                uint nodeIdentifier = uint.Parse(xNode.Element("Identifier").Value);
                if (ContainsNode(nodeIdentifier) == false)
                {
                    var objectMemberNode = ObjectMemberGraph.GetNode(nodeIdentifier);
                    var node = new UpdateAnalysisNode(xNode, objectMemberNode);
                    AddNode(node);
                }
            }
        }

        private void InitializeGraphDependencies(IEnumerable<XElement> xNodes)
        {
            foreach (var xNode in xNodes)
            {
                uint nodeIdentifier = uint.Parse(xNode.Element("Identifier").Value);
                var analysisNode = GetNode(nodeIdentifier);

                IEnumerable<XElement> xSuccessors = xNode.Descendants("Successor");

                foreach (var xSuccessor in xSuccessors)
                {
                    uint successorIdentifier = uint.Parse(xSuccessor.Element("Identifier").Value);
                    var successorAnalysisNode = GetNode(successorIdentifier);
                    if (successorAnalysisNode != null)
                    {
                        analysisNode.AddSuccesor(successorAnalysisNode);
                    }
                }
            }
        }
    }
}
