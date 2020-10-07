using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeAnalyzer.Exceptions;
using ReframeAnalyzer.Graph;
using ReframeCore;
using ReframeCore.Factories;
using ReframeCoreExamples.E09;
using ReframeExporter;

namespace ReframeAnalyzerTests.GraphFactories
{
    [TestClass]
    public class ObjectAnalysisGraphFactoryTests
    {
        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void CreateGraph_XmlSourceIsEmpty_ThrowsException()
        {
            //Arrange
            var factory = new ObjectAnalysisGraphFactory();

            //Act
            var analysisGraph = factory.CreateGraph("");
        }

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void CreateGraph_XmlSourceNotValid_ThrowsException()
        {
            //Arrange
            var factory = new ObjectAnalysisGraphFactory();

            //Act
            var analysisGraph = factory.CreateGraph("<not valid source?_");
        }

        [TestMethod]
        public void CreateGraph_GivenEmptyDependencyGraph_ReturnsEmptyAnalysisGraph()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("R1");
            var factory = new ObjectAnalysisGraphFactory();

            //Act
            var xmlExporter = new XmlReactorDetailExporter(reactor.Identifier);
            string xmlSource = xmlExporter.Export();
            var analysisGraph = factory.CreateGraph(xmlSource);

            //Assert
            Assert.IsTrue(analysisGraph != null && analysisGraph.Nodes.Count == 0);
        }

        [TestMethod]
        public void CreateGraph_GivenObjectLevel_ReturnsObjectAnalysisGraph()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("R1");
            var factory = new ObjectAnalysisGraphFactory();

            //Act
            var xmlExporter = new XmlReactorDetailExporter(reactor.Identifier);
            string xmlSource = xmlExporter.Export();
            var analysisGraph = factory.CreateGraph(xmlSource);

            //Assert
            Assert.IsTrue(analysisGraph.AnalysisLevel == AnalysisLevel.ObjectLevel);
        }

        [TestMethod]
        public void CreateGraph_GivenScenario1_ReturnsObjectAnalysisGraphWithCorrectNodes()
        {
            //Arrange
            IReactor reactor = AnalysisTestHelper.CreateCase1();

            //Act
            var factory = new ObjectAnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource);

            //Assert
            Assert.IsTrue(analysisGraph.Nodes.Count == 3);

            var firstNode = analysisGraph.Nodes[0];
            var secondNode = analysisGraph.Nodes[1];
            var thirdNode = analysisGraph.Nodes[2];

            Assert.IsTrue(firstNode.Name == "First object A" && secondNode.Name == "Second object B" && thirdNode.Name == "Third object C");
        }

        [TestMethod]
        public void CreateGraph_GivenScenario1_ReturnsObjectAnalysisGraphWithCorrectDependencies()
        {
            //Arrange
            IReactor reactor = AnalysisTestHelper.CreateCase1();

            //Act
            var factory = new ObjectAnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource);

            //Assert
            var firstNode = analysisGraph.Nodes[0];
            var secondNode = analysisGraph.Nodes[1];
            var thirdNode = analysisGraph.Nodes[2];

            Assert.IsTrue(firstNode.Predecessors.Count == 2 && firstNode.HasPredecessor(secondNode) && firstNode.HasPredecessor(thirdNode));
            Assert.IsTrue(secondNode.Successors.Count == 1 && secondNode.HasSuccessor(firstNode));
            Assert.IsTrue(thirdNode.Successors.Count == 1 && secondNode.HasSuccessor(firstNode));
        }

        [TestMethod]
        public void CreateGraph_GivenScenario2_ReturnsObjectAnalysisGraphWithCorrectNodes()
        {
            //Arrange
            IReactor reactor = AnalysisTestHelper.CreateCase2();

            //Act
            var factory = new ObjectAnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource);

            //Assert
            Assert.IsTrue(analysisGraph.Nodes.Count == 4);

            var firstNode = analysisGraph.Nodes[0];
            var secondNode = analysisGraph.Nodes[1];
            var thirdNode = analysisGraph.Nodes[2];
            var fourthNode = analysisGraph.Nodes[3];

            Assert.IsTrue(firstNode.Name == "Object G");
            Assert.IsTrue(secondNode.Name == "Object B");
            Assert.IsTrue(thirdNode.Name == "Object C");
            Assert.IsTrue(fourthNode.Name == "Object A");
        }

        [TestMethod]
        public void CreateGraph_GivenScenario2_ReturnsObjectAnalysisGraphWithCorrectDependencies()
        {
            //Arrange
            IReactor reactor = AnalysisTestHelper.CreateCase2();

            //Act
            var factory = new ObjectAnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource);

            //Assert
            var firstNode = analysisGraph.Nodes[0];
            var secondNode = analysisGraph.Nodes[1];
            var thirdNode = analysisGraph.Nodes[2];
            var fourthNode = analysisGraph.Nodes[3];

            Assert.IsTrue(firstNode.Predecessors.Count == 3 && firstNode.HasPredecessor(firstNode) && firstNode.HasPredecessor(secondNode) && firstNode.HasPredecessor(thirdNode));
            Assert.IsTrue(firstNode.Successors.Count == 1 && firstNode.HasSuccessor(firstNode));
            Assert.IsTrue(secondNode.Successors.Count == 1 && secondNode.HasSuccessor(firstNode));
            Assert.IsTrue(thirdNode.Successors.Count == 1 && thirdNode.HasSuccessor(firstNode));

            Assert.IsTrue(secondNode.Predecessors.Count == 1 && secondNode.HasPredecessor(fourthNode));
            Assert.IsTrue(thirdNode.Predecessors.Count == 1 && thirdNode.HasPredecessor(fourthNode));

            Assert.IsTrue(fourthNode.Successors.Count == 2 && fourthNode.HasSuccessor(secondNode) && fourthNode.HasSuccessor(thirdNode));
        }

        [TestMethod]
        public void CreateGraph_GivenScenario3_ReturnsObjectAnalysisGraphWithCorrectNodes()
        {
            //Arrange
            IReactor reactor = AnalysisTestHelper.CreateCase3();

            //Act
            var factory = new ObjectAnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource);

            //Assert
            Assert.IsTrue(analysisGraph.Nodes.Count == 2);

            var firstNode = analysisGraph.Nodes[0];
            var secondNode = analysisGraph.Nodes[1];

            Assert.IsTrue(firstNode.Name == "First object A" && secondNode.Name == "Second object A");
        }

        [TestMethod]
        public void CreateGraph_GivenScenario3_ReturnsObjectAnalysisGraphWithCorrectDependencies()
        {
            //Arrange
            IReactor reactor = AnalysisTestHelper.CreateCase3();

            //Act
            var factory = new ObjectAnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource);

            //Assert
            var firstObject = analysisGraph.Nodes[0];
            var secondObject = analysisGraph.Nodes[1];

            Assert.IsTrue(firstObject.Predecessors.Count == 1 && firstObject.HasPredecessor(secondObject));
            Assert.IsTrue(secondObject.Successors.Count == 1 && secondObject.HasSuccessor(firstObject));
        }

        [TestMethod]
        public void CreateGraph_GivenScenario4_ReturnsObjectAnalysisGraphWithCorrectNodes()
        {
            //Arrange
            var reactor = AnalysisTestHelper.CreateCase4();

            //Act
            var factory = new ObjectAnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource);

            //Assert
            Assert.IsTrue(analysisGraph.Nodes.Count == 1);

            var firstNode = analysisGraph.Nodes[0];

            Assert.IsTrue(firstNode.Name == "First object A");
        }

        [TestMethod]
        public void CreateGraph_GivenScenario4_ReturnsObjectAnalysisGraphWithCorrectDependencies()
        {
            //Arrange
            var reactor = AnalysisTestHelper.CreateCase4();

            //Act
            var factory = new ObjectAnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource);

            //Assert
            Assert.IsTrue(analysisGraph.Nodes.Count == 1);

            var firstNode = analysisGraph.Nodes[0];

            Assert.IsTrue(firstNode.Predecessors.Count == 1 && firstNode.HasPredecessor(firstNode));
            Assert.IsTrue(firstNode.Successors.Count == 1 && firstNode.HasSuccessor(firstNode));
        }

        [TestMethod]
        public void CreateGraph_GivenScenario5_ReturnsObjectAnalysisGraphWithCorrectNodes()
        {
            //Arrange
            var reactor = AnalysisTestHelper.CreateCase5();
            var factory = new ObjectAnalysisGraphFactory();

            //Act
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource);

            //Assert
            Assert.IsTrue(analysisGraph.Nodes.Count == 2);

            var firstNode = analysisGraph.Nodes[0];
            var secondNode = analysisGraph.Nodes[1];

            Assert.IsTrue(firstNode.Name == "First object A" && firstNode.Parent.Name == nameof(ClassA));
            Assert.IsTrue(secondNode.Name == "Second object B" && secondNode.Parent.Name == nameof(ClassB));
        }

        [TestMethod]
        public void CreateGraph_GivenScenario5_ReturnsObjectAnalysisGraphWithCorrectDependencies()
        {
            //Arrange
            var reactor = AnalysisTestHelper.CreateCase5();
            var factory = new ObjectAnalysisGraphFactory();

            //Act
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource);

            //Assert
            Assert.IsTrue(analysisGraph.Nodes.Count == 2);
            var firstNode = analysisGraph.Nodes[0];
            var secondNode = analysisGraph.Nodes[1];

            Assert.IsTrue(firstNode.Predecessors.Count == 1 && firstNode.HasPredecessor(secondNode));
            Assert.IsTrue(secondNode.Successors.Count == 1 && secondNode.HasPredecessor(firstNode));
        }

    }
}
