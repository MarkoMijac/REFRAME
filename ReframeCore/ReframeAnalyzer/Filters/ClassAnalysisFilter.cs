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
        public ClassAnalysisFilter(List<IAnalysisNode> originalNodes) : base(originalNodes)
        {
            Query = new Predicate<IAnalysisNode>(n => IsSelected(n.Parent2) && IsSelected(n.Parent) && IsSelected(n));
        }

        protected override void InitializeOptions()
        {
            IFilterOption assemblyFilterOption = new FilterOption(GetAvailableAssemblyNodes(), AnalysisLevel.AssemblyLevel);
            FilterOptions.Add(assemblyFilterOption);
            assemblyFilterOption.SelectNodes();

            IFilterOption namespaceFilterOption = new FilterOption(GetAvailableNamespaceNodes(), AnalysisLevel.NamespaceLevel);
            FilterOptions.Add(namespaceFilterOption);
            namespaceFilterOption.SelectNodes();

            IFilterOption classFilterOption = new FilterOption(GetAvailableClassNodes(), AnalysisLevel.ClassLevel);
            FilterOptions.Add(classFilterOption);
            classFilterOption.SelectNodes();
        }

        public override void SelectNode(IAnalysisNode node)
        {
            base.SelectNode(node);

            if (node.Level == AnalysisLevel.NamespaceLevel)
            {
                SelectAllNamespaceClasses(node);
            }
        }

        public override void DeselectNode(IAnalysisNode node)
        {
            base.DeselectNode(node);

            if (node.Level == AnalysisLevel.NamespaceLevel)
            {
                DeselectAllNamespaceClasses(node);
            }
        }

        public void SelectAllNamespaceClasses(IAnalysisNode namespaceNode)
        {
            if (namespaceNode == null) return;

            GetFilterOption(AnalysisLevel.ClassLevel).SelectNodes(n => n.Parent.Identifier == namespaceNode.Identifier);
        }

        public void DeselectAllNamespaceClasses(IAnalysisNode namespaceNode)
        {
            if (namespaceNode == null) return;
            GetFilterOption(AnalysisLevel.ClassLevel).DeselectNodes(n => n.Parent.Identifier == namespaceNode.Identifier);
        }
        public List<IAnalysisNode> GetAvailableAssemblyNodes()
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

        public List<IAnalysisNode> GetAvailableNamespaceNodes()
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

        public List<IAnalysisNode> GetAvailableClassNodes()
        {
            List<IAnalysisNode> classNodes = new List<IAnalysisNode>();

            foreach (var classNode in OriginalNodes)
            {
                classNodes.Add(classNode);
            }

            return classNodes;
        }

        public List<IAnalysisNode> GetAvailableClassNodes(IAnalysisNode namespaceNode)
        {
            List<IAnalysisNode> classNodes = new List<IAnalysisNode>();

            foreach (var classNode in OriginalNodes)
            {
                if (classNode.Parent.Identifier == namespaceNode.Identifier && classNodes.Exists(n => n.Identifier == classNode.Identifier) == false)
                {
                    classNodes.Add(classNode);
                }
            }

            return classNodes;
        }

        public void SelectAllAssemblyNodes()
        {
            IFilterOption assemblyFilterOption = GetFilterOption(AnalysisLevel.AssemblyLevel);
            if (assemblyFilterOption != null)
            {
                assemblyFilterOption.SelectNodes();
            }
        }
        public void DeselectAllAssemblyNodes()
        {
            IFilterOption assemblyFilterOption = GetFilterOption(AnalysisLevel.AssemblyLevel);
            if (assemblyFilterOption != null)
            {
                assemblyFilterOption.DeselectNodes();
            }
        }

        public void SelectAllNamespaceNodes()
        {
            GetFilterOption(AnalysisLevel.NamespaceLevel).SelectNodes();
        }

        public void DeselectAllNamespaceNodes()
        {
            GetFilterOption(AnalysisLevel.NamespaceLevel).DeselectNodes();
        }
    }
}
