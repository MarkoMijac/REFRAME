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
        public IFilterOption AssemblyFilterOption { get; set; }
        public IFilterOption NamespaceFilterOption { get; set; }
        public IFilterOption ClassFilterOption { get; set; }

        public ClassAnalysisFilter(List<IAnalysisNode> originalNodes) : base(originalNodes)
        {
            Query = new Predicate<IAnalysisNode>(n => AssemblyFilterOption.IsSelected(n.Parent2) && NamespaceFilterOption.IsSelected(n.Parent) && ClassFilterOption.IsSelected(n));

            AssemblyFilterOption = new FilterOption(GetAssemblyNodes(), AnalysisLevel.AssemblyLevel);
            AssemblyFilterOption.SelectNodes();

            NamespaceFilterOption = new FilterOption(GetNamespaceNodes(), AnalysisLevel.NamespaceLevel);
            NamespaceFilterOption.SelectNodes();
            NamespaceFilterOption.NodeSelected += NamespaceFilterOption_NodeSelected;
            NamespaceFilterOption.NodeDeselected += NamespaceFilterOption_NodeDeselected;

            ClassFilterOption = new FilterOption(OriginalNodes, AnalysisLevel.ClassLevel);
            ClassFilterOption.SelectNodes();
        }

        private List<IAnalysisNode> GetAssemblyNodes()
        {
            List<IAnalysisNode> assemblyNodes = new List<IAnalysisNode>();

            foreach (var classNode in OriginalNodes)
            {
                if (assemblyNodes.Exists(n => n.Identifier == classNode.Parent2.Identifier) == false)
                {
                    assemblyNodes.Add(classNode.Parent2);
                }
            }

            return assemblyNodes;
        }

        private List<IAnalysisNode> GetNamespaceNodes()
        {
            List<IAnalysisNode> namespaceNodes = new List<IAnalysisNode>();

            foreach (var classNode in OriginalNodes)
            {
                if (namespaceNodes.Exists(n => n.Identifier == classNode.Parent.Identifier) == false)
                {
                    namespaceNodes.Add(classNode.Parent);
                }
            }

            return namespaceNodes;
        }

        private void NamespaceFilterOption_NodeDeselected(object sender, EventArgs e)
        {
            var namespaceNode = sender as IAnalysisNode;
            if (namespaceNode != null)
            {
                ClassFilterOption.DeselectNodes(n => n.Parent.Identifier == namespaceNode.Identifier);
            }
        }

        private void NamespaceFilterOption_NodeSelected(object sender, EventArgs e)
        {
            var namespaceNode = sender as IAnalysisNode;
            if (namespaceNode != null)
            {
                ClassFilterOption.SelectNodes(n => n.Parent.Identifier == namespaceNode.Identifier);
            }
        }
    }
}
