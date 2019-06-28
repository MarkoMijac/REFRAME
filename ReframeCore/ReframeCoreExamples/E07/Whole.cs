using ReframeCore.ReactiveCollections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCoreExamples.E07
{
    public class Whole
    {
        #region Properties

        public ReactiveCollection<Part> Parts { get; set; }

        public string Name { get; set; }
        public int A { get; set; }
        public int B { get; set; }
        public int C { get; set; }

        #endregion

        #region Methods

        public Whole()
        {
            Parts = new ReactiveCollection<Part>();
            Parts.Add(new Part { Name = "Part 1", A = 1, B = 2, C = 3});
            Parts.Add(new Part { Name = "Part 2", A = 4, B = 5, C = 6 });
            Parts.Add(new Part { Name = "Part 3", A = 7, B = 8, C = 9 });
        }

        public void Update_A()
        {
            A = 0;
            foreach (var p in Parts)
            {
                A += p.A;
            }
        }

        private void Update_B()
        {
            B = 0;
            foreach (var p in Parts)
            {
                B += p.B;
            }
        }

        private void Update_C()
        {
            C = 0;
            foreach (var p in Parts)
            {
                C += p.C;
            }
        }

        #endregion

    }
}
