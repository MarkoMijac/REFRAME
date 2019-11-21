using ReframeCore;
using ReframeCore.Factories;
using ReframeCore.ReactiveCollections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCoreExamples.E08.E3
{
    public class Order_8_3
    {
        private IDependencyGraph _graph;
        private double _discountA = 0;

        public double DiscountA
        {
            get { return _discountA; }
            set
            {
                _discountA = value;
                _graph.PerformUpdate(this, "DiscountA");
            }
        }

        private double _discountB = 0;
        public double DiscountB
        {
            get { return _discountB; }
            set
            {
                _discountB = value;
                _graph.PerformUpdate(this, "DiscountB");
            }
        }

        public ReactiveCollection<OrderItem_8_3> Items { get; set; }

        public Order_8_3()
        {
            _graph = GraphRegistry.Instance.Get("GRAPH_8_3");
            Items = new ReactiveCollection<OrderItem_8_3>();
        }

    }
}
