﻿using ReframeCore;
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
        protected string Source { get; set; }

        public AnalysisGraph()
        {

        }

        public AnalysisGraph(string source)
        {
            Source = source;
        }

        public string Identifier { get; protected set; }

        private List<IAnalysisNode> _nodes = new List<IAnalysisNode>();

        public List<IAnalysisNode> Nodes => _nodes;

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
