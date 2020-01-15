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
        IReadOnlyList<IAnalysisNode> Nodes { get; }

        void AddNode(IAnalysisNode node);
        void RemoveNode(IAnalysisNode node);
        bool ContainsNode(uint identifier);
        IAnalysisNode GetNode(uint identifier);

        void InitializeGraph(IDependencyGraph graph);

        IReadOnlyList<IAnalysisNode> GetOrphanNodes();
        IReadOnlyList<IAnalysisNode> GetLeafNodes();
        IReadOnlyList<IAnalysisNode> GetSourceNodes();
        IReadOnlyList<IAnalysisNode> GetSinkNodes();
        IReadOnlyList<IAnalysisNode> GetIntermediaryNodes();
    }
}
