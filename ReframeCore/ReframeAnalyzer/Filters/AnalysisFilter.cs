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
        protected IEnumerable<IAnalysisNode> OriginalNodes { get; set; }

        public AnalysisFilter(IEnumerable<IAnalysisNode> originalNodes)
        {
            OriginalNodes = originalNodes;
        }

        protected void AddNode(List<IAnalysisNode> chosenNodes, IAnalysisNode node)
        {
            if (node!=null && chosenNodes.Contains(node) == false)
            {
                chosenNodes.Add(node);
            }
        }

        protected void RemoveNode(List<IAnalysisNode> chosenNodes, IAnalysisNode node)
        {
            if (node != null && chosenNodes.Contains(node) == true)
            {
                chosenNodes.Remove(node);
            }
        }

        public abstract IEnumerable<IAnalysisNode> Apply();
    }
}
