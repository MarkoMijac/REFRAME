using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeAnalyzer.Exceptions;
using ReframeAnalyzer.Nodes;

namespace ReframeAnalyzerTests.Nodes
{
    [TestClass]
    public class AnalysisNodeTests
    {
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
        [ExpectedException(typeof(AnalysisException))]
        public void AddPredecessor_GivenSelfPredecessor_ThrowsException()
        {
            //Arrange
            var node = new AssemblyAnalysisNode(1111);

            //Act
            node.AddPredecessor(node);
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
            Assert.IsTrue(node.HasPredecessor(predecessor));
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
    }
}
