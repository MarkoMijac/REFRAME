using ReframeCore.ReactiveCollections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCoreExamples.E07
{
    public class Part : ICollectionNodeItem
    {
        public event EventHandler UpdateTriggered;

        #region Properties

        public string Name { get; set; }
        public int A { get; set; }
        public int B { get; set; }
        public int C { get; set; }
        public int D { get; set; }

        #endregion

        #region Methods

        public void Update_A()
        {

        }

        private void Update_B()
        {

        }

        private void Update_C()
        {

        }

        #endregion
    }
}
