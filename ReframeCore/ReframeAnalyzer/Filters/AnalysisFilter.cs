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
            SelectAllObjectNodes();
        }

        private void AddNode(List<IAnalysisNode> chosenNodes, IAnalysisNode node)
        {
            if (node!=null && chosenNodes.Contains(node) == false)
            {
                chosenNodes.Add(node);
            }
        }

        private void RemoveNode(List<IAnalysisNode> chosenNodes, IAnalysisNode node)
        {
            if (node != null && chosenNodes.Contains(node) == true)
            {
                chosenNodes.Remove(node);
            }
        }

        private void SelectAllObjectNodes()
        {
            foreach (var objectNode in GetAvailableObjectNodes())
            {
                if (SelectedObjectNodes.Exists(n => n.Identifier == objectNode.Identifier) == false)
                {
                    SelectedObjectNodes.Add(objectNode);
                }
            }
        }

        private void SelectAllClassNodes()
        {
            foreach (var classNode in GetAvailableClassNodes())
            {
                if (SelectedClassNodes.Exists(n => n.Identifier == classNode.Identifier) == false)
                {
                    SelectedClassNodes.Add(classNode);
                }
            }
        }

        public void SelectAllClassObjects(IAnalysisNode classNode)
        {
            if (classNode == null) return;

            foreach (var objectNode in GetAvailableObjectNodes())
            {
                if (objectNode.Parent.Identifier == classNode.Identifier && SelectedObjectNodes.Exists(n => n.Identifier == objectNode.Identifier) == false)
                {
                    SelectedObjectNodes.Add(objectNode);
                }
            }
        }

        private void SelectAllNamespaceObjects(IAnalysisNode namespaceNode)
        {
            if (namespaceNode == null) return;

            foreach (var objectNode in GetAvailableObjectNodes())
            {
                if (objectNode.Parent.Parent.Identifier == namespaceNode.Identifier && SelectedObjectNodes.Exists(n => n.Identifier == objectNode.Identifier) == false)
                {
                    SelectedObjectNodes.Add(objectNode);
                }
            }
        }

        public void DeselectAllClassObjects(IAnalysisNode classNode)
        {
            if (classNode == null) return;
            SelectedObjectNodes.RemoveAll(n => n.Parent.Identifier == classNode.Identifier);
        }

        public void DeselectAllObjectNodes(IAnalysisNode namespaceNode)
        {
            if (namespaceNode == null) return;
            SelectedObjectNodes.RemoveAll(n => n.Parent.Parent.Identifier == namespaceNode.Identifier);
        }

        public void SelectAllNamespaceClasses(IAnalysisNode namespaceNode)
        {
            if (namespaceNode == null) return;
            foreach (var classNode in GetAvailableClassNodes())
            {
                if (classNode.Parent.Identifier == namespaceNode.Identifier && SelectedClassNodes.Exists(n => n.Identifier == classNode.Identifier) == false)
                {
                    SelectedClassNodes.Add(classNode);
                }
            }
        }

        public void DeselectAllNamespaceClasses(IAnalysisNode namespaceNode)
        {
            if (namespaceNode == null) return;
            SelectedClassNodes.RemoveAll(n => n.Parent.Identifier == namespaceNode.Identifier);
        }

        public void SelectAllNamespaceNodes()
        {
            foreach (var namespaceNode in GetAvailableNamespaceNodes())
            {
                if (SelectedNamespaceNodes.Exists(n => n.Identifier == namespaceNode.Identifier) == false)
                {
                    SelectNode(namespaceNode);
                }
            }
        }

        public void DeselectAllNamespaceNodes()
        {
            for (int i = SelectedNamespaceNodes.Count; i > 0; i--)
            {
                DeselectNode(SelectedNamespaceNodes[i-1]);
            }
        }

        public void SelectAllAssemblyNodes()
        {
            foreach (var assemblyNode in GetAvailableAssemblyNodes())
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

        #region Public methods

        public abstract IEnumerable<IAnalysisNode> Apply();

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

        public void SelectNode(IAnalysisNode node)
        {
            if (node != null)
            {
                if (node.Level == AnalysisLevel.NamespaceLevel)
                {
                    AddNode(SelectedNamespaceNodes, node);
                    SelectAllNamespaceClasses(node);
                    SelectAllNamespaceObjects(node);
                }
                else if (node.Level == AnalysisLevel.AssemblyLevel)
                {
                    AddNode(SelectedAssemblyNodes, node);
                }
                else if (node.Level == AnalysisLevel.ClassLevel)
                {
                    AddNode(SelectedClassNodes, node);
                    SelectAllClassObjects(node);
                }
                else if (node.Level == AnalysisLevel.ObjectLevel)
                {
                    AddNode(SelectedObjectNodes, node);
                }
            }
        }

        public void DeselectNode(IAnalysisNode node)
        {
            if (node != null)
            {
                if (node.Level == AnalysisLevel.NamespaceLevel)
                {
                    RemoveNode(SelectedNamespaceNodes, node);
                    DeselectAllNamespaceClasses(node);
                    DeselectAllObjectNodes(node);
                }
                else if (node.Level == AnalysisLevel.AssemblyLevel)
                {
                    RemoveNode(SelectedAssemblyNodes, node);
                }
                else if (node.Level == AnalysisLevel.ClassLevel)
                {
                    RemoveNode(SelectedClassNodes, node);
                    DeselectAllClassObjects(node);
                }
                else if (node.Level == AnalysisLevel.ObjectLevel)
                {
                    RemoveNode(SelectedObjectNodes, node);
                }
            }
        }

        public bool IsSelected(IAnalysisNode node)
        {
            if (node != null)
            {
                if (node.Level == AnalysisLevel.NamespaceLevel)
                {
                    return SelectedNamespaceNodes.Exists(n => n.Identifier == node.Identifier);
                }
                else if (node.Level == AnalysisLevel.AssemblyLevel)
                {
                    return SelectedAssemblyNodes.Exists(n => n.Identifier == node.Identifier);
                }
                else if (node.Level == AnalysisLevel.ClassLevel)
                {
                    return SelectedClassNodes.Exists(n => n.Identifier == node.Identifier);
                }
                else if (node.Level == AnalysisLevel.ObjectLevel)
                {
                    return SelectedObjectNodes.Exists(n => n.Identifier == node.Identifier);
                }
            }
            return false;
        }

        #endregion
    }
}
