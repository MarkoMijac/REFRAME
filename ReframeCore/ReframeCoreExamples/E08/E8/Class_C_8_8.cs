using ReframeCore;
using ReframeCore.FluentAPI;
using ReframeCore.Helpers;
using ReframeCore.ReactiveCollections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCoreExamples.E08.E8
{
    public class Class_C_8_8
    {
        private int _a;

        public int A
        {
            get { return _a; }
            set
            {
                _a = value;
            }
        }

        public int B { get; set; }

        public ReactiveCollection<Class_B_8_8> PartsB { get; set; }

        public Class_C_8_8()
        {
            PartsB = new ReactiveCollection<Class_B_8_8>();
        }

        private void Update_A()
        {
            A = 10;
            foreach (var item in PartsB)
            {
                A += item.A;
            }
        }

        private void Update_B()
        {
            B = A;
            foreach (var item in PartsB)
            {
                B += item.B;
            }
        }
    }
}
