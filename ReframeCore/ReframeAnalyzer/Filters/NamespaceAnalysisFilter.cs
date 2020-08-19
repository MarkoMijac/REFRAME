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
        public IFilterOption AssemblyFilterOption { get; set; }
        public IFilterOption NamespaceFilterOption { get; set; }

        public NamespaceAnalysisFilter(List<IAnalysisNode> originalNodes) : base(originalNodes)
        {
            Query = new Predicate<IAnalysisNode>(n => NamespaceFilterOption.IsSelected(n));

            AssemblyFilterOption = new FilterOption(OriginalNodes, AnalysisLevel.AssemblyLevel);
            AssemblyFilterOption.SelectNodes();

            NamespaceFilterOption = new FilterOption(OriginalNodes, AnalysisLevel.NamespaceLevel);
            NamespaceFilterOption.SelectNodes();
        }

        protected override void InitializeOptions()
        {
            
        }
    }
}
