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
        public List<IAnalysisNode> ChosenAssemblyNodes { get; set; } = new List<IAnalysisNode>();
        public List<IAnalysisNode> ChosenNamespaceNodes { get; set; } = new List<IAnalysisNode>();        

        public ClassAnalysisFilter(IEnumerable<IAnalysisNode> originalNodes) : base(originalNodes)
        {

        }

        public override IEnumerable<IAnalysisNode> Apply()
        {
            List<ClassAnalysisNode> filteredNodes = new List<ClassAnalysisNode>();

            foreach (ClassAnalysisNode classNode in OriginalNodes)
            {
                if (ChosenAssemblyNodes.Exists(n => n.Identifier == classNode.OwnerAssembly.Identifier) && ChosenNamespaceNodes.Exists(n => n.Identifier == classNode.OwnerNamespace.Identifier))
                {
                    filteredNodes.Add(classNode);
                }
            }

            return filteredNodes;
        }

        public List<IAnalysisNode> GetAvailableAssemblyNodes()
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

        public List<IAnalysisNode> GetAvailableNamespaceNodes()
        {
            List<IAnalysisNode> namespaceNodes = new List<IAnalysisNode>();

            foreach (ClassAnalysisNode classNode in OriginalNodes)
            {
                if (namespaceNodes.Exists(n => n.Identifier == classNode.OwnerNamespace.Identifier) == false)
                {
                    namespaceNodes.Add(classNode.OwnerNamespace);
                }
            }

            return namespaceNodes;
        }

        public void AddAssemblyNode(AssemblyAnalysisNode node)
        {
            AddNode(ChosenAssemblyNodes, node);
        }

        public void RemoveAssemblyNode(AssemblyAnalysisNode node)
        {
            RemoveNode(ChosenAssemblyNodes, node);
        }

        public void AddNamespaceNode(NamespaceAnalysisNode node)
        {
            AddNode(ChosenNamespaceNodes, node);
        }

        public void RemoveNamespaceNode(NamespaceAnalysisNode node)
        {
            RemoveNode(ChosenNamespaceNodes, node);
        }
    }
}
