using ReframeCore.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore.ReactiveCollections
{
    public interface IReactiveCollection
    {
        event EventHandler UpdateTriggered;
        ICollectionNode CollectionNode { get; set; }
    }
}
