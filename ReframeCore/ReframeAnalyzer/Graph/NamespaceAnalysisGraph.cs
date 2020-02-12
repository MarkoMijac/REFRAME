﻿using System;
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
            foreach (var classNode in classNodes)
            {
                if (ContainsNode(classNode.Parent.Identifier) == false)
                {
                    AddNode(classNode.Parent);
                }
            }
        }

        private void InitializeGraphDependencies(List<IAnalysisNode> classNodes)
        {
            foreach (var classNode in classNodes)
            {
                var namespaceNode = GetNode(classNode.Parent.Identifier);

                foreach (var classNodeSuccessor in classNode.Successors)
                {
                    var successorNamespaceNode = GetNode(classNodeSuccessor.Parent.Identifier);
                    if (successorNamespaceNode != null)
                    {
                        namespaceNode.AddSuccesor(successorNamespaceNode);
                    }
                }
            }
        }
    }
}
