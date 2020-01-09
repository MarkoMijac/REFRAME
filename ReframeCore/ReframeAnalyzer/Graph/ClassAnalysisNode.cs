using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeAnalyzer.Graph
{
    public class ClassAnalysisNode : AnalysisNode
    {
        public string FullName { get; set; }
        public string Assembly { get; set; }
        public string Namespace { get; set; }
    }
}
