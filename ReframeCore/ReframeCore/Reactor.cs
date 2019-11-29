using ReframeCore.Exceptions;
using ReframeCore.Helpers;
using ReframeCore.Nodes;
using ReframeCore.ReactiveCollections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore
{
    public class Reactor : IReactor
    {
        public string Identifier { get; private set; }
        public IDependencyGraph Graph { get; private set; }
        public IUpdater Updater { get; private set; }

        public Reactor(string identifier, IDependencyGraph graph, IUpdater updater)
        {
            ValidateParameters(identifier, graph, updater);

            Identifier = identifier;
            Graph = graph;
            Updater = updater;
        }

        private void ValidateParameters(string identifier, IDependencyGraph graph, IUpdater updater)
        {
            if (identifier == "")
            {
                throw new ReactorException("Identifier must be set!");
            }

            if (graph == null)
            {
                throw new ReactorException("Dependency graph must be set!");
            }

            if (updater == null)
            {
                throw new ReactorException("Updater must  be set!");
            }
        }

        #region DependencyGraph Methods

        public INode AddNode(INode node)
        {
            return Graph.AddNode(node);
        }

        public INode AddNode(object owner, string member)
        {
            return Graph.AddNode(owner, member);
        }

        public bool RemoveNode(INode node, bool forceRemoval)
        {
            return Graph.RemoveNode(node, forceRemoval);
        }

        public bool ContainsNode(INode node)
        {
            return Graph.ContainsNode(node);
        }

        public bool ContainsNode(object owner, string member)
        {
            return Graph.ContainsNode(owner, member);
        }

        public bool ContainsDependency(INode predecessor, INode successor)
        {
            return Graph.ContainsDependency(predecessor, successor);
        }

        public INode GetNode(INode node)
        {
            return Graph.GetNode(node);
        }

        public INode GetNode(object ownerObject, string memberName)
        {
            return Graph.GetNode(ownerObject, memberName);
        }

        public void AddDependency(INode predecessor, INode successor)
        {
            Graph.AddDependency(predecessor, successor);
        }

        public bool RemoveDependency(INode predecessor, INode successor)
        {
            return Graph.RemoveDependency(predecessor, successor);
        }

        #endregion

        #region Updater Methods

        public Task PerformUpdate()
        {
            return Updater.PerformUpdate();
        }

        public Task PerformUpdate(INode initialNode)
        {
            return Updater.PerformUpdate(initialNode);
        }

        public Task PerformUpdate(INode initialNode, bool skipInitialNode)
        {
            return Updater.PerformUpdate(initialNode, skipInitialNode);
        }

        public Task PerformUpdate(ICollectionNodeItem owner, string member)
        {
            return Updater.PerformUpdate(owner, member);
        }

        public Task PerformUpdate(object owner, string member)
        {
            return Updater.PerformUpdate(owner, member);
        }

        public void SuspendUpdate()
        {
            Updater.SuspendUpdate();
        }

        public void ResumeUpdate()
        {
            Updater.ResumeUpdate();
        }

        #endregion

        #region Events

        private object objectLock = new object();

        public event EventHandler UpdateStarted
        {
            add
            {
                lock (objectLock)
                {
                    Updater.UpdateStarted += value;
                }
            }
            remove
            {
                lock (objectLock)
                {
                    Updater.UpdateStarted -= value;
                }
            }
        }

        public event EventHandler UpdateCompleted
        {
            add
            {
                lock (objectLock)
                {
                    Updater.UpdateCompleted += value;
                }
            }
            remove
            {
                lock (objectLock)
                {
                    Updater.UpdateCompleted -= value;
                }
            }
        }

        public event EventHandler UpdateFailed
        {
            add
            {
                lock (objectLock)
                {
                    Updater.UpdateFailed += value;
                }
            }
            remove
            {
                lock (objectLock)
                {
                    Updater.UpdateFailed -= value;
                }
            }
        }

        #endregion
    }
}
