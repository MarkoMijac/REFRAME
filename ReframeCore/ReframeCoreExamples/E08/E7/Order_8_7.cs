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
    public class Order_8_7
    {
        private Updater _updater;

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
                if (_updater != null)
                {
                    _updater.PerformUpdate(this, nameof(DiscountA));
                }
            }
        }

        private int _discountB;

        public int DiscountB
        {
            get { return _discountB; }
            set
            {
                _discountB = value;
                if (_updater != null)
                {
                    _updater.PerformUpdate(this, nameof(DiscountB));
                }
            }
        }

        public int TotalDiscount { get; set; }

        public Order_8_7(Updater updater)
        {
            _updater = updater;
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
