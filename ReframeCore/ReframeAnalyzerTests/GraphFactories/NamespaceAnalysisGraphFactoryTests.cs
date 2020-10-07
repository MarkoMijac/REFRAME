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
    public class NamespaceAnalysisGraphFactoryTests
    {
        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void CreateGraph_XmlSourceIsEmpty_ThrowsException()
        {
            //Arrange
            var factory = new NamespaceAnalysisGraphFactory();

            //Act
            var analysisGraph = factory.CreateGraph("");
        }

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void CreateGraph_XmlSourceNotValid_ThrowsException()
        {
            //Arrange
            var factory = new NamespaceAnalysisGraphFactory();

            //Act
            var analysisGraph = factory.CreateGraph("<not valid source?_");
        }

        [TestMethod]
        public void CreateGraph_GivenEmptyDependencyGraph_ReturnsEmptyAnalysisGraph()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("R1");
            var factory = new NamespaceAnalysisGraphFactory();

            //Act
            var xmlExporter = new XmlReactorDetailExporter(reactor.Identifier);
            string xmlSource = xmlExporter.Export();
            var analysisGraph = factory.CreateGraph(xmlSource);

            //Assert
            Assert.IsTrue(analysisGraph != null && analysisGraph.Nodes.Count == 0);
        }

        [TestMethod]
        public void CreateGraph_GivenNamespaceLevel_ReturnsNamespaceAnalysisGraph()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("R1");
            var factory = new NamespaceAnalysisGraphFactory();

            //Act
            var xmlExporter = new XmlReactorDetailExporter(reactor.Identifier);
            string xmlSource = xmlExporter.Export();
            var analysisGraph = factory.CreateGraph(xmlSource);

            //Assert
            Assert.IsTrue(analysisGraph.AnalysisLevel == AnalysisLevel.NamespaceLevel);
        }

        [TestMethod]
        public void CreateGraph_GivenScenario1_ReturnsNamespaceAnalysisGraphWithCorrectNodes()
        {
            //Arrange
            IReactor reactor = AnalysisTestHelper.CreateCase1();

            //Act
            var factory = new NamespaceAnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource);

            //Assert
            Assert.IsTrue(analysisGraph.Nodes.Count == 1);

            var firstNode = analysisGraph.Nodes[0];

            Assert.IsTrue(firstNode.Name == typeof(ClassA).Namespace);
        }

        [TestMethod]
        public void CreateGraph_GivenScenario1_ReturnsNamespaceAnalysisGraphWithCorrectDependencies()
        {
            //Arrange
            IReactor reactor = AnalysisTestHelper.CreateCase1();

            //Act
            var factory = new NamespaceAnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource);

            //Assert
            var firstNode = analysisGraph.Nodes[0];

            Assert.IsTrue(firstNode.Predecessors.Count == 1 && firstNode.HasPredecessor(firstNode));
            Assert.IsTrue(firstNode.Successors.Count == 1 && firstNode.HasSuccessor(firstNode));
        }

        [TestMethod]
        public void CreateGraph_GivenScenario2_ReturnsNamespaceAnalysisGraphWithCorrectNodes()
        {
            //Arrange
            IReactor reactor = AnalysisTestHelper.CreateCase2();

            //Act
            var factory = new NamespaceAnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource);

            //Assert
            Assert.IsTrue(analysisGraph.Nodes.Count == 2);

            var firstNode = analysisGraph.Nodes[0];
            var secondNode = analysisGraph.Nodes[1];

            Assert.IsTrue(firstNode.Name == typeof(ClassG).Namespace);
            Assert.IsTrue(secondNode.Name == typeof(ClassB).Namespace);
        }

        [TestMethod]
        public void CreateGraph_GivenScenario2_ReturnsNamespaceAnalysisGraphWithCorrectDependencies()
        {
            //Arrange
            IReactor reactor = AnalysisTestHelper.CreateCase2();

            //Act
            var factory = new NamespaceAnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource);

            //Assert
            var firstNode = analysisGraph.Nodes[0];
            var secondNode = analysisGraph.Nodes[1];

            Assert.IsTrue(firstNode.Predecessors.Count == 2);
            Assert.IsTrue(firstNode.HasPredecessor(firstNode) && firstNode.HasPredecessor(secondNode));

            Assert.IsTrue(firstNode.Successors.Count == 1 && firstNode.HasSuccessor(firstNode));
            Assert.IsTrue(secondNode.Successors.Count == 2 && secondNode.HasSuccessor(firstNode) && secondNode.HasSuccessor(secondNode));
            Assert.IsTrue(secondNode.Predecessors.Count == 1 && secondNode.HasPredecessor(secondNode));
        }

        [TestMethod]
        public void CreateGraph_GivenScenario3_ReturnsNamespaceAnalysisGraphWithCorrectNodes()
        {
            //Arrange
            IReactor reactor = AnalysisTestHelper.CreateCase3();

            //Act
            var factory = new NamespaceAnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource);

            //Assert
            Assert.IsTrue(analysisGraph.Nodes.Count == 1);

            var firstNode = analysisGraph.Nodes[0];

            Assert.IsTrue(firstNode.Name == typeof(ClassA).Namespace);
        }

        [TestMethod]
        public void CreateGraph_GivenScenario3_ReturnsNamespaceAnalysisGraphWithCorrectDependencies()
        {
            //Arrange
            IReactor reactor = AnalysisTestHelper.CreateCase3();

            //Act
            var factory = new NamespaceAnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource);

            //Assert
            var firstNode = analysisGraph.Nodes[0];

            Assert.IsTrue(firstNode.Predecessors.Count == 1 && firstNode.HasPredecessor(firstNode));
            Assert.IsTrue(firstNode.Successors.Count == 1 && firstNode.HasSuccessor(firstNode));
        }

        [TestMethod]
        public void CreateGraph_GivenScenario4_ReturnsNamespaceAnalysisGraphWithCorrectNodes()
        {
            //Arrange
            var reactor = AnalysisTestHelper.CreateCase4();

            //Act
            var factory = new NamespaceAnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource);

            //Assert
            Assert.IsTrue(analysisGraph.Nodes.Count == 1);

            var firstNode = analysisGraph.Nodes[0];

            Assert.IsTrue(firstNode.Name == typeof(ClassA).Namespace);
        }

        [TestMethod]
        public void CreateGraph_GivenScenario4_ReturnsNamespaceAnalysisGraphWithCorrectDependencies()
        {
            //Arrange
            var reactor = AnalysisTestHelper.CreateCase4();

            //Act
            var factory = new NamespaceAnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource);

            //Assert
            var firstNode = analysisGraph.Nodes[0];

            Assert.IsTrue(firstNode.Predecessors.Count == 1 && firstNode.HasPredecessor(firstNode));
            Assert.IsTrue(firstNode.Successors.Count == 1 && firstNode.HasSuccessor(firstNode));
        }

        [TestMethod]
        public void CreateGraph_GivenScenario5_ReturnsNamespaceAnalysisGraphWithCorrectNodes()
        {
            //Arrange
            var reactor = AnalysisTestHelper.CreateCase5();
            var factory = new NamespaceAnalysisGraphFactory();

            //Act
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource);

            //Assert
            Assert.IsTrue(analysisGraph.Nodes.Count == 1);

            var firstNode = analysisGraph.Nodes[0];

            Assert.IsTrue(firstNode.Name == typeof(ClassA).Namespace);
        }

        [TestMethod]
        public void CreateGraph_GivenScenario5_ReturnsNamespaceAnalysisGraphWithCorrectDependencies()
        {
            //Arrange
            var reactor = AnalysisTestHelper.CreateCase5();
            var factory = new NamespaceAnalysisGraphFactory();

            //Act
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource);

            //Assert
            var firstNode = analysisGraph.Nodes[0];

            Assert.IsTrue(firstNode.Name == typeof(ClassA).Namespace);

            Assert.IsTrue(firstNode.Predecessors.Count == 1 && firstNode.HasPredecessor(firstNode));
            Assert.IsTrue(firstNode.Successors.Count == 1 && firstNode.HasSuccessor(firstNode));
        }
    }
}
