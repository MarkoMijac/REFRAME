using ReframeAnalyzer.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeAnalyzer.Filters
{
    public abstract class AnalysisFilter : IAnalysisFilter
    {
        protected Predicate<IAnalysisNode> Query { get; set; }

        protected List<IAnalysisNode> OriginalNodes { get; set; }

        public AnalysisFilter(List<IAnalysisNode> originalNodes)
        {
            OriginalNodes = originalNodes;
        }

        public virtual IEnumerable<IAnalysisNode> Apply()
        {
            List<IAnalysisNode> filteredNodes = new List<IAnalysisNode>();
            filteredNodes.AddRange(OriginalNodes.FindAll(Query));
            return filteredNodes;
        }
    }
}
