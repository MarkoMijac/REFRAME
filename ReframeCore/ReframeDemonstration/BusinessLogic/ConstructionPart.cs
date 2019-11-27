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
                DependencyManager.Updater.PerformUpdate(this, "Height");
            }
        }

        private double width;

        public double Width
        {
            get { return width; }
            set
            {
                width = value;
                DependencyManager.Updater.PerformUpdate(this, "Width");
            }
        }

        public double SurfaceArea { get; set; }

        public double Thickness { get; set; }

        public ConstructionPart()
        {
            CreateDependencies();
        }

        public ConstructionPart(double w, double h)
            : this()
        {
            width = w;
            height = h;
        }

        public ConstructionPart(bool suspendUpdate)
            : this()
        {
            DependencyManager.Updater.UpdateSuspended = true;
            Width = 5;
            Height = 4.5;
            DependencyManager.Updater.UpdateSuspended = true; = false;
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

        private void CreateDependencies()
        {
            DependencyManager.DefaultGraph.Let(() => SurfaceArea).DependOn(() => Width, () => Height);
            DependencyManager.DefaultGraph.Let(() => Thickness).DependOn(Layers, () => Layers[0].Thickness);
        }
    }
}
