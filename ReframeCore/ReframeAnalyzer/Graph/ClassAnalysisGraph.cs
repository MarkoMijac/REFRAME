using ReframeCore;
using ReframeCore.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ReframeAnalyzer.Graph
{
    public class ClassAnalysisGraph : AnalysisGraph
    {
        public ClassAnalysisGraph(string source)
        {
            AnalysisLevel = AnalysisLevel.ClassLevel;
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
                XElement xClass = xNode.Element("OwnerClass");
                uint xClassIdentifier = uint.Parse(xClass.Element("Identifier").Value);

                if (ContainsNode(xClassIdentifier) == false)
                {
                    var classNode = new ClassAnalysisNode()
                    {
                        Identifier = xClassIdentifier,
                        Name = xClass.Element("Name").Value,
                        FullName = xClass.Element("FullName").Value,
                        Namespace = xClass.Element("Namespace").Value,
                        Assembly = xClass.Element("Assembly").Value
                    };

                    AddNode(classNode);
                }
            }
        }

        private void InitializeGraphDependencies(IEnumerable<XElement> xNodes)
        {
            foreach (var xNode in xNodes)
            {
                XElement xClass = xNode.Element("OwnerClass");
                uint xClassIdentifier = uint.Parse(xClass.Element("Identifier").Value);

                var analysisNode = GetNode(xClassIdentifier);
                IEnumerable<XElement> xSuccessors = xNode.Descendants("Successor");

                foreach (var xSuccessor in xSuccessors)
                {
                    string xSuccessorIdentifier = xSuccessor.Element("Identifier").Value;
                    XElement xSuccessorNode = xNodes.First(x => x.Element("Identifier").Value == xSuccessorIdentifier);
                    XElement xSuccessorNodeClass = xSuccessorNode.Element("OwnerClass");
                    uint xSuccessorNodeClassIdentifier = uint.Parse(xSuccessorNodeClass.Element("Identifier").Value);

                    var successorAnalysisNode = GetNode(xSuccessorNodeClassIdentifier);
                    
                    if (successorAnalysisNode != null)
                    {
                        analysisNode.AddSuccesor(successorAnalysisNode);
                        successorAnalysisNode.AddPredecessor(analysisNode);
                    }
                }
            }
        }
    }
}
