using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeAnalyzer.Graph
{
    public interface IUpdateNode : IAnalysisNode
    {
        string NodeType { get; }
        int UpdateOrder { get; }
        int UpdateLayer { get; }
        string UpdateStartedAt { get; }
        string UpdateCompletedAt { get; }
        string UpdateDuration { get; }
        string CurrentValue { get; }
        string PreviousValue { get; }
        bool IsInitialNode { get; }
    }
}
