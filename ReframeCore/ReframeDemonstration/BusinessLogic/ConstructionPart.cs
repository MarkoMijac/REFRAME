using ReframeCore.ReactiveCollections;
using ReframeCoreFluentAPI;

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
                this.Update();
            }
        }

        private double width;

        public double Width
        {
            get { return width; }
            set
            {
                width = value;
                this.Update();
            }
        }

        public double SurfaceArea { get; set; }

        public double Thickness { get; set; }

        public ConstructionPart()
        {
            CreateDependencies();
        }

        public ConstructionPart(bool suspendUpdate)
            : this()
        {
            DependencyManager.DefaultReactor.SuspendUpdate();
            Width = 5;
            Height = 4.5;
            DependencyManager.DefaultReactor.ResumeUpdate();
        }

        public ConstructionPart(double w, double h)
            : this()
        {
            width = w;
            height = h;
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
            DependencyManager.DefaultReactor.Let(() => SurfaceArea).DependOn(() => Width, () => Height);
            DependencyManager.DefaultReactor.Let(() => Thickness).DependOn(Layers, () => Layers[0].Thickness);
        }
    }
}
