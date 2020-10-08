using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeAnalyzer.Exceptions;
using ReframeAnalyzer.Nodes;

namespace ReframeAnalyzerTests.Nodes
{
    [TestClass]
    public class AnalysisNodeTests
    {
        #region AddPredecessor

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void AddPredecessor_GivenNullPredecessor_ThrowsException()
        {
            //Arrange
            var node = new AssemblyAnalysisNode(1111);

            //Act
            node.AddPredecessor(null);
        }

        [TestMethod]
        public void AddPredecessor_GivenNewValidPredecessor_PredecessorIsAdded()
        {
            //Arrange
            var node = new AssemblyAnalysisNode(1111);
            var predecessor = new AssemblyAnalysisNode(2222);

            //Act
            node.AddPredecessor(predecessor);

            //Assert
            Assert.IsTrue(node.HasPredecessor(predecessor) && predecessor.HasSuccessor(node));
        }

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void AddPredecessor_GivenPredecessorOfDifferentLevel_ThrowsException()
        {
            //Arrange
            var node = new AssemblyAnalysisNode(1111);
            var predecessor = new NamespaceAnalysisNode(2222);

            //Act
            node.AddPredecessor(predecessor);
        }

        #endregion

        #region AddSuccessor

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void AddSuccessor_GivenNullSuccessor_ThrowsException()
        {
            //Arrange
            var node = new AssemblyAnalysisNode(1111);

            //Act
            node.AddSuccessor(null);
        }

        [TestMethod]
        public void AddSuccessor_GivenNewValidSuccessor_SuccessorIsAdded()
        {
            //Arrange
            var node = new AssemblyAnalysisNode(1111);
            var successor = new AssemblyAnalysisNode(2222);

            //Act
            node.AddSuccessor(successor);

            //Assert
            Assert.IsTrue(node.HasSuccessor(successor) && successor.HasPredecessor(node));
        }

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void AddSuccessor_GivenPredecessorOfDifferentLevel_ThrowsException()
        {
            //Arrange
            var node = new AssemblyAnalysisNode(1111);
            var successor = new NamespaceAnalysisNode(2222);

            //Act
            node.AddSuccessor(successor);
        }

        #endregion

        #region RemovePredecessor

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void RemovePredecessor_GivenNullPredecessor_ThrowsException()
        {
            //Arrange
            var node = new AssemblyAnalysisNode(1111);

            //Act
            node.RemovePredecessor(null);
        }

        [TestMethod]
        public void RemovePredecessor_GivenExistingPredecessor_PredecessorIsRemoved()
        {
            //Arrange
            var node = new AssemblyAnalysisNode(1111);
            var predecessor = new AssemblyAnalysisNode(2222);
            node.AddPredecessor(predecessor);

            //Act
            node.RemovePredecessor(predecessor);

            //Assert
            Assert.IsTrue(node.HasPredecessor(predecessor) == false && predecessor.HasSuccessor(node) == false);
        }

        [TestMethod]
        public void RemovePredecessor_GivenNonExistingPredecessor_OperationIsIgnored()
        {
            //Arrange
            var node = new AssemblyAnalysisNode(1111);
            var predecessor = new AssemblyAnalysisNode(2222);

            //Act
            node.RemovePredecessor(predecessor);

            //Assert
            Assert.IsTrue(node.HasPredecessor(predecessor) == false && predecessor.HasSuccessor(node) == false);
        }

        #endregion

        #region RemoveSuccessor

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void RemoveSuccessor_GivenNullSuccessor_ThrowsException()
        {
            //Arrange
            var node = new AssemblyAnalysisNode(1111);

            //Act
            node.RemoveSuccessor(null);
        }

        [TestMethod]
        public void RemoveSuccessor_GivenExistingSuccessor_SuccessorIsRemoved()
        {
            //Arrange
            var node = new AssemblyAnalysisNode(1111);
            var successor = new AssemblyAnalysisNode(2222);
            node.AddSuccessor(successor);

            //Act
            node.RemoveSuccessor(successor);

            //Assert
            Assert.IsTrue(node.HasSuccessor(successor) == false && successor.HasPredecessor(node) == false);
        }

        [TestMethod]
        public void RemoveSuccessor_GivenNonExistingSuccessor_OperationIsIgnored()
        {
            //Arrange
            var node = new AssemblyAnalysisNode(1111);
            var successor = new AssemblyAnalysisNode(2222);

            //Act
            node.RemoveSuccessor(successor);

            //Assert
            Assert.IsTrue(node.HasSuccessor(successor) == false && successor.HasPredecessor(node) == false);
        }

        #endregion
    }
}
