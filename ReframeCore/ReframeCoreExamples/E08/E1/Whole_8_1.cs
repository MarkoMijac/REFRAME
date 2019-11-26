using ReframeCore;
using ReframeCore.Factories;
using ReframeCore.Helpers;
using ReframeCore.Nodes;
using ReframeCore.ReactiveCollections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCoreExamples.E08.E1
{
    public class Whole_8_1
    {
        private Updater updater;

        #region Properties

        public ReactiveCollection<Part_8_1> Parts { get; set; }

        public string Name { get; set; }
        public int A { get; set; }
        public int B { get; set; }
        public int C { get; set; }
        public int D { get; set; }

        private int _coeffA;

        public int CoeffA
        {
            get { return _coeffA; }
            set
            {
                _coeffA = value;
                updater.PerformUpdate(this, "CoeffA");
            }
        }

        private int _coeffB;

        public int CoeffB
        {
            get { return _coeffB; }
            set
            {
                _coeffB = value;
                updater.PerformUpdate(this, "CoeffB");
            }
        }

        private int _coeffC;

        public int CoeffC
        {
            get
            {
                return _coeffC;
            }
            set
            {
                _coeffC = value;
                updater.PerformUpdate(this, "CoeffC");
            }
        }

        #endregion

        #region Methods

        public Whole_8_1(Updater updater)
        {
            this.updater = updater;
            CoeffA = 1;
            CoeffB = 2;
            CoeffC = 3;

            Parts = new ReactiveCollection<Part_8_1>();
            Parts.Add(new Part_8_1(this.updater) { Name = "Part 1", A = 1, B = 2, C = 3 });
            Parts.Add(new Part_8_1(this.updater) { Name = "Part 2", A = 4, B = 5, C = 6 });
            Parts.Add(new Part_8_1(this.updater) { Name = "Part 3", A = 7, B = 8, C = 9 });
        }

        private void Update_A()
        {
            A = 0;
            foreach (var p in Parts)
            {
                A += p.A;
            }

            A = CoeffA * A;
        }

        private void Update_B()
        {
            B = 0;
            foreach (var p in Parts)
            {
                B += p.B;
            }

            B = CoeffB * B + A;
        }

        private void Update_C()
        {
            C = 0;
            foreach (var p in Parts)
            {
                C += p.C;
            }

            C = CoeffC * C + B;
        }

        private void Update_D()
        {
            D = B + C;
        }

        #endregion

    }
}
