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
            GraphRegistry.Instance.Clear();
            string identifier = "G1";

            //Act
            var graph = GraphRegistry.Instance.Create(identifier);

            //Assert
            Assert.IsTrue(graph != null && graph.Identifier == identifier);
        }

        [TestMethod]
        public void Create_GivenUniqueIdentifierProvided_CreatesNewDependencyGraph()
        {
            //Arrange
            GraphRegistry.Instance.Clear();
            GraphRegistry.Instance.Create("G1");

            //Act
            var graph = GraphRegistry.Instance.Create("G2");

            //Assert
            Assert.IsNotNull(graph);
        }

        [TestMethod]
        public void Create_GivenAlreadyTakenIdentifierProvided_ThrowsException()
        {
            //Arrange
            GraphRegistry.Instance.Clear();
            var graph = GraphRegistry.Instance.Create("G1");

            //Act & Assert
            Assert.ThrowsException<DependencyGraphException>(() => GraphRegistry.Instance.Create("G1"));
        }

        [TestMethod]
        public void Create_GivenEmptyStringIdentifier_ThrowsException()
        {
            //Arrange
            GraphRegistry.Instance.Clear();

            //Act & Assert
            Assert.ThrowsException<DependencyGraphException>(() => GraphRegistry.Instance.Create(""));
        }

        [TestMethod]
        public void Create_GivenDefaultIdentifierProvided_ThrowsException()
        {
            //Arrange
            GraphRegistry.Instance.Clear();

            //Act & Assert
            Assert.ThrowsException<DependencyGraphException>(() => GraphRegistry.Instance.Create(GraphRegistry.DefaultGraphName));
        }

        #endregion

        #region Get

        [TestMethod]
        public void Get_GivenGraphWithProvidedIdentifierExists_ReturnsDependencyGraph()
        {
            //Arrange
            GraphRegistry.Instance.Clear();
            string identifier = "G1";
            GraphRegistry.Instance.Create(identifier);

            //Act
            var graph = GraphRegistry.Instance.Get(identifier);

            //Assert
            Assert.IsNotNull(graph);
        }

        [TestMethod]
        public void Get_GivenGraphWithProvidedIdentifierDoesNotExist_ReturnsNull()
        {
            //Arrange
            GraphRegistry.Instance.Clear();
            string identifier = "G1";
            string nonexistant = "G2";
            GraphRegistry.Instance.Create(identifier);

            //Act
            var graph = GraphRegistry.Instance.Get(nonexistant);

            //Assert
            Assert.IsNull(graph);
        }

        [TestMethod]
        public void Get_GivenProvidedNodeIsNull_ReturnsNull()
        {
            //Arrange

            //Act&Assert
            Assert.ThrowsException<NodeNullReferenceException>(() => GraphRegistry.Instance.Get((INode)null));
        }

        [TestMethod]
        public void Get_GivenProvidedNodeNotAddedToAnyGraph_ReturnsNull()
        {
            //Arrange
            GraphRegistry.Instance.Clear();
            GraphRegistry.Instance.Create("GraphONE");

            Building00 b = new Building00();
            INode node = factory.CreateNode(b, nameof(b.Area));

            //Act
            var graph = GraphRegistry.Instance.Get(node);

            //Assert
            Assert.IsNull(graph);
        }

        [TestMethod]
        public void Get_GivenProvidedNodeIsAddedToGraph_ReturnsGraphContainigProvidedNode()
        {
            //Arrange
            GraphRegistry.Instance.Clear();
            GraphRegistry.Instance.Create("GraphONE");

            Building00 b = new Building00();
            INode node = factory.CreateNode(b, nameof(b.Area));
            GraphRegistry.Instance.GetDefault().AddNode(node);

            //Act
            var graph = GraphRegistry.Instance.Get(node);

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
            GraphRegistry.Instance.Clear();
            string identifier = "G1";
            GraphRegistry.Instance.Create(identifier);

            //Act
            var graph = GraphRegistry.Instance.GetOrCreate(identifier);

            //Assert
            Assert.IsTrue(graph.Identifier == identifier);
        }

        [TestMethod]
        public void GetOrCreate_GivenGraphWithProvidedIdentifierDoesNotExist_ReturnsNewDependencyGraph()
        {
            //Arrange
            GraphRegistry.Instance.Clear();
            string identifier = "G1";

            //Act
            var graph = GraphRegistry.Instance.GetOrCreate(identifier);

            //Assert
            Assert.IsTrue(graph.Identifier == identifier);
        }

        [TestMethod]
        public void GetOrCreate_GivenEmptyStringIdentifier_ThrowsException()
        {
            //Arrange
            GraphRegistry.Instance.Clear();
            string identifier = "";

            //Act & Assert
            Assert.ThrowsException<DependencyGraphException>(() => GraphRegistry.Instance.GetOrCreate(identifier));
        }

        #endregion

        #region GetRegisteredGraphs

        [TestMethod]
        public void GetRegisteredGraphs_GivenNoGraphsManuallyAdded_ReturnsListWithDefaultGraph()
        {
            //Arrange
            GraphRegistry.Instance.Clear();

            //Act
            List<IDependencyGraph> graphs = GraphRegistry.Instance.GetRegisteredGraphs();

            //Assert
            Assert.IsTrue(graphs.Exists(g => g.Identifier == GraphRegistry.DefaultGraphName));
        }

        [TestMethod]
        public void GetRegisteredGraphs_GivenGraphIsManuallyAdded_ReturnsListAddedGraph()
        {
            //Arrange
            GraphRegistry.Instance.Clear();
            GraphRegistry.Instance.Create("GraphONE");

            //Act
            List<IDependencyGraph> graphs = GraphRegistry.Instance.GetRegisteredGraphs();

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

            Assert.IsTrue(GraphRegistry.Instance.GetRegisteredGraphs().Exists(g => g.Identifier == GraphRegistry.DefaultGraphName));
        }

        [TestMethod]
        public void Clear_GivenMultipleGraphsExist_AllGraphsButDefaultOneAreRemoved()
        {
            //Arrange
            GraphRegistry.Instance.Clear();
            GraphRegistry.Instance.Create("GraphONE");
            GraphRegistry.Instance.Create("GraphTWO");
            GraphRegistry.Instance.Create("GraphTHREE");

            //Act
            GraphRegistry.Instance.Clear();

            //Assert
            Assert.IsTrue(GraphRegistry.Instance.GetRegisteredGraphs().Count == 1);
            Assert.IsTrue(GraphRegistry.Instance.GetRegisteredGraphs().Exists(g => g.Identifier == GraphRegistry.DefaultGraphName));
        }

        #endregion

        #region GetDefault()

        [TestMethod]
        public void GetDefault_ReturnsDefaultGraph()
        {
            //Arrange

            //Act
            var graph = GraphRegistry.Instance.GetDefault();

            //Assert
            Assert.IsTrue(graph.Identifier == GraphRegistry.DefaultGraphName);
        }


        #endregion
    }
}
