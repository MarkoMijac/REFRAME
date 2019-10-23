using ReframeCore;
using ReframeCore.FluentAPI;
using ReframeCore.ReactiveCollections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeDemonstration.BusinessLogic
{
    public class ConstructionPart
    {
        public string Name { get; set; }
        private double height;
        public ReactiveCollection<Layer> Layers { get; set; } = new ReactiveCollection<Layer>();

        public double Height
        {
            get { return height; }
            set
            {
                height = value;
                DependencyManager.DefaultGraph.PerformUpdate(this, "Height");
            }
        }

        private double width;

        public double Width
        {
            get { return width; }
            set
            {
                width = value;
                DependencyManager.DefaultGraph.PerformUpdate(this, "Width");
            }
        }



        public double SurfaceArea { get; set; }

        public double Thickness { get; set; }

        public ConstructionPart()
        {
            Layers.Add(new Layer() { Name="L1", Thickness = 2});
            Layers.Add(new Layer() { Name = "L2", Thickness = 3 });
            Layers.Add(new Layer() { Name = "L3", Thickness = 4 });
        }

        private void Update_SurfaceArea()
        {
            SurfaceArea = Width * Height;
        }

        private void Update_Thickness()
        {
            double result = 0;
            foreach (var layer in Layers)
            {
                result += layer.Thickness;
            }

            Thickness = result;
        }
    }
}
