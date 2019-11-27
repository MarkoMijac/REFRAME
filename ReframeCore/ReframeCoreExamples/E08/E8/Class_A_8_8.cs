using ReframeCore;
using ReframeCore.Helpers;
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
        private Updater _updater;

        private int _a;

        public int A
        {
            get { return _a; }
            set
            {
                _a = value;
                if (_updater != null)
                {
                    _updater.PerformUpdate(this, nameof(A));
                }
                else
                {
                    Graph.PerformUpdate(this, "A");
                }
            }
        }


        public int B { get; set; }

        public Class_A_8_8()
        {

        }

        public Class_A_8_8(Updater updater)
        {
            _updater = updater;
        }

        private void Update_B()
        {
            B = A + 1;
        }
    }
}
