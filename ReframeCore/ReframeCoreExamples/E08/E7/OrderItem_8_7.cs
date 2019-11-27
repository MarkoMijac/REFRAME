using ReframeCore;
using ReframeCore.Factories;
using ReframeCore.Helpers;
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
        private Updater _updater;
        public event EventHandler UpdateTriggered;

        public Order_8_7 Order { get; set; }

        private int _amount;

        public int Amount
        {
            get { return _amount; }
            set
            {
                _amount = value;
                if (_updater != null)
                {
                    _updater.PerformUpdate(this, nameof(Amount));
                }
                else
                {
                    _graph.PerformUpdate(this, nameof(Amount));
                }
            }
        }

        private decimal _unitPrice;

        public decimal UnitPrice
        {
            get { return _unitPrice; }
            set
            {
                _unitPrice = value;
                if (_updater != null)
                {
                    _updater.PerformUpdate(this, nameof(UnitPrice));
                }
                else
                {
                    _graph.PerformUpdate(this, nameof(UnitPrice));
                }
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
            _graph = GraphRegistry.Instance.GetGraph("GRAPH_8_7");
            Order = order;
        }

        public OrderItem_8_7(Order_8_7 order, Updater updater)
        {
            Order = order;
            _updater = updater;
        }
    }
}
