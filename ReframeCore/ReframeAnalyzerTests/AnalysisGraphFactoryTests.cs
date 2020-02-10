using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeCore;
using ReframeAnalyzer.Graph;
using ReframeAnalyzer.Exceptions;
using ReframeCore.Factories;
using ReframeCoreExamples.E09;
using ReframeExporter;
using ReframeCoreFluentAPI;
using ReframeCoreExamples.E09._01;

namespace ReframeAnalyzerTests
{
    [TestClass]
    public class AnalysisGraphFactoryTests
    {
        #region General

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void CreateGraph_XmlSourceIsEmpty_ThrowsException()
        {
            //Arrange
            AnalysisGraphFactory factory = new AnalysisGraphFactory();

            //Act
            var analysisGraph = factory.CreateGraph("", AnalysisLevel.ClassLevel);
        }

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void CreateGraph_XmlSourceNotValid_ThrowsException()
        {
            //Arrange
            AnalysisGraphFactory factory = new AnalysisGraphFactory();

            //Act
            var analysisGraph = factory.CreateGraph("<not valid source?_", AnalysisLevel.ClassLevel);
        }

        [TestMethod]
        public void CreateGraph_GivenEmptyDependencyGraph_ReturnsEmptyAnalysisGraph()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("R1");
            var factory = new AnalysisGraphFactory();

            //Act
            var xmlExporter = new XmlReactorDetailExporter(reactor.Identifier);
            string xmlSource = xmlExporter.Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.ClassLevel);

            //Assert
            Assert.IsTrue(analysisGraph != null && analysisGraph.Nodes.Count == 0);
        }

        [TestMethod]
        public void CreateGraph_GivenObjectMemberLevel_ReturnsObjectMemberAnalysisGraph()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("R1");
            var factory = new AnalysisGraphFactory();

            //Act
            var xmlExporter = new XmlReactorDetailExporter(reactor.Identifier);
            string xmlSource = xmlExporter.Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.ObjectMemberLevel);

            //Assert
            Assert.IsInstanceOfType(analysisGraph, typeof(ObjectMemberAnalysisGraph));
        }

        [TestMethod]
        public void CreateGraph_GivenObjectLevel_ReturnsObjectAnalysisGraph()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("R1");
            var factory = new AnalysisGraphFactory();

            //Act
            var xmlExporter = new XmlReactorDetailExporter(reactor.Identifier);
            string xmlSource = xmlExporter.Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.ObjectLevel);

            //Assert
            Assert.IsInstanceOfType(analysisGraph, typeof(ObjectAnalysisGraph));
        }

        [TestMethod]
        public void CreateGraph_GivenClassMemberLevel_ReturnsClassMemberAnalysisGraph()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("R1");
            var factory = new AnalysisGraphFactory();

            //Act
            var xmlExporter = new XmlReactorDetailExporter(reactor.Identifier);
            string xmlSource = xmlExporter.Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.ClassMemberLevel);

            //Assert
            Assert.IsInstanceOfType(analysisGraph, typeof(ClassMemberAnalysisGraph));
        }

        [TestMethod]
        public void CreateGraph_GivenClassLevel_ReturnsClassAnalysisGraph()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("R1");
            var factory = new AnalysisGraphFactory();

            //Act
            var xmlExporter = new XmlReactorDetailExporter(reactor.Identifier);
            string xmlSource = xmlExporter.Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.ClassLevel);

            //Assert
            Assert.IsInstanceOfType(analysisGraph, typeof(ClassAnalysisGraph));
        }

        [TestMethod]
        public void CreateGraph_GivenNamespaceLevel_ReturnsNamespaceAnalysisGraph()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("R1");
            var factory = new AnalysisGraphFactory();

            //Act
            var xmlExporter = new XmlReactorDetailExporter(reactor.Identifier);
            string xmlSource = xmlExporter.Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.NamespaceLevel);

            //Assert
            Assert.IsInstanceOfType(analysisGraph, typeof(NamespaceAnalysisGraph));
        }

        [TestMethod]
        public void CreateGraph_GivenAssemblyLevel_ReturnsAssemblyAnalysisGraph()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("R1");
            var factory = new AnalysisGraphFactory();

            //Act
            var xmlExporter = new XmlReactorDetailExporter(reactor.Identifier);
            string xmlSource = xmlExporter.Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.AssemblyLevel);

            //Assert
            Assert.IsInstanceOfType(analysisGraph, typeof(AssemblyAnalysisGraph));
        }

        #endregion

        #region CASE 1

        private static IReactor CreateCase1()
        {
            return AnalysisTestCases.CreateCase1();
        }

        [TestMethod]
        public void CreateGraph_GivenScenario1_ReturnsObjectMemberAnalysisGraphWithCorrectNodes()
        {
            //Arrange
            IReactor reactor = CreateCase1();

            //Act
            var factory = new AnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.ObjectMemberLevel);

            //Assert
            Assert.IsTrue(analysisGraph.Nodes.Count == 3);

            var firstNode = analysisGraph.Nodes[0];
            var secondNode = analysisGraph.Nodes[1];
            var thirdNode = analysisGraph.Nodes[2];

            Assert.IsTrue(firstNode.Name == nameof(ClassA.PA1) && secondNode.Name == nameof(ClassB.PB1) && thirdNode.Name == nameof(ClassC.PC1));
        }

        [TestMethod]
        public void CreateGraph_GivenScenario1_ReturnsObjectMemberAnalysisGraphWithCorrectDependencies()
        {
            //Arrange
            IReactor reactor = CreateCase1();

            //Act
            var factory = new AnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.ObjectMemberLevel);

            //Assert
            var firstNode = analysisGraph.Nodes[0];
            var secondNode = analysisGraph.Nodes[1];
            var thirdNode = analysisGraph.Nodes[2];

            Assert.IsTrue(firstNode.Predecessors.Count == 2 && firstNode.HasPredecessor(secondNode) && firstNode.HasPredecessor(thirdNode));
            Assert.IsTrue(secondNode.Successors.Count == 1 && secondNode.HasSuccessor(firstNode));
            Assert.IsTrue(thirdNode.Successors.Count == 1 && secondNode.HasSuccessor(firstNode));
        }

        [TestMethod]
        public void CreateGraph_GivenScenario1_ReturnsObjectAnalysisGraphWithCorrectNodes()
        {
            //Arrange
            IReactor reactor = CreateCase1();

            //Act
            var factory = new AnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.ObjectLevel);

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
            IReactor reactor = CreateCase1();

            //Act
            var factory = new AnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.ObjectLevel);

            //Assert
            var firstNode = analysisGraph.Nodes[0];
            var secondNode = analysisGraph.Nodes[1];
            var thirdNode = analysisGraph.Nodes[2];

            Assert.IsTrue(firstNode.Predecessors.Count == 2 && firstNode.HasPredecessor(secondNode) && firstNode.HasPredecessor(thirdNode));
            Assert.IsTrue(secondNode.Successors.Count == 1 && secondNode.HasSuccessor(firstNode));
            Assert.IsTrue(thirdNode.Successors.Count == 1 && secondNode.HasSuccessor(firstNode));
        }

        [TestMethod]
        public void CreateGraph_GivenScenario1_ReturnsClassMemberAnalysisGraphWithCorrectNodes()
        {
            //Arrange
            IReactor reactor = CreateCase1();

            //Act
            var factory = new AnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.ClassMemberLevel);

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
            IReactor reactor = CreateCase1();

            //Act
            var factory = new AnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.ClassMemberLevel);

            //Assert
            var firstNode = analysisGraph.Nodes[0];
            var secondNode = analysisGraph.Nodes[1];
            var thirdNode = analysisGraph.Nodes[2];

            Assert.IsTrue(firstNode.Predecessors.Count == 2 && firstNode.HasPredecessor(secondNode) && firstNode.HasPredecessor(thirdNode));
            Assert.IsTrue(secondNode.Successors.Count == 1 && secondNode.HasSuccessor(firstNode));
            Assert.IsTrue(thirdNode.Successors.Count == 1 && secondNode.HasSuccessor(firstNode));
        }

        [TestMethod]
        public void CreateGraph_GivenScenario1_ReturnsClassAnalysisGraphWithCorrectNodes()
        {
            //Arrange
            IReactor reactor = CreateCase1();

            //Act
            var factory = new AnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.ClassLevel);

            //Assert
            Assert.IsTrue(analysisGraph.Nodes.Count == 3);

            var firstNode = analysisGraph.Nodes[0];
            var secondNode = analysisGraph.Nodes[1];
            var thirdNode = analysisGraph.Nodes[2];

            Assert.IsTrue(firstNode.Name == nameof(ClassA) && secondNode.Name == nameof(ClassB) && thirdNode.Name == nameof(ClassC));
        }

        [TestMethod]
        public void CreateGraph_GivenScenario1_ReturnsClassAnalysisGraphWithCorrectDependencies()
        {
            //Arrange
            IReactor reactor = CreateCase1();

            //Act
            var factory = new AnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.ClassLevel);

            //Assert
            var firstNode = analysisGraph.Nodes[0];
            var secondNode = analysisGraph.Nodes[1];
            var thirdNode = analysisGraph.Nodes[2];

            Assert.IsTrue(firstNode.Predecessors.Count == 2 && firstNode.HasPredecessor(secondNode) && firstNode.HasPredecessor(thirdNode));
            Assert.IsTrue(secondNode.Successors.Count == 1 && secondNode.HasSuccessor(firstNode));
            Assert.IsTrue(thirdNode.Successors.Count == 1 && secondNode.HasSuccessor(firstNode));
        }

        [TestMethod]
        public void CreateGraph_GivenScenario1_ReturnsNamespaceAnalysisGraphWithCorrectNodes()
        {
            //Arrange
            IReactor reactor = CreateCase1();

            //Act
            var factory = new AnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.NamespaceLevel);

            //Assert
            Assert.IsTrue(analysisGraph.Nodes.Count == 1);

            var firstNode = analysisGraph.Nodes[0];

            Assert.IsTrue(firstNode.Name == typeof(ClassA).Namespace);
        }

        [TestMethod]
        public void CreateGraph_GivenScenario1_ReturnsNamespaceAnalysisGraphWithCorrectDependencies()
        {
            //Arrange
            IReactor reactor = CreateCase1();

            //Act
            var factory = new AnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.NamespaceLevel);

            //Assert
            var firstNode = analysisGraph.Nodes[0];

            Assert.IsTrue(firstNode.Predecessors.Count == 1 && firstNode.HasPredecessor(firstNode));
            Assert.IsTrue(firstNode.Successors.Count == 1 && firstNode.HasSuccessor(firstNode));
        }

        [TestMethod]
        public void CreateGraph_GivenScenario1_ReturnsAssemblyAnalysisGraphWithCorrectNodes()
        {
            //Arrange
            IReactor reactor = CreateCase1();

            //Act
            var factory = new AnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.AssemblyLevel);

            //Assert
            Assert.IsTrue(analysisGraph.Nodes.Count == 1);

            var firstNode = analysisGraph.Nodes[0];

            Assert.IsTrue(firstNode.Name == typeof(ClassA).Assembly.ManifestModule.Name);
        }

        [TestMethod]
        public void CreateGraph_GivenScenario1_ReturnsAssemblyAnalysisGraphWithCorrectDependencies()
        {
            //Arrange
            IReactor reactor = CreateCase1();

            //Act
            var factory = new AnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.AssemblyLevel);

            //Assert
            var firstNode = analysisGraph.Nodes[0];

            Assert.IsTrue(firstNode.Predecessors.Count == 1 && firstNode.HasPredecessor(firstNode));
            Assert.IsTrue(firstNode.Successors.Count == 1 && firstNode.HasSuccessor(firstNode));
        }

        #endregion

        #region CASE 2

        private static IReactor CreateCase2()
        {
            return AnalysisTestCases.CreateCase2();
        }

        [TestMethod]
        public void CreateGraph_GivenScenario2_ReturnsObjectMemberAnalysisGraphWithCorrectNodes()
        {
            //Arrange
            IReactor reactor = CreateCase2();

            //Act
            var factory = new AnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.ObjectMemberLevel);

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
        public void CreateGraph_GivenScenario2_ReturnsObjectMemberAnalysisGraphWithCorrectDependencies()
        {
            //Arrange
            IReactor reactor = CreateCase2();

            //Act
            var factory = new AnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.ObjectMemberLevel);

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
        public void CreateGraph_GivenScenario2_ReturnsObjectAnalysisGraphWithCorrectNodes()
        {
            //Arrange
            IReactor reactor = CreateCase2();

            //Act
            var factory = new AnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.ObjectLevel);

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
            IReactor reactor = CreateCase2();

            //Act
            var factory = new AnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.ObjectLevel);

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
        public void CreateGraph_GivenScenario2_ReturnsClassMemberAnalysisGraphWithCorrectNodes()
        {
            //Arrange
            IReactor reactor = CreateCase2();

            //Act
            var factory = new AnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.ClassMemberLevel);

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
            IReactor reactor = CreateCase2();

            //Act
            var factory = new AnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.ClassMemberLevel);

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
        public void CreateGraph_GivenScenario2_ReturnsClassAnalysisGraphWithCorrectNodes()
        {
            //Arrange
            IReactor reactor = CreateCase2();

            //Act
            var factory = new AnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.ClassLevel);

            //Assert
            Assert.IsTrue(analysisGraph.Nodes.Count == 4);

            var firstNode = analysisGraph.Nodes[0];
            var secondNode = analysisGraph.Nodes[1];
            var thirdNode = analysisGraph.Nodes[2];
            var fourthNode = analysisGraph.Nodes[3];

            Assert.IsTrue(firstNode.Name == nameof(ClassG));
            Assert.IsTrue(secondNode.Name == nameof(ClassB));
            Assert.IsTrue(thirdNode.Name == nameof(ClassC));
            Assert.IsTrue(fourthNode.Name == nameof(ClassA));
        }

        [TestMethod]
        public void CreateGraph_GivenScenario2_ReturnsClassAnalysisGraphWithCorrectDependencies()
        {
            //Arrange
            IReactor reactor = CreateCase2();

            //Act
            var factory = new AnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.ClassLevel);

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
        public void CreateGraph_GivenScenario2_ReturnsNamespaceAnalysisGraphWithCorrectNodes()
        {
            //Arrange
            IReactor reactor = CreateCase2();

            //Act
            var factory = new AnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.NamespaceLevel);

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
            IReactor reactor = CreateCase2();

            //Act
            var factory = new AnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.NamespaceLevel);

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
        public void CreateGraph_GivenScenario2_ReturnsAssemblyAnalysisGraphWithCorrectNodes()
        {
            //Arrange
            IReactor reactor = CreateCase2();

            //Act
            var factory = new AnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.AssemblyLevel);

            //Assert
            Assert.IsTrue(analysisGraph.Nodes.Count == 1);

            var firstNode = analysisGraph.Nodes[0];

            Assert.IsTrue(firstNode.Name == typeof(ClassG).Assembly.ManifestModule.Name);            
        }

        [TestMethod]
        public void CreateGraph_GivenScenario2_ReturnsAssemblyAnalysisGraphWithCorrectDependencies()
        {
            //Arrange
            IReactor reactor = CreateCase2();

            //Act
            var factory = new AnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.AssemblyLevel);

            //Assert
            var firstNode = analysisGraph.Nodes[0];

            Assert.IsTrue(firstNode.Predecessors.Count == 1);
            Assert.IsTrue(firstNode.HasPredecessor(firstNode));

            Assert.IsTrue(firstNode.Successors.Count == 1);
            Assert.IsTrue(firstNode.HasSuccessor(firstNode));
        }

        #endregion

        #region CASE 3

        private static IReactor CreateCase3()
        {
            return AnalysisTestCases.CreateCase3();
        }

        [TestMethod]
        public void CreateGraph_GivenScenario3_ReturnsObjectMemberAnalysisGraphWithCorrectNodes()
        {
            //Arrange
            IReactor reactor = CreateCase3();

            //Act
            var factory = new AnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.ObjectMemberLevel);

            //Assert
            Assert.IsTrue(analysisGraph.Nodes.Count == 2);

            IAnalysisNode firstNode = analysisGraph.Nodes[0];
            IAnalysisNode secondNode = analysisGraph.Nodes[1];

            Assert.IsTrue(firstNode.Name == "PA1" && secondNode.Name == "PA1");
        }

        [TestMethod]
        public void CreateGraph_GivenScenario3_ReturnsObjectMemberAnalysisGraphWithCorrectDependencies()
        {
            //Arrange
            IReactor reactor = CreateCase3();

            //Act
            var factory = new AnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.ObjectMemberLevel);

            //Assert
            var firstObject = analysisGraph.Nodes[0];
            var secondObject = analysisGraph.Nodes[1];
            Assert.IsTrue(firstObject.Predecessors.Count == 1 && firstObject.HasPredecessor(secondObject));
            Assert.IsTrue(secondObject.Successors.Count == 1 && secondObject.HasSuccessor(firstObject));
        }

        [TestMethod]
        public void CreateGraph_GivenScenario3_ReturnsObjectAnalysisGraphWithCorrectNodes()
        {
            //Arrange
            IReactor reactor = CreateCase3();

            //Act
            var factory = new AnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.ObjectLevel);

            //Assert
            Assert.IsTrue(analysisGraph.Nodes.Count == 2);

            var firstNode = analysisGraph.Nodes[0];
            var secondNode = analysisGraph.Nodes[1];

            Assert.IsTrue(firstNode.Name == "First object A"  && secondNode.Name == "Second object A");
        }

        [TestMethod]
        public void CreateGraph_GivenScenario3_ReturnsObjectAnalysisGraphWithCorrectDependencies()
        {
            //Arrange
            IReactor reactor = CreateCase3();

            //Act
            var factory = new AnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.ObjectLevel);

            //Assert
            var firstObject = analysisGraph.Nodes[0];
            var secondObject = analysisGraph.Nodes[1];

            Assert.IsTrue(firstObject.Predecessors.Count == 1 && firstObject.HasPredecessor(secondObject));
            Assert.IsTrue(secondObject.Successors.Count == 1 && secondObject.HasSuccessor(firstObject));
        }

        [TestMethod]
        public void CreateGraph_GivenScenario3_ReturnsClassMemberAnalysisGraphWithCorrectNodes()
        {
            //Arrange
            IReactor reactor = CreateCase3();

            //Act
            var factory = new AnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.ClassMemberLevel);

            //Assert
            Assert.IsTrue(analysisGraph.Nodes.Count == 1);

            ClassMemberAnalysisNode firstNode = analysisGraph.Nodes[0] as ClassMemberAnalysisNode;

            Assert.IsTrue(firstNode.Name == "PA1" && firstNode.OwnerClass.Name == nameof(ClassA));
        }

        [TestMethod]
        public void CreateGraph_GivenScenario3_ReturnsClassMemberAnalysisGraphWithCorrectDependencies()
        {
            //Arrange
            IReactor reactor = CreateCase3();

            //Act
            var factory = new AnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.ClassMemberLevel);

            //Assert
            ClassMemberAnalysisNode firstNode = analysisGraph.Nodes[0] as ClassMemberAnalysisNode;

            Assert.IsTrue(firstNode.Predecessors.Count == 1 && firstNode.HasPredecessor(firstNode));
            Assert.IsTrue(firstNode.Successors.Count == 1 && firstNode.HasSuccessor(firstNode));
        }

        [TestMethod]
        public void CreateGraph_GivenScenario3_ReturnsClassAnalysisGraphWithCorrectNodes()
        {
            //Arrange
            IReactor reactor = CreateCase3();

            //Act
            var factory = new AnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.ClassLevel);

            //Assert
            Assert.IsTrue(analysisGraph.Nodes.Count == 1);

            ClassAnalysisNode firstNode = analysisGraph.Nodes[0] as ClassAnalysisNode;

            Assert.IsTrue(firstNode.Name == nameof(ClassA));
        }

        [TestMethod]
        public void CreateGraph_GivenScenario3_ReturnsClassAnalysisGraphWithCorrectDependencies()
        {
            //Arrange
            IReactor reactor = CreateCase3();

            //Act
            var factory = new AnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.ClassLevel);

            //Assert
            ClassAnalysisNode firstNode = analysisGraph.Nodes[0] as ClassAnalysisNode;

            Assert.IsTrue(firstNode.Predecessors.Count == 1 && firstNode.HasPredecessor(firstNode));
            Assert.IsTrue(firstNode.Successors.Count == 1 && firstNode.HasSuccessor(firstNode));
        }

        [TestMethod]
        public void CreateGraph_GivenScenario3_ReturnsNamespaceAnalysisGraphWithCorrectNodes()
        {
            //Arrange
            IReactor reactor = CreateCase3();

            //Act
            var factory = new AnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.NamespaceLevel);

            //Assert
            Assert.IsTrue(analysisGraph.Nodes.Count == 1);

            NamespaceAnalysisNode firstNode = analysisGraph.Nodes[0] as NamespaceAnalysisNode;

            Assert.IsTrue(firstNode.Name == typeof(ClassA).Namespace);
        }

        [TestMethod]
        public void CreateGraph_GivenScenario3_ReturnsNamespaceAnalysisGraphWithCorrectDependencies()
        {
            //Arrange
            IReactor reactor = CreateCase3();

            //Act
            var factory = new AnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.NamespaceLevel);

            //Assert
            NamespaceAnalysisNode firstNode = analysisGraph.Nodes[0] as NamespaceAnalysisNode;

            Assert.IsTrue(firstNode.Predecessors.Count == 1 && firstNode.HasPredecessor(firstNode));
            Assert.IsTrue(firstNode.Successors.Count == 1 && firstNode.HasSuccessor(firstNode));
        }

        [TestMethod]
        public void CreateGraph_GivenScenario3_ReturnsAssemblyAnalysisGraphWithCorrectNodes()
        {
            //Arrange
            IReactor reactor = CreateCase3();

            //Act
            var factory = new AnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.AssemblyLevel);

            //Assert
            Assert.IsTrue(analysisGraph.Nodes.Count == 1);

            AssemblyAnalysisNode firstNode = analysisGraph.Nodes[0] as AssemblyAnalysisNode;

            Assert.IsTrue(firstNode.Name == typeof(ClassA).Assembly.ManifestModule.Name);
        }

        [TestMethod]
        public void CreateGraph_GivenScenario3_ReturnsAssemblyAnalysisGraphWithCorrectDependencies()
        {
            //Arrange
            IReactor reactor = CreateCase3();

            //Act
            var factory = new AnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.AssemblyLevel);

            //Assert
            AssemblyAnalysisNode firstNode = analysisGraph.Nodes[0] as AssemblyAnalysisNode;

            Assert.IsTrue(firstNode.Predecessors.Count == 1 && firstNode.HasPredecessor(firstNode));
            Assert.IsTrue(firstNode.Successors.Count == 1 && firstNode.HasSuccessor(firstNode));
        }

        #endregion

        #region CASE 4

        /// <summary>
        /// Same class, same objects, different members.
        /// </summary>
        /// <returns></returns>
        private static IReactor CreateCase4()
        {
            return AnalysisTestCases.CreateCase4();
        }

        [TestMethod]
        public void CreateGraph_GivenScenario4_ReturnsObjectMemberAnalysisGraphWithCorrectNodes()
        {
            //Arrange
            var reactor = CreateCase4();

            //Act
            var factory = new AnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.ObjectMemberLevel);

            //Assert
            Assert.IsTrue(analysisGraph.Nodes.Count == 2);

            var firstNode = analysisGraph.Nodes[0] as ObjectMemberAnalysisNode;
            var secondNode = analysisGraph.Nodes[1] as ObjectMemberAnalysisNode;

            Assert.IsTrue(firstNode.Name == "PA1" && secondNode.Name == "PA2");
        }

        [TestMethod]
        public void CreateGraph_GivenScenario4_ReturnsObjectMemberAnalysisGraphWithCorrectDependencies()
        {
            //Arrange
            var reactor = CreateCase4();

            //Act
            var factory = new AnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.ObjectMemberLevel);

            //Assert

            var firstNode = analysisGraph.Nodes[0] as ObjectMemberAnalysisNode;
            var secondNode = analysisGraph.Nodes[1] as ObjectMemberAnalysisNode;

            Assert.IsTrue(firstNode.Predecessors.Count == 1 && firstNode.HasPredecessor(secondNode));
            Assert.IsTrue(secondNode.Successors.Count == 1 && secondNode.HasSuccessor(firstNode));
        }

        [TestMethod]
        public void CreateGraph_GivenScenario4_ReturnsObjectAnalysisGraphWithCorrectNodes()
        {
            //Arrange
            var reactor = CreateCase4();

            //Act
            var factory = new AnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.ObjectLevel);

            //Assert
            Assert.IsTrue(analysisGraph.Nodes.Count == 1);

            var firstNode = analysisGraph.Nodes[0] as ObjectAnalysisNode;

            Assert.IsTrue(firstNode.Name == "First object A");
        }

        [TestMethod]
        public void CreateGraph_GivenScenario4_ReturnsObjectAnalysisGraphWithCorrectDependencies()
        {
            //Arrange
            var reactor = CreateCase4();

            //Act
            var factory = new AnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.ObjectLevel);

            //Assert
            Assert.IsTrue(analysisGraph.Nodes.Count == 1);

            var firstNode = analysisGraph.Nodes[0] as ObjectAnalysisNode;

            Assert.IsTrue(firstNode.Predecessors.Count == 1 && firstNode.HasPredecessor(firstNode));
            Assert.IsTrue(firstNode.Successors.Count == 1 && firstNode.HasSuccessor(firstNode));
        }

        [TestMethod]
        public void CreateGraph_GivenScenario4_ReturnsClassMemberAnalysisGraphWithCorrectNodes()
        {
            //Arrange
            var reactor = CreateCase4();

            //Act
            var factory = new AnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.ClassMemberLevel);

            //Assert
            Assert.IsTrue(analysisGraph.Nodes.Count == 2);

            var firstNode = analysisGraph.Nodes[0] as ClassMemberAnalysisNode;
            var secondNode = analysisGraph.Nodes[1] as ClassMemberAnalysisNode;

            Assert.IsTrue(firstNode.Name == nameof(ClassA.PA1) && firstNode.OwnerClass.Name == nameof(ClassA));
            Assert.IsTrue(secondNode.Name == nameof(ClassA.PA2) && secondNode.OwnerClass.Name == nameof(ClassA));
        }

        [TestMethod]
        public void CreateGraph_GivenScenario4_ReturnsClassMemberAnalysisGraphWithCorrectDependencies()
        {
            //Arrange
            var reactor = CreateCase4();

            //Act
            var factory = new AnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.ClassMemberLevel);

            //Assert
            var firstNode = analysisGraph.Nodes[0] as ClassMemberAnalysisNode;
            var secondNode = analysisGraph.Nodes[1] as ClassMemberAnalysisNode;

            Assert.IsTrue(firstNode.Predecessors.Count == 1 && firstNode.HasPredecessor(secondNode));
            Assert.IsTrue(secondNode.Successors.Count == 1 && secondNode.HasSuccessor(firstNode));
        }

        [TestMethod]
        public void CreateGraph_GivenScenario4_ReturnsClassAnalysisGraphWithCorrectNodes()
        {
            //Arrange
            var reactor = CreateCase4();

            //Act
            var factory = new AnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.ClassLevel);

            //Assert
            Assert.IsTrue(analysisGraph.Nodes.Count == 1);

            var firstNode = analysisGraph.Nodes[0] as ClassAnalysisNode;

            Assert.IsTrue(firstNode.Name == nameof(ClassA));
        }

        [TestMethod]
        public void CreateGraph_GivenScenario4_ReturnsClassAnalysisGraphWithCorrectDependencies()
        {
            //Arrange
            var reactor = CreateCase4();

            //Act
            var factory = new AnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.ClassLevel);

            //Assert
            var firstNode = analysisGraph.Nodes[0] as ClassAnalysisNode;

            Assert.IsTrue(firstNode.Predecessors.Count == 1 && firstNode.HasPredecessor(firstNode));
            Assert.IsTrue(firstNode.Successors.Count == 1 && firstNode.HasSuccessor(firstNode));
        }

        [TestMethod]
        public void CreateGraph_GivenScenario4_ReturnsNamespaceAnalysisGraphWithCorrectNodes()
        {
            //Arrange
            var reactor = CreateCase4();

            //Act
            var factory = new AnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.NamespaceLevel);

            //Assert
            Assert.IsTrue(analysisGraph.Nodes.Count == 1);

            var firstNode = analysisGraph.Nodes[0] as NamespaceAnalysisNode;

            Assert.IsTrue(firstNode.Name == typeof(ClassA).Namespace);
        }

        [TestMethod]
        public void CreateGraph_GivenScenario4_ReturnsNamespaceAnalysisGraphWithCorrectDependencies()
        {
            //Arrange
            var reactor = CreateCase4();

            //Act
            var factory = new AnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.NamespaceLevel);

            //Assert
            var firstNode = analysisGraph.Nodes[0] as NamespaceAnalysisNode;

            Assert.IsTrue(firstNode.Predecessors.Count == 1 && firstNode.HasPredecessor(firstNode));
            Assert.IsTrue(firstNode.Successors.Count == 1 && firstNode.HasSuccessor(firstNode));
        }

        [TestMethod]
        public void CreateGraph_GivenScenario4_ReturnsAssemblyAnalysisGraphWithCorrectNodes()
        {
            //Arrange
            var reactor = CreateCase4();

            //Act
            var factory = new AnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.AssemblyLevel);

            //Assert
            Assert.IsTrue(analysisGraph.Nodes.Count == 1);

            var firstNode = analysisGraph.Nodes[0] as AssemblyAnalysisNode;

            Assert.IsTrue(firstNode.Name == typeof(ClassA).Assembly.ManifestModule.Name);
        }

        [TestMethod]
        public void CreateGraph_GivenScenario4_ReturnsAssemblyAnalysisGraphWithCorrectDependencies()
        {
            //Arrange
            var reactor = CreateCase4();

            //Act
            var factory = new AnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.AssemblyLevel);

            //Assert
            var firstNode = analysisGraph.Nodes[0] as AssemblyAnalysisNode;

            Assert.IsTrue(firstNode.Predecessors.Count == 1 && firstNode.HasPredecessor(firstNode));
            Assert.IsTrue(firstNode.Successors.Count == 1 && firstNode.HasSuccessor(firstNode));
        }

        #endregion

        #region CASE 5

        /// <summary>
        /// Two different classes, two objects, 4 members.
        /// </summary>
        /// <returns></returns>
        private static IReactor CreateCase5()
        {
            return AnalysisTestCases.CreateCase5();
        }

        [TestMethod]
        public void CreateGraph_GivenScenario5_ReturnsObjectMemberAnalysisGraphWithCorrectNodes()
        {
            //Arrange
            var reactor = CreateCase5();
            var factory = new AnalysisGraphFactory();

            //Act
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.ObjectMemberLevel);

            //Assert
            Assert.IsTrue(analysisGraph.Nodes.Count == 4);

            var firstNode = analysisGraph.Nodes[0] as ObjectMemberAnalysisNode;
            var secondNode = analysisGraph.Nodes[1] as ObjectMemberAnalysisNode;
            var thirdNode = analysisGraph.Nodes[2] as ObjectMemberAnalysisNode;
            var fourthNode = analysisGraph.Nodes[3] as ObjectMemberAnalysisNode;

            Assert.IsTrue(firstNode.Name == nameof(ClassA.PA1));
            Assert.IsTrue(secondNode.Name == nameof(ClassB.PB1));
            Assert.IsTrue(thirdNode.Name == nameof(ClassB.PB2));
            Assert.IsTrue(fourthNode.Name == nameof(ClassA.PA2));
        }

        [TestMethod]
        public void CreateGraph_GivenScenario5_ReturnsObjectMemberAnalysisGraphWithCorrectDependencies()
        {
            //Arrange
            var reactor = CreateCase5();
            var factory = new AnalysisGraphFactory();

            //Act
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.ObjectMemberLevel);

            //Assert
            var firstNode = analysisGraph.Nodes[0] as ObjectMemberAnalysisNode;
            var secondNode = analysisGraph.Nodes[1] as ObjectMemberAnalysisNode;
            var thirdNode = analysisGraph.Nodes[2] as ObjectMemberAnalysisNode;
            var fourthNode = analysisGraph.Nodes[3] as ObjectMemberAnalysisNode;

            Assert.IsTrue(firstNode.Predecessors.Count == 1 && firstNode.HasPredecessor(secondNode));
            Assert.IsTrue(secondNode.Successors.Count == 1 && secondNode.HasSuccessor(firstNode));

            Assert.IsTrue(thirdNode.Predecessors.Count == 1 && thirdNode.HasPredecessor(fourthNode));
            Assert.IsTrue(fourthNode.Successors.Count == 1 && fourthNode.HasSuccessor(thirdNode));
        }

        [TestMethod]
        public void CreateGraph_GivenScenario5_ReturnsObjectAnalysisGraphWithCorrectNodes()
        {
            //Arrange
            var reactor = CreateCase5();
            var factory = new AnalysisGraphFactory();

            //Act
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.ObjectLevel);

            //Assert
            Assert.IsTrue(analysisGraph.Nodes.Count == 2);

            var firstNode = analysisGraph.Nodes[0] as ObjectAnalysisNode;
            var secondNode = analysisGraph.Nodes[1] as ObjectAnalysisNode;

            Assert.IsTrue(firstNode.Name == "First object A" && firstNode.OwnerClass.Name == nameof(ClassA));
            Assert.IsTrue(secondNode.Name == "Second object B" && secondNode.OwnerClass.Name == nameof(ClassB));
        }

        [TestMethod]
        public void CreateGraph_GivenScenario5_ReturnsObjectAnalysisGraphWithCorrectDependencies()
        {
            //Arrange
            var reactor = CreateCase5();
            var factory = new AnalysisGraphFactory();

            //Act
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.ObjectLevel);

            //Assert
            Assert.IsTrue(analysisGraph.Nodes.Count == 2);
            var firstNode = analysisGraph.Nodes[0] as ObjectAnalysisNode;
            var secondNode = analysisGraph.Nodes[1] as ObjectAnalysisNode;

            Assert.IsTrue(firstNode.Predecessors.Count == 1 && firstNode.HasPredecessor(secondNode));
            Assert.IsTrue(secondNode.Successors.Count == 1 && secondNode.HasPredecessor(firstNode));
        }

        [TestMethod]
        public void CreateGraph_GivenScenario5_ReturnsClassMemberAnalysisGraphWithCorrectNodes()
        {
            //Arrange
            var reactor = CreateCase5();
            var factory = new AnalysisGraphFactory();

            //Act
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.ClassMemberLevel);

            //Assert
            Assert.IsTrue(analysisGraph.Nodes.Count == 4);

            var firstNode = analysisGraph.Nodes[0] as ClassMemberAnalysisNode;
            var secondNode = analysisGraph.Nodes[1] as ClassMemberAnalysisNode;
            var thirdNode = analysisGraph.Nodes[2] as ClassMemberAnalysisNode;
            var fourthNode = analysisGraph.Nodes[3] as ClassMemberAnalysisNode;

            Assert.IsTrue(firstNode.Name == nameof(ClassA.PA1) && firstNode.OwnerClass.Name == nameof(ClassA));
            Assert.IsTrue(secondNode.Name == nameof(ClassB.PB1) && secondNode.OwnerClass.Name == nameof(ClassB));
            Assert.IsTrue(thirdNode.Name == nameof(ClassB.PB2) && thirdNode.OwnerClass.Name == nameof(ClassB));
            Assert.IsTrue(fourthNode.Name == nameof(ClassA.PA2) && fourthNode.OwnerClass.Name == nameof(ClassA));

        }

        [TestMethod]
        public void CreateGraph_GivenScenario5_ReturnsClassMemberAnalysisGraphWithCorrectDependencies()
        {
            //Arrange
            var reactor = CreateCase5();
            var factory = new AnalysisGraphFactory();

            //Act
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.ClassMemberLevel);

            //Assert
            var firstNode = analysisGraph.Nodes[0] as ClassMemberAnalysisNode;
            var secondNode = analysisGraph.Nodes[1] as ClassMemberAnalysisNode;
            var thirdNode = analysisGraph.Nodes[2] as ClassMemberAnalysisNode;
            var fourthNode = analysisGraph.Nodes[3] as ClassMemberAnalysisNode;

            Assert.IsTrue(firstNode.Predecessors.Count == 1 && firstNode.HasPredecessor(secondNode));
            Assert.IsTrue(secondNode.Successors.Count == 1 && secondNode.HasSuccessor(firstNode));

            Assert.IsTrue(thirdNode.Predecessors.Count == 1 && thirdNode.HasPredecessor(fourthNode));
            Assert.IsTrue(fourthNode.Successors.Count == 1 && fourthNode.HasSuccessor(thirdNode));

        }

        [TestMethod]
        public void CreateGraph_GivenScenario5_ReturnsClassAnalysisGraphWithCorrectNodes()
        {
            //Arrange
            var reactor = CreateCase5();
            var factory = new AnalysisGraphFactory();

            //Act
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.ClassLevel);

            //Assert
            Assert.IsTrue(analysisGraph.Nodes.Count == 2);

            var firstNode = analysisGraph.Nodes[0] as ClassAnalysisNode;
            var secondNode = analysisGraph.Nodes[1] as ClassAnalysisNode;

            Assert.IsTrue(firstNode.Name == nameof(ClassA));
            Assert.IsTrue(secondNode.Name == nameof(ClassB));
        }

        [TestMethod]
        public void CreateGraph_GivenScenario5_ReturnsClassAnalysisGraphWithCorrectDependencies()
        {
            //Arrange
            var reactor = CreateCase5();
            var factory = new AnalysisGraphFactory();

            //Act
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.ClassLevel);

            //Assert

            var firstNode = analysisGraph.Nodes[0] as ClassAnalysisNode;
            var secondNode = analysisGraph.Nodes[1] as ClassAnalysisNode;

            Assert.IsTrue(firstNode.Predecessors.Count == 1 && firstNode.HasPredecessor(secondNode));
            Assert.IsTrue(secondNode.Successors.Count == 1 && secondNode.HasSuccessor(firstNode));
        }

        [TestMethod]
        public void CreateGraph_GivenScenario5_ReturnsNamespaceAnalysisGraphWithCorrectNodes()
        {
            //Arrange
            var reactor = CreateCase5();
            var factory = new AnalysisGraphFactory();

            //Act
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.NamespaceLevel);

            //Assert
            Assert.IsTrue(analysisGraph.Nodes.Count == 1);

            var firstNode = analysisGraph.Nodes[0] as NamespaceAnalysisNode;

            Assert.IsTrue(firstNode.Name == typeof(ClassA).Namespace);
        }

        [TestMethod]
        public void CreateGraph_GivenScenario5_ReturnsNamespaceAnalysisGraphWithCorrectDependencies()
        {
            //Arrange
            var reactor = CreateCase5();
            var factory = new AnalysisGraphFactory();

            //Act
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.NamespaceLevel);

            //Assert
            var firstNode = analysisGraph.Nodes[0] as NamespaceAnalysisNode;

            Assert.IsTrue(firstNode.Name == typeof(ClassA).Namespace);

            Assert.IsTrue(firstNode.Predecessors.Count == 1 && firstNode.HasPredecessor(firstNode));
            Assert.IsTrue(firstNode.Successors.Count == 1 && firstNode.HasSuccessor(firstNode));
        }

        [TestMethod]
        public void CreateGraph_GivenScenario5_ReturnsAssemblyAnalysisGraphWithCorrectNodes()
        {
            //Arrange
            var reactor = CreateCase5();
            var factory = new AnalysisGraphFactory();

            //Act
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.AssemblyLevel);

            //Assert
            Assert.IsTrue(analysisGraph.Nodes.Count == 1);

            var firstNode = analysisGraph.Nodes[0] as AssemblyAnalysisNode;

            Assert.IsTrue(firstNode.Name == typeof(ClassA).Assembly.ManifestModule.Name);
        }

        [TestMethod]
        public void CreateGraph_GivenScenario5_ReturnsAssemblyAnalysisGraphWithCorrectDependencies()
        {
            //Arrange
            var reactor = CreateCase5();
            var factory = new AnalysisGraphFactory();

            //Act
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.AssemblyLevel);

            //Assert
            var firstNode = analysisGraph.Nodes[0] as AssemblyAnalysisNode;

            Assert.IsTrue(firstNode.Name == typeof(ClassA).Assembly.ManifestModule.Name);

            Assert.IsTrue(firstNode.Predecessors.Count == 1 && firstNode.HasPredecessor(firstNode));
            Assert.IsTrue(firstNode.Successors.Count == 1 && firstNode.HasSuccessor(firstNode));
        }

        #endregion
    }
}
