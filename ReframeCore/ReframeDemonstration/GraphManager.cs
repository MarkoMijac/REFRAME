using ReframeCore;
using ReframeCore.FluentAPI;
using ReframeDemonstration.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeDemonstration
{
    public static class GraphManager
    {
        public static IDependencyGraph DefaultGraph
        {
            get
            {
                return GraphFactory.GetOrCreate("DEFAULT_GRAPH");
            }
        }

        private static void CreateDependencies()
        {
            //SurfaceArea = f(Width, Height)
            foreach (var cPart in Repository.ConstructionParts)
            {
                DefaultGraph.Let(() => cPart.SurfaceArea).DependOn(() => cPart.Width, () => cPart.Height);
                DefaultGraph.Let(() => cPart.Thickness).DependOn(cPart.Layers, () =>cPart.Layers[0].Thickness);
            }
        }

        public static void Initialize()
        {
            CreateDependencies();

            DefaultGraph.Initialize();
            DefaultGraph.PerformUpdate();
        }
    }
}
