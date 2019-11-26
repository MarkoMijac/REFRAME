using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore.Helpers
{
    public class ParallelUpdater : Updater
    {
        public ParallelUpdater(IDependencyGraph graph, IScheduler scheduler)
            :base(graph, scheduler)
        {

        }
    }
}
