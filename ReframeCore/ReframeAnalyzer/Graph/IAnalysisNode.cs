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

        IReadOnlyList<IAnalysisNode> Predecessors { get; }
        IReadOnlyList<IAnalysisNode> Successors { get; }

        void AddPredecessor(IAnalysisNode predecessor);
        void RemovePredecessor(IAnalysisNode predecessor);

        void AddSuccesor(IAnalysisNode successor);
        void RemoveSuccessor(IAnalysisNode successor);
    }
}
