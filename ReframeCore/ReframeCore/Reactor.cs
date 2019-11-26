using ReframeCore.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore
{
    public class Reactor
    {
        public IDependencyGraph Graph { get; set; }
        public IScheduler Scheduler { get; set; }
        public Updater Updater { get; set; }

        public Reactor(IDependencyGraph graph, IScheduler scheduler, Updater updater)
        {
            Graph = graph;
            Scheduler = scheduler;
            Updater = updater;
        }


    }
}
