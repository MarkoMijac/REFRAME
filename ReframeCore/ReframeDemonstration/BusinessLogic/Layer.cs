using ReframeCore.ReactiveCollections;
using ReframeCoreFluentAPI;
using System;


namespace ReframeDemonstration.BusinessLogic
{
    public class Layer : ICollectionNodeItem
    {
        public string Name { get; set; }

        private double thickness;

        public double Thickness
        {
            get { return thickness; }
            set
            {
                thickness = value;
                this.Update();
            }
        }

        public event EventHandler UpdateTriggered;

        public Layer(double t)
        {

        }

        public Layer()
        {

        }
    }
}
