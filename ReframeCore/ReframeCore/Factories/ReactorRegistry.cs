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
            ValidateGraph(graph);

            var sorter = new DFS_Sorter();
            var scheduler = new Scheduler(graph, sorter);
            var updater = new Updater(graph, scheduler);

            return AddNewReactor(identifier, graph, updater);
        }

        public IReactor CreateReactor(string identifier, IDependencyGraph graph, IUpdater updater)
        {
            ValidateIdentifier(identifier);
            ValidateGraph(graph);
            ValidateUpdater(graph, updater);
            
            return AddNewReactor(identifier, graph, updater);
        }

        public IReactor CreateReactor(string identifier, IDependencyGraph graph, ISorter sorter)
        {
            ValidateIdentifier(identifier);
            ValidateGraph(graph);
            ValidateSorter(sorter);

            var scheduler = new Scheduler(graph, sorter);
            var updater = new Updater(graph, scheduler);

            return AddNewReactor(identifier, graph, updater);
        }

        public IReactor CreateReactor(string identifier, IDependencyGraph graph, IScheduler scheduler)
        {
            ValidateIdentifier(identifier);
            ValidateGraph(graph);
            ValidateScheduler(graph, scheduler);

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

        public IReactor GetReactor(IDependencyGraph graph)
        {
            return _reactors.FirstOrDefault(r => r.Graph == graph);
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

        #region GetReactors

        public IReadOnlyList<IReactor> GetReactors()
        {
            return _reactors.AsReadOnly();
        }

        #endregion

        #region RemoveReactor

        public void RemoveReactor(string identifier)
        {
            if (CheckIfReactorExists(identifier) == true)
            {
                IReactor reactor = _reactors.FirstOrDefault(r => r.Identifier == identifier);
                _reactors.Remove(reactor);
            }
            else
            {
                throw new ReactorException($"Reactor with identifier \"{identifier}\" does not exist in registry!");
            }
        }

        public void RemoveReactor(IReactor reactor)
        {
            IReactor reactorToRemove = GetReactor(reactor.Identifier);
            _reactors.Remove(reactorToRemove);
        }

        #endregion

        #region Clear

        public void Clear()
        {
            _reactors.Clear();
        }

        #endregion

        #region Validate

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

        private void ValidateGraph(IDependencyGraph graph)
        {
            if (graph == null)
            {
                throw new ReactorException("Dependency graph must be set!");
            }
            else if (CheckIfReactorExists(graph))
            {
                throw new ReactorException($"Reactor for graph \"{graph.Identifier}\" already exists");
            }
        }

        private bool CheckIfReactorExists(IDependencyGraph graph)
        {
            return _reactors.Any(r => r.Graph == graph);
        }

        private void ValidateUpdater(IDependencyGraph graph, IUpdater updater)
        {
            if (updater == null)
            {
                throw new ReactorException("Updater must be set!");
            }
            else if (updater.Graph != graph)
            {
                throw new ReactorException("Provided updater must match with provided dependency graph!");
            }
        }

        private void ValidateSorter(ISorter sorter)
        {
            if (sorter == null)
            {
                throw new ReactorException("Sorter must be set!");
            }
        }

        private void ValidateScheduler(IDependencyGraph graph, IScheduler scheduler)
        {
            if (scheduler == null)
            {
                throw new ReactorException("Scheduler must be set!");
            }
            else if (graph != scheduler.Graph)
            {
                throw new ReactorException("Provided scheduler must match with provided dependency graph!");
            }
        }

        #endregion
    }
}
