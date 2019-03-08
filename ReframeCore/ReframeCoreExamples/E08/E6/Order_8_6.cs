using ReframeCore.ReactiveCollections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCoreExamples.E08.E6
{
    public class Order_8_6
    {
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

        public ReactiveCollection<OrderItem_8_6> Items { get; set; }

        public Order_8_6()
        {
            Items = new ReactiveCollection<OrderItem_8_6>();
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
