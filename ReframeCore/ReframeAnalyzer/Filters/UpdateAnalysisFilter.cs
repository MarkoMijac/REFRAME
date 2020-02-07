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
                if (IsSelected(objectMemberNode.OwnerObject.OwnerClass.OwnerAssembly) && IsSelected(objectMemberNode.OwnerObject.OwnerClass.OwnerNamespace) && IsSelected(objectMemberNode.OwnerObject.OwnerClass) && IsSelected(objectMemberNode.OwnerObject))
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
                if (assemblyNodes.Exists(n => n.Identifier == objectMemberNode.OwnerObject.OwnerClass.OwnerAssembly.Identifier) == false)
                {
                    assemblyNodes.Add(objectMemberNode.OwnerObject.OwnerClass.OwnerAssembly);
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
                if (namespaceNodes.Exists(n => n.Identifier == objectMemberNode.OwnerObject.OwnerClass.OwnerNamespace.Identifier) == false)
                {
                    namespaceNodes.Add(objectMemberNode.OwnerObject.OwnerClass.OwnerNamespace);
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
                if (classNodes.Exists(n => n.Identifier == objectMemberNode.OwnerObject.OwnerClass.Identifier) == false)
                {
                    classNodes.Add(objectMemberNode.OwnerObject.OwnerClass);
                }
            }

            return classNodes;
        }

        public override List<IAnalysisNode> GetAvailableClassNodes(NamespaceAnalysisNode namespaceNode)
        {
            List<IAnalysisNode> classNodes = new List<IAnalysisNode>();

            foreach (UpdateAnalysisNode updateNode in OriginalNodes)
            {
                ObjectMemberAnalysisNode objectMemberNode = updateNode.ObjectMemberNode;
                if (objectMemberNode.OwnerObject.OwnerClass.OwnerNamespace.Identifier == namespaceNode.Identifier && classNodes.Exists(n => n.Identifier == objectMemberNode.OwnerObject.OwnerClass.Identifier) == false)
                {
                    classNodes.Add(objectMemberNode.OwnerObject.OwnerClass);
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
                if (objectNodes.Exists(n => n.Identifier == objectMemberNode.OwnerObject.Identifier) == false)
                {
                    objectNodes.Add(objectMemberNode.OwnerObject);
                }
            }

            return objectNodes;
        }

        public override List<IAnalysisNode> GetAvailableObjectNodes(ClassAnalysisNode classNode)
        {
            List<IAnalysisNode> objectNodes = new List<IAnalysisNode>();

            foreach (UpdateAnalysisNode updateNode in OriginalNodes)
            {
                ObjectMemberAnalysisNode objectMemberNode = updateNode.ObjectMemberNode;
                if (objectMemberNode.OwnerObject.OwnerClass.Identifier == classNode.Identifier && objectNodes.Exists(n => n.Identifier == objectMemberNode.OwnerObject.Identifier) == false)
                {
                    objectNodes.Add(objectMemberNode.OwnerObject);
                }
            }

            return objectNodes;
        }
    }
}
