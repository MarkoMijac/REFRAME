using ReframeAnalyzer.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeAnalyzer.Filters
{
    interface IFilterOption
    {
        List<IAnalysisNode> GetAvailableNodes();
        void SelectAllNodes();
        void DeselectAllNodes();
        void SelectNode(IAnalysisNode node);
        void DeselectNode(IAnalysisNode node);
        bool IsSelected(IAnalysisNode node);
    }
}
