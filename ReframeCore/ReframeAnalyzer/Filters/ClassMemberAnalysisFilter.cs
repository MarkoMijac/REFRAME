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
        public ClassMemberAnalysisFilter(IEnumerable<IAnalysisNode> originalNodes) : base(originalNodes)
        {
            
        }

        public override IEnumerable<IAnalysisNode> Apply()
        {
            List<ClassMemberAnalysisNode> filteredNodes = new List<ClassMemberAnalysisNode>();

            foreach (ClassMemberAnalysisNode classMemberNode in OriginalNodes)
            {
                if (IsSelected(classMemberNode.OwnerClass.OwnerAssembly) && IsSelected(classMemberNode.OwnerClass.OwnerNamespace) && IsSelected(classMemberNode.OwnerClass))
                {
                    filteredNodes.Add(classMemberNode);
                }
            }

            return filteredNodes;
        }

        public override List<IAnalysisNode> GetAvailableAssemblyNodes()
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

        public override List<IAnalysisNode> GetAvailableNamespaceNodes()
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

        public override List<IAnalysisNode> GetAvailableClassNodes()
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

        public override List<IAnalysisNode> GetAvailableClassNodes(NamespaceAnalysisNode namespaceNode)
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
    }
}
