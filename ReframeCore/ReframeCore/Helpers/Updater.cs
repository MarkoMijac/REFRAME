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

    public class Updater : ILoggable
    {
        #region Properties

        private UpdateProcessStatus Status { get; set; }

        public IDependencyGraph DependencyGraph { get; set; }
        public UpdateLogger NodeLog { get; private set; }

        public bool EnableUpdateInSeparateThread { get; set; }
        public bool EnableParallelUpdate { get; set; }

        public UpdateInfo LatestUpdateInfo { get; private set; }

        public IScheduler Scheduler { get; set; }

        #endregion

        #region Constructor

        public Updater(IDependencyGraph graph, IScheduler scheduler)
        {
            NodeLog = new UpdateLogger();
            DependencyGraph = graph;

            Scheduler = scheduler;

            EnableUpdateInSeparateThread = false;
            Status = UpdateProcessStatus.NotSet;
        }

        #endregion

        #region Public methods

        private void CleanGraph()
        {
            DependencyGraph.Clean();
        }

        private void PrepareForUpdate()
        {
            CleanGraph();
        }

        public Task PerformUpdate()
        {
            Task task = null;

            if (EnableUpdateInSeparateThread == true)
            {
                task = PerformUpdateAsync();
            }
            else
            {
                PerformUpdateSync();
            }

            return task;
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

        public Task PerformUpdate(INode initialNode, bool skipInitialNode)
        {
            Task task = null;

            if (EnableUpdateInSeparateThread == true)
            {
                task = PerformUpdateAsync(initialNode, skipInitialNode);
            }
            else
            {
                PerformUpdateSync(initialNode, skipInitialNode);
            }

            return task;
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

        public Task PerformUpdate(ICollectionNodeItem ownerObject, string memberName)
        {
            Task task = null;

            if (EnableUpdateInSeparateThread == true)
            {
                task = PerformUpdateAsync(ownerObject, memberName);
            }
            else
            {
                PerformUpdateSync(ownerObject, memberName);
            }

            return task;
        }

        private void PerformUpdateSync(ICollectionNodeItem ownerObject, string memberName)
        {
            if (UpdateIsAllowed())
            {
                PrepareForUpdate();

                MarkUpdateStart();

                INode initialNode = DependencyGraph.GetNode(ownerObject, memberName);

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

        #endregion

        #region Private methods

        private bool UpdateIsAllowed()
        {
            return DependencyGraph.Status == DependencyGraphStatus.Initialized
                && Status != UpdateProcessStatus.Started
                && (DependencyGraph as DependencyGraph).UpdateSuspended == false;
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
                if (EnableUpdateInSeparateThread == true && EnableParallelUpdate == true)
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
            LatestUpdateInfo.SaveErrorData(e, DependencyGraph, failedNode);

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
            if (DependencyGraph.Settings.EnableLogging == true)
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

        #endregion

        #region Events

        internal event EventHandler UpdateCompleted;

        private void OnUpdateCompleted()
        {
            UpdateCompleted?.Invoke(this, null);
        }

        internal event EventHandler UpdateStarted;

        private void OnUpdateStarted()
        {
            UpdateStarted?.Invoke(this, null);
        }

        internal event EventHandler UpdateFailed;

        private void OnUpdateFailed(UpdateError updateError)
        {
            UpdateFailed?.Invoke(updateError, null);
        }

        #endregion
    }
}
