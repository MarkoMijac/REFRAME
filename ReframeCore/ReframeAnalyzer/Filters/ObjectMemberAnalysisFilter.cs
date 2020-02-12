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
        public ObjectMemberAnalysisFilter(IEnumerable<IAnalysisNode> originalNodes) : base(originalNodes)
        {

        }

        public override IEnumerable<IAnalysisNode> Apply()
        {
            var filteredNodes = new List<ObjectMemberAnalysisNode>();

            foreach (ObjectMemberAnalysisNode objectMember in OriginalNodes)
            {
                if (IsSelected(objectMember.OwnerObject.Parent.Parent2) && IsSelected(objectMember.OwnerObject.Parent.Parent) && IsSelected(objectMember.OwnerObject.Parent) && IsSelected(objectMember.OwnerObject))
                {
                    filteredNodes.Add(objectMember);
                }
            }

            return filteredNodes;
        }

        public override List<IAnalysisNode> GetAvailableAssemblyNodes()
        {
            List<IAnalysisNode> assemblyNodes = new List<IAnalysisNode>();

            foreach (ObjectMemberAnalysisNode objectMemberNode in OriginalNodes)
            {
                if (assemblyNodes.Exists(n => n.Identifier == objectMemberNode.OwnerObject.Parent.Parent2.Identifier) == false)
                {
                    assemblyNodes.Add(objectMemberNode.OwnerObject.Parent.Parent2);
                }
            }

            return assemblyNodes;
        }

        public override List<IAnalysisNode> GetAvailableNamespaceNodes()
        {
            List<IAnalysisNode> namespaceNodes = new List<IAnalysisNode>();

            foreach (ObjectMemberAnalysisNode objectMemberNode in OriginalNodes)
            {
                if (namespaceNodes.Exists(n => n.Identifier == objectMemberNode.OwnerObject.Parent.Parent.Identifier) == false)
                {
                    namespaceNodes.Add(objectMemberNode.OwnerObject.Parent.Parent);
                }
            }

            return namespaceNodes;
        }

        public override List<IAnalysisNode> GetAvailableClassNodes()
        {
            List<IAnalysisNode> classNodes = new List<IAnalysisNode>();

            foreach (ObjectMemberAnalysisNode objectMemberNode in OriginalNodes)
            {
                if (classNodes.Exists(n => n.Identifier == objectMemberNode.OwnerObject.Parent.Identifier) == false)
                {
                    classNodes.Add(objectMemberNode.OwnerObject.Parent);
                }
            }

            return classNodes;
        }

        public override List<IAnalysisNode> GetAvailableClassNodes(IAnalysisNode namespaceNode)
        {
            List<IAnalysisNode> classNodes = new List<IAnalysisNode>();

            foreach (ObjectMemberAnalysisNode objectMemberNode in OriginalNodes)
            {
                if (objectMemberNode.OwnerObject.Parent.Parent.Identifier == namespaceNode.Identifier && classNodes.Exists(n => n.Identifier == objectMemberNode.OwnerObject.Parent.Identifier) == false)
                {
                    classNodes.Add(objectMemberNode.OwnerObject.Parent);
                }
            }

            return classNodes;
        }

        public override List<IAnalysisNode> GetAvailableObjectNodes()
        {
            List<IAnalysisNode> objectNodes = new List<IAnalysisNode>();

            foreach (ObjectMemberAnalysisNode objectMemberNode in OriginalNodes)
            {
                if (objectNodes.Exists(n => n.Identifier == objectMemberNode.OwnerObject.Identifier) == false)
                {
                    objectNodes.Add(objectMemberNode.OwnerObject);
                }
            }

            return objectNodes;
        }

        public override List<IAnalysisNode> GetAvailableObjectNodes(IAnalysisNode classNode)
        {
            List<IAnalysisNode> objectNodes = new List<IAnalysisNode>();

            foreach (ObjectMemberAnalysisNode objectMemberNode in OriginalNodes)
            {
                if (objectMemberNode.OwnerObject.Parent.Identifier == classNode.Identifier && objectNodes.Exists(n => n.Identifier == objectMemberNode.OwnerObject.Identifier) == false)
                {
                    objectNodes.Add(objectMemberNode.OwnerObject);
                }
            }

            return objectNodes;
        }
    }
}
