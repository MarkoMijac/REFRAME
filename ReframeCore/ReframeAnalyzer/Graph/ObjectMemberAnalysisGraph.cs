using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ReframeAnalyzer.Graph
{
    public class ObjectMemberAnalysisGraph : AnalysisGraph
    {
        public ObjectMemberAnalysisGraph(string source)
        {
            AnalysisLevel = AnalysisLevel.ObjectMemberLevel;
            Source = source;

            XElement xReactor = XElement.Parse(source);
            XElement xGraph = xReactor.Element("Graph");

            InitializeGraphBasicData(xGraph);
            IEnumerable<XElement> xNodes = FetchNodes(xGraph);
            if (xNodes.Count() > 0)
            {
                InitializeGraphNodes(xNodes);
                InitializeGraphDependencies(xNodes);
            }
        }

        private IEnumerable<XElement> FetchNodes(XElement xGraph)
        {
            IEnumerable<XElement> xNodes = null;

            if (xGraph != null)
            {
                xNodes = xGraph.Descendants("Node");
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
                    var node = new ObjectMemberAnalysisNode
                    {
                        Identifier = nodeIdentifier,
                        Name = xNode.Element("MemberName").Value,
                        NodeType = xNode.Element("NodeType").Value,
                        OwnerObjectIdentifier = xNode.Element("OwnerObject").Value
                    };

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
