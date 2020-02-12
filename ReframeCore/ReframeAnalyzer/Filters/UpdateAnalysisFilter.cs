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
        public UpdateAnalysisFilter(IEnumerable<IAnalysisNode> originalNodes) : base(originalNodes)
        {

        }

        public override IEnumerable<IAnalysisNode> Apply()
        {
            var filteredNodes = new List<UpdateAnalysisNode>();

            foreach (UpdateAnalysisNode updateNode in OriginalNodes)
            {
                ObjectMemberAnalysisNode objectMemberNode = updateNode.ObjectMemberNode;
                if (IsSelected(objectMemberNode.Parent.Parent.Parent2) && IsSelected(objectMemberNode.Parent.Parent.Parent) && IsSelected(objectMemberNode.Parent.Parent) && IsSelected(objectMemberNode.Parent))
                {
                    filteredNodes.Add(updateNode);
                }
            }

            return filteredNodes;
        }

        public override List<IAnalysisNode> GetAvailableAssemblyNodes()
        {
            List<IAnalysisNode> assemblyNodes = new List<IAnalysisNode>();

            foreach (UpdateAnalysisNode updateNode in OriginalNodes)
            {
                ObjectMemberAnalysisNode objectMemberNode = updateNode.ObjectMemberNode;
                if (assemblyNodes.Exists(n => n.Identifier == objectMemberNode.Parent.Parent.Parent2.Identifier) == false)
                {
                    assemblyNodes.Add(objectMemberNode.Parent.Parent.Parent2);
                }
            }

            return assemblyNodes;
        }

        public override List<IAnalysisNode> GetAvailableNamespaceNodes()
        {
            List<IAnalysisNode> namespaceNodes = new List<IAnalysisNode>();

            foreach (UpdateAnalysisNode updateNode in OriginalNodes)
            {
                ObjectMemberAnalysisNode objectMemberNode = updateNode.ObjectMemberNode;
                if (namespaceNodes.Exists(n => n.Identifier == objectMemberNode.Parent.Parent.Parent.Identifier) == false)
                {
                    namespaceNodes.Add(objectMemberNode.Parent.Parent.Parent);
                }
            }

            return namespaceNodes;
        }

        public override List<IAnalysisNode> GetAvailableClassNodes()
        {
            List<IAnalysisNode> classNodes = new List<IAnalysisNode>();

            foreach (UpdateAnalysisNode updateNode in OriginalNodes)
            {
                ObjectMemberAnalysisNode objectMemberNode = updateNode.ObjectMemberNode;
                if (classNodes.Exists(n => n.Identifier == objectMemberNode.Parent.Parent.Identifier) == false)
                {
                    classNodes.Add(objectMemberNode.Parent.Parent);
                }
            }

            return classNodes;
        }

        public override List<IAnalysisNode> GetAvailableClassNodes(IAnalysisNode namespaceNode)
        {
            List<IAnalysisNode> classNodes = new List<IAnalysisNode>();

            foreach (UpdateAnalysisNode updateNode in OriginalNodes)
            {
                ObjectMemberAnalysisNode objectMemberNode = updateNode.ObjectMemberNode;
                if (objectMemberNode.Parent.Parent.Parent.Identifier == namespaceNode.Identifier && classNodes.Exists(n => n.Identifier == objectMemberNode.Parent.Parent.Identifier) == false)
                {
                    classNodes.Add(objectMemberNode.Parent.Parent);
                }
            }

            return classNodes;
        }

        public override List<IAnalysisNode> GetAvailableObjectNodes()
        {
            List<IAnalysisNode> objectNodes = new List<IAnalysisNode>();

            foreach (UpdateAnalysisNode updateNode in OriginalNodes)
            {
                ObjectMemberAnalysisNode objectMemberNode = updateNode.ObjectMemberNode;
                if (objectNodes.Exists(n => n.Identifier == objectMemberNode.Parent.Identifier) == false)
                {
                    objectNodes.Add(objectMemberNode.Parent);
                }
            }

            return objectNodes;
        }

        public override List<IAnalysisNode> GetAvailableObjectNodes(IAnalysisNode classNode)
        {
            List<IAnalysisNode> objectNodes = new List<IAnalysisNode>();

            foreach (UpdateAnalysisNode updateNode in OriginalNodes)
            {
                ObjectMemberAnalysisNode objectMemberNode = updateNode.ObjectMemberNode;
                if (objectMemberNode.Parent.Parent.Identifier == classNode.Identifier && objectNodes.Exists(n => n.Identifier == objectMemberNode.Parent.Identifier) == false)
                {
                    objectNodes.Add(objectMemberNode.Parent);
                }
            }

            return objectNodes;
        }
    }
}
