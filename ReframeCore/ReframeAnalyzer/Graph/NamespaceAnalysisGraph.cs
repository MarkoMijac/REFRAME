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
                if (ContainsNode(classNode.OwnerNamespace.Identifier) == false)
                {
                    AddNode(classNode.OwnerNamespace);
                }
            }
        }

        private void InitializeGraphDependencies(List<IAnalysisNode> classNodes)
        {
            foreach (ClassAnalysisNode classNode in classNodes)
            {
                var namespaceNode = GetNode(classNode.OwnerNamespace.Identifier);

                foreach (ClassAnalysisNode classNodeSuccessor in classNode.Successors)
                {
                    var successorNamespaceNode = GetNode(classNodeSuccessor.OwnerNamespace.Identifier);
                    if (successorNamespaceNode != null && namespaceNode != successorNamespaceNode)
                    {
                        namespaceNode.AddSuccesor(successorNamespaceNode);
                    }
                }
            }
        }
    }
}
