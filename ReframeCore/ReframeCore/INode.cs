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
        string MemberName { get; }
        object OwnerObject { get; }
        void Update();
    }
}
