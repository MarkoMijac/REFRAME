using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeCore;
using ReframeCore.Exceptions;
using System.Collections.Generic;
using ReframeCore.Nodes;
using ReframeCoreExamples.E00;
using ReframeCore.Factories;

namespace ReframeCoreTests
{
    [TestClass]
    public class GraphRegistryTests
    {
        private NodeFactory factory = new StandardNodeFactory();

        #region CreateGraph

        [TestMethod]
        public void CreateGraph_GivenNoOtherDependencyGraphsExist_CreatesNewDependencyGraph()
        {
            //Arrange
            GraphRegistry.Instance.Clear();
            string identifier = "G1";

            //Act
            var graph = GraphRegistry.Instance.CreateGraph(identifier);

            //Assert
            Assert.IsTrue(graph != null && graph.Identifier == identifier);
        }

        [TestMethod]
        public void CreateGraph_GivenUniqueIdentifierProvided_CreatesNewDependencyGraph()
        {
            //Arrange
            GraphRegistry.Instance.Clear();
            GraphRegistry.Instance.CreateGraph("G1");

            //Act
            var graph = GraphRegistry.Instance.CreateGraph("G2");

            //Assert
            Assert.IsNotNull(graph);
        }

        [TestMethod]
        public void CreateGraph_GivenAlreadyTakenIdentifierProvided_ThrowsException()
        {
            //Arrange
            GraphRegistry.Instance.Clear();
            var graph = GraphRegistry.Instance.CreateGraph("G1");

            //Act & Assert
            Assert.ThrowsException<DependencyGraphException>(() => GraphRegistry.Instance.CreateGraph("G1"));
        }

        [TestMethod]
        public void CreateGraph_GivenEmptyStringIdentifier_ThrowsException()
        {
            //Arrange
            GraphRegistry.Instance.Clear();

            //Act & Assert
            Assert.ThrowsException<DependencyGraphException>(() => GraphRegistry.Instance.CreateGraph(""));
        }

        [TestMethod]
        public void CreateGraph_GivenDefaultIdentifierProvided_ThrowsException()
        {
            //Arrange
            GraphRegistry.Instance.Clear();

            //Act & Assert
            Assert.ThrowsException<DependencyGraphException>(() => GraphRegistry.Instance.CreateGraph(GraphRegistry.DefaultGraphName));
        }

        #endregion

        #region GetGraph

        [TestMethod]
        public void GetGraph_GivenGraphWithProvidedIdentifierExists_ReturnsDependencyGraph()
        {
            //Arrange
            GraphRegistry.Instance.Clear();
            string identifier = "G1";
            GraphRegistry.Instance.CreateGraph(identifier);

            //Act
            var graph = GraphRegistry.Instance.GetGraph(identifier);

            //Assert
            Assert.IsNotNull(graph);
        }

        [TestMethod]
        public void GetGraph_GivenGraphWithProvidedIdentifierDoesNotExist_ThrowsException()
        {
            //Arrange
            GraphRegistry.Instance.Clear();
            string identifier = "G1";
            string nonexistant = "G2";
            GraphRegistry.Instance.CreateGraph(identifier);

            //Act&Assert
            Assert.ThrowsException<DependencyGraphException>(() =>GraphRegistry.Instance.GetGraph(nonexistant));
        }

        #endregion

        #region GetOrCreateGraph

        [TestMethod]
        public void GetOrCreateGraph_GivenGraphWithProvidedIdentifierExists_ReturnsExistingDependencyGraph()
        {
            //Arrange
            GraphRegistry.Instance.Clear();
            string identifier = "G1";
            GraphRegistry.Instance.CreateGraph(identifier);

            //Act
            var graph = GraphRegistry.Instance.GetOrCreateGraph(identifier);

            //Assert
            Assert.IsTrue(graph.Identifier == identifier);
        }

        [TestMethod]
        public void GetOrCreateGraph_GivenGraphWithProvidedIdentifierDoesNotExist_ReturnsNewDependencyGraph()
        {
            //Arrange
            GraphRegistry.Instance.Clear();
            string identifier = "G1";

            //Act
            var graph = GraphRegistry.Instance.GetOrCreateGraph(identifier);

            //Assert
            Assert.IsTrue(graph.Identifier == identifier);
        }

        [TestMethod]
        public void GetOrCreateGraph_GivenEmptyStringIdentifier_ThrowsException()
        {
            //Arrange
            GraphRegistry.Instance.Clear();
            string identifier = "";

            //Act & Assert
            Assert.ThrowsException<DependencyGraphException>(() => GraphRegistry.Instance.GetOrCreateGraph(identifier));
        }

        #endregion

        #region GetGraphs

        [TestMethod]
        public void GetGraphs_GivenNoGraphsManuallyAdded_ReturnsListWithDefaultGraph()
        {
            //Arrange
            GraphRegistry.Instance.Clear();

            //Act
            List<IDependencyGraph> graphs = GraphRegistry.Instance.GetGraphs();

            //Assert
            Assert.IsTrue(graphs.Exists(g => g.Identifier == GraphRegistry.DefaultGraphName));
        }

        [TestMethod]
        public void GetGraphs_GivenGraphIsManuallyAdded_ReturnsListAddedGraph()
        {
            //Arrange
            GraphRegistry.Instance.Clear();
            GraphRegistry.Instance.CreateGraph("GraphONE");

            //Act
            List<IDependencyGraph> graphs = GraphRegistry.Instance.GetGraphs();

            //Assert
            Assert.IsTrue(graphs.Exists(g => g.Identifier == "GraphONE"));
            Assert.IsTrue(graphs.Exists(g => g.Identifier == GraphRegistry.DefaultGraphName));
        }

        #endregion

        #region Clear

        [TestMethod]
        public void Clear_GivenOnlyDefaultGraphExists_DefaultGraphIsNotRemoved()
        {
            GraphRegistry.Instance.Clear();

            Assert.IsTrue(GraphRegistry.Instance.GetGraphs().Exists(g => g.Identifier == GraphRegistry.DefaultGraphName));
        }

        [TestMethod]
        public void Clear_GivenMultipleGraphsExist_AllGraphsButDefaultOneAreRemoved()
        {
            //Arrange
            GraphRegistry.Instance.Clear();
            GraphRegistry.Instance.CreateGraph("GraphONE");
            GraphRegistry.Instance.CreateGraph("GraphTWO");
            GraphRegistry.Instance.CreateGraph("GraphTHREE");

            //Act
            GraphRegistry.Instance.Clear();

            //Assert
            Assert.IsTrue(GraphRegistry.Instance.GetGraphs().Count == 1);
            Assert.IsTrue(GraphRegistry.Instance.GetGraphs().Exists(g => g.Identifier == GraphRegistry.DefaultGraphName));
        }

        #endregion

        #region GetDefaultGraph()

        [TestMethod]
        public void GetDefaultGraph_ReturnsDefaultGraph()
        {
            //Arrange

            //Act
            var graph = GraphRegistry.Instance.GetDefaultGraph();

            //Assert
            Assert.IsTrue(graph.Identifier == GraphRegistry.DefaultGraphName);
        }


        #endregion
    }
}
