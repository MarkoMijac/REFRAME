using ReframeAnalyzer.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeAnalyzer.Filters
{
    public class ObjectAnalysisFilter : AnalysisFilter
    {
        public ObjectAnalysisFilter(IEnumerable<IAnalysisNode> originalNodes) : base(originalNodes)
        {

        }

        public override IEnumerable<IAnalysisNode> Apply()
        {
            List<ObjectAnalysisNode> filteredNodes = new List<ObjectAnalysisNode>();

            foreach (ObjectAnalysisNode objectNode in OriginalNodes)
            {
                if (IsSelected(objectNode.OwnerClass.OwnerAssembly) && IsSelected(objectNode.OwnerClass.OwnerNamespace) && IsSelected(objectNode.OwnerClass) && IsSelected(objectNode))
                {
                    filteredNodes.Add(objectNode);
                }
            }

            return filteredNodes;
        }

        public override List<IAnalysisNode> GetAvailableAssemblyNodes()
        {
            List<IAnalysisNode> assemblyNodes = new List<IAnalysisNode>();

            foreach (ObjectAnalysisNode objectNode in OriginalNodes)
            {
                if (assemblyNodes.Exists(n => n.Identifier == objectNode.OwnerClass.OwnerAssembly.Identifier) == false)
                {
                    assemblyNodes.Add(objectNode.OwnerClass.OwnerAssembly);
                }
            }

            return assemblyNodes;
        }

        public override List<IAnalysisNode> GetAvailableNamespaceNodes()
        {
            List<IAnalysisNode> namespaceNodes = new List<IAnalysisNode>();

            foreach (ObjectAnalysisNode objectNode in OriginalNodes)
            {
                if (namespaceNodes.Exists(n => n.Identifier == objectNode.OwnerClass.OwnerNamespace.Identifier) == false)
                {
                    namespaceNodes.Add(objectNode.OwnerClass.OwnerNamespace);
                }
            }

            return namespaceNodes;
        }

        public override List<IAnalysisNode> GetAvailableClassNodes()
        {
            List<IAnalysisNode> classNodes = new List<IAnalysisNode>();

            foreach (ObjectAnalysisNode objectNode in OriginalNodes)
            {
                if (classNodes.Exists(n => n.Identifier == objectNode.OwnerClass.Identifier) == false)
                {
                    classNodes.Add(objectNode.OwnerClass);
                }
            }

            return classNodes;
        }

        public override List<IAnalysisNode> GetAvailableClassNodes(NamespaceAnalysisNode namespaceNode)
        {
            List<IAnalysisNode> classNodes = new List<IAnalysisNode>();

            foreach (ObjectAnalysisNode objectNode in OriginalNodes)
            {
                if (objectNode.OwnerClass.OwnerNamespace.Identifier == namespaceNode.Identifier && classNodes.Exists(n => n.Identifier == objectNode.OwnerClass.Identifier) == false)
                {
                    classNodes.Add(objectNode.OwnerClass);
                }
            }

            return classNodes;
        }

        public override List<IAnalysisNode> GetAvailableObjectNodes()
        {
            List<IAnalysisNode> objectNodes = new List<IAnalysisNode>();

            foreach (ObjectAnalysisNode objectNode in OriginalNodes)
            {
                objectNodes.Add(objectNode);
            }

            return objectNodes;
        }

        public override List<IAnalysisNode> GetAvailableObjectNodes(ClassAnalysisNode classNode)
        {
            List<IAnalysisNode> objectNodes = new List<IAnalysisNode>();

            foreach (ObjectAnalysisNode objectNode in OriginalNodes)
            {
                if (objectNode.OwnerClass.Identifier == classNode.Identifier && objectNodes.Exists(n => n.Identifier == objectNode.Identifier) == false)
                {
                    objectNodes.Add(objectNode);
                }
            }

            return objectNodes;
        }
    }
}
