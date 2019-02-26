using ReframeCore.ReactiveCollections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCoreExamples.E07_1
{
    public class Whole2
    {
        #region Properties

        public string Name { get; set; }
        public int CoeffA { get; set; }
        public int CoeffB { get; set; }
        public int CoeffC { get; set; }

        public ReactiveCollection<Part2> Parts { get; set; }

        #endregion

        #region Methods

        public Whole2()
        {
            CoeffA = 2;
            CoeffB = 4;
            CoeffC = 6;

            Parts = new ReactiveCollection<Part2>();
            Parts.Add(new Part2 { Name = "P1", Fixed = 1, Whole = this });
            Parts.Add(new Part2 { Name = "P2", Fixed = 2, Whole = this });
            Parts.Add(new Part2 { Name = "P3", Fixed = 3, Whole = this });
        }

        #endregion
    }
}
