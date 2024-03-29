﻿using ReframeCore;
using ReframeCore.Factories;
using ReframeCore.Helpers;
using ReframeCore.ReactiveCollections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCoreExamples.E08.E2
{
    public class OrderItem_8_2 : ICollectionNodeItem
    {
        private Updater _updater;
        public event EventHandler UpdateTriggered;

        private double _total = 1;

        public double Total
        {
            get { return _total; }
            set
            {
                _total = value;
                if (_updater != null)
                {
                    _updater.PerformUpdate(this, "Total");
                }
            }
        }

        public OrderItem_8_2(Updater updater)
        {
            _updater = updater;
        }

        

    }
}
