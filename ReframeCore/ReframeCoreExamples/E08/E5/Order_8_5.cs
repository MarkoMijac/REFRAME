using ReframeCore.ReactiveCollections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCoreExamples.E08.E5
{
    public class Order_8_5
    {
        public double Total { get; set; }
        public double TotalVAT { get; set; }

        public ReactiveCollection<OrderItem_8_5> Items { get; set; }

        public Order_8_5()
        {
            Items = new ReactiveCollection<OrderItem_8_5>();
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
