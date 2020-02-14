using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeAnalyzer.Graph
{
    public interface IUpdateGraph : IAnalysisGraph
    {
        bool UpdateSuccessful { get; }
        string UpdateStartedAt { get; }
        string UpdateEndedAt { get; }
        string UpdateDuration { get; }
        int TotalNodeCount { get; }
        uint FailedNodeIdentifier { get; }
        string FailedNodeName { get; }
        string FailedNodeOwner { get; }
        string SourceException { get; }
        string StackTrace { get; }
        string CauseMessage { get; }
        uint InitialNodeIdentifier { get; }
        string InitialNodeName { get; }
        string InitialNodeOwner { get; }
        string InitialNodeCurrentValue { get; }
        string InitialNodePreviousValue { get; }
    }
}
