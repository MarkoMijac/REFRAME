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
        void AddDependency(INode predecessor, INode successor);
        ISort SortAlgorithm { get; set; }
    }
}
