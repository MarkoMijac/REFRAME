using ReframeCore;
using ReframeCore.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeAnalyzer.Graph
{
    public interface IAnalysisGraph
    {
        string Identifier { get; }
        string Source { get; }
        List<IAnalysisNode> Nodes { get; }
        AnalysisLevel AnalysisLevel { get; }

        void AddNode(IAnalysisNode node);
        void RemoveNode(IAnalysisNode node);
        bool ContainsNode(uint identifier);
        IAnalysisNode GetNode(uint identifier);
    }
}
