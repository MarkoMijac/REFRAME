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

        void Initialize();

        IList<INode> Nodes { get; }
        Settings Settings { get; }
        INode AddNode(INode node);
        INode AddNode(object ownerObject, string memberName);

        bool ContainsNode(INode node);
        bool ContainsNode(object ownerObject, string memberName);

        INode GetNode(INode node);
        INode GetNode(object ownerObject, string memberName);

        void AddDependency(INode predecessor, INode successor);
        bool RemoveDependency(INode predecessor, INode successor);

        void PerformUpdate(INode initialNode, bool skipInitialNode);
        void PerformUpdate(INode initialNode);
        void PerformUpdate(object ownerObject, string memberName);
        void PerformUpdate(ICollectionNodeItem ownerObject, string memberName);
        void PerformUpdate();
    }
}
