﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeAnalyzer.Graph
{
    public class AssemblyAnalysisGraph : AnalysisGraph
    {
        public AssemblyAnalysisGraph(IAnalysisGraph classAnalysisGraph)
        {
            AnalysisLevel = AnalysisLevel.AssemblyLevel;

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

        private void InitializeGraphBasicData(IAnalysisGraph classAnalysisGraph)
        {
            Identifier = classAnalysisGraph.Identifier;
        }

        private void InitializeGraphNodes(List<IAnalysisNode> classNodes)
        {
            foreach (var classNode in classNodes)
            {
                if (ContainsNode(classNode.Parent2.Identifier) == false)
                {
                    AddNode(classNode.Parent2);
                }
            }
        }

        private void InitializeGraphDependencies(List<IAnalysisNode> classNodes)
        {
            foreach (var classNode in classNodes)
            {
                var assemblyNode = GetNode(classNode.Parent2.Identifier);

                foreach (var classNodeSuccessor in classNode.Successors)
                {
                    var successorAssemblyNode = GetNode(classNodeSuccessor.Parent2.Identifier);
                    if (successorAssemblyNode != null)
                    {
                        assemblyNode.AddSuccesor(successorAssemblyNode);
                    }
                }
            }
        }
    }
}
