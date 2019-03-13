﻿using ReframeCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCoreExamples.E08.E8
{
    public class Class_A_8_8
    {
        public IDependencyGraph Graph { get; set; }

        private int _a;

        public int A
        {
            get { return _a; }
            set
            {
                _a = value;
                Graph.PerformUpdate(this, "A");
            }
        }


        public int B { get; set; }

        public Class_A_8_8()
        {

        }

        private void Update_B()
        {
            B = A + 1;
        }
    }
}
