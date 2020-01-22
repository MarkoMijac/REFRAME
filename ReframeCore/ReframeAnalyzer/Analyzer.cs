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
            return analysisGraph.Nodes.Where(n => n.Degree == 0);
        }

        public IEnumerable<IAnalysisNode> GetLeafNodes(IAnalysisGraph analysisGraph)
        {
            return analysisGraph.Nodes.Where(
                n => (n.InDegree == 0 || n.OutDegree == 0) == true
                && (n.InDegree == 0 && n.OutDegree == 0) == false
                );
        }

        public IEnumerable<IAnalysisNode> GetSourceNodes(IAnalysisGraph analysisGraph)
        {
            return analysisGraph.Nodes.Where(n => n.InDegree == 0 && n.OutDegree > 0);
        }

        public IEnumerable<IAnalysisNode> GetSinkNodes(IAnalysisGraph analysisGraph)
        {
            return analysisGraph.Nodes.Where(n => n.InDegree > 0 && n.OutDegree == 0);
        }

        public IEnumerable<IAnalysisNode> GetIntermediaryNodes(IAnalysisGraph analysisGraph)
        {
            return analysisGraph.Nodes.Where(n => n.InDegree > 0 && n.OutDegree > 0);
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
            neighbours.AddRange(predecessors);
            neighbours.AddRange(successors);

            return neighbours;
        }

        #endregion
    }
}
