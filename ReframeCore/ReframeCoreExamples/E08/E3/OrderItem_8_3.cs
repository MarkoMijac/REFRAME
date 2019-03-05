using ReframeCore;
using ReframeCore.ReactiveCollections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCoreExamples.E08.E3
{
    public class OrderItem_8_3 : ICollectionNodeItem
    {
        private IDependencyGraph _graph;
        public event EventHandler UpdateTriggered;


        private double _fixedValue;

        public double FixedValue
        {
            get { return _fixedValue; }
            set { _fixedValue = value; }
        }


        private double _total = 1;

        public double Total
        {
            get { return _total; }
            set
            {
                _total = value;
            }
        }

        public Order_8_3 Order { get; set; }

        public OrderItem_8_3(Order_8_3 order)
        {
            _graph = GraphFactory.Get("GRAPH_8_3");
            Order = order;
        }

        private void Update_Total()
        {
            Total = FixedValue * (1 - (Order.DiscountA + Order.DiscountB)/100);
        }

    }
}
