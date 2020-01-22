using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeCore;
using ReframeAnalyzer.Graph;
using ReframeAnalyzer.Exceptions;
using ReframeCore.Factories;
using ReframeCoreExamples.E09;
using ReframeCore.FluentAPI;
using ReframeExporter;

namespace ReframeAnalyzerTests
{
    [TestClass]
    public class AnalysisGraphFactoryTests
    {
        private XmlExporter xmlExporter = new XmlExporter();

        [TestMethod]
        [ExpectedException(typeof(AnalyzerException))]
        public void GetGraph_GivenDependencyGraphIsNull_ThrowsException()
        {
            //Arrange
            IReactor reactor = null;
            AnalysisGraphFactory factory = new AnalysisGraphFactory();
            
            //Act
            var analysisGraph = factory.CreateGraph("", AnalysisLevel.ClassLevel);
        }

        [TestMethod]
        public void GetGraph_GivenEmptyDependencyGraph_ReturnsEmptyAnalysisGraph()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("R1");
            AnalysisGraphFactory factory = new AnalysisGraphFactory();

            //Act
            string xmlSource = xmlExporter.Export(reactor);
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.ClassLevel);

            //Assert
            Assert.IsTrue(analysisGraph != null && analysisGraph.Nodes.Count == 0);
        }

        [TestMethod]
        public void GetGraph_GivenScenario1_ReturnsAnalysisGraphWithCorrectNodes()
        {
            //Arrange
            IReactor reactor = CreateScenario1();

            //Act
            var factory = new AnalysisGraphFactory();
            var analysisGraph = factory.CreateGraph(xmlExporter.Export(reactor), AnalysisLevel.ClassLevel);

            //Assert
            Assert.IsTrue(analysisGraph.Nodes.Count == 3
                && analysisGraph.Nodes[0].Name == nameof(ClassA)
                && analysisGraph.Nodes[1].Name == nameof(ClassB)
                && analysisGraph.Nodes[2].Name == nameof(ClassC));
        }

        [TestMethod]
        public void GetGraph_GivenScenario1_ReturnsAnalysisGraphWithCorrectDependencies()
        {
            //Arrange
            IReactor reactor = CreateScenario1();

            //Act
            var factory = new AnalysisGraphFactory();
            var analysisGraph = factory.CreateGraph(xmlExporter.Export(reactor), AnalysisLevel.ClassLevel);

            //Assert
            var classANode = analysisGraph.Nodes[0];
            var classBNode = analysisGraph.Nodes[1];
            var classCNode = analysisGraph.Nodes[2];

            Assert.IsTrue(classANode.Predecessors.Count == 2 && classANode.Successors.Count == 0);
            Assert.IsTrue(classANode.HasPredecessor(classBNode) && classANode.HasPredecessor(classCNode));

            Assert.IsTrue(classBNode.Predecessors.Count == 0 && classBNode.Successors.Count == 1);
            Assert.IsTrue(classBNode.HasSuccessor(classANode));

            Assert.IsTrue(classCNode.Predecessors.Count == 0 && classCNode.Successors.Count == 1);
            Assert.IsTrue(classCNode.HasSuccessor(classANode));
        }

        private static IReactor CreateScenario1()
        {
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("S1");

            ClassA objA = new ClassA();
            ClassB objB = new ClassB();
            ClassC objC = new ClassC();

            reactor.Let(() => objA.PA1)
                .DependOn(() => objB.PB1, () => objC.PC1);
            return reactor;
        }

        [TestMethod]
        public void GetGraph_GivenScenario2_ReturnsAnalysisGraphWithCorrectNodes()
        {
            //Arrange
            IReactor reactor = CreateScenario2();

            //Act
            var factory = new AnalysisGraphFactory();
            var analysisGraph = factory.CreateGraph(xmlExporter.Export(reactor), AnalysisLevel.ClassLevel);

            //Assert
            Assert.IsTrue(analysisGraph.Nodes.Count == 3
                && analysisGraph.Nodes[0].Name == nameof(ClassC)
                && analysisGraph.Nodes[1].Name == nameof(ClassB)
                && analysisGraph.Nodes[2].Name == nameof(ClassA));
        }

        [TestMethod]
        public void GetGraph_GivenScenario2_ReturnsAnalysisGraphWithCorrectDependencies()
        {
            //Arrange
            IReactor reactor = CreateScenario2();

            //Act
            var factory = new AnalysisGraphFactory();
            var analysisGraph = factory.CreateGraph(xmlExporter.Export(reactor), AnalysisLevel.ClassLevel);

            //Assert
            var classCNode = analysisGraph.Nodes[0];
            var classBNode = analysisGraph.Nodes[1];
            var classANode = analysisGraph.Nodes[2];

            Assert.IsTrue(classANode.Predecessors.Count == 0 && classANode.Successors.Count == 1);
            Assert.IsTrue(classANode.HasSuccessor(classBNode));

            Assert.IsTrue(classBNode.Predecessors.Count == 1 && classBNode.Successors.Count == 1);
            Assert.IsTrue(classBNode.HasPredecessor(classANode) && classBNode.HasSuccessor(classCNode));

            Assert.IsTrue(classCNode.Predecessors.Count == 1 && classCNode.Successors.Count == 0);
            Assert.IsTrue(classCNode.HasPredecessor(classBNode));
        }

        private static IReactor CreateScenario2()
        {
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("S1");

            ClassA objA = new ClassA();
            ClassB objB = new ClassB();
            ClassC objC = new ClassC();

            reactor.Let(() => objC.PC1)
                .DependOn(() => objB.PB1);
            reactor.Let(() => objB.PB2).DependOn(() => objA.PA3);
            return reactor;
        }

        [TestMethod]
        public void GetGraph_GivenScenario3_ReturnsAnalysisGraphWithCorrectNodes()
        {
            //Arrange
            IReactor reactor = CreateScenario3();

            //Act
            var factory = new AnalysisGraphFactory();
            var analysisGraph = factory.CreateGraph(xmlExporter.Export(reactor), AnalysisLevel.ClassLevel);

            //Assert
            Assert.IsTrue(analysisGraph.Nodes.Count == 1
                && analysisGraph.Nodes[0].Name == nameof(ClassA));
        }

        [TestMethod]
        public void GetGraph_GivenScenario3_ReturnsAnalysisGraphWithCorrectDependencies()
        {
            //Arrange
            IReactor reactor = CreateScenario3();

            //Act
            var factory = new AnalysisGraphFactory();
            var analysisGraph = factory.CreateGraph(xmlExporter.Export(reactor), AnalysisLevel.ClassLevel);

            //Assert
            var classANode = analysisGraph.Nodes[0];
            Assert.IsTrue(classANode.Predecessors.Count == 1 && classANode.Successors.Count == 1);
            Assert.IsTrue(classANode.HasPredecessor(classANode) && classANode.HasSuccessor(classANode));
        }

        private static IReactor CreateScenario3()
        {
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("S1");

            ClassA objA = new ClassA();
            ClassA objA1 = new ClassA();

            reactor.Let(() => objA.PA1)
                .DependOn(() => objA1.PA1);
            return reactor;
        }

        [TestMethod]
        public void GetGraph_GivenScenario4_ReturnsAnalysisGraphWithCorrectNodes()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("S1");
            var factory = new AnalysisGraphFactory();

            ClassA objA = new ClassA();

            reactor.Let(() => objA.PA1)
                .DependOn(() => objA.PA2);

            //Act
            var analysisGraph = factory.CreateGraph(xmlExporter.Export(reactor), AnalysisLevel.ClassLevel);

            //Assert
            Assert.IsTrue(analysisGraph.Nodes.Count == 1
                && analysisGraph.Nodes[0].Name == nameof(ClassA));
        }

        [TestMethod]
        public void GetGraph_GivenScenario4_ReturnsAnalysisGraphWithCorrectDependencies()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("S1");
            var factory = new AnalysisGraphFactory();

            ClassA objA = new ClassA();

            reactor.Let(() => objA.PA1)
                .DependOn(() => objA.PA2);

            //Act
            var analysisGraph = factory.CreateGraph(xmlExporter.Export(reactor), AnalysisLevel.ClassLevel);

            //Assert
            var classANode = analysisGraph.Nodes[0];
            Assert.IsTrue(classANode.Predecessors.Count == 1 && classANode.Successors.Count == 1);
            Assert.IsTrue(classANode.HasPredecessor(classANode) && classANode.HasSuccessor(classANode));
        }

        [TestMethod]
        public void GetGraph_GivenScenario5_ReturnsAnalysisGraphWithCorrectNodes()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("S1");
            var factory = new AnalysisGraphFactory();

            ClassA objA = new ClassA();
            ClassB objB = new ClassB();

            reactor.Let(() => objA.PA1).DependOn(() => objB.PB1);
            reactor.Let(() => objB.PB2).DependOn(() => objA.PA2);

            //Act
            var analysisGraph = factory.CreateGraph(xmlExporter.Export(reactor), AnalysisLevel.ClassLevel);

            //Assert
            Assert.IsTrue(analysisGraph.Nodes.Count == 2
                && analysisGraph.Nodes[0].Name == nameof(ClassA)
                && analysisGraph.Nodes[1].Name == nameof(ClassB));
        }

        [TestMethod]
        public void GetGraph_GivenScenario5_ReturnsAnalysisGraphWithCorrectDependencies()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("S1");
            var factory = new AnalysisGraphFactory();

            ClassA objA = new ClassA();
            ClassB objB = new ClassB();

            reactor.Let(() => objA.PA1).DependOn(() => objB.PB1);
            reactor.Let(() => objB.PB2).DependOn(() => objA.PA2);

            //Act
            var analysisGraph = factory.CreateGraph(xmlExporter.Export(reactor), AnalysisLevel.ClassLevel);

            //Assert
            var classANode = analysisGraph.Nodes[0];
            var classBNode = analysisGraph.Nodes[1];

            Assert.IsTrue(classANode.Predecessors.Count == 1 && classANode.Successors.Count == 1);
            Assert.IsTrue(classANode.HasPredecessor(classBNode) && classANode.HasSuccessor(classBNode));

            Assert.IsTrue(classBNode.Predecessors.Count == 1 && classBNode.Successors.Count == 1);
            Assert.IsTrue(classBNode.HasPredecessor(classANode) && classBNode.HasSuccessor(classANode));
        }
    }
}
