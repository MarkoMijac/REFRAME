using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeCore;
using ReframeAnalyzer.Graph;
using ReframeAnalyzer.Exceptions;
using ReframeCore.Factories;
using ReframeCoreExamples.E09;
using ReframeExporter;
using ReframeCoreFluentAPI;

namespace ReframeAnalyzerTests
{
    [TestClass]
    public class AnalysisGraphFactoryTests
    {
        [TestMethod]
        [ExpectedException(typeof(AnalyzerException))]
        public void CreateGraph_XmlSourceIsEmpty_ThrowsException()
        {
            //Arrange
            AnalysisGraphFactory factory = new AnalysisGraphFactory();
            
            //Act
            var analysisGraph = factory.CreateGraph("", AnalysisLevel.ClassLevel);
        }

        [TestMethod]
        [ExpectedException(typeof(AnalyzerException))]
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
        public void CreateGraph_GivenScenario1_ReturnsAnalysisGraphWithCorrectNodes()
        {
            //Arrange
            IReactor reactor = CreateScenario1();

            //Act
            var factory = new AnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.ClassLevel);

            //Assert
            Assert.IsTrue(analysisGraph.Nodes.Count == 3
                && analysisGraph.Nodes[0].Name == nameof(ClassA)
                && analysisGraph.Nodes[1].Name == nameof(ClassB)
                && analysisGraph.Nodes[2].Name == nameof(ClassC));
        }

        [TestMethod]
        public void CreateGraph_GivenScenario1_ReturnsAnalysisGraphWithCorrectDependencies()
        {
            //Arrange
            IReactor reactor = CreateScenario1();

            //Act
            var factory = new AnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.ClassLevel);

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
        public void CreateGraph_GivenScenario2_ReturnsAnalysisGraphWithCorrectNodes()
        {
            //Arrange
            IReactor reactor = CreateScenario2();

            //Act
            var factory = new AnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.ClassLevel);

            //Assert
            Assert.IsTrue(analysisGraph.Nodes.Count == 3
                && analysisGraph.Nodes[0].Name == nameof(ClassC)
                && analysisGraph.Nodes[1].Name == nameof(ClassB)
                && analysisGraph.Nodes[2].Name == nameof(ClassA));
        }

        [TestMethod]
        public void CreateGraph_GivenScenario2_ReturnsAnalysisGraphWithCorrectDependencies()
        {
            //Arrange
            IReactor reactor = CreateScenario2();

            //Act
            var factory = new AnalysisGraphFactory();
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            var analysisGraph = factory.CreateGraph(xmlSource, AnalysisLevel.ClassLevel);

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

        #region CASE 3

        private static IReactor CreateCase3()
        {
            /*
             * Same class, different objects, same member.
             */
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("S1");

            ClassA firstObject = new ClassA("First object A");
            ClassA secondObject = new ClassA("Second object A");

            reactor.Let(() => firstObject.PA1)
                .DependOn(() => secondObject.PA1);
            return reactor;
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
            /*
             * Same class, same objects, different members.
             */
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("S1");
            
            ClassA objA = new ClassA("First object A");
            reactor.Let(() => objA.PA1)
                .DependOn(() => objA.PA2);

            return reactor;
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
        private IReactor CreateCase5()
        {
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("S1");
            ClassA objA = new ClassA("First object A");
            ClassB objB = new ClassB("Second object B");

            reactor.Let(() => objA.PA1).DependOn(() => objB.PB1);
            reactor.Let(() => objB.PB2).DependOn(() => objA.PA2);

            return reactor;
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

        #endregion





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
            var analysisGraph = factory.CreateGraph(new XmlReactorDetailExporter(reactor.Identifier).Export(), AnalysisLevel.ClassLevel);

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
