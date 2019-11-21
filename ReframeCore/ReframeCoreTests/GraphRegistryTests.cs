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

        #region Create

        [TestMethod]
        public void Create_GivenNoOtherDependencyGraphsExist_CreatesNewDependencyGraph()
        {
            //Arrange
            GraphRegistry.Clear();
            string identifier = "G1";

            //Act
            var graph = GraphRegistry.Create(identifier);

            //Assert
            Assert.IsTrue(graph != null && graph.Identifier == identifier);
        }

        [TestMethod]
        public void Create_GivenUniqueIdentifierProvided_CreatesNewDependencyGraph()
        {
            //Arrange
            GraphRegistry.Clear();
            GraphRegistry.Create("G1");

            //Act
            var graph = GraphRegistry.Create("G2");

            //Assert
            Assert.IsNotNull(graph);
        }

        [TestMethod]
        public void Create_GivenAlreadyTakenIdentifierProvided_ThrowsException()
        {
            //Arrange
            GraphRegistry.Clear();
            var graph = GraphRegistry.Create("G1");

            //Act & Assert
            Assert.ThrowsException<DependencyGraphException>(() => GraphRegistry.Create("G1"));
        }

        [TestMethod]
        public void Create_GivenEmptyStringIdentifier_ThrowsException()
        {
            //Arrange
            GraphRegistry.Clear();

            //Act & Assert
            Assert.ThrowsException<DependencyGraphException>(() => GraphRegistry.Create(""));
        }

        [TestMethod]
        public void Create_GivenDefaultIdentifierProvided_ThrowsException()
        {
            //Arrange
            GraphRegistry.Clear();

            //Act & Assert
            Assert.ThrowsException<DependencyGraphException>(() => GraphRegistry.Create(GraphRegistry.DefaultGraphName));
        }

        #endregion

        #region Get

        [TestMethod]
        public void Get_GivenGraphWithProvidedIdentifierExists_ReturnsDependencyGraph()
        {
            //Arrange
            GraphRegistry.Clear();
            string identifier = "G1";
            GraphRegistry.Create(identifier);

            //Act
            var graph = GraphRegistry.Get(identifier);

            //Assert
            Assert.IsNotNull(graph);
        }

        [TestMethod]
        public void Get_GivenGraphWithProvidedIdentifierDoesNotExist_ReturnsNull()
        {
            //Arrange
            GraphRegistry.Clear();
            string identifier = "G1";
            string nonexistant = "G2";
            GraphRegistry.Create(identifier);

            //Act
            var graph = GraphRegistry.Get(nonexistant);

            //Assert
            Assert.IsNull(graph);
        }

        [TestMethod]
        public void Get_GivenProvidedNodeIsNull_ReturnsNull()
        {
            //Arrange

            //Act&Assert
            Assert.ThrowsException<NodeNullReferenceException>(() => GraphRegistry.Get((INode)null));
        }

        [TestMethod]
        public void Get_GivenProvidedNodeNotAddedToAnyGraph_ReturnsNull()
        {
            //Arrange
            GraphRegistry.Clear();
            GraphRegistry.Create("GraphONE");

            Building00 b = new Building00();
            INode node = factory.CreateNode(b, nameof(b.Area));

            //Act
            var graph = GraphRegistry.Get(node);

            //Assert
            Assert.IsNull(graph);
        }

        [TestMethod]
        public void Get_GivenProvidedNodeIsAddedToGraph_ReturnsGraphContainigProvidedNode()
        {
            //Arrange
            GraphRegistry.Clear();
            GraphRegistry.Create("GraphONE");

            Building00 b = new Building00();
            INode node = factory.CreateNode(b, nameof(b.Area));
            GraphRegistry.GetDefault().AddNode(node);

            //Act
            var graph = GraphRegistry.Get(node);

            //Assert
            Assert.IsNotNull(graph);
            Assert.IsTrue(graph.Identifier == GraphRegistry.DefaultGraphName);
        }

        #endregion

        #region GetOrCreate

        [TestMethod]
        public void GetOrCreate_GivenGraphWithProvidedIdentifierExists_ReturnsExistingDependencyGraph()
        {
            //Arrange
            GraphRegistry.Clear();
            string identifier = "G1";
            GraphRegistry.Create(identifier);

            //Act
            var graph = GraphRegistry.GetOrCreate(identifier);

            //Assert
            Assert.IsTrue(graph.Identifier == identifier);
        }

        [TestMethod]
        public void GetOrCreate_GivenGraphWithProvidedIdentifierDoesNotExist_ReturnsNewDependencyGraph()
        {
            //Arrange
            GraphRegistry.Clear();
            string identifier = "G1";

            //Act
            var graph = GraphRegistry.GetOrCreate(identifier);

            //Assert
            Assert.IsTrue(graph.Identifier == identifier);
        }

        [TestMethod]
        public void GetOrCreate_GivenEmptyStringIdentifier_ThrowsException()
        {
            //Arrange
            GraphRegistry.Clear();
            string identifier = "";

            //Act & Assert
            Assert.ThrowsException<DependencyGraphException>(() => GraphRegistry.GetOrCreate(identifier));
        }

        #endregion

        #region GetRegisteredGraphs

        [TestMethod]
        public void GetRegisteredGraphs_GivenNoGraphsManuallyAdded_ReturnsListWithDefaultGraph()
        {
            //Arrange
            GraphRegistry.Clear();

            //Act
            List<IDependencyGraph> graphs = GraphRegistry.GetRegisteredGraphs();

            //Assert
            Assert.IsTrue(graphs.Exists(g => g.Identifier == GraphRegistry.DefaultGraphName));
        }

        [TestMethod]
        public void GetRegisteredGraphs_GivenGraphIsManuallyAdded_ReturnsListAddedGraph()
        {
            //Arrange
            GraphRegistry.Clear();
            GraphRegistry.Create("GraphONE");

            //Act
            List<IDependencyGraph> graphs = GraphRegistry.GetRegisteredGraphs();

            //Assert
            Assert.IsTrue(graphs.Exists(g => g.Identifier == "GraphONE"));
            Assert.IsTrue(graphs.Exists(g => g.Identifier == GraphRegistry.DefaultGraphName));
        }

        #endregion

        #region Clear

        [TestMethod]
        public void Clear_GivenOnlyDefaultGraphExists_DefaultGraphIsNotRemoved()
        {
            GraphRegistry.Clear();

            Assert.IsTrue(GraphRegistry.GetRegisteredGraphs().Exists(g => g.Identifier == GraphRegistry.DefaultGraphName));
        }

        [TestMethod]
        public void Clear_GivenMultipleGraphsExist_AllGraphsButDefaultOneAreRemoved()
        {
            //Arrange
            GraphRegistry.Clear();
            GraphRegistry.Create("GraphONE");
            GraphRegistry.Create("GraphTWO");
            GraphRegistry.Create("GraphTHREE");

            //Act
            GraphRegistry.Clear();

            //Assert
            Assert.IsTrue(GraphRegistry.GetRegisteredGraphs().Count == 1);
            Assert.IsTrue(GraphRegistry.GetRegisteredGraphs().Exists(g => g.Identifier == GraphRegistry.DefaultGraphName));
        }

        #endregion

        #region GetDefault()

        [TestMethod]
        public void GetDefault_ReturnsDefaultGraph()
        {
            //Arrange

            //Act
            var graph = GraphRegistry.GetDefault();

            //Assert
            Assert.IsTrue(graph.Identifier == GraphRegistry.DefaultGraphName);
        }


        #endregion
    }
}
