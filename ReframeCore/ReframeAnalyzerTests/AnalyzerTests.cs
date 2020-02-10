using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeAnalyzer;
using ReframeCore.Factories;
using ReframeCoreExamples.E09;
using ReframeAnalyzer.Graph;
using System.Collections.Generic;
using ReframeExporter;
using ReframeCoreFluentAPI;
using ReframeAnalyzer.Exceptions;
using ReframeCoreExamples.E09._01;

namespace ReframeAnalyzerTests
{
    [TestClass]
    public class AnalyzerTests
    {
        #region GetOrphanNodes

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void GetOrphanNodes_GivenGraphIsNull_ThrowsException()
        {
            //Arrange
            IAnalysisGraph analysisGraph = null;
            var analyzer = new Analyzer();

            //Act
            var orphanNodes = analyzer.GetOrphanNodes(analysisGraph);
        }

        [TestMethod]
        public void GetOrphanNodes_GivenGraphIsEmpty_ReturnsEmptyCollection()
        {
            //Arrange
            var reactor = AnalysisTestHelper.CreateEmptyReactor();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);
            var analyzer = new Analyzer();

            //Act
            var orphanNodes = analyzer.GetOrphanNodes(analysisGraph);

            //Assert
            Assert.IsTrue(orphanNodes.Count == 0);
        }

        [TestMethod]
        public void GetOrphanNodes_GivenGraphContainsNoOrphanNodes_ReturnsEmptyCollection()
        {
            //Arrange
            var reactor = AnalysisTestHelper.CreateCase1();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);
            var analyzer = new Analyzer();

            //Act
            var orphanNodes = analyzer.GetOrphanNodes(analysisGraph);

            //Assert
            Assert.IsTrue(orphanNodes.Count == 0);
        }

        [TestMethod]
        public void GetOrphanNodes_GivenGraphContainsOrphanNodes_ReturnsCorrectCollection()
        {
            //Arrange
            var reactor = AnalysisTestHelper.CreateCase6();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);
            var analyzer = new Analyzer();

            //Act
            var orphanNodes = analyzer.GetOrphanNodes(analysisGraph);

            //Assert
            Assert.IsTrue(orphanNodes.Count == 2);

            var orphan1 = orphanNodes[0];
            var orphan2 = orphanNodes[1];

            Assert.IsTrue(orphan1.Name == nameof(ClassA.PA2));
            Assert.IsTrue(orphan2.Name == nameof(ClassB.PB2));
        }

        #endregion

        #region GetLeafNodes

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void GetLeafNodes_GivenGraphIsNull_ThrowsException()
        {
            //Arrange
            IAnalysisGraph analysisGraph = null;
            var analyzer = new Analyzer();

            //Act
            var leafNodes = analyzer.GetLeafNodes(analysisGraph);
        }

        [TestMethod]
        public void GetLeafNodes_GivenGraphIsEmpty_ReturnsEmptyCollection()
        {
            //Arrange
            var reactor = AnalysisTestHelper.CreateEmptyReactor();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);
            var analyzer = new Analyzer();

            //Act
            var leafNodes = analyzer.GetLeafNodes(analysisGraph);

            //Assert
            Assert.IsTrue(leafNodes.Count == 0);
        }

        [TestMethod]
        public void GetLeafNodes_GivenGraphContainsNoLeafNodes_ReturnsEmptyCollection()
        {
            //Arrange
            var reactor = AnalysisTestHelper.CreateCase7();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);
            var analyzer = new Analyzer();

            //Act
            var leafNodes = analyzer.GetLeafNodes(analysisGraph);

            //Assert
            Assert.IsTrue(leafNodes.Count == 0);
        }

        [TestMethod]
        public void GetLeafNodes_GivenGraphContainsLeafNodes_ReturnsCorrectCollection()
        {
            //Arrange
            var reactor = AnalysisTestHelper.CreateCase6();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);
            var analyzer = new Analyzer();

            //Act
            var leafNodes = analyzer.GetLeafNodes(analysisGraph);

            //Assert
            Assert.IsTrue(leafNodes.Count == 3);

            var sink1 = leafNodes[0];
            var source1 = leafNodes[1];
            var source2 = leafNodes[2];

            Assert.IsTrue(sink1.Name == nameof(ClassG.PG1));
            Assert.IsTrue(source1.Name == nameof(ClassA.PA1));
            Assert.IsTrue(source2.Name == nameof(ClassD.PD1));
        }

        #endregion

        #region GetSourceNodes

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void GetSourceNodes_GivenGraphIsNull_ThrowsException()
        {
            //Arrange
            IAnalysisGraph analysisGraph = null;
            var analyzer = new Analyzer();

            //Act
            var sourceNodes = analyzer.GetSourceNodes(analysisGraph);
        }

        [TestMethod]
        public void GetSourceNodes_GivenGraphIsEmpty_ReturnsEmptyCollection()
        {
            //Arrange
            var reactor = AnalysisTestHelper.CreateEmptyReactor();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);
            var analyzer = new Analyzer();

            //Act
            var sourceNodes = analyzer.GetSourceNodes(analysisGraph);

            //Assert
            Assert.IsTrue(sourceNodes.Count == 0);
        }

        [TestMethod]
        public void GetSourceNodes_GivenGraphContainsNoSourceNodes_ReturnsEmptyCollection()
        {
            //Arrange
            var reactor = AnalysisTestHelper.CreateCase7();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);
            var analyzer = new Analyzer();

            //Act
            var sourceNodes = analyzer.GetSourceNodes(analysisGraph);

            //Assert
            Assert.IsTrue(sourceNodes.Count == 0);
        }

        [TestMethod]
        public void GetSourceNodes_GivenGraphContainsSourceNodes_ReturnsCorrectCollection()
        {
            //Arrange
            var reactor = AnalysisTestHelper.CreateCase6();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);
            var analyzer = new Analyzer();

            //Act
            var sourceNodes = analyzer.GetSourceNodes(analysisGraph);

            //Assert
            Assert.IsTrue(sourceNodes.Count == 2);

            var source1 = sourceNodes[0];
            var source2 = sourceNodes[1];

            Assert.IsTrue(source1.Name == nameof(ClassA.PA1));
            Assert.IsTrue(source2.Name == nameof(ClassD.PD1));
        }

        #endregion

        #region GetSinkNodes

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void GetSinkNodes_GivenGraphIsNull_ThrowsException()
        {
            //Arrange
            IAnalysisGraph analysisGraph = null;
            var analyzer = new Analyzer();

            //Act
            var sinkNodes = analyzer.GetSinkNodes(analysisGraph);
        }

        [TestMethod]
        public void GetSinkNodes_GivenGraphIsEmpty_ReturnsEmptyCollection()
        {
            //Arrange
            var reactor = AnalysisTestHelper.CreateEmptyReactor();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);
            var analyzer = new Analyzer();

            //Act
            var sinkNodes = analyzer.GetSinkNodes(analysisGraph);

            //Assert
            Assert.IsTrue(sinkNodes.Count == 0);
        }

        [TestMethod]
        public void GetSinkNodes_GivenGraphContainsNoSinkNodes_ReturnsEmptyCollection()
        {
            //Arrange
            var reactor = AnalysisTestHelper.CreateCase7();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);
            var analyzer = new Analyzer();

            //Act
            var sinkNodes = analyzer.GetSinkNodes(analysisGraph);

            //Assert
            Assert.IsTrue(sinkNodes.Count == 0);
        }

        [TestMethod]
        public void GetSinkNodes_GivenGraphContainsSinkNodes_ReturnsCorrectCollection()
        {
            //Arrange
            var reactor = AnalysisTestHelper.CreateCase6();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);
            var analyzer = new Analyzer();

            //Act
            var sinkNodes = analyzer.GetSinkNodes(analysisGraph);

            //Assert
            Assert.IsTrue(sinkNodes.Count == 1);

            var sink1 = sinkNodes[0];

            Assert.IsTrue(sink1.Name == nameof(ClassG.PG1));
        }

        #endregion

        #region GetIntermediaryNodes

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void GetIntermediaryNodes_GivenGraphIsNull_ThrowsException()
        {
            //Arrange
            IAnalysisGraph analysisGraph = null;
            var analyzer = new Analyzer();

            //Act
            var intermediaryNodes = analyzer.GetIntermediaryNodes(analysisGraph);
        }

        [TestMethod]
        public void GetIntermediaryNodes_GivenGraphIsEmpty_ReturnsEmptyCollection()
        {
            //Arrange
            var reactor = AnalysisTestHelper.CreateEmptyReactor();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);
            var analyzer = new Analyzer();

            //Act
            var intermediaryNodes = analyzer.GetIntermediaryNodes(analysisGraph);

            //Assert
            Assert.IsTrue(intermediaryNodes.Count == 0);
        }

        [TestMethod]
        public void GetIntermediaryNodes_GivenGraphContainsNoIntermediaryNodes_ReturnsEmptyCollection()
        {
            //Arrange
            var reactor = AnalysisTestHelper.CreateCase8();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);
            var analyzer = new Analyzer();

            //Act
            var intermediaryNodes = analyzer.GetIntermediaryNodes(analysisGraph);

            //Assert
            Assert.IsTrue(intermediaryNodes.Count == 0);
        }

        [TestMethod]
        public void GetIntermediaryNodes_GivenGraphContainsIntermediaryNodes_ReturnsCorrectCollection()
        {
            //Arrange
            var reactor = AnalysisTestHelper.CreateCase6();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);
            var analyzer = new Analyzer();

            //Act
            var intermediaryNodes = analyzer.GetIntermediaryNodes(analysisGraph);

            //Assert
            Assert.IsTrue(intermediaryNodes.Count == 4);

            var nodeF = intermediaryNodes[0];
            var nodeE = intermediaryNodes[1];
            var nodeB = intermediaryNodes[2];
            var nodeC = intermediaryNodes[3];

            Assert.IsTrue(nodeF.Name == nameof(ClassF.PF1));
            Assert.IsTrue(nodeE.Name == nameof(ClassE.PE1));
            Assert.IsTrue(nodeB.Name == nameof(ClassB.PB1));
            Assert.IsTrue(nodeC.Name == nameof(ClassC.PC1));
        }

        #endregion

        #region GetNodeAndItsPredecessors(IAnalysisGraph analysisGraph, string nodeIdentifier)

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void GetNodeAndItsPredecessors_GivenNonExistingNodeIdentifier_ThrowsException()
        {
            //Arrange
            uint nodeIdentifier = 11111111;
            var reactor = AnalysisTestHelper.CreateCase1();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);
            var analyzer = new Analyzer();

            //Act
            var predecessors = analyzer.GetNodeAndItsPredecessors(analysisGraph, nodeIdentifier);
        }

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void GetNodeAndItsPredecessors_GivenAnalysisGraphIsNull_ThrowsException()
        {
            //Arrange
            var reactor = AnalysisTestHelper.CreateCase1();
            IAnalysisGraph analysisGraph = null;
            var analyzer = new Analyzer();
            var nodeIdentifier = reactor.Graph.Nodes[0].Identifier;

            //Act
            var predecessors = analyzer.GetNodeAndItsPredecessors(analysisGraph, nodeIdentifier);
        }

        [TestMethod]
        public void GetNodeAndItsPredecessors_GivenNodeHasNoPredecessors_ReturnOnlySelectedNode()
        {
            var reactor = AnalysisTestHelper.CreateCase6();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);
            var analyzer = new Analyzer();
            var sourceNodes = analyzer.GetSourceNodes(analysisGraph);
            var nodeIdentifier = sourceNodes[0].Identifier;

            //Act
            var predecessors = analyzer.GetNodeAndItsPredecessors(analysisGraph, nodeIdentifier);

            //Assert
            Assert.IsTrue(predecessors.Count == 1);
            Assert.IsTrue(predecessors.Contains(sourceNodes[0]));
        }

        [TestMethod]
        public void GetNodeAndItsPredecessors_GivenNodeHasPredecessors_ReturnsCorrectPredecessorsCollection()
        {
            var reactor = AnalysisTestHelper.CreateCase6();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);
            var analyzer = new Analyzer();
            var nodeIdentifier = reactor.Graph.Nodes[1].Identifier;

            //Act
            var predecessors = analyzer.GetNodeAndItsPredecessors(analysisGraph, nodeIdentifier);

            //Assert
            Assert.IsTrue(predecessors.Count == 3);

            Assert.IsTrue(predecessors.Exists(n => n.Name == nameof(ClassF.PF1)));
            Assert.IsTrue(predecessors.Exists(n => n.Name == nameof(ClassB.PB1)));
            Assert.IsTrue(predecessors.Exists(n => n.Name == nameof(ClassA.PA1)));
        }

        #endregion

        #region GetNodeAndItsPredecessors(IAnalysisGraph analysisGraph, uint nodeIdentifier, int maxDepth)

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void GetNodeAndItsPredecessors2_GivenNonExistingNodeIdentifier_ThrowsException()
        {
            //Arrange
            uint nodeIdentifier = 11111111;
            var reactor = AnalysisTestHelper.CreateCase1();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);
            var analyzer = new Analyzer();
            int maxDepth = 1;

            //Act
            var predecessors = analyzer.GetNodeAndItsPredecessors(analysisGraph, nodeIdentifier, maxDepth);
        }

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void GetNodeAndItsPredecessors2_GivenAnalysisGraphIsNull_ThrowsException()
        {
            //Arrange
            var reactor = AnalysisTestHelper.CreateCase1();
            IAnalysisGraph analysisGraph = null;
            var analyzer = new Analyzer();
            var nodeIdentifier = reactor.Graph.Nodes[0].Identifier;
            int maxDepth = 1;

            //Act
            var predecessors = analyzer.GetNodeAndItsPredecessors(analysisGraph, nodeIdentifier, maxDepth);
        }

        [TestMethod]
        public void GetNodeAndItsPredecessors2_GivenNodeHasNoPredecessors_ReturnsOnlySelectedNode()
        {
            var reactor = AnalysisTestHelper.CreateCase6();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);
            var analyzer = new Analyzer();
            var sourceNodes = analyzer.GetSourceNodes(analysisGraph);
            var nodeIdentifier = sourceNodes[0].Identifier;
            int maxDepth = 1;

            //Act
            var predecessors = analyzer.GetNodeAndItsPredecessors(analysisGraph, nodeIdentifier, maxDepth);

            //Assert
            Assert.IsTrue(predecessors.Count == 1);
            Assert.IsTrue(predecessors.Contains(sourceNodes[0]));
        }

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void GetNodeAndItsPredecessors2_GivenInvalidMaxDepth_ThrowsException()
        {
            var reactor = AnalysisTestHelper.CreateCase6();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);
            var analyzer = new Analyzer();
            var nodeIdentifier = reactor.Graph.Nodes[1].Identifier;

            //Act
            var predecessors = analyzer.GetNodeAndItsPredecessors(analysisGraph, nodeIdentifier, 0);
        }

        [TestMethod]
        public void GetNodeAndItsPredecessors2_GivenNodeHasPredecessors_ReturnsCorrectPredecessorsCollection()
        {
            var reactor = AnalysisTestHelper.CreateCase6();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);
            var analyzer = new Analyzer();
            var nodeIdentifier = reactor.Graph.Nodes[0].Identifier;
            int maxDepth = 2;

            //Act
            var predecessors = analyzer.GetNodeAndItsPredecessors(analysisGraph, nodeIdentifier, maxDepth);

            //Assert
            Assert.IsTrue(predecessors.Count == 5);
            Assert.IsTrue(predecessors.Exists(n => n.Name == nameof(ClassG.PG1)));
            Assert.IsTrue(predecessors.Exists(n => n.Name == nameof(ClassF.PF1)));
            Assert.IsTrue(predecessors.Exists(n => n.Name == nameof(ClassE.PE1)));
            Assert.IsTrue(predecessors.Exists(n => n.Name == nameof(ClassB.PB1)));
            Assert.IsTrue(predecessors.Exists(n => n.Name == nameof(ClassC.PC1)));
        }

        #endregion

        #region GetSuccessors(IAnalysisGraph analysisGraph, uint nodeIdentifier)

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void GetSuccessors_GivenNonExistingNodeIdentifier_ThrowsException()
        {
            //Arrange
            uint nodeIdentifier = 11111111;
            var reactor = AnalysisTestHelper.CreateCase1();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);
            var analyzer = new Analyzer();

            //Act
            var successors = analyzer.GetSuccessors(analysisGraph, nodeIdentifier);
        }

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void GetSuccessors_GivenAnalysisGraphIsNull_ThrowsException()
        {
            //Arrange
            var reactor = AnalysisTestHelper.CreateCase1();
            IAnalysisGraph analysisGraph = null;
            var analyzer = new Analyzer();
            var nodeIdentifier = reactor.Graph.Nodes[0].Identifier;

            //Act
            var successors = analyzer.GetSuccessors(analysisGraph, nodeIdentifier);
        }

        [TestMethod]
        public void GetSuccessors_GivenNodeHasNoSuccessors_ReturnsEmptySuccessorsCollection()
        {
            var reactor = AnalysisTestHelper.CreateCase6();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);
            var analyzer = new Analyzer();
            var sinkNodes = analyzer.GetSinkNodes(analysisGraph);
            var nodeIdentifier = sinkNodes[0].Identifier;

            //Act
            var successors = analyzer.GetSuccessors(analysisGraph, nodeIdentifier);

            //Assert
            Assert.IsTrue(successors.Count == 0);
        }

        [TestMethod]
        public void GetSuccessors_GivenNodeHasSuccessors_ReturnsCorrectSucessorsCollection()
        {
            var reactor = AnalysisTestHelper.CreateCase6();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);
            var analyzer = new Analyzer();
            var nodeIdentifier = reactor.Graph.Nodes[3].Identifier;

            //Act
            var successors = analyzer.GetSuccessors(analysisGraph, nodeIdentifier);

            //Assert
            Assert.IsTrue(successors.Count == 3);
            var s1 = successors[0];
            var s2 = successors[1];
            var s3 = successors[2];

            Assert.IsTrue(s1.Name == nameof(ClassF.PF1));
            Assert.IsTrue(s2.Name == nameof(ClassG.PG1));
            Assert.IsTrue(s3.Name == nameof(ClassE.PE1));
        }

        #endregion

        #region GetSuccessors(IAnalysisGraph analysisGraph, uint nodeIdentifier, int maxDepth)

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void GetSuccessors2_GivenNonExistingNodeIdentifier_ThrowsException()
        {
            //Arrange
            uint nodeIdentifier = 11111111;
            var reactor = AnalysisTestHelper.CreateCase1();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);
            var analyzer = new Analyzer();
            int maxDepth = 1;

            //Act
            var successors = analyzer.GetSuccessors(analysisGraph, nodeIdentifier, maxDepth);
        }

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void GetSuccessors2_GivenAnalysisGraphIsNull_ThrowsException()
        {
            //Arrange
            var reactor = AnalysisTestHelper.CreateCase1();
            IAnalysisGraph analysisGraph = null;
            var analyzer = new Analyzer();
            var nodeIdentifier = reactor.Graph.Nodes[0].Identifier;
            int maxDepth = 1;

            //Act
            var successors = analyzer.GetSuccessors(analysisGraph, nodeIdentifier, maxDepth);
        }

        [TestMethod]
        public void GetSuccessors2_GivenNodeHasNoSuccessors_ReturnsEmptySuccessorsCollection()
        {
            var reactor = AnalysisTestHelper.CreateCase6();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);
            var analyzer = new Analyzer();
            var sinkNodes = analyzer.GetSinkNodes(analysisGraph);
            var nodeIdentifier = sinkNodes[0].Identifier;
            int maxDepth = 1;

            //Act
            var successors = analyzer.GetSuccessors(analysisGraph, nodeIdentifier, maxDepth);

            //Assert
            Assert.IsTrue(successors.Count == 0);
        }

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void GetSuccessors2_GivenInvalidMaxDepth_ThrowsException()
        {
            var reactor = AnalysisTestHelper.CreateCase6();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);
            var analyzer = new Analyzer();
            var nodeIdentifier = reactor.Graph.Nodes[1].Identifier;

            //Act
            var successors = analyzer.GetSuccessors(analysisGraph, nodeIdentifier, 0);
        }

        [TestMethod]
        public void GetSuccessors2_GivenNodeHasSuccessors_ReturnsCorrectSuccessorsCollection()
        {
            var reactor = AnalysisTestHelper.CreateCase6();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);
            var analyzer = new Analyzer();
            var nodeIdentifier = reactor.Graph.Nodes[3].Identifier;
            int maxDepth = 1;

            //Act
            var successors = analyzer.GetSuccessors(analysisGraph, nodeIdentifier, maxDepth);

            //Assert
            Assert.IsTrue(successors.Count == 2);
            var p1 = successors[0];
            var p2 = successors[1];

            Assert.IsTrue(p1.Name == nameof(ClassF.PF1));
            Assert.IsTrue(p2.Name == nameof(ClassE.PE1));
        }

        #endregion

        #region GetNeighbours(IAnalysisGraph analysisGraph, uint nodeIdentifier)

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void GetNeighbours_GivenNonExistingNodeIdentifier_ThrowsException()
        {
            //Arrange
            uint nodeIdentifier = 11111111;
            var reactor = AnalysisTestHelper.CreateCase1();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);
            var analyzer = new Analyzer();

            //Act
            var neighbours = analyzer.GetNeighbours(analysisGraph, nodeIdentifier);
        }

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void GetNeighbours_GivenAnalysisGraphIsNull_ThrowsException()
        {
            //Arrange
            var reactor = AnalysisTestHelper.CreateCase1();
            IAnalysisGraph analysisGraph = null;
            var analyzer = new Analyzer();
            var nodeIdentifier = reactor.Graph.Nodes[0].Identifier;

            //Act
            var neighbours = analyzer.GetNeighbours(analysisGraph, nodeIdentifier);
        }

        [TestMethod]
        public void GetNeighbours_GivenNodeHasNoNeighbours_ReturnsEmptySuccessorsCollection()
        {
            var reactor = AnalysisTestHelper.CreateCase6();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);
            var analyzer = new Analyzer();
            var orphanNodes = analyzer.GetOrphanNodes(analysisGraph);
            var nodeIdentifier = orphanNodes[0].Identifier;

            //Act
            var neighbours = analyzer.GetNeighbours(analysisGraph, nodeIdentifier);

            //Assert
            Assert.IsTrue(neighbours.Count == 0);
        }

        [TestMethod]
        public void GetNeighbours_GivenNodeHasSuccessors_ReturnsCorrectSucessorsCollection()
        {
            var reactor = AnalysisTestHelper.CreateCase6();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);
            var analyzer = new Analyzer();
            var nodeIdentifier = reactor.Graph.Nodes[3].Identifier;

            //Act
            var neighbours = analyzer.GetNeighbours(analysisGraph, nodeIdentifier);

            //Assert
            Assert.IsTrue(neighbours.Count == 5);
            Assert.IsTrue(neighbours.Exists(n => n.Name == nameof(ClassA.PA1)));
            Assert.IsTrue(neighbours.Exists(n => n.Name == nameof(ClassB.PB1)));
            Assert.IsTrue(neighbours.Exists(n => n.Name == nameof(ClassF.PF1)));
            Assert.IsTrue(neighbours.Exists(n => n.Name == nameof(ClassE.PE1)));
            Assert.IsTrue(neighbours.Exists(n => n.Name == nameof(ClassG.PG1)));

        }

        #endregion

        private AnalysisGraphFactory graphFactory = new AnalysisGraphFactory();

        [TestMethod]
        public void GetClassAnalysisGraphSourceNodes()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.GetOrCreateReactor("R1");

            ClassA objA = new ClassA();
            ClassB objB = new ClassB();
            ClassC objC = new ClassC();

            reactor.Let(() => objA.PA1)
                .DependOn(() => objB.PB1, () => objC.PC1);

            var xmlExporter = new XmlReactorDetailExporter(reactor.Identifier);
            var xmlSource = xmlExporter.Export();

            //Act
            var analyzer = new Analyzer();
            var analysisGraph = graphFactory.CreateGraph(xmlSource, AnalysisLevel.ClassLevel);

            IEnumerable<IAnalysisNode> sourceNodes = analyzer.GetSourceNodes(analysisGraph);

            //Assert
            Assert.IsTrue(sourceNodes != null);
        }

        [TestMethod]
        public void GetPredecessors()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.GetOrCreateReactor("R1");

            ClassA objA = new ClassA();
            ClassB objB = new ClassB();
            ClassC objC = new ClassC();
            ClassD objD = new ClassD();
            ClassE objE = new ClassE();
            ClassF objF = new ClassF();

            reactor.Let(() => objE.PE1).DependOn(() => objB.PB1);
            reactor.Let(() => objD.PD1).DependOn(() => objB.PB1);
            reactor.Let(() => objD.PD1).DependOn(() => objC.PC1);
            reactor.Let(() => objB.PB1).DependOn(() => objA.PA1);
            reactor.Let(() => objB.PB1).DependOn(() => objF.PF1);
            reactor.Let(() => objC.PC1).DependOn(() => objA.PA1);

            //Act
            var xmlExporter = new XmlReactorDetailExporter(reactor.Identifier);
            string xmlSource = xmlExporter.Export();
            var analyzer = new Analyzer();
            var analysisGraph = graphFactory.CreateGraph(xmlSource, AnalysisLevel.ClassLevel);
            var startingNode = analysisGraph.Nodes.Find(n => n.Name == nameof(ClassD));
            
            IEnumerable<IAnalysisNode> predecessors = analyzer.GetNodeAndItsPredecessors(analysisGraph, startingNode.Identifier);

            //Assert
            Assert.IsTrue(predecessors != null);
        }

        [TestMethod]
        public void GetPredecessors_WithMaxDepth()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.GetOrCreateReactor("R1");

            ClassA objA = new ClassA();
            ClassB objB = new ClassB();
            ClassC objC = new ClassC();
            ClassD objD = new ClassD();
            ClassE objE = new ClassE();
            ClassF objF = new ClassF();

            reactor.Let(() => objF.PF1).DependOn(() => objD.PD1);
            reactor.Let(() => objF.PF1).DependOn(() => objE.PE1);
            reactor.Let(() => objD.PD1).DependOn(() => objB.PB1);
            reactor.Let(() => objD.PD1).DependOn(() => objC.PC1);
            reactor.Let(() => objE.PE1).DependOn(() => objC.PC1);
            reactor.Let(() => objB.PB1).DependOn(() => objA.PA1);
            reactor.Let(() => objC.PC1).DependOn(() => objA.PA1);

            //Act
            var xmlExporter = new XmlReactorDetailExporter(reactor.Identifier);
            string xmlSource = xmlExporter.Export();
            var analyzer = new Analyzer();
            var analysisGraph = graphFactory.CreateGraph(xmlSource, AnalysisLevel.ClassLevel);
            var startingNode = analysisGraph.Nodes.Find(n => n.Name == nameof(ClassF));

            IEnumerable<IAnalysisNode> predecessors = analyzer.GetNodeAndItsPredecessors(analysisGraph, startingNode.Identifier, 3);

            //Assert
            Assert.IsTrue(predecessors != null);
        }

        [TestMethod]
        public void GetSuccessors_ForDepth()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.GetOrCreateReactor("R1");

            ClassA objA = new ClassA();
            ClassB objB = new ClassB();
            ClassC objC = new ClassC();
            ClassD objD = new ClassD();
            ClassE objE = new ClassE();
            ClassF objF = new ClassF();

            reactor.Let(() => objF.PF1).DependOn(() => objD.PD1);
            reactor.Let(() => objF.PF1).DependOn(() => objE.PE1);
            reactor.Let(() => objD.PD1).DependOn(() => objB.PB1);
            reactor.Let(() => objD.PD1).DependOn(() => objC.PC1);
            reactor.Let(() => objE.PE1).DependOn(() => objC.PC1);
            reactor.Let(() => objB.PB1).DependOn(() => objA.PA1);
            reactor.Let(() => objC.PC1).DependOn(() => objA.PA1);

            //Act
            var xmlExporter = new XmlReactorDetailExporter(reactor.Identifier);
            string xmlSource = xmlExporter.Export();
            var analyzer = new Analyzer();
            var analysisGraph = graphFactory.CreateGraph(xmlSource, AnalysisLevel.ClassLevel);
            var startingNode = analysisGraph.Nodes.Find(n => n.Name == nameof(ClassC));

            IEnumerable<IAnalysisNode> successors = analyzer.GetSuccessors(analysisGraph, startingNode.Identifier, 2);

            //Assert
            Assert.IsTrue(successors != null);
        }
    }
}
