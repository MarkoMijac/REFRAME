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
        protected IEnumerable<IAnalysisNode> OriginalNodes { get; set; }

        protected List<IAnalysisNode> SelectedAssemblyNodes { get; set; } = new List<IAnalysisNode>();
        protected List<IAnalysisNode> SelectedNamespaceNodes { get; set; } = new List<IAnalysisNode>();
        protected List<IAnalysisNode> SelectedClassNodes { get; set; } = new List<IAnalysisNode>();
        protected List<IAnalysisNode> SelectedObjectNodes { get; set; } = new List<IAnalysisNode>();

        public AnalysisFilter(IEnumerable<IAnalysisNode> originalNodes)
        {
            OriginalNodes = originalNodes;
            InitializeSelectedNodes();
        }

        private void InitializeSelectedNodes()
        {
            SelectAllAssemblyNodes();
            SelectAllNamespaceNodes();
            SelectAllClassNodes();
        }

        protected void AddNode(List<IAnalysisNode> chosenNodes, IAnalysisNode node)
        {
            if (node!=null && chosenNodes.Contains(node) == false)
            {
                chosenNodes.Add(node);
            }
        }

        protected void RemoveNode(List<IAnalysisNode> chosenNodes, IAnalysisNode node)
        {
            if (node != null && chosenNodes.Contains(node) == true)
            {
                chosenNodes.Remove(node);
            }
        }

        public bool IsSelected(NamespaceAnalysisNode node)
        {
            return SelectedNamespaceNodes.Exists(n => n.Identifier == node.Identifier);
        }

        public bool IsSelected(AssemblyAnalysisNode node)
        {
            return SelectedAssemblyNodes.Exists(n => n.Identifier == node.Identifier);
        }

        public bool IsSelected(ClassAnalysisNode node)
        {
            return SelectedClassNodes.Exists(n => n.Identifier == node.Identifier);
        }

        public bool IsSelected(ObjectAnalysisNode node)
        {
            return SelectedObjectNodes.Exists(n => n.Identifier == node.Identifier);
        }

        public void SelectAssemblyNode(AssemblyAnalysisNode node)
        {
            AddNode(SelectedAssemblyNodes, node);
        }

        public void DeselectAssemblyNode(AssemblyAnalysisNode node)
        {
            RemoveNode(SelectedAssemblyNodes, node);
        }

        public void SelectNamespaceNode(NamespaceAnalysisNode node)
        {
            AddNode(SelectedNamespaceNodes, node);
            SelectAllClassNodes(node);
        }

        public void DeselectNamespaceNode(NamespaceAnalysisNode node)
        {
            RemoveNode(SelectedNamespaceNodes, node);
            DeselectAllClassNodes(node);
        }

        public void SelectClassNode(ClassAnalysisNode node)
        {
            AddNode(SelectedClassNodes, node);
        }

        public void DeselectClassNode(ClassAnalysisNode node)
        {
            RemoveNode(SelectedClassNodes, node);
        }

        protected void SelectAllClassNodes()
        {
            foreach (ClassAnalysisNode classNode in GetAvailableClassNodes())
            {
                if (SelectedClassNodes.Exists(n => n.Identifier == classNode.Identifier) == false)
                {
                    SelectedClassNodes.Add(classNode);
                }
            }
        }

        public void SelectAllClassNodes(NamespaceAnalysisNode namespaceNode)
        {
            if (namespaceNode == null) return;
            foreach (ClassAnalysisNode classNode in GetAvailableClassNodes())
            {
                if (classNode.OwnerNamespace.Identifier == namespaceNode.Identifier && SelectedClassNodes.Exists(n => n.Identifier == classNode.Identifier) == false)
                {
                    SelectedClassNodes.Add(classNode);
                }
            }
        }

        public void DeselectAllClassNodes(NamespaceAnalysisNode namespaceNode)
        {
            if (namespaceNode == null) return;
            SelectedClassNodes.RemoveAll(n => (n as ClassAnalysisNode).OwnerNamespace.Identifier == namespaceNode.Identifier);
        }

        public void SelectAllNamespaceNodes()
        {
            foreach (NamespaceAnalysisNode namespaceNode in GetAvailableNamespaceNodes())
            {
                if (SelectedNamespaceNodes.Exists(n => n.Identifier == namespaceNode.Identifier) == false)
                {
                    SelectedNamespaceNodes.Add(namespaceNode);
                }
            }
        }

        public void DeselectAllNamespaceNodes()
        {
            SelectedNamespaceNodes.Clear();
        }

        public void SelectAllAssemblyNodes()
        {
            foreach (AssemblyAnalysisNode assemblyNode in GetAvailableAssemblyNodes())
            {
                if (SelectedAssemblyNodes.Exists(n => n.Identifier == assemblyNode.Identifier) == false)
                {
                    SelectedAssemblyNodes.Add(assemblyNode);
                }
            }
        }

        public void DeselectAllAssemblyNodes()
        {
            SelectedAssemblyNodes.Clear();
        }

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

        public abstract IEnumerable<IAnalysisNode> Apply();
    }
}
