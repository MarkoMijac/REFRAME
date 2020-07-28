using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeAnalyzer.Graph
{
    public class ObjectAnalysisGraph : AnalysisGraph
    {
        public ObjectAnalysisGraph(string identifier, AnalysisLevel level)
        {
            Identifier = identifier;
            AnalysisLevel = level;
        }
    }
}
