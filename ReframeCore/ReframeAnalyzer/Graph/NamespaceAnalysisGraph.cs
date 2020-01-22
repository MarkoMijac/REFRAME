using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeAnalyzer.Graph
{
    public class NamespaceAnalysisGraph : AnalysisGraph
    {
        public NamespaceAnalysisGraph(ClassAnalysisGraph classAnalysisGraph)
        {
            AnalysisLevel = AnalysisLevel.NamespaceLevel;

            if (classAnalysisGraph != null)
            {
                InitializeGraphBasicData(classAnalysisGraph);
                if (classAnalysisGraph.Nodes.Count > 0)
                {
                    InitializeGraphNodes(classAnalysisGraph.Nodes);
                    InitializeGraphDependencies(classAnalysisGraph.Nodes);
                }
            }
        }

        private void InitializeGraphBasicData(ClassAnalysisGraph classAnalysisGraph)
        {
            Identifier = classAnalysisGraph.Identifier;
        }

        private void InitializeGraphNodes(List<IAnalysisNode> classNodes)
        {
            foreach (ClassAnalysisNode classNode in classNodes)
            {
                uint namespaceIdentifier = (uint)classNode.Namespace.GetHashCode();
                if (ContainsNode(namespaceIdentifier) == false)
                {
                    var namespaceNode = new NamespaceAnalysisNode
                    {
                        Identifier = namespaceIdentifier,
                        Name = classNode.Namespace
                    };

                    AddNode(namespaceNode);
                }
            }
        }

        private void InitializeGraphDependencies(List<IAnalysisNode> classNodes)
        {
            foreach (ClassAnalysisNode classNode in classNodes)
            {
                uint namespaceIdentifier = (uint)classNode.Namespace.GetHashCode();
                var namespaceNode = GetNode(namespaceIdentifier);

                foreach (ClassAnalysisNode classNodeSuccessor in classNode.Successors)
                {
                    uint successorNamespaceIdentifier = (uint)classNodeSuccessor.Namespace.GetHashCode();
                    var successorNamespaceNode = GetNode(successorNamespaceIdentifier);
                    if (successorNamespaceNode != null && namespaceNode != successorNamespaceNode)
                    {
                        namespaceNode.AddSuccesor(successorNamespaceNode);
                    }
                }
            }
        }
    }
}
