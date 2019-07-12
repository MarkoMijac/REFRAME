using ReframeCore.Exceptions;
using ReframeCore.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore
{
    public static class GraphFactory
    {
        private static List<IDependencyGraph> _graphs = new List<IDependencyGraph>();

        /// <summary>
        /// Creates and registers new dependency graph with unique identifier.
        /// </summary>
        /// <param name="identifier">Graph's unique identifier.</param>
        /// <returns>New dependency graph.</returns>
        public static IDependencyGraph Create(string identifier)
        {
            ValidateIdentifier(identifier);
            
            var graph = new DependencyGraph(identifier);
            _graphs.Add(graph);
            return graph;
        }

        private static void ValidateIdentifier(string identifier)
        {
            if (identifier == "")
            {
                throw new DependencyGraphException("Identifier for dependency graph cannot be empty!");
            }
            else if (CheckIfGraphAlreadyExists(identifier))
            {
                throw new DependencyGraphException("Dependency graph cannot be created! Dependency graph with identifier " + identifier + " already exists!");
            }
        }

        private static bool CheckIfGraphAlreadyExists(string identifier)
        {
            return _graphs.Any(g => g.Identifier == identifier);
        }

        /// <summary>
        /// Returns registered graph with provided identifier if exists.
        /// </summary>
        /// <param name="identifier">Graph identifier.</param>
        /// <returns>Dependency graph if exists, otherwise null.</returns>
        public static IDependencyGraph Get(string identifier)
        {
            return _graphs.FirstOrDefault(g => g.Identifier == identifier);
        }

        /// <summary>
        /// Gets graph if there is one registered with provided identifier. Otherwise it creates one.
        /// </summary>
        /// <param name="identifier">Graph identifier.</param>
        /// <returns>Existing or newly created dependency graph.</returns>
        public static IDependencyGraph GetOrCreate(string identifier)
        {
            var graph = Get(identifier);
            if (graph == null)
            {
                graph = Create(identifier);
            }

            return graph;
        }

        public static List<IDependencyGraph> GetRegisteredGraphs()
        {
            return _graphs.ToList();
        }

        /// <summary>
        /// Clears all dependency graphs.
        /// </summary>
        public static void Clear()
        {
            _graphs.Clear();
        }
    }
}
