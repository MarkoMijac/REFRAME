using ReframeCore.ReactiveCollections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCoreExamples.E07_1
{
    public class Part2 : ICollectionNodeItem<Part2>
    {
        public event ReactiveCollectionEventHandler<Part2> UpdateTriggered;

        #region Properties

        public int Fixed { get; set; }
        public string Name { get; set; }
        public int A { get; set; }
        public int B { get; set; }
        public int C { get; set; }

        public Whole2 Whole { get; set; }

        #endregion

        #region Methods

        private void Update_A()
        {
            A = Fixed * Whole.CoeffA;
        }

        private void Update_B()
        {
            B = Fixed * Whole.CoeffB;
        }

        private void Update_C()
        {
            C = Fixed * Whole.CoeffC;
        }

        #endregion
    }
}
