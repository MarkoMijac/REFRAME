using ReframeAnalyzer.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReframeAnalyzer.Filters
{
    public class AssemblyAnalysisFilter : AnalysisFilter
    {
        public IFilterOption AssemblyFilterOption { get; set; }

        public AssemblyAnalysisFilter(List<IAnalysisNode> originalNodes) : base(originalNodes)
        {
            Query = new Predicate<IAnalysisNode>(n => AssemblyFilterOption.IsSelected(n) == true);

            AssemblyFilterOption = new FilterOption(OriginalNodes);
            AssemblyFilterOption.SelectNodes();
        }
    }
}
