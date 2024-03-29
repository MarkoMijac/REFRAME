﻿using ReframeCore.ReactiveCollections;
using ReframeCore.Helpers;

namespace ReframeCoreExamples.E08.E4
{
    public class Order_8_4
    {
        private Updater _updater;

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
                if (_updater != null)
                {
                    _updater.PerformUpdate(this, nameof(DiscountA));
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

        public ReactiveCollection<OrderItem_8_4> Items { get; set; }

        public Order_8_4(Updater updater)
        {
            Items = new ReactiveCollection<OrderItem_8_4>();
            _updater = updater;
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
