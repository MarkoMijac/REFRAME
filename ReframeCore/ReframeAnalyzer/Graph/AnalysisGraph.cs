using System.Collections.Generic;
using System.Linq;

namespace ReframeAnalyzer.Graph
{
    public abstract class AnalysisGraph : IAnalysisGraph
    {
        public string Source { get; protected set; }

        public string Identifier { get; protected set; }

        private List<IAnalysisNode> _nodes = new List<IAnalysisNode>();

        public List<IAnalysisNode> Nodes => _nodes;

        public AnalysisLevel AnalysisLevel { get; protected set; }

        public void AddNode(IAnalysisNode node)
        {
            if (node != null && ContainsNode(node.Identifier) == false)
            {
                _nodes.Add(node);
            }
        }

        public bool ContainsNode(uint identifier)
        {
            return _nodes.Exists(n => n.Identifier == identifier);
        }

        public IAnalysisNode GetNode(uint identifier)
        {
            return _nodes.FirstOrDefault(n => n.Identifier == identifier);
        }

        public void RemoveNode(IAnalysisNode node)
        {
            if (node != null)
            {
                _nodes.Remove(node);
            }
        }
    }
}
