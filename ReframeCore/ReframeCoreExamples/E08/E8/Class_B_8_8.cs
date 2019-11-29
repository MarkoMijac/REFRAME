using ReframeCore;
using ReframeCore.Helpers;
using ReframeCore.ReactiveCollections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCoreExamples.E08.E8
{
    public class Class_B_8_8: ICollectionNodeItem
    {
        public event EventHandler UpdateTriggered;

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

        public Class_A_8_8 PartA { get; set; }

        private void Update_A()
        {
            A = 10 + PartA.A;
        }

        private void Update_B()
        {
            B = A + PartA.B;
        }

        public Class_B_8_8()
        {

        }
    }
}
