using ReframeAnalyzer.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeAnalyzer.Filters
{
    public class AssemblyFilterOption : FilterOption
    {
        public AssemblyFilterOption(List<IAnalysisNode> allNodes, AnalysisLevel level) : base(allNodes, level)
        {

        }

        public AssemblyFilterOption(List<IAnalysisNode> originalNodes) : base(originalNodes)
        {
            Level = AnalysisLevel.AssemblyLevel;
        }

        protected override List<IAnalysisNode> GetAllNodes(List<IAnalysisNode> originalNodes)
        {
            List<IAnalysisNode> assemblyNodes = new List<IAnalysisNode>();

            foreach (var assemblyNode in originalNodes)
            {
                assemblyNodes.Add(assemblyNode);
            }

            return assemblyNodes;
        }
    }
}
