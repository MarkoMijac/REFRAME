using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeAnalyzer.Exceptions;
using ReframeAnalyzer.Graph;
using ReframeCore;
using ReframeCore.Factories;
using ReframeCoreExamples.E09;
using ReframeCoreExamples.E09._01;
using ReframeExporter;

namespace ReframeAnalyzerTests.GraphFactories
{
    [TestClass]
    public class ClassMemberAnalysisGraphFactoryTests
    {
        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void CreateGraph_XmlSourceIsEmpty_ThrowsException()
        {
            //Arrange
            var factory = new ClassMemberAnalysisGraphFactory();

            //Act
            var analysisGraph = factory.CreateGraph("");
        }

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void CreateGraph_XmlSourceNotValid_ThrowsException()
        {
            //Arrange
            var factory = new ClassMemberAnalysisGraphFactory();

            //Act
            var analysisGraph = factory.CreateGraph("<not valid source?_");
        }

        [TestMethod]
        public void CreateGraph_GivenEmptyDependencyGraph_ReturnsEmptyAnalysisGraph()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("R1");
            var factory = new ClassMemberAnalysisGraphFactory();

            //Act
            var xmlExporter = new XmlReactorDetailExporter(reactor.Identifier);
            string xmlSource = xmlExporter.Export();
            var analysisGraph = factory.CreateGraph(xmlSource);

            //Assert
            Assert.IsTrue(analysisGraph != null && analysisGraph.Nodes.Count == 0);
        }

        [TestMethod]
        public void CreateGraph_GivenClassMemberLevel_ReturnsClassMemberAnalysisGraph()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("R1");
            var factory = new ClassMemberAnalysisGraphFactory();

            //Act
            var xmlExporter = new XmlReactorDetailExporter(reactor.Identifier);
            string xmlSource = xmlExporter.Export();
            var analysisGraph = factory.CreateGraph(xmlSource);

            //Assert
            Assert.IsTrue(analysisGraph.AnalysisLevel == AnalysisLevel.ClassMemberLevel);
        }

        [TestMethod]
        public void CreateGraph_GivenScenario1_ReturnsClassMemberAnalysisGraphWithCorrectNodes()
        {
            //Arrange
            IReactor reactor = AnalysisTestHelper.CreateCase1();

            //Act
            var factory = new ClassMemberAnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource);

            //Assert
            Assert.IsTrue(analysisGraph.Nodes.Count == 3);

            var firstNode = analysisGraph.Nodes[0];
            var secondNode = analysisGraph.Nodes[1];
            var thirdNode = analysisGraph.Nodes[2];

            Assert.IsTrue(firstNode.Name == nameof(ClassA.PA1) && secondNode.Name == nameof(ClassB.PB1) && thirdNode.Name == nameof(ClassC.PC1));
        }

        [TestMethod]
        public void CreateGraph_GivenScenario1_ReturnsClassMemberAnalysisGraphWithCorrectDependencies()
        {
            //Arrange
            IReactor reactor = AnalysisTestHelper.CreateCase1();

            //Act
            var factory = new ClassMemberAnalysisGraphFactory();
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
        public void CreateGraph_GivenScenario2_ReturnsClassMemberAnalysisGraphWithCorrectNodes()
        {
            //Arrange
            IReactor reactor = AnalysisTestHelper.CreateCase2();

            //Act
            var factory = new ClassMemberAnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource);

            //Assert
            Assert.IsTrue(analysisGraph.Nodes.Count == 5);

            var firstNode = analysisGraph.Nodes[0];
            var secondNode = analysisGraph.Nodes[1];
            var thirdNode = analysisGraph.Nodes[2];
            var fourthNode = analysisGraph.Nodes[3];
            var fifthNode = analysisGraph.Nodes[4];

            Assert.IsTrue(firstNode.Name == nameof(ClassG.PG1));
            Assert.IsTrue(secondNode.Name == nameof(ClassB.PB1));
            Assert.IsTrue(thirdNode.Name == nameof(ClassC.PC1));
            Assert.IsTrue(fourthNode.Name == nameof(ClassG.PG2));
            Assert.IsTrue(fifthNode.Name == nameof(ClassA.PA1));
        }

        [TestMethod]
        public void CreateGraph_GivenScenario2_ReturnsClassMemberAnalysisGraphWithCorrectDependencies()
        {
            //Arrange
            IReactor reactor = AnalysisTestHelper.CreateCase2();

            //Act
            var factory = new ClassMemberAnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource);

            //Assert
            var firstNode = analysisGraph.Nodes[0];
            var secondNode = analysisGraph.Nodes[1];
            var thirdNode = analysisGraph.Nodes[2];
            var fourthNode = analysisGraph.Nodes[3];
            var fifthNode = analysisGraph.Nodes[4];

            Assert.IsTrue(firstNode.Predecessors.Count == 3 && firstNode.HasPredecessor(secondNode) && firstNode.HasPredecessor(thirdNode) && firstNode.HasPredecessor(fourthNode));
            Assert.IsTrue(secondNode.Successors.Count == 1 && secondNode.HasSuccessor(firstNode));
            Assert.IsTrue(thirdNode.Successors.Count == 1 && thirdNode.HasSuccessor(firstNode));
            Assert.IsTrue(fourthNode.Successors.Count == 1 && fourthNode.HasSuccessor(firstNode));

            Assert.IsTrue(secondNode.Predecessors.Count == 1 && secondNode.HasPredecessor(fifthNode));
            Assert.IsTrue(thirdNode.Predecessors.Count == 1 && thirdNode.HasPredecessor(fifthNode));

            Assert.IsTrue(fifthNode.Successors.Count == 2 && fifthNode.HasSuccessor(secondNode) && fifthNode.HasSuccessor(thirdNode));
        }

        [TestMethod]
        public void CreateGraph_GivenScenario3_ReturnsClassMemberAnalysisGraphWithCorrectNodes()
        {
            //Arrange
            IReactor reactor = AnalysisTestHelper.CreateCase3();

            //Act
            var factory = new ClassMemberAnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource);

            //Assert
            Assert.IsTrue(analysisGraph.Nodes.Count == 1);

            var firstNode = analysisGraph.Nodes[0];

            Assert.IsTrue(firstNode.Name == "PA1" && firstNode.Parent.Name == nameof(ClassA));
        }

        [TestMethod]
        public void CreateGraph_GivenScenario3_ReturnsClassMemberAnalysisGraphWithCorrectDependencies()
        {
            //Arrange
            IReactor reactor = AnalysisTestHelper.CreateCase3();

            //Act
            var factory = new ClassMemberAnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource);

            //Assert
            var firstNode = analysisGraph.Nodes[0];

            Assert.IsTrue(firstNode.Predecessors.Count == 1 && firstNode.HasPredecessor(firstNode));
            Assert.IsTrue(firstNode.Successors.Count == 1 && firstNode.HasSuccessor(firstNode));
        }

        [TestMethod]
        public void CreateGraph_GivenScenario4_ReturnsClassMemberAnalysisGraphWithCorrectNodes()
        {
            //Arrange
            var reactor = AnalysisTestHelper.CreateCase4();

            //Act
            var factory = new ClassMemberAnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource);

            //Assert
            Assert.IsTrue(analysisGraph.Nodes.Count == 2);

            var firstNode = analysisGraph.Nodes[0];
            var secondNode = analysisGraph.Nodes[1];

            Assert.IsTrue(firstNode.Name == nameof(ClassA.PA1) && firstNode.Parent.Name == nameof(ClassA));
            Assert.IsTrue(secondNode.Name == nameof(ClassA.PA2) && secondNode.Parent.Name == nameof(ClassA));
        }

        [TestMethod]
        public void CreateGraph_GivenScenario4_ReturnsClassMemberAnalysisGraphWithCorrectDependencies()
        {
            //Arrange
            var reactor = AnalysisTestHelper.CreateCase4();

            //Act
            var factory = new ClassMemberAnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource);

            //Assert
            var firstNode = analysisGraph.Nodes[0];
            var secondNode = analysisGraph.Nodes[1];

            Assert.IsTrue(firstNode.Predecessors.Count == 1 && firstNode.HasPredecessor(secondNode));
            Assert.IsTrue(secondNode.Successors.Count == 1 && secondNode.HasSuccessor(firstNode));
        }

        [TestMethod]
        public void CreateGraph_GivenScenario5_ReturnsClassMemberAnalysisGraphWithCorrectNodes()
        {
            //Arrange
            var reactor = AnalysisTestHelper.CreateCase5();
            var factory = new ClassMemberAnalysisGraphFactory();

            //Act
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource);

            //Assert
            Assert.IsTrue(analysisGraph.Nodes.Count == 4);

            var firstNode = analysisGraph.Nodes[0];
            var secondNode = analysisGraph.Nodes[1];
            var thirdNode = analysisGraph.Nodes[2];
            var fourthNode = analysisGraph.Nodes[3];

            Assert.IsTrue(firstNode.Name == nameof(ClassA.PA1) && firstNode.Parent.Name == nameof(ClassA));
            Assert.IsTrue(secondNode.Name == nameof(ClassB.PB1) && secondNode.Parent.Name == nameof(ClassB));
            Assert.IsTrue(thirdNode.Name == nameof(ClassB.PB2) && thirdNode.Parent.Name == nameof(ClassB));
            Assert.IsTrue(fourthNode.Name == nameof(ClassA.PA2) && fourthNode.Parent.Name == nameof(ClassA));

        }

        [TestMethod]
        public void CreateGraph_GivenScenario5_ReturnsClassMemberAnalysisGraphWithCorrectDependencies()
        {
            //Arrange
            var reactor = AnalysisTestHelper.CreateCase5();
            var factory = new ClassMemberAnalysisGraphFactory();

            //Act
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource);

            //Assert
            var firstNode = analysisGraph.Nodes[0];
            var secondNode = analysisGraph.Nodes[1];
            var thirdNode = analysisGraph.Nodes[2];
            var fourthNode = analysisGraph.Nodes[3];

            Assert.IsTrue(firstNode.Predecessors.Count == 1 && firstNode.HasPredecessor(secondNode));
            Assert.IsTrue(secondNode.Successors.Count == 1 && secondNode.HasSuccessor(firstNode));

            Assert.IsTrue(thirdNode.Predecessors.Count == 1 && thirdNode.HasPredecessor(fourthNode));
            Assert.IsTrue(fourthNode.Successors.Count == 1 && fourthNode.HasSuccessor(thirdNode));

        }
    }
}
