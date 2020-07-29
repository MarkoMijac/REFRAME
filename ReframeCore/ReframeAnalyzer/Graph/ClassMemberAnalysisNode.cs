using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ReframeAnalyzer.Graph
{
    public class ClassMemberAnalysisNode : AnalysisNode, IHasType
    {
       public string NodeType { get; set; }

        public ClassMemberAnalysisNode(uint identifier, AnalysisLevel level) : base(identifier, level)
        {

        }
    }
}
