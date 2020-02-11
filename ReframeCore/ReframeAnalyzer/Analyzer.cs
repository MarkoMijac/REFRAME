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

        public List<IAnalysisNode> GetNodeAndItsPredecessors(IAnalysisGraph analysisGraph, uint nodeIdentifier)
        {
            ValidateAnalysisGraph(analysisGraph);
            ValidateNode(analysisGraph, nodeIdentifier);

            IAnalysisNode selected = analysisGraph.Nodes.FirstOrDefault(n => n.Identifier == nodeIdentifier);
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

        public List<IAnalysisNode> GetNodeAndItsPredecessors(IAnalysisGraph analysisGraph, uint nodeIdentifier, int maxDepth)
        {
            ValidateAnalysisGraph(analysisGraph);
            ValidateNode(analysisGraph, nodeIdentifier);
            ValidateMaxDepth(maxDepth);

            IAnalysisNode selectedNode = analysisGraph.Nodes.FirstOrDefault(n => n.Identifier == nodeIdentifier);
            selectedNode.Tag = "SelectedNode";

            Dictionary<IAnalysisNode, int> dict = new Dictionary<IAnalysisNode, int>();
            dict.Add(selectedNode, 0);

            for (int i = 1; i <= maxDepth; i++)
            {
                ProcessDepthLevel(dict, i);
            }

            return dict.Keys.ToList();
        }

        private void ValidateMaxDepth(int maxDepth)
        {
            if (maxDepth <= 0)
            {
                throw new AnalysisException("Invalid maximal depth level provided!");
            }
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

        public List<IAnalysisNode> GetNodeAndItsSuccessors(IAnalysisGraph analysisGraph, uint nodeIdentifier)
        {
            ValidateAnalysisGraph(analysisGraph);
            ValidateNode(analysisGraph, nodeIdentifier);

            IAnalysisNode selectedNode = analysisGraph.Nodes.FirstOrDefault(n => n.Identifier == nodeIdentifier);
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

        public List<IAnalysisNode> GetNodeAndItsSuccessors(IAnalysisGraph analysisGraph, uint nodeIdentifier, int maxDepth)
        {
            ValidateAnalysisGraph(analysisGraph);
            ValidateNode(analysisGraph, nodeIdentifier);
            ValidateMaxDepth(maxDepth);
            IAnalysisNode selectedNode = analysisGraph.Nodes.FirstOrDefault(n => n.Identifier == nodeIdentifier);
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

        public List<IAnalysisNode> GetNodeAndItsNeighbours(IAnalysisGraph analysisGraph, uint nodeIdentifier)
        {
            IEnumerable<IAnalysisNode> predecessors = GetNodeAndItsPredecessors(analysisGraph, nodeIdentifier);
            IEnumerable<IAnalysisNode> successors = GetNodeAndItsSuccessors(analysisGraph, nodeIdentifier);

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

        public List<IAnalysisNode> GetNodeAndItsNeighbours(IAnalysisGraph analysisGraph, uint nodeIdentifier, int maxDepth)
        {
            IEnumerable<IAnalysisNode> predecessors = GetNodeAndItsPredecessors(analysisGraph, nodeIdentifier, maxDepth);
            IEnumerable<IAnalysisNode> successors = GetNodeAndItsSuccessors(analysisGraph, nodeIdentifier, maxDepth);

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

        public List<IAnalysisNode> GetSourceNodes(IAnalysisGraph analysisGraph, uint nodeIdentifier)
        {
            var predecessorNodes = GetNodeAndItsPredecessors(analysisGraph, nodeIdentifier);
            var sourceNodes = GetSourceNodes(predecessorNodes);
            var selectedNode = sourceNodes.FirstOrDefault(n => n.Identifier == nodeIdentifier);
            sourceNodes.Remove(selectedNode);

            return sourceNodes;
        }

        public List<IAnalysisNode> GetSinkNodes(IAnalysisGraph analysisGraph, uint nodeIdentifier)
        {
            var successorNodes = GetNodeAndItsSuccessors(analysisGraph, nodeIdentifier);
            var sinkNodes = GetSinkNodes(successorNodes);
            var selectedNode = sinkNodes.FirstOrDefault(n => n.Identifier == nodeIdentifier);
            sinkNodes.Remove(selectedNode);
            return sinkNodes;
        }

        public List<IAnalysisNode> GetLeafNodes(IAnalysisGraph analysisGraph, uint nodeIdentifier)
        {
            var neighbourNodes = GetNodeAndItsNeighbours(analysisGraph, nodeIdentifier);
            var leafNodes = GetLeafNodes(neighbourNodes);
            var selectedNode = leafNodes.FirstOrDefault(n => n.Identifier == nodeIdentifier);
            leafNodes.Remove(selectedNode);
            return leafNodes;
        }

        public List<IAnalysisNode> GetIntermediaryPredecessors(IAnalysisGraph analysisGraph, uint nodeIdentifier)
        {
            var predecessorNodes = GetNodeAndItsPredecessors(analysisGraph, nodeIdentifier);
            var intermediaryNodes = GetIntermediaryNodes(predecessorNodes);
            var selectedNode = intermediaryNodes.FirstOrDefault(n => n.Identifier == nodeIdentifier);
            intermediaryNodes.Remove(selectedNode);

            return intermediaryNodes;
        }

        public List<IAnalysisNode> GetIntermediarySuccessors(IAnalysisGraph analysisGraph, uint nodeIdentifier)
        {
            var successorNodes = GetNodeAndItsSuccessors(analysisGraph, nodeIdentifier);
            var intermediaryNodes = GetIntermediaryNodes(successorNodes);
            var selectedNode = intermediaryNodes.FirstOrDefault(n => n.Identifier == nodeIdentifier);
            intermediaryNodes.Remove(selectedNode);
            return intermediaryNodes;
        }

        public List<IAnalysisNode> GetIntermediaryNodes(IAnalysisGraph analysisGraph, uint nodeIdentifier)
        {
            var neighbourNodes = GetNodeAndItsNeighbours(analysisGraph, nodeIdentifier);
            var intermediaryNodes = GetIntermediaryNodes(neighbourNodes);
            var selectedNode = intermediaryNodes.FirstOrDefault(n => n.Identifier == nodeIdentifier);
            intermediaryNodes.Remove(selectedNode);
            return intermediaryNodes;
        }

        #endregion
    }
}
