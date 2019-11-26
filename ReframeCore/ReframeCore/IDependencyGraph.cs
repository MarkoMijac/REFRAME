using ReframeCore.Nodes;
using ReframeCore.ReactiveCollections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore
{
    /// <summary>
    /// Interface describing dependency graph
    /// </summary>
    public interface IDependencyGraph
    {
        string Identifier { get; }
        DependencyGraphStatus Status { get; }

        void Initialize();

        IList<INode> Nodes { get; }
        Settings Settings { get; }
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

        Task PerformUpdate(INode initialNode, bool skipInitialNode);
        Task PerformUpdate(INode initialNode);
        Task PerformUpdate(object ownerObject, string memberName);
        Task PerformUpdate(ICollectionNodeItem ownerObject, string memberName);
        Task PerformUpdate();
        void Clean();

        #region Events

        event EventHandler UpdateCompleted;

        #endregion
    }
}
