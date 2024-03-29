﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCoreExamples.E00
{
    /// <summary>
    /// This class represents simple building example.
    /// </summary>
    public class Building00
    {
        #region Constructor

        public Building00()
        {
            
        }

        #endregion

        #region  Properties

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private double _width;

        public double Width
        {
            get { return _width; }
            set { _width = value; }
        }

        private double _length;

        public double Length
        {
            get { return _length; }
            set { _length = value; }
        }

        private double _height;

        public double Height
        {
            get { return _height; }
            set { _height = value; }
        }

        private double _consumption;

        public double Consumption
        {
            get { return _consumption; }
            set { _consumption = value; }
        }

        private double _area;

        public double Area
        {
            get { return _area; }
            private set { _area = value; }
        }

        private double _volume;

        public double Volume
        {
            get { return _volume; }
            private set { _volume = value; }
        }

        private double _totalConsumption;

        public double TotalConsumption
        {
            get { return _totalConsumption; }
            private set { _totalConsumption = value; }
        }

        private double _totalConsumptionPer_m3;

        public double TotalConsumptionPer_m3
        {
            get { return _totalConsumptionPer_m3; }
            private set { _totalConsumptionPer_m3 = value; }
        }

        #endregion

        #region Methods

        private void Update_Area()
        {
            Area = Width * Length;
        }

        private void Update_Volume()
        {
            Volume = Area * Height;
        }

        private void Update_TotalConsumption()
        {
            TotalConsumption = Consumption * Area;
        }

        private void Update_TotalConsumptionPer_m3()
        {
            double consumptionPer_m3 = TotalConsumption / Volume;
            if (double.IsNaN(consumptionPer_m3))
            {
                TotalConsumptionPer_m3 = 0;
            }
            else
            {
                TotalConsumptionPer_m3 = consumptionPer_m3;
            }
        }

        #endregion
    }
}
