using ReframeCore.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore
{
    /// <summary>
    /// Algorithm for topological sorting of dependency graph.
    /// </summary>
    public class TopologicalSort : ISort
    {
        /// <summary>
        /// Executes depth-first search algorithm on provided graph.
        /// </summary>
        /// <typeparam name="T">Type of nodes in graph.</typeparam>
        /// <param name="graph">Initial, unsorted directed-acyclic graph.</param>
        /// <param name="currentNode">Node that is currently processed.</param>
        /// <param name="visited">A dictionary maintaining visited nodes.</param>
        /// <param name="getDependents">Function which gets dependent nodes.</param>
        /// <param name="sorted">List representing topologically sorted graph.</param>
        private void DFS(INode currentNode, Dictionary<INode, bool> visited, Func<INode, IEnumerable<INode>> getDependents, List<INode> sorted)
        {
            visited[currentNode] = true;
            var dependents = getDependents(currentNode);
            foreach (var dependent in dependents)
            {
                bool f;
                if (visited.TryGetValue(dependent, out f) == false)
                {
                    DFS(dependent, visited, getDependents, sorted);
                }
            }
            sorted.Add(currentNode);
        }

        /// <summary>
        /// Performs topological sorting on entire graph using DFS algorithm.
        /// </summary>
        /// <typeparam name="T">Type of nodes in graph.</typeparam>
        /// <param name="graph">Initial, unsorted directed-acyclic graph.</param>
        /// <param name="getDependents">Function which gets dependent nodes.</param>
        /// <returns>List representing topologically sorted graph.</returns>
        public IList<INode> Sort(IEnumerable<INode> graph, Func<INode, IEnumerable<INode>> getDependents)
        {
            var sorted = new List<INode>();
            Dictionary<INode, bool> visited = new Dictionary<INode, bool>();

            foreach (var currentNode in graph)
            {
                bool f = false;
                if (visited.TryGetValue(currentNode, out f) == false || f == false)
                {
                    DFS(currentNode, visited, getDependents, sorted);
                }
            }

            sorted.Reverse();
            return sorted;
        }

        /// <summary>
        /// Performs topological sorting on part of graph starting from specified initial node.
        /// In this way all nodes that directly or indirectly depend on initial node are found
        /// and topologically sorted.
        /// </summary>
        /// <typeparam name="T">Type of nodes in graph.</typeparam>
        /// <param name="graph">Initial, unsorted directed-acyclic graph.</param>
        /// <param name="getDependents">Function which gets dependent nodes.</param>
        /// <param name="initialNode">Initial node which is used as a root for topologically sorted graph.</param>
        /// <returns>List representing topologically sorted part of graph containing all nodes
        /// that directly or indirectly depend on initial node.</returns>
        public IList<INode> Sort(IEnumerable<INode> graph, Func<INode, IEnumerable<INode>> getDependents, INode initialNode, bool omitInitialNode)
        {
            var sorted = new List<INode>();
            Dictionary<INode, bool> visited = new Dictionary<INode, bool>();

            DFS(initialNode, visited, getDependents, sorted);

            if (omitInitialNode == true)
            {
                sorted.Remove(initialNode);
            }

            sorted.Reverse();
            return sorted;
        }
    }
}
