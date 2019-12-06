using ReframeCore.Exceptions;
using ReframeCore.Nodes;
using ReframeCore.ReactiveCollections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore.Helpers
{
    internal enum UpdateProcessStatus
    {
        NotSet,
        Started,
        Ended
    }

    public class Updater : IUpdater, ILoggable
    {
        #region Properties

        private UpdateProcessStatus Status { get; set; }

        public IDependencyGraph Graph { get; set; }
        public NodeLog NodeLog { get; private set; }

        public bool AsynchronousUpdateEnabled { get; set; }
        public bool ParallelUpdateEnabled { get; set; }
        private bool UpdateSuspended { get; set; } = false;
        public bool LoggingEnabled { get; set; } = true;

        public UpdateInfo LatestUpdateInfo { get; private set; }

        public IScheduler Scheduler { get; set; }

        #endregion

        #region Constructor

        public Updater(IDependencyGraph graph, IScheduler scheduler)
        {
            NodeLog = new NodeLog();
            Graph = graph;
            

            Scheduler = scheduler;

            AsynchronousUpdateEnabled = false;
            Status = UpdateProcessStatus.NotSet;
        }

        #endregion

        #region Public methods

        public Task PerformUpdate()
        {
            Task task = null;

            if (AsynchronousUpdateEnabled == true)
            {
                task = PerformUpdateAsync();
            }
            else
            {
                PerformUpdateSync();
            }

            return task;
        }

        public Task PerformUpdate(INode initialNode, bool skipInitialNode)
        {
            Task task = null;

            if (AsynchronousUpdateEnabled == true)
            {
                task = PerformUpdateAsync(initialNode, skipInitialNode);
            }
            else
            {
                PerformUpdateSync(initialNode, skipInitialNode);
            }

            return task;
        }

        public Task PerformUpdate(ICollectionNodeItem ownerObject, string memberName)
        {
            Task task = null;

            if (AsynchronousUpdateEnabled == true)
            {
                task = PerformUpdateAsync(ownerObject, memberName);
            }
            else
            {
                PerformUpdateSync(ownerObject, memberName);
            }

            return task;
        }

        /// <summary>
        /// Performs update of all nodes that depend on provided initial node.
        /// Proper order of update is handled by topologically sorting dependent nodes.
        /// </summary>
        /// <param name="initialNode">Initial node that triggered the update.</param>
        /// <param name="skipInitialNode">Specifies whether the initial node will be skipped from updating.l</param>
        public Task PerformUpdate(INode initialNode)
        {
            return PerformUpdate(initialNode, true);
        }

        public Task PerformUpdate(object ownerObject, string memberName)
        {
            INode initialNode = Graph.GetNode(ownerObject, memberName);
            return PerformUpdate(initialNode);
        }

        public void SuspendUpdate()
        {
            UpdateSuspended = true;
        }

        public void ResumeUpdate()
        {
            UpdateSuspended = false;
        }

        #endregion

        #region Private methods

        private void CleanGraph()
        {
            Graph.Clean();
        }

        private void PrepareForUpdate()
        {
            CleanGraph();
        }

        private bool UpdateIsAllowed()
        {
            return Status != UpdateProcessStatus.Started
                && UpdateSuspended == false;
        }

        private void MarkUpdateStart()
        {
            Status = UpdateProcessStatus.Started;
            LatestUpdateInfo = new UpdateInfo();
            LatestUpdateInfo.StartUpdate();
            OnUpdateStarted();
        }

        private void MarkUpdateEnd()
        {
            Status = UpdateProcessStatus.Ended;
            LatestUpdateInfo.EndUpdate();
            OnUpdateCompleted();
        }

        private void Update(Dictionary<INode, bool> nodesForUpdate)
        {
            NodeLog.ClearLog();

            try
            {
                if (AsynchronousUpdateEnabled == true && ParallelUpdateEnabled == true)
                {
                    UpdateInParallel(nodesForUpdate);
                }
                else
                {
                    UpdateSequentially(nodesForUpdate);
                }
            }
            catch (Exception e)
            {
                INode failedNode = GetFailedNode(nodesForUpdate);
                ManageError(e, failedNode);
            }
        }

        private void ManageError(Exception e, INode failedNode)
        {
            LatestUpdateInfo.SaveErrorData(e, Graph, failedNode);

            OnUpdateFailed(LatestUpdateInfo.ErrorData);

            GraphUpdateException ex = new GraphUpdateException(LatestUpdateInfo.ErrorData);
            throw ex;
        }

        private void UpdateSequentially(Dictionary<INode, bool> nodesForUpdate)
        {
            foreach (var node in nodesForUpdate.Keys.ToList())
            {
                node.Update();
                MarkAsUpdated(nodesForUpdate, node);
            }
        }

        private void UpdateInParallel(Dictionary<INode, bool> nodesForUpdate)
        {
            IList<INode> list = nodesForUpdate.Keys.ToList();
            int maxLevel = list[0].Level;

            for (int i = maxLevel; i >= 0; i--)
            {
                UpdateLevel(nodesForUpdate, i).Wait();
            }
        }

        private Task UpdateLevel(Dictionary<INode, bool> nodesForUpdate, int level)
        {
            IList<INode> nodesAtLevel = GetNodesAtLevel(nodesForUpdate.Keys.ToList(), level);
            List<Task> tasks = new List<Task>();
            foreach (var node in nodesAtLevel)
            {
                Task task = new Task(() => node.Update());
                task.ContinueWith(t => MarkAsUpdated(nodesForUpdate, node));
                tasks.Add(task);
            }

            Task groupTask = Task.WhenAll(tasks);

            foreach (var task in tasks)
            {
                task.Start();
            }

            return groupTask;
        }

        private IList<INode> GetNodesAtLevel(IList<INode> allNodes, int level)
        {
            return allNodes.Where(n => n.Level == level).ToList();
        }
        
        private INode GetFailedNode(Dictionary<INode, bool> nodesForUpdate)
        {
            INode node = null;

            foreach (var item in nodesForUpdate)
            {
                if (item.Value == false)
                {
                    node = item.Key;
                    break;
                }
            }

            return node;
        }

        private void MarkAsUpdated(Dictionary<INode, bool> nodesForUpdate, INode node)
        {
            if (LoggingEnabled == true)
            {
                NodeLog.Log(node);
            }

            nodesForUpdate[node] = true;
        }

        private void ValidateInitialNode(INode initialNode)
        {
            if (initialNode == null)
            {
                throw new NodeNullReferenceException("Reactive node set as initial node of the update process is not part of the graph!");
            }
        }

        private void PerformUpdateSync(ICollectionNodeItem ownerObject, string memberName)
        {
            if (UpdateIsAllowed())
            {
                PrepareForUpdate();

                MarkUpdateStart();

                INode initialNode = Graph.GetNode(ownerObject, memberName);

                if (initialNode == null)
                {
                    initialNode = GraphUtility.GetCollectionNode(ownerObject, memberName);
                }

                var nodesForUpdate = GetUpdateSchedule(initialNode, true);
                Update(nodesForUpdate);

                MarkUpdateEnd();
            }
        }

        private Task PerformUpdateAsync(ICollectionNodeItem ownerObject, string memberName)
        {
            return Task.Run(() => PerformUpdateSync(ownerObject, memberName));
        }

        private void PerformUpdateSync(INode initialNode, bool skipInitialNode)
        {
            if (UpdateIsAllowed())
            {
                PrepareForUpdate();

                MarkUpdateStart();

                ValidateInitialNode(initialNode);
                var nodesForUpdate = GetUpdateSchedule(initialNode, skipInitialNode);
                Update(nodesForUpdate);

                MarkUpdateEnd();
            }
        }

        private Task PerformUpdateAsync(INode initialNode, bool skipInitialNode)
        {
            return Task.Run(() => PerformUpdateSync(initialNode, skipInitialNode));
        }

        private void PerformUpdateSync()
        {
            if (UpdateIsAllowed())
            {
                PrepareForUpdate();

                MarkUpdateStart();

                var nodesForUpdate = GetUpdateSchedule();
                Update(nodesForUpdate);

                MarkUpdateEnd();
            }
        }

        private Task PerformUpdateAsync()
        {
            return Task.Run(() => PerformUpdateSync());
        }

        /// <summary>
        /// Gets all nodes from dependency graph, arranged in order they should be updated.
        /// </summary>
        /// <returns>List of all nodes from dependency graph.</returns>
        private Dictionary<INode, bool> GetUpdateSchedule()
        {
            IList<INode> nodesForUpdate = Scheduler.GetNodesForUpdate();
            return ConvertToDictionary(nodesForUpdate);
        }

        private Dictionary<INode, bool> ConvertToDictionary(IList<INode> nodesForUpdate)
        {
            Dictionary<INode, bool> dictionary = new Dictionary<INode, bool>();
            if (nodesForUpdate != null)
            {
                foreach (var n in nodesForUpdate)
                {
                    dictionary.Add(n, false);
                }
            }

            return dictionary;
        }

        /// <summary>
        /// Gets nodes from dependency graph that need to be updated, arranged in order they should be updated.
        /// </summary>
        /// <param name="node">Initial node that triggered the update.</param>
        /// <param name="skipInitialNode">Specifies whether the initial node will be skipped from updating.</param>
        /// <returns>List of nodes from dependency graph that need to be updated.</returns>
        private Dictionary<INode, bool> GetUpdateSchedule(INode node, bool skipInitialNode)
        {
            IList<INode> nodesForUpdate = Scheduler.GetNodesForUpdate(node, skipInitialNode);
            return ConvertToDictionary(nodesForUpdate);
        }

        #endregion

        #region Events

        public event EventHandler UpdateCompleted;

        private void OnUpdateCompleted()
        {
            UpdateCompleted?.Invoke(this, null);
        }

        public event EventHandler UpdateStarted;

        private void OnUpdateStarted()
        {
            UpdateStarted?.Invoke(this, null);
        }

        public event EventHandler UpdateFailed;

        private void OnUpdateFailed(UpdateError updateError)
        {
            UpdateFailed?.Invoke(updateError, null);
        }

        #endregion
    }
}
