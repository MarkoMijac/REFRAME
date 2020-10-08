using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeAnalyzer;
using ReframeAnalyzer.Exceptions;
using ReframeAnalyzer.Graph;
using ReframeAnalyzer.Nodes;

namespace ReframeAnalyzerTests.Graph
{
    [TestClass]
    public class AnalysisGraphTests
    {
        #region AddNode

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void AddNode_GivenNullNode_ThrowsException()
        {
            //Arrange
            var graph = new AnalysisGraph("G1", AnalysisLevel.AssemblyLevel);

            //Act
            graph.AddNode(null);
        }

        [TestMethod]
        public void AddNode_GivenAlreadyAddedNode_SkipsAdding()
        {
            //Arrange
            var graph = new AnalysisGraph("G1", AnalysisLevel.AssemblyLevel);
            var node = new AssemblyAnalysisNode(1111);
            graph.AddNode(node);

            //Act
            graph.AddNode(node);

            //Assert
            Assert.IsTrue(graph.Nodes.FindAll(n => n.Identifier == node.Identifier).Count == 1);
        }

        [TestMethod]
        public void AddNode_GivenNotContainingNode_AddsNode()
        {
            //Arrange
            var graph = new AnalysisGraph("G1", AnalysisLevel.AssemblyLevel);
            var node = new AssemblyAnalysisNode(1111);

            //Act
            graph.AddNode(node);

            //Assert
            Assert.IsTrue(graph.ContainsNode(node.Identifier));
        }

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void AddNode_GivenWrongLevelNode_ThrowsException()
        {
            //Arrange
            var graph = new AnalysisGraph("G1", AnalysisLevel.AssemblyLevel);
            var node = new NamespaceAnalysisNode(1111);

            //Act
            graph.AddNode(node);
        }

        #endregion

        #region RemoveNode

        [TestMethod]
        public void RemoveNode_GivenNullNode_SkipsRemoving()
        {
            //Arrange
            var graph = new AnalysisGraph("G1", AnalysisLevel.AssemblyLevel);

            //Act
            graph.RemoveNode(null);

            //Assert
            Assert.IsTrue(graph.Nodes.Count == 0);
        }

        [TestMethod]
        public void RemoveNode_GivenNotAddedNode_SkipsRemoving()
        {
            //Arrange
            var graph = new AnalysisGraph("G1", AnalysisLevel.AssemblyLevel);
            var node = new AssemblyAnalysisNode(1111);

            //Act
            graph.RemoveNode(node);

            //Assert
            Assert.IsFalse(graph.ContainsNode(node.Identifier));
        }

        [TestMethod]
        public void AddNode_GivenAlreadyAddedNode_RemovesNode()
        {
            //Arrange
            var graph = new AnalysisGraph("G1", AnalysisLevel.AssemblyLevel);
            var node = new AssemblyAnalysisNode(1111);
            graph.AddNode(node);

            //Act
            graph.RemoveNode(node);

            //Assert
            Assert.IsFalse(graph.ContainsNode(node.Identifier));
        }

        #endregion

        #region GetNode

        [TestMethod]
        public void GetNode_GivenNonExistingIdentifier_ReturnsNull()
        {
            //Arrange
            var graph = new AnalysisGraph("G1", AnalysisLevel.AssemblyLevel);

            //Act
            var node = graph.GetNode(1111);

            //Assert
            Assert.IsNull(node);
        }

        [TestMethod]
        public void GetNode_GivenExistingIdentifier_ReturnsNode()
        {
            //Arrange
            var graph = new AnalysisGraph("G1", AnalysisLevel.AssemblyLevel);
            var node = new AssemblyAnalysisNode(1111);
            graph.AddNode(node);

            //Act
            var n = graph.GetNode(1111);

            //Assert
            Assert.IsTrue(node.Identifier == n.Identifier);
        }

        #endregion
    }
}
