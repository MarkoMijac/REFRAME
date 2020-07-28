using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace ReframeAnalyzer.Graph
{
    public class NamespaceAnalysisGraphFactory : AnalysisGraphAbstractFactory
    {
        protected override IAnalysisGraph CreateGraph()
        {
            var factory = new ClassAnalysisGraphFactory();
            var classAnalysisGraph = factory.CreateGraph(XmlSource) as ClassAnalysisGraph;

            var graph = new NamespaceAnalysisGraph(classAnalysisGraph.Identifier, AnalysisLevel.NamespaceLevel);
            InitializeGraph(graph, classAnalysisGraph);
            return graph;
        }

        private void InitializeGraph(NamespaceAnalysisGraph graph, ClassAnalysisGraph classAnalysisGraph)
        {
            if (graph != null && classAnalysisGraph != null)
            {
                InitializeGraphNodes(graph, classAnalysisGraph.Nodes);
                InitializeGraphDependencies(graph, classAnalysisGraph.Nodes);
            }
        }

        private void InitializeGraphNodes(NamespaceAnalysisGraph graph, List<IAnalysisNode> nodes)
        {
            foreach (var classNode in nodes)
            {
                if (graph.ContainsNode(classNode.Parent.Identifier) == false)
                {
                    graph.AddNode(classNode.Parent);
                }
            }
        }

        private void InitializeGraphDependencies(NamespaceAnalysisGraph graph, List<IAnalysisNode> nodes)
        {
            foreach(var classNode in nodes)
            {
                var namespaceNode = graph.GetNode(classNode.Parent.Identifier);

                foreach (var classNodeSuccessor in classNode.Successors)
                {
                    var successorNamespaceNode = graph.GetNode(classNodeSuccessor.Parent.Identifier);
                    if (successorNamespaceNode != null)
                    {
                        namespaceNode.AddSuccesor(successorNamespaceNode);
                    }
                }
            }
        }
    }
}
