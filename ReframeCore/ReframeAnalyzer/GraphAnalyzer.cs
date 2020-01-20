using ReframeAnalyzer.Graph;
using ReframeAnalyzer.Xml;
using ReframeCore;
using ReframeCore.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeAnalyzer
{
    public abstract class GraphAnalyzer
    {
        #region Constructors

        protected IDependencyGraph _dependencyGraph;

        public GraphAnalyzer()
        {

        }

        public GraphAnalyzer(IDependencyGraph graph)
        {
            _dependencyGraph = graph;
            CreateAnalysisGraph();
        }

        #endregion

        #region Methods

        protected IAnalysisGraph _analysisGraph;

        protected abstract void CreateAnalysisGraph();

        public abstract IAnalysisGraph CreateGraph(string source);

        public IAnalysisGraph GetAnalysisGraph()
        {
            return _analysisGraph;
        }

        public IEnumerable<IAnalysisNode> GetOrphanNodes()
        {
            return _analysisGraph.Nodes.Where(n => n.Degree == 0);
        }
           
        public IEnumerable<IAnalysisNode> GetLeafNodes()
        {
            return _analysisGraph.Nodes.Where(
                n => (n.InDegree == 0 || n.OutDegree == 0) == true
                && (n.InDegree == 0 && n.OutDegree == 0) == false
                );
        }

        public IEnumerable<IAnalysisNode> GetSourceNodes()
        {
            return _analysisGraph.Nodes.Where(n => n.InDegree == 0 && n.OutDegree > 0);
        }

        public IEnumerable<IAnalysisNode> GetSinkNodes()
        {
            return _analysisGraph.Nodes.Where(n => n.InDegree > 0 && n.OutDegree == 0);
        }

        public IEnumerable<IAnalysisNode> GetIntermediaryNodes()
        {
            return _analysisGraph.Nodes.Where(n => n.InDegree > 0 && n.OutDegree > 0);
        }

        public IEnumerable<IAnalysisNode> GetPredecessors(string nodeIdentifier)
        {
            uint id = uint.Parse(nodeIdentifier);
            IAnalysisNode startingNode = _analysisGraph.Nodes.FirstOrDefault(n => n.Identifier == id);

            var predecessors = new List<IAnalysisNode>();

            if (startingNode != null)
            {
                TraversePredecessors(startingNode, predecessors);
            }

            return predecessors;
        }

        private void TraversePredecessors(IAnalysisNode analysisNode, List<IAnalysisNode> list)
        {
            if (list.Contains(analysisNode) == false)
            {
                list.Add(analysisNode);

                foreach (var p in analysisNode.Predecessors)
                {
                    TraversePredecessors(p, list);
                }
            }
        }

        public IEnumerable<IAnalysisNode> GetPredecessors(string nodeIdentifier, int maxDepth)
        {
            uint id = uint.Parse(nodeIdentifier);
            IAnalysisNode initialNode = _analysisGraph.Nodes.FirstOrDefault(n => n.Identifier == id);

            Dictionary<IAnalysisNode, int> dict = new Dictionary<IAnalysisNode, int>();
            dict.Add(initialNode, 0);

            for (int i = 1; i <= maxDepth; i++)
            {
                ProcessDepthLevel(dict, i);
            }

            return dict.Keys.ToList();
        }

        private void ProcessDepthLevel(Dictionary<IAnalysisNode, int> dict, int currentDepth)
        {
            int prevDepth = currentDepth - 1;
            var itemsToProcess = dict.Where(p => p.Value == prevDepth).ToDictionary(i => i.Key, i => i.Value);

            foreach (var item in itemsToProcess)
            {
                IAnalysisNode node = item.Key;
                foreach (var p in node.Predecessors)
                {
                    if (dict.ContainsKey(p) == false)
                    {
                        dict.Add(p, currentDepth);
                    }
                }
            }
        }

        public IEnumerable<IAnalysisNode> GetSuccessors(string nodeIdentifier)
        {
            uint id = uint.Parse(nodeIdentifier);
            IAnalysisNode startingNode = _analysisGraph.Nodes.FirstOrDefault(n => n.Identifier == id);

            var successors = new List<IAnalysisNode>();

            if (startingNode != null)
            {
                TraverseSuccessors(startingNode, successors);
            }

            return successors;
        }

        private void TraverseSuccessors(IAnalysisNode analysisNode, List<IAnalysisNode> list)
        {
            if (list.Contains(analysisNode) == false)
            {
                list.Add(analysisNode);

                foreach (var p in analysisNode.Successors)
                {
                    TraverseSuccessors(p, list);
                }
            }
        }

        public IEnumerable<IAnalysisNode> GetSuccessors(string nodeIdentifier, int maxDepth)
        {
            uint id = uint.Parse(nodeIdentifier);
            IAnalysisNode initialNode = _analysisGraph.Nodes.FirstOrDefault(n => n.Identifier == id);

            Dictionary<IAnalysisNode, int> dict = new Dictionary<IAnalysisNode, int>();
            dict.Add(initialNode, 0);

            for (int i = 1; i <= maxDepth; i++)
            {
                ProcessSuccessorsAtDepth(dict, i);
            }

            return dict.Keys.ToList();
        }

        private void ProcessSuccessorsAtDepth(Dictionary<IAnalysisNode, int> dict, int currentDepth)
        {
            int prevDepth = currentDepth - 1;
            var itemsToProcess = dict.Where(p => p.Value == prevDepth).ToDictionary(i => i.Key, i => i.Value);

            foreach (var item in itemsToProcess)
            {
                IAnalysisNode node = item.Key;
                foreach (var p in node.Successors)
                {
                    if (dict.ContainsKey(p) == false)
                    {
                        dict.Add(p, currentDepth);
                    }
                }
            }
        }

        public IEnumerable<IAnalysisNode> GetNeighbours(string nodeIdentifier)
        {
            IEnumerable<IAnalysisNode> predecessors = GetPredecessors(nodeIdentifier);
            IEnumerable<IAnalysisNode> successors = GetSuccessors(nodeIdentifier);

            List<IAnalysisNode> neighbours = new List<IAnalysisNode>();
            neighbours.AddRange(predecessors);
            neighbours.AddRange(successors);

            return neighbours;
        }

        public IEnumerable<IAnalysisNode> GetNeighbours(string nodeIdentifier, int depth)
        {
            IEnumerable<IAnalysisNode> predecessors = GetPredecessors(nodeIdentifier, depth);
            IEnumerable<IAnalysisNode> successors = GetSuccessors(nodeIdentifier, depth);

            List<IAnalysisNode> neighbours = new List<IAnalysisNode>();
            neighbours.AddRange(predecessors);
            neighbours.AddRange(successors);

            return neighbours;
        }

        #endregion
    }
}
