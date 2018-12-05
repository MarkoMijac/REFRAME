using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore
{
    /// <summary>
    /// Interface describing reactive node.
    /// </summary>
    public interface INode
    {
        int Identifier { get; }
        string MemberName { get; }
        object OwnerObject { get; }
        void Update();
        bool HasSameIdentifier(object ownerObject, string memberName);
        bool HasSameIdentifier(INode node);

        IList<INode> Predecessors { get; }
        IList<INode> Successors { get; }

        bool AddPredecessor(INode predecessor);
        bool AddSuccessor(INode successor);
        bool RemovePredecessor(INode predecessor);
        bool RemoveSuccessor(INode successor);

    }
}
