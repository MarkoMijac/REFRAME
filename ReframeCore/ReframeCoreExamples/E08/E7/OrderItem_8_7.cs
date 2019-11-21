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
    public class OrderItem_8_7 : ICollectionNodeItem
    {
        private IDependencyGraph _graph;
        public event EventHandler UpdateTriggered;

        public Order_8_7 Order { get; set; }

        private int _amount;

        public int Amount
        {
            get { return _amount; }
            set
            {
                _amount = value;
                _graph.PerformUpdate(this, "Amount");
            }
        }

        private decimal _unitPrice;

        public decimal UnitPrice
        {
            get { return _unitPrice; }
            set
            {
                _unitPrice = value;
                _graph.PerformUpdate(this, "UnitPrice");
            }
        }

        public decimal Total { get; set; }

        public decimal TotalVAT { get; set; }

        private void Update_Total()
        {
            Total = (Amount * UnitPrice) * (1 - Order.TotalDiscount / (decimal)100);
        }

        private void Update_TotalVAT()
        {
            TotalVAT = Total * 1.25m;
        }

        public OrderItem_8_7(Order_8_7 order)
        {
            _graph = GraphRegistry.Get("GRAPH_8_7");
            Order = order;
        }
    }
}
