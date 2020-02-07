using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeAnalyzer.Graph
{
    public interface IAnalysisNode
    {
        uint Identifier { get;}
        string Name { get;}
        int Degree { get; }
        int InDegree { get; }
        int OutDegree { get; }

        IReadOnlyList<IAnalysisNode> Predecessors { get; }
        IReadOnlyList<IAnalysisNode> Successors { get; }

        void AddPredecessor(IAnalysisNode predecessor);
        void RemovePredecessor(IAnalysisNode predecessor);
        bool HasPredecessor(IAnalysisNode predecessor);

        void AddSuccesor(IAnalysisNode successor);
        void RemoveSuccessor(IAnalysisNode successor);
        bool HasSuccessor(IAnalysisNode successor);

        string Tag { get; set; }
    }
}
