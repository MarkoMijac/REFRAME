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
    public static class DependencyManager
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
            foreach (var cPart in Repository.ConstructionParts)
            {
                CreateDependencies(cPart);
            }
        }

        public static void CreateDependencies(ConstructionPart part)
        {
            DefaultGraph.Let(() => part.SurfaceArea).DependOn(() => part.Width, () => part.Height);
            DefaultGraph.Let(() => part.Thickness).DependOn(part.Layers, () => part.Layers[0].Thickness);
        }

        public static void Initialize()
        {
            CreateDependencies();

            DefaultGraph.Initialize();
            DefaultGraph.PerformUpdate();
        }
    }
}
