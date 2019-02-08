using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCoreExamples.E04
{
    public class Apartment04
    {
        #region Constructor

        public Apartment04()
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

        private double _heatedArea;

        public double HeatedArea
        {
            get { return _heatedArea; }
            set { _heatedArea = value; }
        }

        private double _height;

        public double Height
        {
            get { return _height; }
            set { _height = value; }
        }

        private double _heatedVolume;

        public double HeatedVolume
        {
            get { return _heatedVolume; }
            set { _heatedVolume = value; }
        }

        private double _consumption;

        public double Consumption
        {
            get { return _consumption; }
            set { _consumption = value; }
        }

        private double _totalConsumption;

        public double TotalConsumption
        {
            get { return _totalConsumption; }
            set { _totalConsumption = value; }
        }

        private AdditionalPart04 _balcony;

        public AdditionalPart04 Balcony
        {
            get { return _balcony; }
            set { _balcony = value; }
        }

        private AdditionalPart04 _basement;

        public AdditionalPart04 Basement
        {
            get { return _basement; }
            set { _basement = value; }
        }

        private double _totalArea;

        public double TotalArea
        {
            get { return _totalArea; }
            set { _totalArea = value; }
        }

        #endregion

        #region Methods

        private void Update_HeatedArea()
        {
            HeatedArea = Width * Length;
        }

        private void Update_HeatedVolume()
        {
            HeatedVolume = HeatedArea * Height;
        }

        private void Update_TotalConsumption()
        {
            TotalConsumption = Consumption * HeatedVolume;
        }

        private void Update_TotalArea()
        {
            TotalArea = HeatedArea + Balcony.UtilityArea + Basement.UtilityArea;
        }

        #endregion
    }
}
