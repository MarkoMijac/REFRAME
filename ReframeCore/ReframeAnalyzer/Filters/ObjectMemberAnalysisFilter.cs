using ReframeAnalyzer.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeAnalyzer.Filters
{
    public class ObjectMemberAnalysisFilter : AnalysisFilter
    {
        public IFilterOption AssemblyFilterOption { get; set; }
        public IFilterOption NamespaceFilterOption { get; set; }
        public IFilterOption ClassFilterOption { get; set; }
        public IFilterOption ObjectFilterOption { get; set; }

        public ObjectMemberAnalysisFilter(List<IAnalysisNode> originalNodes) : base(originalNodes)
        {
            Query = new Predicate<IAnalysisNode>(n => AssemblyFilterOption.IsSelected(n.Parent.Parent.Parent2) && NamespaceFilterOption.IsSelected(n.Parent.Parent.Parent) && ClassFilterOption.IsSelected(n.Parent.Parent) && ObjectFilterOption.IsSelected(n.Parent));

            AssemblyFilterOption = new AssemblyFilterOption(GetAssemblyNodes(), AnalysisLevel.AssemblyLevel);
            AssemblyFilterOption.SelectNodes();

            NamespaceFilterOption = new NamespaceFilterOption(GetNamespaceNodes(), AnalysisLevel.NamespaceLevel);
            NamespaceFilterOption.SelectNodes();
            NamespaceFilterOption.NodeSelected += NamespaceFilterOption_NodeSelected;
            NamespaceFilterOption.NodeDeselected += NamespaceFilterOption_NodeDeselected;

            ClassFilterOption = new ClassFilterOption(GetClassNodes(), AnalysisLevel.ClassLevel);
            ClassFilterOption.SelectNodes();
            ClassFilterOption.NodeSelected += ClassFilterOption_NodeSelected;
            ClassFilterOption.NodeDeselected += ClassFilterOption_NodeDeselected;

            ObjectFilterOption = new ObjectFilterOption(GetObjectNodes(), AnalysisLevel.ObjectLevel);
            ObjectFilterOption.SelectNodes();
        }

        private void ClassFilterOption_NodeDeselected(object sender, EventArgs e)
        {
            var classNode = sender as IAnalysisNode;
            ObjectFilterOption.DeselectNodes(n => n.Parent.Identifier == classNode.Identifier);
        }

        private void ClassFilterOption_NodeSelected(object sender, EventArgs e)
        {
            var classNode = sender as IAnalysisNode;
            ObjectFilterOption.SelectNodes(n => n.Parent.Identifier == classNode.Identifier);
        }

        private void NamespaceFilterOption_NodeDeselected(object sender, EventArgs e)
        {
            var namespaceNode = sender as IAnalysisNode;
            ClassFilterOption.DeselectNodes(n => n.Parent.Identifier == namespaceNode.Identifier);
            ObjectFilterOption.DeselectNodes(n => n.Parent.Parent.Identifier == namespaceNode.Identifier);
        }

        private void NamespaceFilterOption_NodeSelected(object sender, EventArgs e)
        {
            var namespaceNode = sender as IAnalysisNode;
            ClassFilterOption.SelectNodes(n => n.Parent.Identifier == namespaceNode.Identifier);
            ObjectFilterOption.SelectNodes(n => n.Parent.Parent.Identifier == namespaceNode.Identifier);
        }

        private List<IAnalysisNode> GetAssemblyNodes()
        {
            List<IAnalysisNode> assemblyNodes = new List<IAnalysisNode>();

            foreach (var objectMemberNode in OriginalNodes)
            {
                if (assemblyNodes.Exists(n => n.Identifier == objectMemberNode.Parent.Parent.Parent2.Identifier) == false)
                {
                    assemblyNodes.Add(objectMemberNode.Parent.Parent.Parent2);
                }
            }

            return assemblyNodes;
        }

        private List<IAnalysisNode> GetNamespaceNodes()
        {
            List<IAnalysisNode> namespaceNodes = new List<IAnalysisNode>();

            foreach (var objectMemberNode in OriginalNodes)
            {
                if (namespaceNodes.Exists(n => n.Identifier == objectMemberNode.Parent.Parent.Parent.Identifier) == false)
                {
                    namespaceNodes.Add(objectMemberNode.Parent.Parent.Parent);
                }
            }

            return namespaceNodes;
        }

        private List<IAnalysisNode> GetClassNodes()
        {
            List<IAnalysisNode> classNodes = new List<IAnalysisNode>();

            foreach (var objectMemberNode in OriginalNodes)
            {
                if (classNodes.Exists(n => n.Identifier == objectMemberNode.Parent.Parent.Identifier) == false)
                {
                    classNodes.Add(objectMemberNode.Parent.Parent);
                }
            }

            return classNodes;
        }

        private List<IAnalysisNode> GetObjectNodes()
        {
            List<IAnalysisNode> objectNodes = new List<IAnalysisNode>();

            foreach (var objectMemberNode in OriginalNodes)
            {
                if (objectNodes.Exists(n => n.Identifier == objectMemberNode.Parent.Identifier) == false)
                {
                    objectNodes.Add(objectMemberNode.Parent);
                }
            }

            return objectNodes;
        }
    }
}
