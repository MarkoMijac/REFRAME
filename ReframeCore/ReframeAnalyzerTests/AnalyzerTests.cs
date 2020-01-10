using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeCore;
using ReframeCore.Helpers;
using ReframeAnalyzer;
using ReframeCoreExamples.E00;
using ReframeCore.FluentAPI;
using IPCServer;
using ReframeCore.Factories;
using ReframeCoreExamples.E09;
using ReframeAnalyzer.Graph;

namespace ReframeAnalyzerTests
{
    [TestClass]
    public class AnalyzerTests
    {
        [TestMethod]
        public void GetRegisteredReactors_GivenThereAreRegisteredReactors_ReturnsXmlRepresentation()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            ReactorRegistry.Instance.GetOrCreateReactor("R1");
            ReactorRegistry.Instance.GetOrCreateReactor("G2");
            //Act
            string xml = Analyzer.GetRegisteredReactors();

            //Assert
            Assert.AreNotEqual("", xml);
        }

        [TestMethod]
        public void GetRegisteredGraphs_GivenOneRegisteredGraph_ReturnsSerializedGraph()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            ReactorRegistry.Instance.GetOrCreateReactor("R1");
            ReactorRegistry.Instance.GetOrCreateReactor("G2");
            //Act
            string xml = Analyzer.GetRegisteredGraphs();

            //Assert
            Assert.AreNotEqual("", xml);
        }

        [TestMethod]
        public void GetGraphNodes_GivenValidGraph_ReturnsXmlWithNodes()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.GetOrCreateReactor("R1");

            GenericReactiveObject obj = new GenericReactiveObject();

            reactor.Let(() =>obj.A).DependOn(() =>obj.B, () =>obj.C);
            reactor.Let(() =>obj.C).DependOn(() =>obj.D, () =>obj.E);

            //Act
            string xml = Analyzer.GetGraphNodes(reactor.Graph);

            //Assert
            Assert.AreNotEqual("", xml);
        }

        [TestMethod]
        public void MyTestMethod()
        {
            string xml = @"<ServerCommand><RouterIdentifier>AnalyzerRouter</RouterIdentifier><CommandName>GetRegisteredGraphs</CommandName><Parameters/></ServerCommand>";

            string identifier = CommandRouter.GetRouterIdentifier(xml);

            Assert.AreEqual("AnalyzerRouter", identifier);
        }

        [TestMethod]
        public void MyTestMethod1()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.GetOrCreateReactor("R1");

            GenericReactiveObject obj = new GenericReactiveObject();

            reactor.Let(() => obj.A).DependOn(() => obj.B, () => obj.C);
            reactor.Let(() => obj.C).DependOn(() => obj.D, () => obj.E);

            string xml = @"<ServerCommand><RouterIdentifier>AnalyzerRouter</RouterIdentifier><CommandName>GetGraphNodes</CommandName><Parameters><Parameter><Name>GraphIdentifier</Name><Value>R1</Value></Parameter></Parameters></ServerCommand>";

            AnalyzerRouter router = new AnalyzerRouter();

            //Act
            string result = router.RouteCommand(xml);

            //Assert
            Assert.IsTrue(result != "");
        }

        [TestMethod]
        public void GetClassAnalysisGraph()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.GetOrCreateReactor("R1");

            ClassA objA = new ClassA();
            ClassB objB = new ClassB();
            ClassC objC = new ClassC();

            reactor.Let(() => objA.PA1)
                .DependOn(() => objB.PB1, () => objC.PC1);

            //Act
            string xml = Analyzer.GetClassAnalysisGraph(reactor.Graph);

            //Assert
            Assert.AreNotEqual("", xml);
        }

        //[TestMethod]
        public void GetClassMemberNodes()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.GetOrCreateReactor("R1");

            ClassA objA = new ClassA();
            ClassB objB = new ClassB();
            ClassC objC = new ClassC();

            reactor.Let(() => objA.PA1)
                .DependOn(() => objB.PB1, () => objC.PC1);


        }
    }
}
