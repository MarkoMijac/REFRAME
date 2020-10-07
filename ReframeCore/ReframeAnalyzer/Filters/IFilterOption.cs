using ReframeAnalyzer.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeAnalyzer.Filters
{
    public interface IFilterOption
    {
        List<IAnalysisNode> GetNodes(Predicate<IAnalysisNode> condition = null);
        void SelectNodes(Predicate<IAnalysisNode> condition = null);
        void DeselectNodes(Predicate<IAnalysisNode> condition = null);
        void SelectNode(IAnalysisNode node);
        void DeselectNode(IAnalysisNode node);
        bool IsSelected(IAnalysisNode node);

        event EventHandler NodeSelected;
        event EventHandler NodeDeselected;
    }
}
