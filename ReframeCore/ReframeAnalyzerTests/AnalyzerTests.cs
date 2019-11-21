using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeCore;
using ReframeCore.Helpers;
using ReframeAnalyzer;
using ReframeCoreExamples.E00;
using ReframeCore.FluentAPI;
using IPCServer;
using ReframeCore.Factories;

namespace ReframeAnalyzerTests
{
    [TestClass]
    public class AnalyzerTests
    {
        [TestMethod]
        public void GetRegisteredGraphs_GivenOneRegisteredGraph_ReturnsSerializedGraph()
        {
            //Arrange
            GraphRegistry.Clear();
            GraphRegistry.GetOrCreate("G1");
            GraphRegistry.GetOrCreate("G2");
            //Act
            string xml = Analyzer.GetRegisteredGraphs();

            //Assert
            Assert.AreNotEqual("", xml);
        }

        [TestMethod]
        public void GetGraphNodes_GivenValidGraph_ReturnsXmlWithNodes()
        {
            //Arrange
            GraphRegistry.Clear();
            var graph = GraphRegistry.GetOrCreate("G1");

            GenericReactiveObject obj = new GenericReactiveObject();

            graph.Let(() =>obj.A).DependOn(() =>obj.B, () =>obj.C);
            graph.Let(() =>obj.C).DependOn(() =>obj.D, () =>obj.E);

            //Act
            string xml = Analyzer.GetGraphNodes(graph);

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
            string xml = @"<ServerCommand><RouterIdentifier>AnalyzerRouter</RouterIdentifier><CommandName>GetGraphNodes</CommandName><Parameters><Parameter><Name>GraphIdentifier</Name><Value>Graph-Floors</Value></Parameter></Parameters></ServerCommand>";

            AnalyzerRouter router = new AnalyzerRouter();
            string result = router.RouteCommand(xml);

            Assert.IsTrue(result != "");
        }

        [TestMethod]
        public void GetClassNodes()
        {
            //Arrange
            GraphRegistry.Clear();
            var graph = GraphRegistry.GetOrCreate("G1");

            GenericReactiveObject obj = new GenericReactiveObject();

            graph.Let(() => obj.A).DependOn(() => obj.B, () => obj.C);
            graph.Let(() => obj.C).DependOn(() => obj.D, () => obj.E);

            //Act
            string xml = Analyzer.GetClassNodes(graph);

            //Assert
            Assert.AreNotEqual("", xml);
        }
    }
}
