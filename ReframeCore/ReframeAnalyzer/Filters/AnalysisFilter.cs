﻿using ReframeAnalyzer.Graph;
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

        

        private bool IsSelected(NamespaceAnalysisNode node)
        {
            return SelectedNamespaceNodes.Exists(n => n.Identifier == node.Identifier);
        }

        private bool IsSelected(AssemblyAnalysisNode node)
        {
            return SelectedAssemblyNodes.Exists(n => n.Identifier == node.Identifier);
        }

        private bool IsSelected(ClassAnalysisNode node)
        {
            return SelectedClassNodes.Exists(n => n.Identifier == node.Identifier);
        }

        private bool IsSelected(ObjectAnalysisNode node)
        {
            return SelectedObjectNodes.Exists(n => n.Identifier == node.Identifier);
        }

        private void SelectNode(AssemblyAnalysisNode node)
        {
            AddNode(SelectedAssemblyNodes, node);
        }

        private void DeselectNode(AssemblyAnalysisNode node)
        {
            RemoveNode(SelectedAssemblyNodes, node);
        }

        private void SelectNode(NamespaceAnalysisNode node)
        {
            AddNode(SelectedNamespaceNodes, node);
            SelectAllClassNodes(node);
        }

        private void DeselectNode(NamespaceAnalysisNode node)
        {
            RemoveNode(SelectedNamespaceNodes, node);
            DeselectAllClassNodes(node);
        }

        private void SelectNode(ClassAnalysisNode node)
        {
            AddNode(SelectedClassNodes, node);
        }

        private void DeselectNode(ClassAnalysisNode node)
        {
            RemoveNode(SelectedClassNodes, node);
        }

        private void SelectAllClassNodes()
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

        public virtual List<IAnalysisNode> GetAvailableClassNodes(NamespaceAnalysisNode namespaceNode)
        {
            return new List<IAnalysisNode>();
        }

        public void SelectNode(IAnalysisNode node)
        {
            if (node is NamespaceAnalysisNode)
            {
                SelectNode(node as NamespaceAnalysisNode);
            }
            else if (node is AssemblyAnalysisNode)
            {
                SelectNode(node as AssemblyAnalysisNode);
            }
            else if (node is ClassAnalysisNode)
            {
                SelectNode(node as ClassAnalysisNode);
            }
            else if (node is ObjectAnalysisNode)
            {
                SelectNode(node as ObjectAnalysisNode);
            }
        }

        public void DeselectNode(IAnalysisNode node)
        {
            if (node is NamespaceAnalysisNode)
            {
                DeselectNode(node as NamespaceAnalysisNode);
            }
            else if (node is AssemblyAnalysisNode)
            {
                DeselectNode(node as AssemblyAnalysisNode);
            }
            else if (node is ClassAnalysisNode)
            {
                DeselectNode(node as ClassAnalysisNode);
            }
            else if (node is ObjectAnalysisNode)
            {
                DeselectNode(node as ObjectAnalysisNode);
            }
        }

        public bool IsSelected(IAnalysisNode node)
        {
            if (node is NamespaceAnalysisNode)
            {
                return IsSelected(node as NamespaceAnalysisNode);
            }
            else if (node is AssemblyAnalysisNode)
            {
                return IsSelected(node as AssemblyAnalysisNode);
            }
            else if (node is ClassAnalysisNode)
            {
                return IsSelected(node as ClassAnalysisNode);
            }
            else if (node is ObjectAnalysisNode)
            {
                return IsSelected(node as ObjectAnalysisNode);
            }
            return false;
        }

        #endregion
    }
}