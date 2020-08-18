using ReframeAnalyzer.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeAnalyzer.Filters
{
    public class ClassMemberFilterOption : FilterOption
    {
        public ClassMemberFilterOption(List<IAnalysisNode> originalNodes) : base(originalNodes)
        {
            Level = AnalysisLevel.ClassMemberLevel;
        }

        public ClassMemberFilterOption(List<IAnalysisNode> allNodes, AnalysisLevel level) : base(allNodes, level)
        {
        }
    }
}
