using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReframeAnalyzer.Graph;

namespace ReframeAnalyzer.Filters
{
    public class ClassAnalysisFilter : AnalysisFilter
    {
        public ClassAnalysisFilter(IEnumerable<IAnalysisNode> originalNodes) : base(originalNodes)
        {

        }

        public override IEnumerable<IAnalysisNode> Apply()
        {
            List<ClassAnalysisNode> filteredNodes = new List<ClassAnalysisNode>();

            foreach (ClassAnalysisNode classNode in OriginalNodes)
            {
                if (IsSelected(classNode.OwnerAssembly) && IsSelected(classNode.Parent) && IsSelected(classNode))
                {
                    filteredNodes.Add(classNode);
                }
            }

            return filteredNodes;
        }

        public override List<IAnalysisNode> GetAvailableAssemblyNodes()
        {
            List<IAnalysisNode> assemblyNodes = new List<IAnalysisNode>();

            foreach (ClassAnalysisNode classNode in OriginalNodes)
            {
                if (assemblyNodes.Exists(n => n.Identifier == classNode.OwnerAssembly.Identifier) == false)
                {
                    assemblyNodes.Add(classNode.OwnerAssembly);
                }
            }

            return assemblyNodes;
        }

        public override List<IAnalysisNode> GetAvailableNamespaceNodes()
        {
            List<IAnalysisNode> namespaceNodes = new List<IAnalysisNode>();

            foreach (ClassAnalysisNode classNode in OriginalNodes)
            {
                if (namespaceNodes.Exists(n => n.Identifier == classNode.Parent.Identifier) == false)
                {
                    namespaceNodes.Add(classNode.Parent);
                }
            }

            return namespaceNodes;
        }

        public override List<IAnalysisNode> GetAvailableClassNodes()
        {
            List<IAnalysisNode> classNodes = new List<IAnalysisNode>();

            foreach (ClassAnalysisNode classNode in OriginalNodes)
            {
                classNodes.Add(classNode);
            }

            return classNodes;
        }

        public override List<IAnalysisNode> GetAvailableClassNodes(IAnalysisNode namespaceNode)
        {
            List<IAnalysisNode> classNodes = new List<IAnalysisNode>();

            foreach (ClassAnalysisNode classNode in OriginalNodes)
            {
                if (classNode.Parent.Identifier == namespaceNode.Identifier && classNodes.Exists(n => n.Identifier == classNode.Identifier) == false)
                {
                    classNodes.Add(classNode);
                }
            }

            return classNodes;
        }
    }
}
