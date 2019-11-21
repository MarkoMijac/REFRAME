using ReframeCore.Exceptions;
using ReframeCore.Helpers;
using ReframeCore.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore.Factories
{
    public class GraphRegistry
    {
        private static GraphRegistry _instance = new GraphRegistry();

        public static GraphRegistry Instance
        {
            get
            {
                return _instance;
            }
        }

        private List<IDependencyGraph> _graphs = new List<IDependencyGraph>();
        public const string DefaultGraphName = "DEFAULT";



        private GraphRegistry()
        {
            CreateDefaultGraph();
        }

        private void CreateDefaultGraph()
        {
            _graphs.Add(new DependencyGraph(DefaultGraphName));
        }

        /// <summary>
        /// Creates and registers new dependency graph with unique identifier.
        /// </summary>
        /// <param name="identifier">Graph's unique identifier.</param>
        /// <returns>New dependency graph.</returns>
        public IDependencyGraph Create(string identifier)
        {
            ValidateIdentifier(identifier);
            
            var graph = new DependencyGraph(identifier);
            _graphs.Add(graph);
            return graph;
        }

        private void ValidateIdentifier(string identifier)
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

        private bool CheckIfGraphAlreadyExists(string identifier)
        {
            return _graphs.Any(g => g.Identifier == identifier);
        }

        /// <summary>
        /// Returns registered graph with provided identifier if exists.
        /// </summary>
        /// <param name="identifier">Graph identifier.</param>
        /// <returns>Dependency graph if exists, otherwise null.</returns>
        public IDependencyGraph Get(string identifier)
        {
            return _graphs.FirstOrDefault(g => g.Identifier == identifier);
        }

        /// <summary>
        /// Returns registered graph which contains provided node.
        /// </summary>
        /// <param name="node">Reactive node</param>
        /// <returns>Dependency graph if exists, otherwise null.</returns>
        public IDependencyGraph Get(INode node)
        {
            return _graphs.FirstOrDefault(g => g.ContainsNode(node));
        }

        /// <summary>
        /// Gets graph if there is one registered with provided identifier. Otherwise it creates one.
        /// </summary>
        /// <param name="identifier">Graph identifier.</param>
        /// <returns>Existing or newly created dependency graph.</returns>
        public IDependencyGraph GetOrCreate(string identifier)
        {
            var graph = Get(identifier);
            if (graph == null)
            {
                graph = Create(identifier);
            }

            return graph;
        }

        /// <summary>
        /// Gets default graph.
        /// </summary>
        /// <returns></returns>
        public IDependencyGraph GetDefault()
        {
            return Get(DefaultGraphName);
        }

        public List<IDependencyGraph> GetRegisteredGraphs()
        {
            return _graphs.ToList();
        }

        /// <summary>
        /// Clears all dependency graphs.
        /// </summary>
        public void Clear()
        {
            _graphs.RemoveAll(g => g.Identifier != DefaultGraphName);
        }
    }
}
