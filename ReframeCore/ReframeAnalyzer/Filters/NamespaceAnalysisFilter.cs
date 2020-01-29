using ReframeAnalyzer.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeAnalyzer.Filters
{
    public class NamespaceAnalysisFilter : AnalysisFilter
    {
        public NamespaceAnalysisFilter(IEnumerable<IAnalysisNode> originalNodes) : base(originalNodes)
        {

        }

        public override IEnumerable<IAnalysisNode> Apply()
        {
            List<NamespaceAnalysisNode> filteredNodes = new List<NamespaceAnalysisNode>();

            foreach (NamespaceAnalysisNode namespaceNode in OriginalNodes)
            {
                if (IsSelected(namespaceNode))
                {
                    filteredNodes.Add(namespaceNode);
                }
            }

            return filteredNodes;
        }

        public override List<IAnalysisNode> GetAvailableNamespaceNodes()
        {
            List<IAnalysisNode> namespaceNodes = new List<IAnalysisNode>();

            foreach (NamespaceAnalysisNode namespaceNode in OriginalNodes)
            {
                namespaceNodes.Add(namespaceNode);
            }

            return namespaceNodes;
        }
    }
}
