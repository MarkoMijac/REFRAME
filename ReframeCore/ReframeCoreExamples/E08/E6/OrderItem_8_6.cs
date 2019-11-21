using ReframeCore;
using ReframeCore.Factories;
using ReframeCore.ReactiveCollections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCoreExamples.E08.E6
{
    public class OrderItem_8_6 : ICollectionNodeItem
    {
        private IDependencyGraph _graph;
        public event EventHandler UpdateTriggered;

        private double _total = 1;

        public double Total
        {
            get { return _total; }
            set
            {
                _total = value;
                _graph.PerformUpdate(this, "Total");
            }
        }

        public OrderItem_8_6()
        {
            _graph = GraphRegistry.Get("GRAPH_8_6");
        }

    }
}
