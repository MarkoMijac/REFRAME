using ReframeCore;
using ReframeCore.Factories;
using ReframeCore.FluentAPI;
using ReframeCore.Helpers;
using ReframeDemonstration.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeDemonstration
{
    public static class DependencyManager
    {
        public static IDependencyGraph DefaultGraph
        {
            get
            {
                return GraphRegistry.Instance.GetOrCreateGraph("DEFAULT_GRAPH");
            }
        }

        private static Updater _updater;

        public static Updater Updater
        {
            get
            {
                if (_updater == null)
                {
                    _updater = GetUpdater();
                }
                return _updater;
            }
        }

        private static Updater GetUpdater()
        {
            IScheduler scheduler = new Scheduler(DefaultGraph, new DFS_Sorter());
            return new Updater(DefaultGraph, scheduler);
        }
    }
}
