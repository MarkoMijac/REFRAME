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
        public List<IAnalysisNode> SelectedAssemblyNodes { get; private set; } = new List<IAnalysisNode>();
        public List<IAnalysisNode> SelectedNamespaceNodes { get; set; } = new List<IAnalysisNode>();
        public List<IAnalysisNode> SelectedClassNodes { get; set; } = new List<IAnalysisNode>();

        public ClassMemberAnalysisFilter(IEnumerable<IAnalysisNode> originalNodes) : base(originalNodes)
        {
            InitializeChosenNodes();
        }

        private void InitializeChosenNodes()
        {
            SelectAllAssemblyNodes();
            SelectAllNamespaceNodes();
            SelectAllClassNodes();
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

        public override IEnumerable<IAnalysisNode> Apply()
        {
            List<ClassMemberAnalysisNode> filteredNodes = new List<ClassMemberAnalysisNode>();

            foreach (ClassMemberAnalysisNode classMemberNode in OriginalNodes)
            {
                if (SelectedAssemblyNodes.Exists(a => a.Identifier == classMemberNode.OwnerClass.OwnerAssembly.Identifier) && SelectedNamespaceNodes.Exists(n => n.Identifier == classMemberNode.OwnerClass.OwnerNamespace.Identifier) && SelectedClassNodes.Exists(c => c.Identifier == classMemberNode.OwnerClass.Identifier))
                {
                    filteredNodes.Add(classMemberNode);
                }
            }

            return filteredNodes;
        }

        public List<IAnalysisNode> GetAvailableAssemblyNodes()
        {
            List<IAnalysisNode> assemblyNodes = new List<IAnalysisNode>();

            foreach (ClassMemberAnalysisNode classMemberNode in OriginalNodes)
            {
                if (assemblyNodes.Exists(n => n.Identifier == classMemberNode.OwnerClass.OwnerAssembly.Identifier) == false)
                {
                    assemblyNodes.Add(classMemberNode.OwnerClass.OwnerAssembly);
                }
            }

            return assemblyNodes;
        }

        public List<IAnalysisNode> GetAvailableNamespaceNodes()
        {
            List<IAnalysisNode> namespaceNodes = new List<IAnalysisNode>();

            foreach (ClassMemberAnalysisNode classMemberNode in OriginalNodes)
            {
                if (namespaceNodes.Exists(n => n.Identifier == classMemberNode.OwnerClass.OwnerNamespace.Identifier) == false)
                {
                    namespaceNodes.Add(classMemberNode.OwnerClass.OwnerNamespace);
                }
            }

            return namespaceNodes;
        }

        private List<IAnalysisNode> GetAvailableClassNodes()
        {
            List<IAnalysisNode> classNodes = new List<IAnalysisNode>();

            foreach (ClassMemberAnalysisNode classMemberNode in OriginalNodes)
            {
                if (classNodes.Exists(n => n.Identifier == classMemberNode.OwnerClass.Identifier) == false)
                {
                    classNodes.Add(classMemberNode.OwnerClass);
                }
            }

            return classNodes;
        }

        public List<IAnalysisNode> GetAvailableClassNodes(NamespaceAnalysisNode namespaceNode)
        {
            List<IAnalysisNode> classNodes = new List<IAnalysisNode>();

            foreach (ClassMemberAnalysisNode classMemberNode in OriginalNodes)
            {
                if (classMemberNode.OwnerClass.OwnerNamespace.Identifier == namespaceNode.Identifier && classNodes.Exists(n => n.Identifier == classMemberNode.OwnerClass.Identifier) == false)
                {
                    classNodes.Add(classMemberNode.OwnerClass);
                }
            }

            return classNodes;
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

        public bool IsSelected(NamespaceAnalysisNode node)
        {
            return SelectedNamespaceNodes.Exists(n => n.Identifier == node.Identifier);
        }
    }
}
