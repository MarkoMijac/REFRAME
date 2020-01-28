using ReframeAnalyzer.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeAnalyzer
{
    public interface IAnalysisFilter
    {
        IEnumerable<IAnalysisNode> Apply();
    }
}
