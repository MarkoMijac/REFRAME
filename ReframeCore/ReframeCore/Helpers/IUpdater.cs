using ReframeCore.Nodes;
using ReframeCore.ReactiveCollections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore.Helpers
{
    public interface IUpdater
    {
        Task PerformUpdate();
        Task PerformUpdate(INode initialNode);
        Task PerformUpdate(INode initialNode, bool skipInitialNode);
        Task PerformUpdate(ICollectionNodeItem ownerObject, string memberName);
        Task PerformUpdate(object ownerObject, string memberName);

        IDependencyGraph Graph { get; }
        IScheduler Scheduler { get; }
    }
}
