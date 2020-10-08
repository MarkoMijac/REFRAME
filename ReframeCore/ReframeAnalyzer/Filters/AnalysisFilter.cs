using ReframeAnalyzer.Exceptions;
using ReframeAnalyzer.Nodes;
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
            if (originalNodes == null) throw new AnalysisException("Cannot query null list of nodes!");

            OriginalNodes = originalNodes;
        }

        public virtual List<IAnalysisNode> Apply()
        {
            if (OriginalNodes == null) throw new AnalysisException("Cannot query null list of nodes!");

            List<IAnalysisNode> filteredNodes = new List<IAnalysisNode>();
            filteredNodes.AddRange(OriginalNodes.FindAll(Query));
            return filteredNodes;
        }
    }
}
