using ReframeAnalyzer.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeAnalyzer.Filters
{
    public abstract class AnalysisFilter : IAnalysisFilter
    {
        protected Predicate<IAnalysisNode> Query { get; set; }

        protected List<IAnalysisNode> OriginalNodes { get; set; }

        protected List<IFilterOption> FilterOptions { get; set; } = new List<IFilterOption>();

        public AnalysisFilter(List<IAnalysisNode> originalNodes)
        {
            OriginalNodes = originalNodes;
            InitializeOptions();
            InitializeSelectedNodes();
        }

        protected virtual void InitializeOptions()
        {

        }

        private void InitializeSelectedNodes()
        {
            foreach (var option in FilterOptions)
            {
                option.SelectNodes();
            }
        }

        public virtual IEnumerable<IAnalysisNode> Apply()
        {
            List<IAnalysisNode> filteredNodes = new List<IAnalysisNode>();
            filteredNodes.AddRange(OriginalNodes.FindAll(Query));
            return filteredNodes;
        }
        public IFilterOption GetFilterOption(AnalysisLevel level)
        {
            return FilterOptions.FirstOrDefault(o => o.Level == level);
        }

        private void SelectAllObjectNodes()
        {
            GetFilterOption(AnalysisLevel.ObjectLevel).SelectNodes();
        }

        private void SelectAllClassNodes()
        {
            GetFilterOption(AnalysisLevel.ClassLevel).SelectNodes();
        }

        public void SelectAllClassObjects(IAnalysisNode classNode)
        {
            if (classNode == null) return;

            GetFilterOption(AnalysisLevel.ObjectLevel).DeselectNodes(n => n.Parent.Identifier == classNode.Identifier);
        }

        private void SelectAllNamespaceObjects(IAnalysisNode namespaceNode)
        {
            if (namespaceNode == null) return;
            GetFilterOption(AnalysisLevel.ObjectLevel).SelectNodes(n => n.Parent.Parent.Identifier == namespaceNode.Identifier);
        }

        public void DeselectAllClassObjects(IAnalysisNode classNode)
        {
            if (classNode == null) return;
            GetFilterOption(AnalysisLevel.ObjectLevel).DeselectNodes(n => n.Parent.Identifier == classNode.Identifier);
        }

        public void DeselectAllObjectNodes(IAnalysisNode namespaceNode)
        {
            if (namespaceNode == null) return;
            GetFilterOption(AnalysisLevel.ObjectLevel).DeselectNodes(n => n.Parent.Parent.Identifier == namespaceNode.Identifier);
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

        #region Public methods

        public virtual List<IAnalysisNode> GetAvailableAssemblyNodes()
        {
            return new List<IAnalysisNode>();
        }
        public virtual List<IAnalysisNode> GetAvailableNamespaceNodes()
        {
            return new List<IAnalysisNode>();
        }
        public virtual List<IAnalysisNode> GetAvailableClassNodes()
        {
            return new List<IAnalysisNode>();
        }

        public virtual List<IAnalysisNode> GetAvailableClassNodes(IAnalysisNode namespaceNode)
        {
            return new List<IAnalysisNode>();
        }

        public virtual List<IAnalysisNode> GetAvailableObjectNodes()
        {
            return new List<IAnalysisNode>();
        }

        public virtual List<IAnalysisNode> GetAvailableObjectNodes(IAnalysisNode classNode)
        {
            return new List<IAnalysisNode>();
        }

        public virtual void SelectNode(IAnalysisNode node)
        {
            if (node != null)
            {
                GetFilterOption(node.Level)?.SelectNode(node);
                if (node.Level == AnalysisLevel.NamespaceLevel)
                {
                    SelectAllNamespaceClasses(node);
                    SelectAllNamespaceObjects(node);
                }
                else if (node.Level == AnalysisLevel.ClassLevel)
                {
                    SelectAllClassObjects(node);
                }
                else if (node.Level == AnalysisLevel.ObjectLevel)
                {

                }
            }
        }

        public virtual void DeselectNode(IAnalysisNode node)
        {
            if (node != null)
            {
                GetFilterOption(node.Level)?.DeselectNode(node);
                if (node.Level == AnalysisLevel.NamespaceLevel)
                {
                    DeselectAllNamespaceClasses(node);
                    DeselectAllObjectNodes(node);
                }
                else if (node.Level == AnalysisLevel.ClassLevel)
                {
                    DeselectAllClassObjects(node);
                }
                else if (node.Level == AnalysisLevel.ObjectLevel)
                {
                    
                }
            }
        }

        public bool IsSelected(IAnalysisNode node)
        {
            if (node != null)
            {
                if (node.Level == AnalysisLevel.NamespaceLevel)
                {
                    return GetFilterOption(AnalysisLevel.NamespaceLevel).IsSelected(node);
                }
                else if (node.Level == AnalysisLevel.AssemblyLevel)
                {
                    return GetFilterOption(AnalysisLevel.AssemblyLevel).IsSelected(node);
                }
                else if (node.Level == AnalysisLevel.ClassLevel)
                {
                    return GetFilterOption(AnalysisLevel.ClassLevel).IsSelected(node);
                }
                else if (node.Level == AnalysisLevel.ObjectLevel)
                {
                    return GetFilterOption(AnalysisLevel.ObjectLevel).IsSelected(node);
                }
            }
            return false;
        }

        #endregion
    }
}
