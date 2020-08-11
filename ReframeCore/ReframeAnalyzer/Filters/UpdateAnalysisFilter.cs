using ReframeAnalyzer.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeAnalyzer.Filters
{
    public class UpdateAnalysisFilter : AnalysisFilter
    {
        public UpdateAnalysisFilter(List<IAnalysisNode> originalNodes) : base(originalNodes)
        {
            Query = new Predicate<IAnalysisNode>(objectMemberNode => IsSelected(objectMemberNode.Parent.Parent.Parent2) && IsSelected(objectMemberNode.Parent.Parent.Parent) && IsSelected(objectMemberNode.Parent.Parent) && IsSelected(objectMemberNode.Parent));
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

            IFilterOption objectFilterOption = new FilterOption(GetAvailableObjectNodes(), AnalysisLevel.ObjectLevel);
            FilterOptions.Add(objectFilterOption);
            objectFilterOption.SelectNodes();
        }

        public override void SelectNode(IAnalysisNode node)
        {
            base.SelectNode(node);

            if (node.Level == AnalysisLevel.NamespaceLevel)
            {
                SelectAllNamespaceClasses(node);
                SelectAllNamespaceObjects(node);
            }
            else if (node.Level == AnalysisLevel.ClassLevel)
            {
                SelectAllClassObjects(node);
            }
        }

        public void SelectAllNamespaceClasses(IAnalysisNode namespaceNode)
        {
            if (namespaceNode == null) return;

            GetFilterOption(AnalysisLevel.ClassLevel).SelectNodes(n => n.Parent.Identifier == namespaceNode.Identifier);
        }

        public void SelectAllNamespaceObjects(IAnalysisNode namespaceNode)
        {
            if (namespaceNode == null) return;
            GetFilterOption(AnalysisLevel.ObjectLevel).SelectNodes(n => n.Parent.Parent.Identifier == namespaceNode.Identifier);
        }

        public void SelectAllClassObjects(IAnalysisNode classNode)
        {
            if (classNode == null) return;

            GetFilterOption(AnalysisLevel.ObjectLevel).DeselectNodes(n => n.Parent.Identifier == classNode.Identifier);
        }

        public override void DeselectNode(IAnalysisNode node)
        {
            base.DeselectNode(node);

            if (node.Level == AnalysisLevel.NamespaceLevel)
            {
                DeselectAllNamespaceClasses(node);
                DeselectAllObjectNodes(node);
            }
            else if (node.Level == AnalysisLevel.ClassLevel)
            {
                DeselectAllClassObjects(node);
            }
        }

        public void DeselectAllNamespaceClasses(IAnalysisNode namespaceNode)
        {
            if (namespaceNode == null) return;
            GetFilterOption(AnalysisLevel.ClassLevel).DeselectNodes(n => n.Parent.Identifier == namespaceNode.Identifier);
        }

        public void DeselectAllObjectNodes(IAnalysisNode namespaceNode)
        {
            if (namespaceNode == null) return;
            GetFilterOption(AnalysisLevel.ObjectLevel).DeselectNodes(n => n.Parent.Parent.Identifier == namespaceNode.Identifier);
        }

        public void DeselectAllClassObjects(IAnalysisNode classNode)
        {
            if (classNode == null) return;
            GetFilterOption(AnalysisLevel.ObjectLevel).DeselectNodes(n => n.Parent.Identifier == classNode.Identifier);
        }
        public List<IAnalysisNode> GetAvailableAssemblyNodes()
        {
            List<IAnalysisNode> assemblyNodes = new List<IAnalysisNode>();

            foreach (var updateNode in OriginalNodes)
            {
                var objectMemberNode = updateNode.Parent;
                if (assemblyNodes.Exists(n => n.Identifier == objectMemberNode.Parent.Parent.Parent2.Identifier) == false)
                {
                    assemblyNodes.Add(objectMemberNode.Parent.Parent.Parent2);
                }
            }

            return assemblyNodes;
        }

        public List<IAnalysisNode> GetAvailableNamespaceNodes()
        {
            List<IAnalysisNode> namespaceNodes = new List<IAnalysisNode>();

            foreach (var updateNode in OriginalNodes)
            {
                var objectMemberNode = updateNode.Parent;
                if (namespaceNodes.Exists(n => n.Identifier == objectMemberNode.Parent.Parent.Parent.Identifier) == false)
                {
                    namespaceNodes.Add(objectMemberNode.Parent.Parent.Parent);
                }
            }

            return namespaceNodes;
        }

        public List<IAnalysisNode> GetAvailableClassNodes()
        {
            List<IAnalysisNode> classNodes = new List<IAnalysisNode>();

            foreach (var updateNode in OriginalNodes)
            {
                var objectMemberNode = updateNode.Parent;
                if (classNodes.Exists(n => n.Identifier == objectMemberNode.Parent.Parent.Identifier) == false)
                {
                    classNodes.Add(objectMemberNode.Parent.Parent);
                }
            }

            return classNodes;
        }

        public List<IAnalysisNode> GetAvailableClassNodes(IAnalysisNode namespaceNode)
        {
            List<IAnalysisNode> classNodes = new List<IAnalysisNode>();

            foreach (var updateNode in OriginalNodes)
            {
                var objectMemberNode = updateNode.Parent;
                if (objectMemberNode.Parent.Parent.Parent.Identifier == namespaceNode.Identifier && classNodes.Exists(n => n.Identifier == objectMemberNode.Parent.Parent.Identifier) == false)
                {
                    classNodes.Add(objectMemberNode.Parent.Parent);
                }
            }

            return classNodes;
        }

        public List<IAnalysisNode> GetAvailableObjectNodes()
        {
            List<IAnalysisNode> objectNodes = new List<IAnalysisNode>();

            foreach (var updateNode in OriginalNodes)
            {
                var objectMemberNode = updateNode.Parent;
                if (objectNodes.Exists(n => n.Identifier == objectMemberNode.Parent.Identifier) == false)
                {
                    objectNodes.Add(objectMemberNode.Parent);
                }
            }

            return objectNodes;
        }

        public List<IAnalysisNode> GetAvailableObjectNodes(IAnalysisNode classNode)
        {
            List<IAnalysisNode> objectNodes = new List<IAnalysisNode>();

            foreach (var updateNode in OriginalNodes)
            {
                var objectMemberNode = updateNode.Parent;
                if (objectMemberNode.Parent.Parent.Identifier == classNode.Identifier && objectNodes.Exists(n => n.Identifier == objectMemberNode.Parent.Identifier) == false)
                {
                    objectNodes.Add(objectMemberNode.Parent);
                }
            }

            return objectNodes;
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
