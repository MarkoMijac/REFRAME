using ReframeCore;
using ReframeCore.FluentAPI;
using ReframeCore.ReactiveCollections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCoreExamples.E08.E4
{
    public class Order_8_4
    {
        private IDependencyGraph _graph;

        private double _total;

        public double Total
        {
            get { return _total; }
            set { _total = value; }
        }

        private double _totalVAT;

        public double TotalVAT
        {
            get { return _totalVAT; }
            set { _totalVAT = value; }
        }

        private double _discountA = 0;

        public double DiscountA
        {
            get { return _discountA; }
            set
            {
                _discountA = value;
                this.Update(_graph);
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

        public ReactiveCollection<OrderItem_8_4> Items { get; set; }

        public Order_8_4()
        {
            _graph = GraphFactory.Get("GRAPH_8_4");
            Items = new ReactiveCollection<OrderItem_8_4>();
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
                TotalVAT += item.Total * 1.25;
            }
        }
    }
}
