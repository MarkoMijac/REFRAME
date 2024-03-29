﻿using ReframeCore;
using ReframeCore.ReactiveCollections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReframeCore.Factories;
using ReframeCore.Helpers;

namespace ReframeCoreExamples.E08.E5
{
    public class OrderItem_8_5 : ICollectionNodeItem
    {
        private Updater _updater;
        public event EventHandler UpdateTriggered;

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
            }
        }

        private double _unitPrice;

        public double UnitPrice
        {
            get { return _unitPrice; }
            set
            {
                _unitPrice = value;
                if (_updater != null)
                {
                    _updater.PerformUpdate(this, nameof(UnitPrice));
                }
            }
        }

        public double Total { get; set; }

        private void Update_Total()
        {
            Total = Amount * UnitPrice;
        }

        public OrderItem_8_5(Updater updater)
        {
            _updater = updater;
        }
    }
}
