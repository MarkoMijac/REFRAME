using ReframeCore.Exceptions;
using ReframeCore.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore.Factories
{
    public class ReactorRegistry
    {
        private static ReactorRegistry _instance = new ReactorRegistry();

        public static ReactorRegistry Instance
        {
            get
            {
                return _instance;
            }
        }

        private List<IReactor> _reactors = new List<IReactor>();

        private ReactorRegistry()
        {
            
        }

        #region CreateReactor

        private IReactor AddNewReactor(string identifier, IDependencyGraph graph, IUpdater updater)
        {
            var reactor = new Reactor(identifier, graph, updater);
            _reactors.Add(reactor);
            return reactor;
        }

        public IReactor CreateReactor(string identifier)
        {
            ValidateIdentifier(identifier);
            var graph = new DependencyGraph(identifier);

            return CreateReactor(identifier, graph);
        }

        public IReactor CreateReactor(string identifier, IDependencyGraph graph)
        {
            ValidateIdentifier(identifier);

            var sorter = new DFS_Sorter();
            var scheduler = new Scheduler(graph, sorter);
            var updater = new Updater(graph, scheduler);

            return AddNewReactor(identifier, graph, updater);
        }

        public IReactor CreateReactor(string identifier, IDependencyGraph graph, IUpdater updater)
        {
            ValidateIdentifier(identifier);
            return AddNewReactor(identifier, graph, updater);
        }

        public IReactor CreateReactor(string identifier, IDependencyGraph graph, ISorter sorter)
        {
            ValidateIdentifier(identifier);

            var scheduler = new Scheduler(graph, sorter);
            var updater = new Updater(graph, scheduler);

            return AddNewReactor(identifier, graph, updater);
        }

        public IReactor CreateReactor(string identifier, IDependencyGraph graph, IScheduler scheduler)
        {
            ValidateIdentifier(identifier);

            var updater = new Updater(graph, scheduler);
            return AddNewReactor(identifier, graph, updater);
        }

        #endregion

        #region GetReactor

        public IReactor GetReactor(string identifier)
        {
            if (CheckIfReactorExists(identifier) == true)
            {
                return _reactors.FirstOrDefault(r => r.Identifier == identifier);
            }
            else
            {
                throw new ReactorException($"Reactor with identifier \"{identifier}\" does not exist in registry!");
            }
        }

        #endregion

        #region GetOrCreateReactor

        public IReactor GetOrCreateReactor(string identifier)
        {
            if (CheckIfReactorExists(identifier) == true)
            {
                return _reactors.FirstOrDefault(r => r.Identifier == identifier);
            }
            else
            {
                return CreateReactor(identifier);
            }
        }

        #endregion

        public IReadOnlyList<IReactor> GetReactors()
        {
            return _reactors.AsReadOnly();
        }

        public void Clear()
        {
            _reactors.Clear();
        }

        private void ValidateIdentifier(string identifier)
        {
            if (identifier == "")
            {
                throw new ReactorException("Reactor identifier cannot be empty!");
            }
            else if(CheckIfReactorExists(identifier))
            {
                throw new ReactorException($"Reactor cannot be created! Reactor with identifier {identifier} already exists!");
            }
        }

        private bool CheckIfReactorExists(string identifier)
        {
            return _reactors.Any(r => r.Identifier == identifier);
        }
    }
}
