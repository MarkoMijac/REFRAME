using ReframeCore;
using ReframeCore.ReactiveCollections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReframeCore.Factories;
using ReframeCore.Helpers;

namespace ReframeCoreExamples.E08.E3
{
    public class OrderItem_8_3 : ICollectionNodeItem
    {
        private IDependencyGraph _graph;
        private Updater _updater;
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
            Order = order;
        }

        public OrderItem_8_3(Order_8_3 order, Updater updater)
        {
            _updater = updater;
            Order = order;
        }

        private void Update_Total()
        {
            Total = FixedValue * (1 - (Order.DiscountA + Order.DiscountB)/100);
        }

    }
}
