using ReframeAnalyzer.Filters;
using ReframeAnalyzer.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeTools.GUI
{
    public interface IFilterForm
    {
        IEnumerable<IAnalysisNode> OriginalNodes { get; set; }

        AnalysisLevel Level { get; set; }

        IAnalysisFilter Filter { get; set; }
    }
}
