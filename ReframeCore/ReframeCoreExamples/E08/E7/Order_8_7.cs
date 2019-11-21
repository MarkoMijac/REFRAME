using ReframeCore;
using ReframeCore.Factories;
using ReframeCore.ReactiveCollections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCoreExamples.E08.E7
{
    public class Order_8_7
    {
        private IDependencyGraph _graph;

        public decimal Total { get; set; }
        public decimal TotalVAT { get; set; }

        public ReactiveCollection<OrderItem_8_7> Items { get; set; }

        private int _discountA;

        public int DiscountA
        {
            get { return _discountA; }
            set
            {
                _discountA = value;
                _graph.PerformUpdate(this, "DiscountA");
            }
        }

        private int _discountB;

        public int DiscountB
        {
            get { return _discountB; }
            set
            {
                _discountB = value;
                _graph.PerformUpdate(this, "DiscountB");
            }
        }

        public int TotalDiscount { get; set; }

        public Order_8_7()
        {
            _graph = GraphRegistry.Get("GRAPH_8_7");
            Items = new ReactiveCollection<OrderItem_8_7>();
        }

        private void Update_Total()
        {
            Total = 0;
            foreach (var item in Items)
            {
                Total += item.Total;
            }
        }

        private void Update_TotalVAT()
        {
            TotalVAT = 0;
            foreach (var item in Items)
            {
                TotalVAT += item.Total * (decimal)1.25;
            }
        }

        private void Update_TotalDiscount()
        {
            TotalDiscount = DiscountA + DiscountB;
        }
    }
}
