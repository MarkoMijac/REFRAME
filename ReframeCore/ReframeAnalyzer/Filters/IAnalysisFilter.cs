using ReframeAnalyzer.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeAnalyzer.Filters
{
    public interface IAnalysisFilter
    {
        IEnumerable<IAnalysisNode> Apply();
        bool IsSelected(IAnalysisNode node);
        void SelectNode(IAnalysisNode node);
        void DeselectNode(IAnalysisNode node);
    }
}
