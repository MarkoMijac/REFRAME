using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ReframeAnalyzer.Nodes
{
    public class UpdateAnalysisNode : AnalysisNode, IUpdateNode
    {
        public string NodeType { get; set; }
        public int UpdateOrder { get; set; }
        public int UpdateLayer { get; set; }
        public string UpdateStartedAt { get; set; }
        public string UpdateCompletedAt { get; set; }
        public string UpdateDuration { get; set; }
        public string CurrentValue { get; set; }
        public string PreviousValue { get; set; }
        public bool IsInitialNode { get; set; } = false;

        public UpdateAnalysisNode(uint identifier, AnalysisLevel level) : base(identifier, level)
        {

        }
    }
}
