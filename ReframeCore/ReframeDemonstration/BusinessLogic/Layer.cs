using ReframeCore.ReactiveCollections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                DependencyManager.DefaultGraph.PerformUpdate(this, "Thickness");
            }
        }


        public event EventHandler UpdateTriggered;
    }
}
