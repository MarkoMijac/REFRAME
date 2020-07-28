using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ReframeAnalyzer.Graph
{
    public class ClassMemberAnalysisGraph : AnalysisGraph
    {
        public ClassMemberAnalysisGraph(string identifier, AnalysisLevel level)
        {
            Identifier = identifier;
            AnalysisLevel = level;
        }
    }
}
