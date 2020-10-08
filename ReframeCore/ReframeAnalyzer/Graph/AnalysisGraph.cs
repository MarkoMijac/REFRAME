using ReframeAnalyzer.Exceptions;
using ReframeAnalyzer.Nodes;
using System.Collections.Generic;
using System.Linq;

namespace ReframeAnalyzer.Graph
{
    public class AnalysisGraph : IAnalysisGraph
    {
        public AnalysisGraph(string identifier, AnalysisLevel level)
        {
            Identifier = identifier;
            AnalysisLevel = level;
        }

        public string Identifier { get; protected set; }

        private List<IAnalysisNode> _nodes = new List<IAnalysisNode>();

        public List<IAnalysisNode> Nodes => _nodes;

        public AnalysisLevel AnalysisLevel { get; protected set; }

        public void AddNode(IAnalysisNode node)
        {
            if (node == null) throw new AnalysisException("Cannot add null node to graph!");
            if (node.Level != AnalysisLevel) throw new AnalysisException("Node has to be the same analysis level as the graph!");

            if (ContainsNode(node.Identifier) == false)
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
