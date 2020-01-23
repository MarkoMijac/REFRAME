using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeAnalyzer.Graph
{
    public class ObjectMemberAnalysisNode : AnalysisNode
    {
        public string NodeType { get; set; }
        public string OwnerObjectIdentifier { get; set; }

    }
}
