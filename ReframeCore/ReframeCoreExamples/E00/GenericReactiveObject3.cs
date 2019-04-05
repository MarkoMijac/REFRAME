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

        public void Update_A()
        {
            Task.Delay(timeOut).Wait();
            A = C + E;
        }

        private void Update_B()
        {
            Task.Delay(timeOut).Wait();
            B = C + 1;
        }

        private void Update_C()
        {
            Task.Delay(timeOut).Wait();
            C = 1;
        }

        private void Update_D()
        {
            Task.Delay(timeOut).Wait();
            D = A + B + E;
        }

        private void Update_E()
        {
            Task.Delay(timeOut).Wait();
            E = 1;
        }

        private void Update_F()
        {
            Task.Delay(timeOut).Wait();
            F = E + 1;
        }

        private void Update_G()
        {
            Task.Delay(timeOut).Wait();
            G = D + 1;
        }

        private void Update_H()
        {
            Task.Delay(timeOut).Wait();
            H = D + 1;
        }

        private void Update_I()
        {
            Task.Delay(timeOut).Wait();
            I = G + H;
        }

        private void Update_J()
        {
            Task.Delay(timeOut).Wait();
            J = F + K + H;
        }

        private void Update_K()
        {
            Task.Delay(timeOut).Wait();
            K = F + 1;
        }

        private void Update_L()
        {
            Task.Delay(timeOut).Wait();
            L = I + J;
        }

        private void Update_M()
        {
            Task.Delay(timeOut).Wait();
            M = J + 1;
        }

        private void Update_BadNode()
        {
            throw new NullReferenceException();
        }

        #endregion
    }
}
