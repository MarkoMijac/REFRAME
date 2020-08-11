using ReframeAnalyzer.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeAnalyzer.Filters
{
    public class ClassMemberAnalysisFilter : AnalysisFilter
    {
        public ClassMemberAnalysisFilter(List<IAnalysisNode> originalNodes) : base(originalNodes)
        {
            Query = new Predicate<IAnalysisNode>(n => IsSelected(n.Parent.Parent2) && IsSelected(n.Parent.Parent) && IsSelected(n.Parent));
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

            foreach (var classMemberNode in OriginalNodes)
            {
                if (assemblyNodes.Exists(n => n.Identifier == classMemberNode.Parent.Parent2.Identifier) == false)
                {
                    assemblyNodes.Add(classMemberNode.Parent.Parent2);
                }
            }

            return assemblyNodes;
        }

        public List<IAnalysisNode> GetAvailableNamespaceNodes()
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

        public List<IAnalysisNode> GetAvailableClassNodes()
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

        public List<IAnalysisNode> GetAvailableClassNodes(IAnalysisNode namespaceNode)
        {
            List<IAnalysisNode> classNodes = new List<IAnalysisNode>();

            foreach (var classMemberNode in OriginalNodes)
            {
                if (classMemberNode.Parent.Parent.Identifier == namespaceNode.Identifier && classNodes.Exists(n => n.Identifier == classMemberNode.Parent.Identifier) == false)
                {
                    classNodes.Add(classMemberNode.Parent);
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
            //for (int i = SelectedNamespaceNodes.Count; i > 0; i--)
            //{
            //    DeselectNode(SelectedNamespaceNodes[i-1]);
            //}
            GetFilterOption(AnalysisLevel.NamespaceLevel).DeselectNodes();
        }
    }
}
