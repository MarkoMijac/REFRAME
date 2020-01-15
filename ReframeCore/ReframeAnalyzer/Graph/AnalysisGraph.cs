using ReframeCore;
using ReframeCore.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeAnalyzer.Graph
{
    public abstract class AnalysisGraph : IAnalysisGraph
    {
        public IDependencyGraph DependencyGraph { get; protected set; }

        private List<IAnalysisNode> _nodes = new List<IAnalysisNode>();

        public IReadOnlyList<IAnalysisNode> Nodes => _nodes.AsReadOnly();

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

        public abstract void InitializeGraph(IDependencyGraph graph);

        public abstract void InitializeGraph(IDependencyGraph graph, IEnumerable<INode> nodes);

        public IReadOnlyList<IAnalysisNode> GetOrphanNodes()
        {
            return _nodes.FindAll(n => n.Degree == 0).AsReadOnly();
        }

        public IReadOnlyList<IAnalysisNode> GetLeafNodes()
        {
            return _nodes.FindAll(
                n => (n.InDegree == 0 || n.OutDegree == 0) == true 
                && (n.InDegree == 0 && n.OutDegree == 0) == false
                );
        }

        public IReadOnlyList<IAnalysisNode> GetSourceNodes()
        {
            return _nodes.FindAll(n => n.InDegree == 0 && n.OutDegree > 0).AsReadOnly();
        }

        public IReadOnlyList<IAnalysisNode> GetSinkNodes()
        {
            return _nodes.FindAll(n => n.InDegree > 0 && n.OutDegree == 0).AsReadOnly();
        }

        public IReadOnlyList<IAnalysisNode> GetIntermediaryNodes()
        {
            return _nodes.FindAll(n => n.InDegree > 0 && n.OutDegree > 0).AsReadOnly();
        }
    }
}
