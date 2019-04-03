using ReframeCore.Exceptions;
using ReframeCore.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore
{
    public class TopologicalSort2 : ISort
    {
        public IList<INode> Sort(IEnumerable<INode> graph, Func<INode, IEnumerable<INode>> getDependents)
        {
            var sorted = new List<INode>();
            var visited = new Dictionary<INode, bool>();

            foreach (var currentNode in graph)
            {
                Visit(currentNode, visited, getDependents, sorted);
            }

            sorted.Reverse();
            return sorted;
        }

        public IList<INode> Sort(IEnumerable<INode> sourceGraph, Func<INode, IEnumerable<INode>> getDependents, INode initialNode, bool omitInitialNode)
        {
            var sorted = new List<INode>();
            var visited = new Dictionary<INode, bool>();

            Visit(initialNode, visited, getDependents, sorted);

            if (omitInitialNode == true)
            {
                sorted.Remove(initialNode);
            }

            sorted.Reverse();
            return sorted;
        }

        private void Visit(INode currentNode, Dictionary<INode, bool> visited, Func<INode, IEnumerable<INode>> getDependents, List<INode> sorted)
        {
            bool inProcess;
            var alreadyVisited = visited.TryGetValue(currentNode, out inProcess);

            if (alreadyVisited == true)
            {
                if (inProcess == true)
                {
                    throw new CyclicReactiveDependencyException();
                }
            }
            else
            {
                visited[currentNode] = true;

                var dependents = getDependents(currentNode);
                foreach (var dependent in dependents)
                {
                    Visit(dependent, visited, getDependents, sorted); 
                }

                visited[currentNode] = false;
                sorted.Add(currentNode);
            }
        }
    }
}
