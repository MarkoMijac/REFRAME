using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCoreExamples.E04
{
    public class AdditionalPart04
    {
        #region Properties

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

        private double _area;

        public double Area
        {
            get { return _area; }
            set { _area = value; }
        }

        private double _utilCoeff;

        public double UtilCoeff
        {
            get { return _utilCoeff; }
            set { _utilCoeff = value; }
        }

        private double _utilityArea;

        public double UtilityArea
        {
            get { return _utilityArea; }
            set { _utilityArea = value; }
        }


        #endregion

        #region Constructor

        public AdditionalPart04()
        {

        }

        #endregion

        #region Methods

        private void Update_Area()
        {
            Area = Width * Length;
        }

        private void Update_UtilityArea()
        {
            UtilityArea = Area * UtilCoeff;
        }

        public override string ToString()
        {
            return Name;
        }

        #endregion
    }
}
