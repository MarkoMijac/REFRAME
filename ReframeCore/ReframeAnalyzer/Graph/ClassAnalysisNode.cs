using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ReframeAnalyzer.Graph
{
    public class ClassAnalysisNode : AnalysisNode
    {
        public string FullName { get; set; }

        public ClassAnalysisNode(uint identifier, AnalysisLevel level) : base(identifier, level)
        {

        }
    }
}
