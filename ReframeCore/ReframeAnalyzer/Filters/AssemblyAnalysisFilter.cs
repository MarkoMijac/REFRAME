using ReframeAnalyzer.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeAnalyzer.Filters
{
    public class AssemblyAnalysisFilter : AnalysisFilter
    {
        public AssemblyAnalysisFilter(IEnumerable<IAnalysisNode> originalNodes) : base(originalNodes)
        {

        }

        public override List<IAnalysisNode> GetAvailableAssemblyNodes()
        {
            List<IAnalysisNode> assemblyNodes = new List<IAnalysisNode>();

            foreach (var assemblyNode in OriginalNodes)
            {
                assemblyNodes.Add(assemblyNode);
            }

            return assemblyNodes;
        }

        public override IEnumerable<IAnalysisNode> Apply()
        {
            List<IAnalysisNode> filteredNodes = new List<IAnalysisNode>();

            foreach (var assemblyNode in OriginalNodes)
            {
                if (IsSelected(assemblyNode))
                {
                    filteredNodes.Add(assemblyNode);
                }
            }

            return filteredNodes;
        }
    }
}
