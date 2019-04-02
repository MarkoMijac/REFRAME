using ReframeCore.ReactiveCollections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCoreExamples.E00
{
    public class GenericReactiveObject3
    : ICollectionNodeItem
    {
        private int timeOut = 0;

        #region Properties

        public int A { get; set; }
        public int B { get; set; }
        public int C { get; set; }
        public int D { get; set; }
        public int E { get; set; }
        public int F { get; set; }
        public int G { get; set; }
        public int H { get; set; }
        public int I { get; set; }
        public int J { get; set; }
        public int K { get; set; }
        public int L { get; set; }
        public int M { get; set; }
        public int BadNode { get; set; }

        public event EventHandler UpdateTriggered;

        #endregion

        #region Methods

        private void Update_A()
        {
            Task.Delay(timeOut).Wait();
        }

        private void Update_B()
        {
            Task.Delay(timeOut).Wait();
        }

        private void Update_C()
        {
            Task.Delay(timeOut).Wait();
        }

        private void Update_D()
        {
            Task.Delay(timeOut).Wait();
        }

        private void Update_E()
        {
            Task.Delay(timeOut).Wait();
        }

        private void Update_F()
        {
            Task.Delay(timeOut).Wait();
        }

        private void Update_G()
        {
            Task.Delay(timeOut).Wait();
        }

        private void Update_H()
        {
            Task.Delay(timeOut).Wait();
        }

        private void Update_I()
        {
            Task.Delay(timeOut).Wait();
        }

        private void Update_J()
        {
            Task.Delay(timeOut).Wait();
        }

        private void Update_K()
        {
            Task.Delay(timeOut).Wait();
        }

        private void Update_L()
        {
            Task.Delay(timeOut).Wait();
        }

        private void Update_M()
        {
            Task.Delay(timeOut).Wait();
        }

        private void Update_BadNode()
        {
            throw new NullReferenceException();
        }

        #endregion
    }
}
