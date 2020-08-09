using ReframeAnalyzer.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeAnalyzer.Filters
{
    public interface IFilterOption
    {
        AnalysisLevel Level { get; }
        void SelectNodes();
        void SelectNodes(Predicate<IAnalysisNode> condition);
        void DeselectNodes();
        void DeselectNodes(Predicate<IAnalysisNode> condition);
        void SelectNode(IAnalysisNode node);
        void DeselectNode(IAnalysisNode node);
        bool IsSelected(IAnalysisNode node);
    }
}
