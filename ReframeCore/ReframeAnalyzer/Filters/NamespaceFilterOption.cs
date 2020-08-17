using ReframeAnalyzer.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeAnalyzer.Filters
{
    public class NamespaceFilterOption : FilterOption
    {
        public NamespaceFilterOption(List<IAnalysisNode> originalNodes) : base(originalNodes)
        {
            Level = AnalysisLevel.NamespaceLevel;
        }

        public NamespaceFilterOption(List<IAnalysisNode> allNodes, AnalysisLevel level) : base(allNodes, level)
        {
            
        }
    }
}
