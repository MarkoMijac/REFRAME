using ReframeAnalyzer.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeAnalyzer.Nodes
{
    public abstract class AnalysisNode : IAnalysisNode
    {
        public uint Identifier { get; set; }

        public string Name { get; set; }

        public int Degree { get => InDegree + OutDegree; }

        public int InDegree { get => Predecessors.Count; }

        public int OutDegree { get => Successors.Count; }

        public IAnalysisNode Parent { get; set; }
        public IAnalysisNode Parent2 { get; set; }

        public AnalysisLevel Level { get; protected set; }

        private List<IAnalysisNode> _predecessors = new List<IAnalysisNode>();

        public IReadOnlyList<IAnalysisNode> Predecessors
        {
            get
            {
                return _predecessors.AsReadOnly();
            }
        }

        private List<IAnalysisNode> _successors = new List<IAnalysisNode>();

        public IReadOnlyList<IAnalysisNode> Successors
        {
            get
            {
                return _successors.AsReadOnly();
            }
        }

        public AnalysisNode(uint identifier, AnalysisLevel level)
        {
            Identifier = identifier;
            Level = level;
        }

        public void AddPredecessor(IAnalysisNode predecessor)
        {
            ValidateDependencies(predecessor);

            if (_predecessors.Contains(predecessor) == false)
            {
                _predecessors.Add(predecessor);
                predecessor.AddSuccesor(this);
            }
        }

        private void ValidateDependencies(IAnalysisNode associatedNode)
        {
            if (associatedNode == null) throw new AnalysisException("Associated node cannot be null!");
            if (associatedNode.Identifier == Identifier) throw new AnalysisException("Node cannot be associated to itself!");
            if (associatedNode.Level != Level) throw new AnalysisException("Nodes need to have the same analysis level!");
        }

        public void AddSuccesor(IAnalysisNode successor)
        {
            ValidateDependencies(successor);

            if (_successors.Contains(successor) == false)
            {
                _successors.Add(successor);
                successor.AddPredecessor(this);
            }
        }

        public void RemovePredecessor(IAnalysisNode predecessor)
        {
            if (predecessor != null)
            {
                if (_predecessors.Contains(predecessor) == true)
                {
                    _predecessors.Remove(predecessor);
                }

                if (predecessor.Successors.Contains(this) == true)
                {
                    predecessor.RemoveSuccessor(this);
                }
            }
        }

        public void RemoveSuccessor(IAnalysisNode successor)
        {
            if (successor != null)
            {
                if (_successors.Contains(successor) == true)
                {
                    _successors.Remove(successor);
                }

                if (successor.Predecessors.Contains(this) == true)
                {
                    successor.RemovePredecessor(this);
                }
            }
        }

        public bool HasPredecessor(IAnalysisNode predecessor)
        {
            return _predecessors.Contains(predecessor);
        }

        public bool HasSuccessor(IAnalysisNode successor)
        {
            return _successors.Contains(successor);
        }

        public string Tag { get; set; }

        public string Source { get; set; }

        public override string ToString()
        {
            return $"[{Identifier}] {Name}";
        }
    }
}
