using ReframeAnalyzer.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeAnalyzer.Filters
{
    public class ClassFilterOption : FilterOption
    {
        public ClassFilterOption(List<IAnalysisNode> originalNodes) : base(originalNodes)
        {
            Level = AnalysisLevel.ClassLevel;
        }

        public ClassFilterOption(List<IAnalysisNode> allNodes, AnalysisLevel level) : base(allNodes, level)
        {
        }
    }
}
