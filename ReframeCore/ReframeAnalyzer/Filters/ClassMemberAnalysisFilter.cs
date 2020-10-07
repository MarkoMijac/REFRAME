using ReframeAnalyzer.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeAnalyzer.Filters
{
    public class ClassMemberAnalysisFilter : AnalysisFilter
    {
        public IFilterOption AssemblyFilterOption { get; set; }
        public IFilterOption NamespaceFilterOption { get; set; }
        public IFilterOption ClassFilterOption { get; set; }

        public ClassMemberAnalysisFilter(List<IAnalysisNode> originalNodes) : base(originalNodes)
        {
            Query = new Predicate<IAnalysisNode>(n => AssemblyFilterOption.IsSelected(n.Parent.Parent2) && NamespaceFilterOption.IsSelected(n.Parent.Parent) && ClassFilterOption.IsSelected(n.Parent));

            AssemblyFilterOption = new FilterOption(GetAssemblyNodes());
            AssemblyFilterOption.SelectNodes();

            NamespaceFilterOption = new FilterOption(GetNamespaceNodes());
            NamespaceFilterOption.SelectNodes();
            NamespaceFilterOption.NodeSelected += NamespaceFilterOption_NodeSelected;
            NamespaceFilterOption.NodeDeselected += NamespaceFilterOption_NodeDeselected;

            ClassFilterOption = new FilterOption(GetClassNodes());
            ClassFilterOption.SelectNodes();
        }

        private List<IAnalysisNode> GetAssemblyNodes()
        {
            List<IAnalysisNode> assemblyNodes = new List<IAnalysisNode>();

            foreach (var classMemberNode in OriginalNodes)
            {
                if (assemblyNodes.Exists(n => n.Identifier == classMemberNode.Parent.Parent2.Identifier) == false)
                {
                    assemblyNodes.Add(classMemberNode.Parent.Parent2);
                }
            }

            return assemblyNodes;
        }

        private List<IAnalysisNode> GetNamespaceNodes()
        {
            List<IAnalysisNode> namespaceNodes = new List<IAnalysisNode>();

            foreach (var classMemberNode in OriginalNodes)
            {
                if (namespaceNodes.Exists(n => n.Identifier == classMemberNode.Parent.Parent.Identifier) == false)
                {
                    namespaceNodes.Add(classMemberNode.Parent.Parent);
                }
            }

            return namespaceNodes;
        }

        private List<IAnalysisNode> GetClassNodes()
        {
            List<IAnalysisNode> classNodes = new List<IAnalysisNode>();

            foreach (var classMemberNode in OriginalNodes)
            {
                if (classNodes.Exists(n => n.Identifier == classMemberNode.Parent.Identifier) == false)
                {
                    classNodes.Add(classMemberNode.Parent);
                }
            }

            return classNodes;
        }

        private void NamespaceFilterOption_NodeDeselected(object sender, EventArgs e)
        {
            var namespaceNode = sender as IAnalysisNode;
            ClassFilterOption.DeselectNodes(n => n.Parent.Identifier == namespaceNode.Identifier);
        }

        private void NamespaceFilterOption_NodeSelected(object sender, EventArgs e)
        {
            var namespaceNode = sender as IAnalysisNode;
            ClassFilterOption.SelectNodes(n => n.Parent.Identifier == namespaceNode.Identifier);
        }
    }
}
