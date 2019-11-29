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
    public interface IReactor
    {
        string Identifier { get; }
        IDependencyGraph Graph { get; }
        IUpdater Updater { get; }

        INode AddNode(INode node);
        INode AddNode(object ownerObject, string memberName);
        bool RemoveNode(INode node, bool forceRemoval);

        bool ContainsNode(INode node);
        bool ContainsNode(object ownerObject, string memberName);
        bool ContainsDependency(INode predecessor, INode successor);

        INode GetNode(INode node);
        INode GetNode(object ownerObject, string memberName);

        void AddDependency(INode predecessor, INode successor);
        bool RemoveDependency(INode predecessor, INode successor);

        Task PerformUpdate();
        Task PerformUpdate(INode initialNode);
        Task PerformUpdate(INode initialNode, bool skipInitialNode);
        Task PerformUpdate(ICollectionNodeItem ownerObject, string memberName);
        Task PerformUpdate(object ownerObject, string memberName);

        event EventHandler UpdateCompleted;
        event EventHandler UpdateStarted;
        event EventHandler UpdateFailed;
    }
}
