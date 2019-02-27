using ReframeCore.Nodes;
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
        ISort SortAlgorithm { get; set; }

        void Initialize();

        INode AddNode(INode node);
        INode AddNode(object ownerObject, string memberName);        

        INode GetNode(INode node);
        INode GetNode(object ownerObject, string memberName);

        void AddDependency(INode predecessor, INode successor);

        void PerformUpdate(INode initialNode, bool skipInitialNode);
        void PerformUpdate(INode initialNode);
        void PerformUpdate(object ownerObject, string memberName);
        void PerformUpdate();
    }
}
