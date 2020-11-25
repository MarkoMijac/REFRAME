using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeCore;
using ReframeCore.Factories;
using ReframeCoreFluentAPI;

namespace ReframeCoreTests
{
    [TestClass]
    public class ReactorTests
    {
        private float[] _fim = new float[12];
        public float[] Fim
        {
            get { return _fim; }
        }

        public void Azuriraj_Fim()
        {

        }

        public int A { get; set; }

        [TestMethod]
        public void PerformUpdate_()
        {
            var reactor = ReactorRegistry.Instance.GetOrCreateReactor("RTest");
            ((reactor.Graph as DependencyGraph).NodeFactory as StandardNodeFactory).UpdateMethodNamePrefix = "Azuriraj_";
            reactor.Let(() => Fim).DependOn(() => A);
        }
    }
}
