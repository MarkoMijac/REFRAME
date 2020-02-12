using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ReframeAnalyzer.Graph
{
    public class ClassMemberAnalysisGraph : AnalysisGraph
    {
        public ClassMemberAnalysisGraph(string source)
        {
            AnalysisLevel = AnalysisLevel.ClassMemberLevel;

            var graphFactory = new AnalysisGraphFactory();
            var objectMemberGraph = graphFactory.CreateGraph(source, AnalysisLevel.ObjectMemberLevel);

            if (objectMemberGraph != null)
            {
                InitializeGraphBasicData(objectMemberGraph);
                if (objectMemberGraph.Nodes.Count > 0)
                {
                    InitializeGraphNodes(objectMemberGraph.Nodes);
                    InitializeGraphDependencies(objectMemberGraph.Nodes);
                }
            }
        }

        private void InitializeGraphDependencies(List<IAnalysisNode> nodes)
        {
            foreach (var objectMemberNode in nodes)
            {
                XElement xNode = XElement.Parse(objectMemberNode.Source);
                var node = NodeFactory.CreateNode(xNode, AnalysisLevel.ClassMemberLevel);

                var classMemberNode = GetNode(node.Identifier);

                foreach (var objectMemberNodeSuccessor in objectMemberNode.Successors)
                {
                    XElement xSNode = XElement.Parse(objectMemberNodeSuccessor.Source);
                    var sNode = NodeFactory.CreateNode(xSNode, AnalysisLevel.ClassMemberLevel);
                    var successorClassMemberNode = GetNode(sNode.Identifier);
                    if (successorClassMemberNode != null)
                    {
                        classMemberNode.AddSuccesor(successorClassMemberNode);
                    }
                }
            }
        }

        private void InitializeGraphNodes(List<IAnalysisNode> nodes)
        {
            foreach (var objectMemberNode in nodes)
            {
                XElement xNode = XElement.Parse(objectMemberNode.Source);
                var classMemberNode = NodeFactory.CreateNode(xNode, AnalysisLevel.ClassMemberLevel);

                if (classMemberNode!=null && ContainsNode(classMemberNode.Identifier) == false)
                {
                    AddNode(classMemberNode);
                }
            }
        }

        private void InitializeGraphBasicData(IAnalysisGraph objectMemberGraph)
        {
            Identifier = objectMemberGraph.Identifier;
        }
    }
}
