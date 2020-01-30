using ReframeAnalyzer.Graph;
using ReframeCore;
using ReframeCore.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeAnalyzer
{
    public class Analyzer
    {
        #region Constructors

        public Analyzer()
        {

        }

        #endregion

        #region Methods

        public IEnumerable<IAnalysisNode> GetOrphanNodes(IAnalysisGraph analysisGraph)
        {
            return GetOrphanNodes(analysisGraph.Nodes);
        }

        public IEnumerable<IAnalysisNode> GetOrphanNodes(IEnumerable<IAnalysisNode> nodes)
        {
            return nodes.Where(n => n.Degree == 0);
        }

        public IEnumerable<IAnalysisNode> GetLeafNodes(IAnalysisGraph analysisGraph)
        {
            return GetLeafNodes(analysisGraph.Nodes);
        }

        public IEnumerable<IAnalysisNode> GetLeafNodes(IEnumerable<IAnalysisNode> nodes)
        {
            return nodes.Where(
                n => (n.InDegree == 0 || n.OutDegree == 0) == true
                && (n.InDegree == 0 && n.OutDegree == 0) == false
                );
        }

        public IEnumerable<IAnalysisNode> GetSourceNodes(IAnalysisGraph analysisGraph)
        {
            return GetSourceNodes(analysisGraph.Nodes);
        }

        public IEnumerable<IAnalysisNode> GetSourceNodes(IEnumerable<IAnalysisNode> nodes)
        {
            return nodes.Where(n => n.InDegree == 0 && n.OutDegree > 0);
        }

        public IEnumerable<IAnalysisNode> GetSinkNodes(IAnalysisGraph analysisGraph)
        {
            return GetSinkNodes(analysisGraph.Nodes);
        }

        public IEnumerable<IAnalysisNode> GetSinkNodes(IEnumerable<IAnalysisNode> nodes)
        {
            return nodes.Where(n => n.InDegree > 0 && n.OutDegree == 0);
        }

        public IEnumerable<IAnalysisNode> GetIntermediaryNodes(IAnalysisGraph analysisGraph)
        {
            return GetIntermediaryNodes(analysisGraph.Nodes);
        }

        public IEnumerable<IAnalysisNode> GetIntermediaryNodes(IEnumerable<IAnalysisNode> nodes)
        {
            return nodes.Where(n => n.InDegree > 0 && n.OutDegree > 0);
        }

        public IEnumerable<IAnalysisNode> GetPredecessors(IAnalysisGraph analysisGraph, string nodeIdentifier)
        {
            uint id = uint.Parse(nodeIdentifier);
            IAnalysisNode startingNode = analysisGraph.Nodes.FirstOrDefault(n => n.Identifier == id);

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

        public IEnumerable<IAnalysisNode> GetPredecessors(IAnalysisGraph analysisGraph, string nodeIdentifier, int maxDepth)
        {
            uint id = uint.Parse(nodeIdentifier);
            IAnalysisNode initialNode = analysisGraph.Nodes.FirstOrDefault(n => n.Identifier == id);

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

        public IEnumerable<IAnalysisNode> GetSuccessors(IAnalysisGraph analysisGraph, string nodeIdentifier)
        {
            uint id = uint.Parse(nodeIdentifier);
            IAnalysisNode startingNode = analysisGraph.Nodes.FirstOrDefault(n => n.Identifier == id);

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

        public IEnumerable<IAnalysisNode> GetSuccessors(IAnalysisGraph analysisGraph, string nodeIdentifier, int maxDepth)
        {
            uint id = uint.Parse(nodeIdentifier);
            IAnalysisNode initialNode = analysisGraph.Nodes.FirstOrDefault(n => n.Identifier == id);

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

        public IEnumerable<IAnalysisNode> GetNeighbours(IAnalysisGraph analysisGraph, string nodeIdentifier)
        {
            IEnumerable<IAnalysisNode> predecessors = GetPredecessors(analysisGraph, nodeIdentifier);
            IEnumerable<IAnalysisNode> successors = GetSuccessors(analysisGraph, nodeIdentifier);

            List<IAnalysisNode> neighbours = new List<IAnalysisNode>();
            foreach (var p in predecessors)
            {
                if (neighbours.Contains(p) == false)
                {
                    neighbours.Add(p);
                }
            }

            foreach (var s in successors)
            {
                if (neighbours.Contains(s) == false)
                {
                    neighbours.Add(s);
                }
            }

            return neighbours;
        }

        public IEnumerable<IAnalysisNode> GetNeighbours(IAnalysisGraph analysisGraph, string nodeIdentifier, int maxDepth)
        {
            IEnumerable<IAnalysisNode> predecessors = GetPredecessors(analysisGraph, nodeIdentifier, maxDepth);
            IEnumerable<IAnalysisNode> successors = GetSuccessors(analysisGraph, nodeIdentifier, maxDepth);

            List<IAnalysisNode> neighbours = new List<IAnalysisNode>();
            foreach (var p in predecessors)
            {
                if (neighbours.Contains(p) == false)
                {
                    neighbours.Add(p);
                }
            }

            foreach (var s in successors)
            {
                if (neighbours.Contains(s) == false)
                {
                    neighbours.Add(s);
                }
            }

            return neighbours;
        }

        public IEnumerable<IAnalysisNode> GetSourceNodes(IAnalysisGraph analysisGraph, string nodeIdentifier)
        {
            var predecessorNodes = GetPredecessors(analysisGraph, nodeIdentifier);
            return GetSourceNodes(predecessorNodes);
        }

        public IEnumerable<IAnalysisNode> GetSinkNodes(IAnalysisGraph analysisGraph, string nodeIdentifier)
        {
            var successorNodes = GetSuccessors(analysisGraph, nodeIdentifier);
            return GetSinkNodes(successorNodes);
        }

        public IEnumerable<IAnalysisNode> GetLeafNodes(IAnalysisGraph analysisGraph, string nodeIdentifier)
        {
            var neighbourNodes = GetNeighbours(analysisGraph, nodeIdentifier);
            return GetLeafNodes(neighbourNodes);
        }

        public IEnumerable<IAnalysisNode> GetIntermediaryPredecessors(IAnalysisGraph analysisGraph, string nodeIdentifier)
        {
            var predecessorNodes = GetPredecessors(analysisGraph, nodeIdentifier);
            return GetIntermediaryNodes(predecessorNodes);
        }

        public IEnumerable<IAnalysisNode> GetIntermediarySuccessors(IAnalysisGraph analysisGraph, string nodeIdentifier)
        {
            var successorNodes = GetSuccessors(analysisGraph, nodeIdentifier);
            return GetIntermediaryNodes(successorNodes);
        }

        public IEnumerable<IAnalysisNode> GetIntermediaryNodes(IAnalysisGraph analysisGraph, string nodeIdentifier)
        {
            var neighbourNodes = GetNeighbours(analysisGraph, nodeIdentifier);
            return GetIntermediaryNodes(neighbourNodes);
        }

        #endregion
    }
}
