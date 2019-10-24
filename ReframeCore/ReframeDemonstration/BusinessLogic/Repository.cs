using ReframeCore.ReactiveCollections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeDemonstration.BusinessLogic
{
    static class Repository
    {
        public static List<ConstructionPart> ConstructionParts { get; set; } = new List<ConstructionPart>();

        public static ReactiveCollection<ConstructionPart> ConstructionPartsReact { get; set; } = new ReactiveCollection<ConstructionPart>();

        private static void LoadConstructionParts()
        {
            ConstructionParts.Add(new ConstructionPart { Name = "CP1", Height = 2.8, Width = 10});
            ConstructionParts.Add(new ConstructionPart { Name = "CP2", Height = 3.2, Width = 11 });
            ConstructionParts.Add(new ConstructionPart { Name = "CP3", Height = 2.5, Width = 12 });
            ConstructionParts.Add(new ConstructionPart { Name = "CP4", Height = 2.2, Width = 13 });
            ConstructionParts.Add(new ConstructionPart { Name = "CP5", Height = 2.3, Width = 14 });
            ConstructionParts.Add(new ConstructionPart { Name = "CP6", Height = 2.5, Width = 15 });
        }

        public static void LoadData()
        {
            LoadConstructionParts();
        }
    }
}
