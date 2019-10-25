using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeCore;
using ReframeCore.Exceptions;
using System.Collections.Generic;

namespace ReframeCoreTests
{
    [TestClass]
    public class GraphFactoryTests
    {
        #region Create

        [TestMethod]
        public void Create_GivenNoOtherDependencyGraphsExist_CreatesNewDependencyGraph()
        {
            //Arrange
            GraphFactory.Clear();
            string identifier = "G1";

            //Act
            var graph = GraphFactory.Create(identifier);

            //Assert
            Assert.IsTrue(graph != null && graph.Identifier == identifier);
        }

        [TestMethod]
        public void Create_GivenUniqueIdentifierProvided_CreatesNewDependencyGraph()
        {
            //Arrange
            GraphFactory.Clear();
            GraphFactory.Create("G1");

            //Act
            var graph = GraphFactory.Create("G2");

            //Assert
            Assert.IsNotNull(graph);
        }

        [TestMethod]
        public void Create_GivenAlreadyTakenIdentifierProvided_ThrowsException()
        {
            //Arrange
            GraphFactory.Clear();
            var graph = GraphFactory.Create("G1");

            //Act & Assert
            Assert.ThrowsException<DependencyGraphException>(() => GraphFactory.Create("G1"));
        }

        [TestMethod]
        public void Create_GivenEmptyStringIdentifier_ThrowsException()
        {
            //Arrange
            GraphFactory.Clear();

            //Act & Assert
            Assert.ThrowsException<DependencyGraphException>(() => GraphFactory.Create(""));
        }

        [TestMethod]
        public void Create_GivenDefaultIdentifierProvided_ThrowsException()
        {
            //Arrange
            GraphFactory.Clear();

            //Act & Assert
            Assert.ThrowsException<DependencyGraphException>(() => GraphFactory.Create(GraphFactory.DefaultGraphName));
        }

        #endregion

        #region Get

        [TestMethod]
        public void Get_GivenGraphWithProvidedIdentifierExists_ReturnsDependencyGraph()
        {
            //Arrange
            GraphFactory.Clear();
            string identifier = "G1";
            GraphFactory.Create(identifier);

            //Act
            var graph = GraphFactory.Get(identifier);

            //Assert
            Assert.IsNotNull(graph);
        }

        [TestMethod]
        public void Get_GivenGraphWithProvidedIdentifierDoesNotExist_ReturnsNull()
        {
            //Arrange
            GraphFactory.Clear();
            string identifier = "G1";
            string nonexistant = "G2";
            GraphFactory.Create(identifier);

            //Act
            var graph = GraphFactory.Get(nonexistant);

            //Assert
            Assert.IsNull(graph);
        }

        #endregion

        #region GetOrCreate

        [TestMethod]
        public void GetOrCreate_GivenGraphWithProvidedIdentifierExists_ReturnsExistingDependencyGraph()
        {
            //Arrange
            GraphFactory.Clear();
            string identifier = "G1";
            GraphFactory.Create(identifier);

            //Act
            var graph = GraphFactory.GetOrCreate(identifier);

            //Assert
            Assert.IsTrue(graph.Identifier == identifier);
        }

        [TestMethod]
        public void GetOrCreate_GivenGraphWithProvidedIdentifierDoesNotExist_ReturnsNewDependencyGraph()
        {
            //Arrange
            GraphFactory.Clear();
            string identifier = "G1";

            //Act
            var graph = GraphFactory.GetOrCreate(identifier);

            //Assert
            Assert.IsTrue(graph.Identifier == identifier);
        }

        [TestMethod]
        public void GetOrCreate_GivenEmptyStringIdentifier_ThrowsException()
        {
            //Arrange
            GraphFactory.Clear();
            string identifier = "";

            //Act & Assert
            Assert.ThrowsException<DependencyGraphException>(() => GraphFactory.GetOrCreate(identifier));
        }

        #endregion

        #region GetRegisteredGraphs

        [TestMethod]
        public void GetRegisteredGraphs_GivenNoGraphsManuallyAdded_ReturnsListWithDefaultGraph()
        {
            //Arrange
            GraphFactory.Clear();

            //Act
            List<IDependencyGraph> graphs = GraphFactory.GetRegisteredGraphs();

            //Assert
            Assert.IsTrue(graphs.Exists(g => g.Identifier == GraphFactory.DefaultGraphName));
        }

        [TestMethod]
        public void GetRegisteredGraphs_GivenGraphIsManuallyAdded_ReturnsListAddedGraph()
        {
            //Arrange
            GraphFactory.Clear();
            GraphFactory.Create("GraphONE");

            //Act
            List<IDependencyGraph> graphs = GraphFactory.GetRegisteredGraphs();

            //Assert
            Assert.IsTrue(graphs.Exists(g => g.Identifier == "GraphONE"));
            Assert.IsTrue(graphs.Exists(g => g.Identifier == GraphFactory.DefaultGraphName));
        }

        #endregion

        #region Clear

        [TestMethod]
        public void Clear_GivenOnlyDefaultGraphExists_DefaultGraphIsNotRemoved()
        {
            GraphFactory.Clear();

            Assert.IsTrue(GraphFactory.GetRegisteredGraphs().Exists(g => g.Identifier == GraphFactory.DefaultGraphName));
        }

        [TestMethod]
        public void Clear_GivenMultipleGraphsExist_AllGraphsButDefaultOneAreRemoved()
        {
            //Arrange
            GraphFactory.Clear();
            GraphFactory.Create("GraphONE");
            GraphFactory.Create("GraphTWO");
            GraphFactory.Create("GraphTHREE");

            //Act
            GraphFactory.Clear();

            //Assert
            Assert.IsTrue(GraphFactory.GetRegisteredGraphs().Count == 1);
            Assert.IsTrue(GraphFactory.GetRegisteredGraphs().Exists(g => g.Identifier == GraphFactory.DefaultGraphName));
        }

        #endregion
    }
}
