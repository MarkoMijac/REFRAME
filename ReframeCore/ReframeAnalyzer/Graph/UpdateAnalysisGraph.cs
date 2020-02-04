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
        private ObjectMemberAnalysisGraph _objectMemberGraph;

        public UpdateAnalysisGraph(string source, ObjectMemberAnalysisGraph objectMemberGraph)
        {
            AnalysisLevel = AnalysisLevel.ObjectMemberLevel;
            Source = source;
            _objectMemberGraph = objectMemberGraph;

            XElement xRoot = XElement.Parse(source);
            XElement xGraph = xRoot.Element("Graph");
            XElement xUpdateInfos = xRoot.Element("UpdateInfos");

            InitializeGraphBasicData(xGraph);
            IEnumerable<XElement> xNodes = FetchNodes(xUpdateInfos);
            if (xNodes.Count() > 0)
            {
                InitializeGraphNodes(xNodes);
                InitializeGraphDependencies(xNodes);
            }
        }

        private IEnumerable<XElement> FetchNodes(XElement xUpdateInfos)
        {
            IEnumerable<XElement> xNodes = null;

            if (xUpdateInfos != null)
            {
                xNodes = xUpdateInfos.Descendants("UpdateInfo");
            }

            return xNodes;
        }

        private void InitializeGraphBasicData(XElement xGraph)
        {
            Identifier = xGraph.Element("Identifier").Value;
        }

        private void InitializeGraphNodes(IEnumerable<XElement> xNodes)
        {
            foreach (var xNode in xNodes)
            {
                uint nodeIdentifier = uint.Parse(xNode.Element("Identifier").Value);
                if (ContainsNode(nodeIdentifier) == false)
                {
                    ObjectMemberAnalysisNode objectMemberNode = _objectMemberGraph.GetNode(nodeIdentifier) as ObjectMemberAnalysisNode;
                    if (objectMemberNode != null)
                    {
                        var node = new UpdateAnalysisNode(xNode, objectMemberNode);
                        AddNode(node);
                    }
                }
            }
        }

        private void InitializeGraphDependencies(IEnumerable<XElement> xNodes)
        {
            foreach (var xNode in xNodes)
            {
                uint nodeIdentifier = uint.Parse(xNode.Element("Identifier").Value);
                var analysisNode = GetNode(nodeIdentifier);
                var objectMemberNode = _objectMemberGraph.GetNode(nodeIdentifier);

                foreach (ObjectMemberAnalysisNode objectMemberSuccessor in objectMemberNode.Successors)
                {
                    var successorAnalysisNode = GetNode(objectMemberSuccessor.Identifier);
                    if (successorAnalysisNode != null)
                    {
                        analysisNode.AddSuccesor(successorAnalysisNode);
                    }
                }
            }
        }
    }
}
