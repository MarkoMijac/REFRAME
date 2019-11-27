using ReframeCore;
using ReframeCore.Factories;
using ReframeCore.Helpers;
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
        private Updater _updater;
        private double _discountA = 0;

        public double DiscountA
        {
            get { return _discountA; }
            set
            {
                _discountA = value;
                if (_updater != null)
                {
                    _updater.PerformUpdate(this, "DiscountA");
                }
            }
        }

        private double _discountB = 0;
        public double DiscountB
        {
            get { return _discountB; }
            set
            {
                _discountB = value;
                if (_updater != null)
                {
                    _updater.PerformUpdate(this, "DiscountB");
                }
            }
        }

        public ReactiveCollection<OrderItem_8_3> Items { get; set; }

        public Order_8_3(Updater updater)
        {
            _updater = updater;
            Items = new ReactiveCollection<OrderItem_8_3>();
        }

    }
}
