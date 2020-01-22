using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeAnalyzer.Graph
{
    public class AssemblyAnalysisGraph : AnalysisGraph
    {
        public AssemblyAnalysisGraph(ClassAnalysisGraph classAnalysisGraph)
        {
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
                uint assemblyIdentifier = (uint)classNode.Assembly.GetHashCode();
                if (ContainsNode(assemblyIdentifier) == false)
                {
                    var assemblyNode = new AssemblyAnalysisNode
                    {
                        Identifier = assemblyIdentifier,
                        Name = classNode.Assembly
                    };

                    AddNode(assemblyNode);
                }
            }
        }

        private void InitializeGraphDependencies(List<IAnalysisNode> classNodes)
        {
            foreach (ClassAnalysisNode classNode in classNodes)
            {
                uint assemblyIdentifier = (uint)classNode.Assembly.GetHashCode();
                var assemblyNode = GetNode(assemblyIdentifier);

                foreach (ClassAnalysisNode classNodeSuccessor in classNode.Successors)
                {
                    uint successorAssemblyIdentifier = (uint)classNodeSuccessor.Assembly.GetHashCode();
                    var successorAssemblyNode = GetNode(successorAssemblyIdentifier);
                    if (successorAssemblyNode != null && assemblyNode != successorAssemblyNode)
                    {
                        assemblyNode.AddSuccesor(successorAssemblyNode);
                    }
                }
            }
        }
    }
}
