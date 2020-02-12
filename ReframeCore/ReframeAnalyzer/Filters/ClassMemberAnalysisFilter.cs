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
                if (IsSelected(classMemberNode.Parent.Parent2) && IsSelected(classMemberNode.Parent.Parent) && IsSelected(classMemberNode.Parent))
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
                if (assemblyNodes.Exists(n => n.Identifier == classMemberNode.Parent.Parent2.Identifier) == false)
                {
                    assemblyNodes.Add(classMemberNode.Parent.Parent2);
                }
            }

            return assemblyNodes;
        }

        public override List<IAnalysisNode> GetAvailableNamespaceNodes()
        {
            List<IAnalysisNode> namespaceNodes = new List<IAnalysisNode>();

            foreach (ClassMemberAnalysisNode classMemberNode in OriginalNodes)
            {
                if (namespaceNodes.Exists(n => n.Identifier == classMemberNode.Parent.Parent.Identifier) == false)
                {
                    namespaceNodes.Add(classMemberNode.Parent.Parent);
                }
            }

            return namespaceNodes;
        }

        public override List<IAnalysisNode> GetAvailableClassNodes()
        {
            List<IAnalysisNode> classNodes = new List<IAnalysisNode>();

            foreach (ClassMemberAnalysisNode classMemberNode in OriginalNodes)
            {
                if (classNodes.Exists(n => n.Identifier == classMemberNode.Parent.Identifier) == false)
                {
                    classNodes.Add(classMemberNode.Parent);
                }
            }

            return classNodes;
        }

        public override List<IAnalysisNode> GetAvailableClassNodes(IAnalysisNode namespaceNode)
        {
            List<IAnalysisNode> classNodes = new List<IAnalysisNode>();

            foreach (ClassMemberAnalysisNode classMemberNode in OriginalNodes)
            {
                if (classMemberNode.Parent.Parent.Identifier == namespaceNode.Identifier && classNodes.Exists(n => n.Identifier == classMemberNode.Parent.Identifier) == false)
                {
                    classNodes.Add(classMemberNode.Parent);
                }
            }

            return classNodes;
        }
    }
}
