using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ReframeAnalyzer.Graph
{
    public class UpdateAnalysisGraph : AnalysisGraph, IUpdateGraph
    {
        public bool UpdateSuccessful { get; set; }
        public string UpdateStartedAt { get; set; }
        public string UpdateEndedAt { get; set; }
        public string UpdateDuration { get; set; }
        public int TotalNodeCount { get; set; }
        public uint FailedNodeIdentifier { get; set; }
        public string FailedNodeName { get; set; }
        public string FailedNodeOwner { get; set; }
        public string SourceException { get; set; }
        public string StackTrace { get; set; }
        public string CauseMessage { get; set; }
        public uint InitialNodeIdentifier { get; set; }
        public string InitialNodeName { get; set; }
        public string InitialNodeOwner { get; set; }
        public string InitialNodeCurrentValue { get; set; }
        public string InitialNodePreviousValue { get; set; }

        private IAnalysisGraph ObjectMemberGraph { get; set; }

        public UpdateAnalysisGraph(string identifier, AnalysisLevel level) : base(identifier, level)
        {

        }
    }
}
