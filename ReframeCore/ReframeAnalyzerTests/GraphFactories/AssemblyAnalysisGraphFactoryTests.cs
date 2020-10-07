using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeAnalyzer;
using ReframeAnalyzer.Exceptions;
using ReframeAnalyzer.GraphFactories;
using ReframeCore;
using ReframeCore.Factories;
using ReframeCoreExamples.E09;
using ReframeCoreExamples.E09._01;
using ReframeExporter;

namespace ReframeAnalyzerTests.GraphFactories
{
    [TestClass]
    public class AssemblyAnalysisGraphFactoryTests
    {
        #region General

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void CreateGraph_XmlSourceIsEmpty_ThrowsException()
        {
            //Arrange
            var factory = new AssemblyAnalysisGraphFactory();

            //Act
            var analysisGraph = factory.CreateGraph("");
        }

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void CreateGraph_XmlSourceNotValid_ThrowsException()
        {
            //Arrange
            var factory = new AssemblyAnalysisGraphFactory();

            //Act
            var analysisGraph = factory.CreateGraph("<not valid source?_");
        }

        [TestMethod]
        public void CreateGraph_GivenEmptyDependencyGraph_ReturnsEmptyAnalysisGraph()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("R1");
            var factory = new AssemblyAnalysisGraphFactory();

            //Act
            var xmlExporter = new XmlReactorDetailExporter(reactor.Identifier);
            string xmlSource = xmlExporter.Export();
            var analysisGraph = factory.CreateGraph(xmlSource);

            //Assert
            Assert.IsTrue(analysisGraph != null && analysisGraph.Nodes.Count == 0);
        }

        [TestMethod]
        public void CreateGraph_GivenAssemblyLevel_ReturnsAssemblyAnalysisGraph()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("R1");
            var factory = new AssemblyAnalysisGraphFactory();

            //Act
            var xmlExporter = new XmlReactorDetailExporter(reactor.Identifier);
            string xmlSource = xmlExporter.Export();
            var analysisGraph = factory.CreateGraph(xmlSource);

            //Assert
            Assert.IsTrue(analysisGraph.AnalysisLevel == AnalysisLevel.AssemblyLevel);
        }

        [TestMethod]
        public void CreateGraph_GivenScenario1_ReturnsAssemblyAnalysisGraphWithCorrectNodes()
        {
            //Arrange
            IReactor reactor = AnalysisTestHelper.CreateCase1();

            //Act
            var factory = new AssemblyAnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource);

            //Assert
            Assert.IsTrue(analysisGraph.Nodes.Count == 1);

            var firstNode = analysisGraph.Nodes[0];

            Assert.IsTrue(firstNode.Name == typeof(ClassA).Assembly.ManifestModule.Name);
        }

        [TestMethod]
        public void CreateGraph_GivenScenario1_ReturnsAssemblyAnalysisGraphWithCorrectDependencies()
        {
            //Arrange
            IReactor reactor = AnalysisTestHelper.CreateCase1();

            //Act
            var factory = new AssemblyAnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource);

            //Assert
            var firstNode = analysisGraph.Nodes[0];

            Assert.IsTrue(firstNode.Predecessors.Count == 1 && firstNode.HasPredecessor(firstNode));
            Assert.IsTrue(firstNode.Successors.Count == 1 && firstNode.HasSuccessor(firstNode));
        }

        [TestMethod]
        public void CreateGraph_GivenScenario2_ReturnsAssemblyAnalysisGraphWithCorrectNodes()
        {
            //Arrange
            IReactor reactor = AnalysisTestHelper.CreateCase2();

            //Act
            var factory = new AssemblyAnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource);

            //Assert
            Assert.IsTrue(analysisGraph.Nodes.Count == 1);

            var firstNode = analysisGraph.Nodes[0];

            Assert.IsTrue(firstNode.Name == typeof(ClassG).Assembly.ManifestModule.Name);
        }

        [TestMethod]
        public void CreateGraph_GivenScenario2_ReturnsAssemblyAnalysisGraphWithCorrectDependencies()
        {
            //Arrange
            IReactor reactor = AnalysisTestHelper.CreateCase2();

            //Act
            var factory = new AssemblyAnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource);

            //Assert
            var firstNode = analysisGraph.Nodes[0];

            Assert.IsTrue(firstNode.Predecessors.Count == 1);
            Assert.IsTrue(firstNode.HasPredecessor(firstNode));

            Assert.IsTrue(firstNode.Successors.Count == 1);
            Assert.IsTrue(firstNode.HasSuccessor(firstNode));
        }

        [TestMethod]
        public void CreateGraph_GivenScenario3_ReturnsAssemblyAnalysisGraphWithCorrectNodes()
        {
            //Arrange
            IReactor reactor = AnalysisTestHelper.CreateCase3();

            //Act
            var factory = new AssemblyAnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource);

            //Assert
            Assert.IsTrue(analysisGraph.Nodes.Count == 1);

            var firstNode = analysisGraph.Nodes[0];

            Assert.IsTrue(firstNode.Name == typeof(ClassA).Assembly.ManifestModule.Name);
        }

        [TestMethod]
        public void CreateGraph_GivenScenario3_ReturnsAssemblyAnalysisGraphWithCorrectDependencies()
        {
            //Arrange
            IReactor reactor = AnalysisTestHelper.CreateCase3();

            //Act
            var factory = new AssemblyAnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource);

            //Assert
            var firstNode = analysisGraph.Nodes[0];

            Assert.IsTrue(firstNode.Predecessors.Count == 1 && firstNode.HasPredecessor(firstNode));
            Assert.IsTrue(firstNode.Successors.Count == 1 && firstNode.HasSuccessor(firstNode));
        }

        [TestMethod]
        public void CreateGraph_GivenScenario4_ReturnsAssemblyAnalysisGraphWithCorrectNodes()
        {
            //Arrange
            var reactor = AnalysisTestHelper.CreateCase4();

            //Act
            var factory = new AssemblyAnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource);

            //Assert
            Assert.IsTrue(analysisGraph.Nodes.Count == 1);

            var firstNode = analysisGraph.Nodes[0];

            Assert.IsTrue(firstNode.Name == typeof(ClassA).Assembly.ManifestModule.Name);
        }

        [TestMethod]
        public void CreateGraph_GivenScenario4_ReturnsAssemblyAnalysisGraphWithCorrectDependencies()
        {
            //Arrange
            var reactor = AnalysisTestHelper.CreateCase4();

            //Act
            var factory = new AssemblyAnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource);

            //Assert
            var firstNode = analysisGraph.Nodes[0];

            Assert.IsTrue(firstNode.Predecessors.Count == 1 && firstNode.HasPredecessor(firstNode));
            Assert.IsTrue(firstNode.Successors.Count == 1 && firstNode.HasSuccessor(firstNode));
        }

        [TestMethod]
        public void CreateGraph_GivenScenario5_ReturnsAssemblyAnalysisGraphWithCorrectNodes()
        {
            //Arrange
            var reactor = AnalysisTestHelper.CreateCase5();
            var factory = new AssemblyAnalysisGraphFactory();

            //Act
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource);

            //Assert
            Assert.IsTrue(analysisGraph.Nodes.Count == 1);

            var firstNode = analysisGraph.Nodes[0];

            Assert.IsTrue(firstNode.Name == typeof(ClassA).Assembly.ManifestModule.Name);
        }

        [TestMethod]
        public void CreateGraph_GivenScenario5_ReturnsAssemblyAnalysisGraphWithCorrectDependencies()
        {
            //Arrange
            var reactor = AnalysisTestHelper.CreateCase5();
            var factory = new AssemblyAnalysisGraphFactory();

            //Act
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource);

            //Assert
            var firstNode = analysisGraph.Nodes[0];

            Assert.IsTrue(firstNode.Name == typeof(ClassA).Assembly.ManifestModule.Name);

            Assert.IsTrue(firstNode.Predecessors.Count == 1 && firstNode.HasPredecessor(firstNode));
            Assert.IsTrue(firstNode.Successors.Count == 1 && firstNode.HasSuccessor(firstNode));
        }

        #endregion
    }
}
