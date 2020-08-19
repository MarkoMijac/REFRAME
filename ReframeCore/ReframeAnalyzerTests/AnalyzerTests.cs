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

        #region GetNodeAndItsSuccessors(IAnalysisGraph analysisGraph, uint nodeIdentifier)

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void GetNodeAndItsSuccessors_GivenNonExistingNodeIdentifier_ThrowsException()
        {
            //Arrange
            uint nodeIdentifier = 11111111;
            var reactor = AnalysisTestHelper.CreateCase1();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);
            var analyzer = new Analyzer();

            //Act
            var successors = analyzer.GetNodeAndItsPredecessors(analysisGraph, nodeIdentifier);
        }

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void GetNodeAndItsSuccessors_GivenAnalysisGraphIsNull_ThrowsException()
        {
            //Arrange
            var reactor = AnalysisTestHelper.CreateCase1();
            IAnalysisGraph analysisGraph = null;
            var analyzer = new Analyzer();
            var nodeIdentifier = reactor.Graph.Nodes[0].Identifier;

            //Act
            var successors = analyzer.GetNodeAndItsPredecessors(analysisGraph, nodeIdentifier);
        }

        [TestMethod]
        public void GetNodeAndItsSuccessors_GivenNodeHasNoSuccessors_ReturnsOnlySelectedNode()
        {
            var reactor = AnalysisTestHelper.CreateCase6();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);
            var analyzer = new Analyzer();
            var sinkNodes = analyzer.GetSinkNodes(analysisGraph);
            var nodeIdentifier = sinkNodes[0].Identifier;

            //Act
            var successors = analyzer.GetNodeAndItsPredecessors(analysisGraph, nodeIdentifier);

            //Assert
            Assert.IsTrue(successors.Count == 1);
            Assert.IsTrue(successors.Contains(sinkNodes[0]));
        }

        [TestMethod]
        public void GetNodeAndItsSuccessors_GivenNodeHasSuccessors_ReturnsCorrectSucessorsCollection()
        {
            var reactor = AnalysisTestHelper.CreateCase6();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);
            var analyzer = new Analyzer();
            var nodeIdentifier = reactor.Graph.Nodes[3].Identifier;

            //Act
            var successors = analyzer.GetNodeAndItsPredecessors(analysisGraph, nodeIdentifier);

            //Assert
            Assert.IsTrue(successors.Count == 4);
            Assert.IsTrue(successors.Exists(n => n.Name == nameof(ClassF.PF1)));
            Assert.IsTrue(successors.Exists(n => n.Name == nameof(ClassG.PG1)));
            Assert.IsTrue(successors.Exists(n => n.Name == nameof(ClassE.PE1)));
            Assert.IsTrue(successors.Exists(n => n.Name == nameof(ClassB.PB1)));
        }

        #endregion

        #region GetNodeAndItsSuccessors(IAnalysisGraph analysisGraph, uint nodeIdentifier, int maxDepth)

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void GetNodeAndItsSuccessors2_GivenNonExistingNodeIdentifier_ThrowsException()
        {
            //Arrange
            uint nodeIdentifier = 11111111;
            var reactor = AnalysisTestHelper.CreateCase1();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);
            var analyzer = new Analyzer();
            int maxDepth = 1;

            //Act
            var successors = analyzer.GetNodeAndItsSuccessors(analysisGraph, nodeIdentifier, maxDepth);
        }

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void GetNodeAndItsSuccessors2_GivenAnalysisGraphIsNull_ThrowsException()
        {
            //Arrange
            var reactor = AnalysisTestHelper.CreateCase1();
            IAnalysisGraph analysisGraph = null;
            var analyzer = new Analyzer();
            var nodeIdentifier = reactor.Graph.Nodes[0].Identifier;
            int maxDepth = 1;

            //Act
            var successors = analyzer.GetNodeAndItsSuccessors(analysisGraph, nodeIdentifier, maxDepth);
        }

        [TestMethod]
        public void GetNodeAndItsSuccessors2_GivenNodeHasNoSuccessors_ReturnsEmptySuccessorsCollection()
        {
            var reactor = AnalysisTestHelper.CreateCase6();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);
            var analyzer = new Analyzer();
            var sinkNodes = analyzer.GetSinkNodes(analysisGraph);
            var nodeIdentifier = sinkNodes[0].Identifier;
            int maxDepth = 1;

            //Act
            var successors = analyzer.GetNodeAndItsSuccessors(analysisGraph, nodeIdentifier, maxDepth);

            //Assert
            Assert.IsTrue(successors.Count == 1);
            successors.Contains(sinkNodes[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void GetNodeAndItsSuccessors2_GivenInvalidMaxDepth_ThrowsException()
        {
            var reactor = AnalysisTestHelper.CreateCase6();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);
            var analyzer = new Analyzer();
            var nodeIdentifier = reactor.Graph.Nodes[1].Identifier;

            //Act
            var successors = analyzer.GetNodeAndItsSuccessors(analysisGraph, nodeIdentifier, 0);
        }

        [TestMethod]
        public void GetNodeAndItsSuccessors2_GivenNodeHasSuccessors_ReturnsCorrectSuccessorsCollection()
        {
            var reactor = AnalysisTestHelper.CreateCase6();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);
            var analyzer = new Analyzer();
            var nodeIdentifier = reactor.Graph.Nodes[3].Identifier;
            int maxDepth = 1;

            //Act
            var successors = analyzer.GetNodeAndItsSuccessors(analysisGraph, nodeIdentifier, maxDepth);

            //Assert
            Assert.IsTrue(successors.Count == 3);
            Assert.IsTrue(successors.Exists(n => n.Name == nameof(ClassF.PF1)));
            Assert.IsTrue(successors.Exists(n => n.Name == nameof(ClassE.PE1)));
            Assert.IsTrue(successors.Exists(n => n.Name == nameof(ClassB.PB1)));
        }

        #endregion

        #region GetNodeAndItsNeighbours(IAnalysisGraph analysisGraph, uint nodeIdentifier)

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void GetNodeAndItsNeighbours_GivenNonExistingNodeIdentifier_ThrowsException()
        {
            //Arrange
            uint nodeIdentifier = 11111111;
            var reactor = AnalysisTestHelper.CreateCase1();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);
            var analyzer = new Analyzer();

            //Act
            var neighbours = analyzer.GetNodeAndItsNeighbours(analysisGraph, nodeIdentifier);
        }

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void GetNodeAndItsNeighbours_GivenAnalysisGraphIsNull_ThrowsException()
        {
            //Arrange
            var reactor = AnalysisTestHelper.CreateCase1();
            IAnalysisGraph analysisGraph = null;
            var analyzer = new Analyzer();
            var nodeIdentifier = reactor.Graph.Nodes[0].Identifier;

            //Act
            var neighbours = analyzer.GetNodeAndItsNeighbours(analysisGraph, nodeIdentifier);
        }

        [TestMethod]
        public void GetNodeAndItsNeighbours_GivenNodeHasNoNeighbours_ReturnsOnlySelectedNode()
        {
            var reactor = AnalysisTestHelper.CreateCase6();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);
            var analyzer = new Analyzer();
            var orphanNodes = analyzer.GetOrphanNodes(analysisGraph);
            var nodeIdentifier = orphanNodes[0].Identifier;

            //Act
            var neighbours = analyzer.GetNodeAndItsNeighbours(analysisGraph, nodeIdentifier);

            //Assert
            Assert.IsTrue(neighbours.Count == 1);
            Assert.IsTrue(neighbours.Contains(orphanNodes[0]) == true);
        }

        [TestMethod]
        public void GetNodeAndItsNeighbours_GivenNodeHasNeighbours_ReturnsCorrectSucessorsCollection()
        {
            var reactor = AnalysisTestHelper.CreateCase6();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);
            var analyzer = new Analyzer();
            var nodeIdentifier = reactor.Graph.Nodes[3].Identifier;

            //Act
            var neighbours = analyzer.GetNodeAndItsNeighbours(analysisGraph, nodeIdentifier);

            //Assert
            Assert.IsTrue(neighbours.Count == 5);
            Assert.IsTrue(neighbours.Exists(n => n.Name == nameof(ClassA.PA1)));
            Assert.IsTrue(neighbours.Exists(n => n.Name == nameof(ClassB.PB1)));
            Assert.IsTrue(neighbours.Exists(n => n.Name == nameof(ClassF.PF1)));
            Assert.IsTrue(neighbours.Exists(n => n.Name == nameof(ClassE.PE1)));
            Assert.IsTrue(neighbours.Exists(n => n.Name == nameof(ClassG.PG1)));

        }

        #endregion

        #region GetNodeAndItsNeighbours(IAnalysisGraph analysisGraph, uint nodeIdentifier, int maxDepth)

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void GetNodeAndItsNeighbours2_GivenNonExistingNodeIdentifier_ThrowsException()
        {
            //Arrange
            uint nodeIdentifier = 11111111;
            var reactor = AnalysisTestHelper.CreateCase1();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);
            var analyzer = new Analyzer();
            int maxDepth = 1;

            //Act
            var neighbours = analyzer.GetNodeAndItsNeighbours(analysisGraph, nodeIdentifier, maxDepth);
        }

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void GetNodeAndItsNeighbours2_GivenAnalysisGraphIsNull_ThrowsException()
        {
            //Arrange
            var reactor = AnalysisTestHelper.CreateCase1();
            IAnalysisGraph analysisGraph = null;
            var analyzer = new Analyzer();
            var nodeIdentifier = reactor.Graph.Nodes[0].Identifier;

            //Act
            var neighbours = analyzer.GetNodeAndItsNeighbours(analysisGraph, nodeIdentifier, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void GetNodeAndItsNeighbours2_GivenInvalidMaxDepth_ThrowsException()
        {
            //Arrange
            uint nodeIdentifier = 11111111;
            var reactor = AnalysisTestHelper.CreateCase1();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);
            var analyzer = new Analyzer();
            int maxDepth = 0;

            //Act
            var neighbours = analyzer.GetNodeAndItsNeighbours(analysisGraph, nodeIdentifier, maxDepth);
        }

        [TestMethod]
        public void GetNodeAndItsNeighbours2_GivenNodeHasNoNeighbours_ReturnsOnlySelectedNode()
        {
            var reactor = AnalysisTestHelper.CreateCase6();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);
            var analyzer = new Analyzer();
            var orphanNodes = analyzer.GetOrphanNodes(analysisGraph);
            var nodeIdentifier = orphanNodes[0].Identifier;

            //Act
            var neighbours = analyzer.GetNodeAndItsNeighbours(analysisGraph, nodeIdentifier, 1);

            //Assert
            Assert.IsTrue(neighbours.Count == 1);
            Assert.IsTrue(neighbours.Contains(orphanNodes[0]) == true);
        }

        [TestMethod]
        public void GetNodesAndItsNeighbours2_GivenNodeHasNeighbours_ReturnsCorrectSucessorsCollection()
        {
            var reactor = AnalysisTestHelper.CreateCase6();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);
            var analyzer = new Analyzer();
            var nodeIdentifier = reactor.Graph.Nodes[3].Identifier;

            //Act
            var neighbours = analyzer.GetNodeAndItsNeighbours(analysisGraph, nodeIdentifier, 1);

            //Assert
            Assert.IsTrue(neighbours.Count == 4);
            Assert.IsTrue(neighbours.Exists(n => n.Name == nameof(ClassA.PA1)));
            Assert.IsTrue(neighbours.Exists(n => n.Name == nameof(ClassB.PB1)));
            Assert.IsTrue(neighbours.Exists(n => n.Name == nameof(ClassF.PF1)));
            Assert.IsTrue(neighbours.Exists(n => n.Name == nameof(ClassE.PE1)));

        }

        #endregion

        #region GetSourceNodes

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void GetSourceNodes_GivenNonExistingNodeIdentifier_ThrowsException()
        {
            //Arrange
            uint nodeIdentifier = 11111111;
            var reactor = AnalysisTestHelper.CreateCase1();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);
            var analyzer = new Analyzer();

            //Act
            var sourceNodes = analyzer.GetSourceNodes(analysisGraph, nodeIdentifier);
        }

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void GetSourceNodes_GivenAnalysisGraphIsNull_ThrowsException()
        {
            //Arrange
            var reactor = AnalysisTestHelper.CreateCase1();
            IAnalysisGraph analysisGraph = null;
            var analyzer = new Analyzer();
            var nodeIdentifier = reactor.Graph.Nodes[0].Identifier;

            //Act
            var sourceNodes = analyzer.GetSourceNodes(analysisGraph, nodeIdentifier);
        }

        [TestMethod]
        public void GetSourceNodes_GivenNodeHasNoSourceNodes_ReturnsEmptyCollection()
        {
            var reactor = AnalysisTestHelper.CreateCase6();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);
            var analyzer = new Analyzer();

            var sources = analyzer.GetSourceNodes(analysisGraph);
            var nodeIdentifier = sources[0].Identifier;

            //Act
            var sourceNodes = analyzer.GetSourceNodes(analysisGraph, nodeIdentifier);

            //Assert
            Assert.IsTrue(sourceNodes.Count == 0);
        }

        [TestMethod]
        public void GetSourceNodes_GivenNodeHasSourceNodes_ReturnsCorrectCollection()
        {
            var reactor = AnalysisTestHelper.CreateCase6();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);
            var analyzer = new Analyzer();
            var nodeIdentifier = reactor.Graph.Nodes[3].Identifier;

            //Act
            var sourceNodes = analyzer.GetSourceNodes(analysisGraph, nodeIdentifier);

            //Assert
            Assert.IsTrue(sourceNodes.Count == 1);
            Assert.IsTrue(sourceNodes.Exists(n => n.Name == nameof(ClassA.PA1)));
        }

        #endregion

        #region GetSinkNodes

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void GetSinkNodes_GivenNonExistingNodeIdentifier_ThrowsException()
        {
            //Arrange
            uint nodeIdentifier = 11111111;
            var reactor = AnalysisTestHelper.CreateCase1();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);
            var analyzer = new Analyzer();

            //Act
            var sinkNodes = analyzer.GetSinkNodes(analysisGraph, nodeIdentifier);
        }

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void GetSinkNodes_GivenAnalysisGraphIsNull_ThrowsException()
        {
            //Arrange
            var reactor = AnalysisTestHelper.CreateCase1();
            IAnalysisGraph analysisGraph = null;
            var analyzer = new Analyzer();
            var nodeIdentifier = reactor.Graph.Nodes[0].Identifier;

            //Act
            var sinkNodes = analyzer.GetSinkNodes(analysisGraph, nodeIdentifier);
        }

        [TestMethod]
        public void GetSinkNodes_GivenNodeHasNoSinkNodes_ReturnsEmptyCollection()
        {
            var reactor = AnalysisTestHelper.CreateCase6();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);
            var analyzer = new Analyzer();
            var sinks = analyzer.GetSinkNodes(analysisGraph);
            var nodeIdentifier = sinks[0].Identifier;

            //Act
            var sinkNodes = analyzer.GetSinkNodes(analysisGraph, nodeIdentifier);

            //Assert
            Assert.IsTrue(sinkNodes.Count == 0);
        }

        [TestMethod]
        public void GetSinkNodes_GivenNodeHasSinkNodes_ReturnsCorrectCollection()
        {
            var reactor = AnalysisTestHelper.CreateCase6();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);
            var analyzer = new Analyzer();
            var nodeIdentifier = reactor.Graph.Nodes[3].Identifier;

            //Act
            var sinkNodes = analyzer.GetSinkNodes(analysisGraph, nodeIdentifier);

            //Assert
            Assert.IsTrue(sinkNodes.Count == 1);
            Assert.IsTrue(sinkNodes.Exists(n => n.Name == nameof(ClassG.PG1)));
        }

        #endregion

        #region GetLeafNodes

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void GetLeafNodes_GivenNonExistingNodeIdentifier_ThrowsException()
        {
            //Arrange
            uint nodeIdentifier = 11111111;
            var reactor = AnalysisTestHelper.CreateCase1();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);
            var analyzer = new Analyzer();

            //Act
            var leafNodes = analyzer.GetLeafNodes(analysisGraph, nodeIdentifier);
        }

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void GetLeafNodes_GivenAnalysisGraphIsNull_ThrowsException()
        {
            //Arrange
            var reactor = AnalysisTestHelper.CreateCase1();
            IAnalysisGraph analysisGraph = null;
            var analyzer = new Analyzer();
            var nodeIdentifier = reactor.Graph.Nodes[0].Identifier;

            //Act
            var leafNodes = analyzer.GetLeafNodes(analysisGraph, nodeIdentifier);
        }

        [TestMethod]
        public void GetLeafNodes_GivenNodeHasNoLeafNodes_ReturnsEmptyCollection()
        {
            var reactor = AnalysisTestHelper.CreateCase6();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);
            var analyzer = new Analyzer();
            var orphans = analyzer.GetOrphanNodes(analysisGraph);
            var nodeIdentifier = orphans[0].Identifier;

            //Act
            var leafNodes = analyzer.GetLeafNodes(analysisGraph, nodeIdentifier);

            //Assert
            Assert.IsTrue(leafNodes.Count == 0);
        }

        [TestMethod]
        public void GetLeafNodes_GivenNodeHasLeafNodes_ReturnsCorrectCollection()
        {
            var reactor = AnalysisTestHelper.CreateCase6();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);
            var analyzer = new Analyzer();
            var nodeIdentifier = reactor.Graph.Nodes[3].Identifier;

            //Act
            var leafNodes = analyzer.GetLeafNodes(analysisGraph, nodeIdentifier);

            //Assert
            Assert.IsTrue(leafNodes.Count == 2);
            Assert.IsTrue(leafNodes.Exists(n => n.Name == nameof(ClassG.PG1)));
            Assert.IsTrue(leafNodes.Exists(n => n.Name == nameof(ClassA.PA1)));
        }

        #endregion

        #region GetIntermediaryPredecessors

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void GetIntermediaryPredecessors_GivenNonExistingNodeIdentifier_ThrowsException()
        {
            //Arrange
            uint nodeIdentifier = 11111111;
            var reactor = AnalysisTestHelper.CreateCase1();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);
            var analyzer = new Analyzer();

            //Act
            var intermediaryNodes = analyzer.GetIntermediaryPredecessors(analysisGraph, nodeIdentifier);
        }

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void GetIntermediaryPredecessors_GivenAnalysisGraphIsNull_ThrowsException()
        {
            //Arrange
            var reactor = AnalysisTestHelper.CreateCase1();
            IAnalysisGraph analysisGraph = null;
            var analyzer = new Analyzer();
            var nodeIdentifier = reactor.Graph.Nodes[0].Identifier;

            //Act
            var intermediaryNodes = analyzer.GetIntermediaryPredecessors(analysisGraph, nodeIdentifier);
        }

        [TestMethod]
        public void GetIntermediaryPredecessors_GivenNodeHasNoIntermediaryPredecessors_ReturnsEmptyCollection()
        {
            var reactor = AnalysisTestHelper.CreateCase6();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);
            var analyzer = new Analyzer();
            var nodeIdentifier = analysisGraph.Nodes[3].Identifier;

            //Act
            var intermediaryNodes = analyzer.GetIntermediaryPredecessors(analysisGraph, nodeIdentifier);

            //Assert
            Assert.IsTrue(intermediaryNodes.Count == 0);
        }

        [TestMethod]
        public void GetIntermediaryPredecessors_GivenNodeHasIntermediaryPredecessors_ReturnsCorrectCollection()
        {
            var reactor = AnalysisTestHelper.CreateCase6();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);
            var analyzer = new Analyzer();
            var nodeIdentifier = analysisGraph.Nodes.Find(n => n.Name == nameof(ClassF.PF1)).Identifier;

            //Act
            var intermediaryNodes = analyzer.GetIntermediaryPredecessors(analysisGraph, nodeIdentifier);

            //Assert
            Assert.IsTrue(intermediaryNodes.Count == 1);
            Assert.IsTrue(intermediaryNodes.Exists(n => n.Name == nameof(ClassB.PB1)));
        }

        #endregion

        #region GetIntermediarySuccessors

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void GetIntermediarySuccessors_GivenNonExistingNodeIdentifier_ThrowsException()
        {
            //Arrange
            uint nodeIdentifier = 11111111;
            var reactor = AnalysisTestHelper.CreateCase1();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);
            var analyzer = new Analyzer();

            //Act
            var intermediaryNodes = analyzer.GetIntermediarySuccessors(analysisGraph, nodeIdentifier);
        }

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void GetIntermediarySuccessors_GivenAnalysisGraphIsNull_ThrowsException()
        {
            //Arrange
            var reactor = AnalysisTestHelper.CreateCase1();
            IAnalysisGraph analysisGraph = null;
            var analyzer = new Analyzer();
            var nodeIdentifier = reactor.Graph.Nodes[0].Identifier;

            //Act
            var intermediaryNodes = analyzer.GetIntermediarySuccessors(analysisGraph, nodeIdentifier);
        }

        [TestMethod]
        public void GetIntermediarySuccessors_GivenNodeHasNoIntermediarySuccessors_ReturnsEmptyCollection()
        {
            var reactor = AnalysisTestHelper.CreateCase6();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);
            var analyzer = new Analyzer();
            var nodeIdentifier = analysisGraph.Nodes.Find(n => n.Name == nameof(ClassF.PF1)).Identifier;

            //Act
            var intermediaryNodes = analyzer.GetIntermediarySuccessors(analysisGraph, nodeIdentifier);

            //Assert
            Assert.IsTrue(intermediaryNodes.Count == 0);
        }

        [TestMethod]
        public void GetIntermediarySuccessors_GivenNodeHasIntermediarySuccessors_ReturnsCorrectCollection()
        {
            var reactor = AnalysisTestHelper.CreateCase6();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);
            var analyzer = new Analyzer();
            var nodeIdentifier = analysisGraph.Nodes.Find(n => n.Name == nameof(ClassB.PB1)).Identifier;

            //Act
            var intermediaryNodes = analyzer.GetIntermediarySuccessors(analysisGraph, nodeIdentifier);

            //Assert
            Assert.IsTrue(intermediaryNodes.Count == 2);
            Assert.IsTrue(intermediaryNodes.Exists(n => n.Name == nameof(ClassF.PF1)));
            Assert.IsTrue(intermediaryNodes.Exists(n => n.Name == nameof(ClassE.PE1)));
        }

        #endregion

        #region GetIntermediaryNodes

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void GetIntermediaryNodes_GivenNonExistingNodeIdentifier_ThrowsException()
        {
            //Arrange
            uint nodeIdentifier = 11111111;
            var reactor = AnalysisTestHelper.CreateCase1();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);
            var analyzer = new Analyzer();

            //Act
            var intermediaryNodes = analyzer.GetIntermediaryNodes(analysisGraph, nodeIdentifier);
        }

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void GetIntermediaryNodes_GivenAnalysisGraphIsNull_ThrowsException()
        {
            //Arrange
            var reactor = AnalysisTestHelper.CreateCase1();
            IAnalysisGraph analysisGraph = null;
            var analyzer = new Analyzer();
            var nodeIdentifier = reactor.Graph.Nodes[0].Identifier;

            //Act
            var intermediaryNodes = analyzer.GetIntermediaryNodes(analysisGraph, nodeIdentifier);
        }

        [TestMethod]
        public void GetIntermediaryNodes_GivenNodeHasNoIntermediaryNodes_ReturnsEmptyCollection()
        {
            var reactor = AnalysisTestHelper.CreateCase9();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);
            var analyzer = new Analyzer();
            var nodeIdentifier = analysisGraph.Nodes.Find(n => n.Name == nameof(ClassB.PB1)).Identifier;

            //Act
            var intermediaryNodes = analyzer.GetIntermediaryNodes(analysisGraph, nodeIdentifier);

            //Assert
            Assert.IsTrue(intermediaryNodes.Count == 0);
        }

        [TestMethod]
        public void GetIntermediaryNodes_GivenNodeHasIntermediaryNodes_ReturnsCorrectCollection()
        {
            var reactor = AnalysisTestHelper.CreateCase6();
            var analysisGraph = AnalysisTestHelper.CreateAnalysisGraph(reactor, AnalysisLevel.ObjectMemberLevel);
            var analyzer = new Analyzer();
            var nodeIdentifier = analysisGraph.Nodes.Find(n => n.Name == nameof(ClassB.PB1)).Identifier;

            //Act
            var intermediaryNodes = analyzer.GetIntermediarySuccessors(analysisGraph, nodeIdentifier);

            //Assert
            Assert.IsTrue(intermediaryNodes.Count == 2);
            Assert.IsTrue(intermediaryNodes.Exists(n => n.Name == nameof(ClassF.PF1)));
            Assert.IsTrue(intermediaryNodes.Exists(n => n.Name == nameof(ClassE.PE1)));
        }

        #endregion
    }
}
