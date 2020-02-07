using System.Collections.Generic;

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
