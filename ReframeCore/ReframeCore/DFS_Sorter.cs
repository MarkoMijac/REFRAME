using ReframeCore.Exceptions;
using ReframeCore.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore
{
    public class DFS_Sorter : ISorter
    {
        public IList<INode> Sort(IEnumerable<INode> graphNodes)
        {
            var sorted = new List<INode>();
            var visited = new Dictionary<INode, bool>();

            if (graphNodes != null)
            {
                foreach (var currentNode in graphNodes)
                {
                    Visit(currentNode, visited, sorted);
                }
            }

            sorted.Reverse();
            return sorted;
        }

        public IList<INode> Sort(IEnumerable<INode> graphNodes, INode initialNode)
        {
            var sorted = new List<INode>();
            var visited = new Dictionary<INode, bool>();

            if (graphNodes != null && graphNodes.Contains(initialNode))
            {
                Visit(initialNode, visited, sorted);
            }

            sorted.Reverse();
            return sorted;
        }

        private void Visit(INode currentNode, Dictionary<INode, bool> visitedNodes, IList<INode> sortedNodes)
        {
            bool inProcess;
            var alreadyVisited = visitedNodes.TryGetValue(currentNode, out inProcess);

            if (alreadyVisited == true)
            {
                if (inProcess == true)
                {
                    throw new CyclicReactiveDependencyException();
                }
            }
            else
            {
                visitedNodes[currentNode] = true;

                var successors = currentNode.Successors;
                foreach (var successor in successors)
                {
                    Visit(successor, visitedNodes, sortedNodes);
                }

                visitedNodes[currentNode] = false;
                sortedNodes.Add(currentNode);
            }
        }
    }
}
