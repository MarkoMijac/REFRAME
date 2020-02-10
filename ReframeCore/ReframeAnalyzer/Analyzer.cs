using ReframeAnalyzer.Exceptions;
using ReframeAnalyzer.Graph;
using System.Collections.Generic;
using System.Linq;
using System;

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

        public List<IAnalysisNode> GetOrphanNodes(IAnalysisGraph analysisGraph)
        {
            if (analysisGraph == null)
            {
                throw new AnalysisException("Analysis graph is null!");
            }

            return GetOrphanNodes(analysisGraph.Nodes);
        }

        public List<IAnalysisNode> GetOrphanNodes(IEnumerable<IAnalysisNode> nodes)
        {
            if (nodes == null)
            {
                throw new AnalysisException("Analysis graph is null!");
            }

            return nodes.Where(n => n.Degree == 0).ToList();
        }

        public List<IAnalysisNode> GetLeafNodes(IAnalysisGraph analysisGraph)
        {
            if (analysisGraph == null)
            {
                throw new AnalysisException("Analysis graph is null!");
            }

            return GetLeafNodes(analysisGraph.Nodes);
        }

        public List<IAnalysisNode> GetLeafNodes(IEnumerable<IAnalysisNode> nodes)
        {
            if (nodes == null)
            {
                throw new AnalysisException("Analysis graph is null!");
            }

            return nodes.Where(
                n => (n.InDegree == 0 || n.OutDegree == 0) == true
                && (n.InDegree == 0 && n.OutDegree == 0) == false
                ).ToList();
        }

        public List<IAnalysisNode> GetSourceNodes(IAnalysisGraph analysisGraph)
        {
            if (analysisGraph == null)
            {
                throw new AnalysisException("Analysis graph is null!");
            }

            return GetSourceNodes(analysisGraph.Nodes);
        }

        public List<IAnalysisNode> GetSourceNodes(IEnumerable<IAnalysisNode> nodes)
        {
            if (nodes == null)
            {
                throw new AnalysisException("Analysis graph is null!");
            }

            return nodes.Where(n => n.InDegree == 0 && n.OutDegree > 0).ToList();
        }

        public List<IAnalysisNode> GetSinkNodes(IAnalysisGraph analysisGraph)
        {
            if (analysisGraph == null)
            {
                throw new AnalysisException("Analysis graph is null!");
            }

            return GetSinkNodes(analysisGraph.Nodes);
        }

        public List<IAnalysisNode> GetSinkNodes(IEnumerable<IAnalysisNode> nodes)
        {
            if (nodes == null)
            {
                throw new AnalysisException("Analysis graph is null!");
            }

            return nodes.Where(n => n.InDegree > 0 && n.OutDegree == 0).ToList();
        }

        public List<IAnalysisNode> GetIntermediaryNodes(IAnalysisGraph analysisGraph)
        {
            if (analysisGraph == null)
            {
                throw new AnalysisException("Analysis graph is null!");
            }

            return GetIntermediaryNodes(analysisGraph.Nodes);
        }

        public List<IAnalysisNode> GetIntermediaryNodes(IEnumerable<IAnalysisNode> nodes)
        {
            if (nodes == null)
            {
                throw new AnalysisException("Analysis graph is null!");
            }

            return nodes.Where(n => n.InDegree > 0 && n.OutDegree > 0).ToList();
        }

        private void ValidateNodeIdentifier(string nodeIdentifier)
        {
            if (nodeIdentifier == "")
            {
                throw new AnalysisException("Empty node identifier!");
            }

            int id;
            if (int.TryParse(nodeIdentifier, out id) == false)
            {
                throw new AnalysisException("Invalid node identifier!");
            }
        }

        public List<IAnalysisNode> GetPredecessors(IAnalysisGraph analysisGraph, string nodeIdentifier)
        {
            ValidateAnalysisGraph(analysisGraph);
            ValidateNodeIdentifier(nodeIdentifier);
            uint id = uint.Parse(nodeIdentifier);
            ValidateNode(analysisGraph, id);

            IAnalysisNode selected = analysisGraph.Nodes.FirstOrDefault(n => n.Identifier == id);
            selected.Tag = "SelectedNode";

            var predecessors = new List<IAnalysisNode>();

            if (selected != null)
            {
                TraversePredecessors(selected, predecessors);
            }

            return predecessors;
        }

        private void ValidateAnalysisGraph(IAnalysisGraph analysisGraph)
        {
            if (analysisGraph == null)
            {
                throw new AnalysisException("Analysis graph is null!");
            }
        }

        private void ValidateNode(IAnalysisGraph analysisGraph, uint nodeIdentifier)
        {
            var node = analysisGraph.Nodes.FirstOrDefault(n => n.Identifier == nodeIdentifier);
            if (node == null)
            {
                throw new AnalysisException($"Node with identifier {nodeIdentifier} does not exist!");
            }
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

        public List<IAnalysisNode> GetPredecessors(IAnalysisGraph analysisGraph, string nodeIdentifier, int maxDepth)
        {
            ValidateNodeIdentifier(nodeIdentifier);
            uint id = uint.Parse(nodeIdentifier);
            ValidateNode(analysisGraph, id);

            IAnalysisNode selectedNode = analysisGraph.Nodes.FirstOrDefault(n => n.Identifier == id);
            selectedNode.Tag = "SelectedNode";

            Dictionary<IAnalysisNode, int> dict = new Dictionary<IAnalysisNode, int>();
            dict.Add(selectedNode, 0);

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
            IAnalysisNode selectedNode = analysisGraph.Nodes.FirstOrDefault(n => n.Identifier == id);
            selectedNode.Tag = "SelectedNode";

            var successors = new List<IAnalysisNode>();

            if (selectedNode != null)
            {
                TraverseSuccessors(selectedNode, successors);
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
            IAnalysisNode selectedNode = analysisGraph.Nodes.FirstOrDefault(n => n.Identifier == id);
            selectedNode.Tag = "SelectedNode";

            Dictionary<IAnalysisNode, int> dict = new Dictionary<IAnalysisNode, int>();
            dict.Add(selectedNode, 0);

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
