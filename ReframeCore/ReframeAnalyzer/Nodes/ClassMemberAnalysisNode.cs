using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ReframeAnalyzer.Nodes
{
    public class ClassMemberAnalysisNode : AnalysisNode, IHasType
    {
        public string NodeType { get; set; }

        public ClassMemberAnalysisNode(uint identifier) : base(identifier, AnalysisLevel.ClassMemberLevel)
        {

        }
    }
}
