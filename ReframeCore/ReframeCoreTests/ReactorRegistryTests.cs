using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeCore.Factories;
using ReframeCore.Exceptions;
using ReframeCore;
using ReframeCore.Helpers;

namespace ReframeCoreTests
{
    [TestClass]
    public class ReactorRegistryTests
    {
        #region CreateReactor(string identifier)

        [TestMethod]
        public void CreateReactor_GivenNoOtherReactorExist_CreatesNewReactor()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            string identifier = "R1";

            //Act
            var reactor = ReactorRegistry.Instance.CreateReactor(identifier);

            //Assert
            Assert.IsTrue(reactor != null && reactor.Identifier == identifier);
        }

        [TestMethod]
        public void CreateReactor_GivenUniqueIdentifierProvided_CreatesNewReactor()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            ReactorRegistry.Instance.CreateReactor("R1");

            //Act
            var reactor = ReactorRegistry.Instance.CreateReactor("R2");

            //Assert
            Assert.IsNotNull(reactor);
        }

        [TestMethod]
        public void CreateReactor_GivenAlreadyTakenIdentifierProvided_ThrowsException()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("R1");

            //Act & Assert
            Assert.ThrowsException<ReactorException>(() => ReactorRegistry.Instance.CreateReactor("R1"));
        }

        [TestMethod]
        public void CreateReactor_GivenEmptyStringIdentifier_ThrowsException()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();

            //Act & Assert
            Assert.ThrowsException<ReactorException>(() => ReactorRegistry.Instance.CreateReactor(""));
        }

        #endregion

        #region CreateReactor(string identifier, IDependencyGraph graph)

        [TestMethod]
        public void CreateReactor2_GivenIdentifierAndGraph_CreatesNewReactor()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            string identifier = "R1";
            var graph = new DependencyGraph(identifier);

            //Act
            var reactor = ReactorRegistry.Instance.CreateReactor(identifier, graph);

            //Assert
            Assert.IsTrue(reactor != null && reactor.Identifier == identifier && reactor.Graph == graph);
        }

        [TestMethod]
        public void CreateReactor2_GivenAlreadyTakenIdentifier_ThrowsException()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("R1");
            var graph = new DependencyGraph("R1");

            //Act & Assert
            Assert.ThrowsException<ReactorException>(() => ReactorRegistry.Instance.CreateReactor("R1", graph));
        }

        [TestMethod]
        public void CreateReactor2_GivenEmptyStringIdentifier_ThrowsException()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            IDependencyGraph graph = new DependencyGraph("R1");

            //Act & Assert
            Assert.ThrowsException<ReactorException>(() => ReactorRegistry.Instance.CreateReactor("", graph));
        }

        [TestMethod]
        public void CreateReactor2_GivenDependencyGraphIsNull_ThrowsException()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            string identifier = "R1";
            IDependencyGraph graph = null;

            //Act & Assert
            Assert.ThrowsException<ReactorException>(() => ReactorRegistry.Instance.CreateReactor(identifier, graph));
        }

        [TestMethod]
        public void CreateReactor2_GivenThereIsAlreadyReactorForProvidedGraph_ThrowsException()
        {
            //Arrange
            string identifier = "R1";
            var graph = new DependencyGraph(identifier);

            var reactor = ReactorRegistry.Instance.CreateReactor(identifier, graph);

            //Act
            Assert.ThrowsException<ReactorException>(()=>ReactorRegistry.Instance.CreateReactor("R2", graph));
        }

        #endregion

        #region CreateReactor(string identifier, IDependencyGraph graph, IUpdater updater)

        [TestMethod]
        public void CreateReactor3_GivenAllParameters_CreatesNewReactor()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            string identifier = "R1";
            var graph = new DependencyGraph(identifier);
            var scheduler = new Scheduler(graph, new DFS_Sorter());
            var updater = new Updater(graph, scheduler);

            //Act
            var reactor = ReactorRegistry.Instance.CreateReactor(identifier, graph, updater);

            //Assert
            Assert.IsTrue(reactor != null && reactor.Identifier == identifier 
                && reactor.Graph == graph && reactor.Updater == updater);
        }

        [TestMethod]
        public void CreateReactor3_GivenAlreadyTakenIdentifier_ThrowsException()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            string identifier = "R1";
            var graph = new DependencyGraph(identifier);
            var scheduler = new Scheduler(graph, new DFS_Sorter());
            var updater = new Updater(graph, scheduler);
            ReactorRegistry.Instance.CreateReactor(identifier);

            //Act
            Assert.ThrowsException<ReactorException>(() => ReactorRegistry.Instance.CreateReactor(identifier, graph, updater));
        }

        [TestMethod]
        public void CreateReactor3_GivenEmptyStringIdentifier_ThrowsException()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            string identifier = "";
            var graph = new DependencyGraph(identifier);
            var scheduler = new Scheduler(graph, new DFS_Sorter());
            var updater = new Updater(graph, scheduler);

            //Act
            Assert.ThrowsException<ReactorException>(() => ReactorRegistry.Instance.CreateReactor(identifier, graph, updater));
        }

        [TestMethod]
        public void CreateReactor3_GivenDependencyGraphIsNull_ThrowsException()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            string identifier = "R1";
            var graph = new DependencyGraph(identifier);
            var scheduler = new Scheduler(graph, new DFS_Sorter());
            var updater = new Updater(graph, scheduler);

            //Act&Assert
            Assert.ThrowsException<ReactorException>(() => ReactorRegistry.Instance.CreateReactor(identifier, null, updater));
        }

        [TestMethod]
        public void CreateReactor3_GivenThereIsAlreadyReactorForProvidedGraph_ThrowsException()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            string identifier = "R1";
            var graph = new DependencyGraph(identifier);
            var scheduler = new Scheduler(graph, new DFS_Sorter());
            var updater = new Updater(graph, scheduler);

            var reactor = ReactorRegistry.Instance.CreateReactor("ExistingReactor", graph);

            //Act&Assert
            Assert.ThrowsException<ReactorException>(() => ReactorRegistry.Instance.CreateReactor(identifier, graph, updater));
        }

        [TestMethod]
        public void CreateReactor3_GivenUpdaterIsNull_ThrowsException()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            string identifier = "R1";
            var graph = new DependencyGraph(identifier);
            var scheduler = new Scheduler(graph, new DFS_Sorter());
            var updater = null as IUpdater;

            //Act&Assert
            Assert.ThrowsException<ReactorException>(() => ReactorRegistry.Instance.CreateReactor(identifier, graph, updater));
        }

        [TestMethod]
        public void CreateReactor3_GivenGraphAndUpdaterDoNotMatch_ThrowsException()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            string identifier = "R1";
            var graph = new DependencyGraph(identifier);
            var scheduler = new Scheduler(graph, new DFS_Sorter());
            var updater = new Updater(new DependencyGraph("R2"), scheduler);

            //Act
            Assert.ThrowsException<ReactorException>(()=> ReactorRegistry.Instance.CreateReactor(identifier, graph, updater));
        }

        #endregion

        #region CreateReactor(string identifier, IDependencyGraph graph, ISorter sorter)

        [TestMethod]
        public void CreateReactor4_GivenAllParameters_CreatesNewReactor()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            string identifier = "R1";
            var graph = new DependencyGraph(identifier);
            var sorter = new DFS_Sorter();

            //Act
            var reactor = ReactorRegistry.Instance.CreateReactor(identifier, graph, sorter);

            //Assert
            Assert.IsTrue(reactor != null && reactor.Identifier == identifier
                && reactor.Graph == graph && reactor.Updater.Scheduler.Sorter == sorter);
        }

        [TestMethod]
        public void CreateReactor4_GivenAlreadyTakenIdentifier_ThrowsException()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            string identifier = "R1";
            var graph = new DependencyGraph(identifier);
            var sorter = new DFS_Sorter();
            ReactorRegistry.Instance.CreateReactor(identifier);

            //Act
            Assert.ThrowsException<ReactorException>(() => ReactorRegistry.Instance.CreateReactor(identifier, graph, sorter));
        }

        [TestMethod]
        public void CreateReactor4_GivenEmptyStringIdentifier_ThrowsException()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            string identifier = "";
            var graph = new DependencyGraph(identifier);
            var sorter = new DFS_Sorter();

            //Act
            Assert.ThrowsException<ReactorException>(() => ReactorRegistry.Instance.CreateReactor(identifier, graph, sorter));
        }

        [TestMethod]
        public void CreateReactor4_GivenDependencyGraphIsNull_ThrowsException()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            string identifier = "R1";
            var graph = new DependencyGraph(identifier);
            var sorter = new DFS_Sorter();

            //Act&Assert
            Assert.ThrowsException<ReactorException>(() => ReactorRegistry.Instance.CreateReactor(identifier, null, sorter));
        }

        [TestMethod]
        public void CreateReactor4_GivenThereIsAlreadyReactorForProvidedGraph_ThrowsException()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            string identifier = "R1";
            var graph = new DependencyGraph(identifier);
            var sorter = new DFS_Sorter();

            var reactor = ReactorRegistry.Instance.CreateReactor("ExistingReactor", graph);

            //Act&Assert
            Assert.ThrowsException<ReactorException>(() => ReactorRegistry.Instance.CreateReactor(identifier, graph, sorter));
        }

        [TestMethod]
        public void CreateReactor4_GivenSorterIsNull_ThrowsException()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            string identifier = "R1";
            var graph = new DependencyGraph(identifier);
            ISorter sorter = null;

            //Act&Assert
            Assert.ThrowsException<ReactorException>(() => ReactorRegistry.Instance.CreateReactor(identifier, graph, sorter));
        }

        #endregion

        #region CreateReactor(string identifier, IDependencyGraph graph, IScheduler scheduler)

        [TestMethod]
        public void CreateReactor5_GivenAllParameters_CreatesNewReactor()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            string identifier = "R1";
            var graph = new DependencyGraph(identifier);
            var sorter = new DFS_Sorter();
            var scheduler = new Scheduler(graph, sorter);

            //Act
            var reactor = ReactorRegistry.Instance.CreateReactor(identifier, graph, scheduler);

            //Assert
            Assert.IsTrue(reactor != null && reactor.Identifier == identifier
                && reactor.Graph == graph && reactor.Updater.Scheduler == scheduler);
        }

        [TestMethod]
        public void CreateReactor5_GivenAlreadyTakenIdentifier_ThrowsException()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            string identifier = "R1";
            var graph = new DependencyGraph(identifier);
            var sorter = new DFS_Sorter();
            var scheduler = new Scheduler(graph, sorter);
            ReactorRegistry.Instance.CreateReactor(identifier);

            //Act
            Assert.ThrowsException<ReactorException>(() => ReactorRegistry.Instance.CreateReactor(identifier, graph, scheduler));
        }

        [TestMethod]
        public void CreateReactor5_GivenEmptyStringIdentifier_ThrowsException()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            string identifier = "";
            var graph = new DependencyGraph(identifier);
            var sorter = new DFS_Sorter();
            var scheduler = new Scheduler(graph, sorter);

            //Act
            Assert.ThrowsException<ReactorException>(() => ReactorRegistry.Instance.CreateReactor(identifier, graph, scheduler));
        }

        [TestMethod]
        public void CreateReactor5_GivenDependencyGraphIsNull_ThrowsException()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            string identifier = "R1";
            var graph = new DependencyGraph(identifier);
            var sorter = new DFS_Sorter();
            var scheduler = new Scheduler(graph, sorter);

            //Act&Assert
            Assert.ThrowsException<ReactorException>(() => ReactorRegistry.Instance.CreateReactor(identifier, null, scheduler));
        }

        [TestMethod]
        public void CreateReactor5_GivenThereIsAlreadyReactorForProvidedGraph_ThrowsException()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            string identifier = "R1";
            var graph = new DependencyGraph(identifier);
            var sorter = new DFS_Sorter();
            var scheduler = new Scheduler(graph, sorter);

            var reactor = ReactorRegistry.Instance.CreateReactor("ExistingReactor", graph);

            //Act&Assert
            Assert.ThrowsException<ReactorException>(() => ReactorRegistry.Instance.CreateReactor(identifier, graph, scheduler));
        }

        [TestMethod]
        public void CreateReactor5_GivenSchedulerIsNull_ThrowsException()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            string identifier = "R1";
            var graph = new DependencyGraph(identifier);
            IScheduler scheduler = null;

            //Act&Assert
            Assert.ThrowsException<ReactorException>(() => ReactorRegistry.Instance.CreateReactor(identifier, graph, scheduler));
        }

        [TestMethod]
        public void CreateReactor5_GivenGraphAndSchedulerDoNotMatch_ThrowsException()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            string identifier = "R1";
            var graph = new DependencyGraph(identifier);
            var scheduler = new Scheduler(new DependencyGraph("R2"), new DFS_Sorter());

            //Act
            Assert.ThrowsException<ReactorException>(() => ReactorRegistry.Instance.CreateReactor(identifier, graph, scheduler));
        }

        #endregion

        #region GetReactor(string identifier)

        [TestMethod]
        public void GetReactor_GivenReactorWithProvidedIdentifierExists_ReturnsReactor()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            string identifier = "R1";
            ReactorRegistry.Instance.CreateReactor(identifier);

            //Act
            var reactor = ReactorRegistry.Instance.GetReactor(identifier);

            //Assert
            Assert.IsNotNull(reactor);
        }

        [TestMethod]
        public void GetReactor_GivenReactorWithProvidedIdentifierDoesNotExist_ThrowsException()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            string identifier = "R1";
            string nonexistant = "R2";
            ReactorRegistry.Instance.CreateReactor(identifier);

            //Act&Assert
            Assert.ThrowsException<ReactorException>(() => ReactorRegistry.Instance.GetReactor(nonexistant));
        }

        #endregion

        #region GetReactor(IDependencyGraph graph)

        [TestMethod]
        public void GetReactor_GivenReactorForProvidedGraphExists_ReturnsReactor()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            string identifier = "R1";
            var graph = new DependencyGraph(identifier);
            var reactor = ReactorRegistry.Instance.CreateReactor(identifier, graph);

            //Act
            var foundReactor = ReactorRegistry.Instance.GetReactor(graph);

            //Assert
            Assert.AreEqual(reactor, foundReactor);
        }

        [TestMethod]
        public void GetReactor_GivenReactorForProvidedGraphDoesNotExist_ReturnsNull()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            string identifier = "R1";
            string nonexistant = "R2";
            ReactorRegistry.Instance.CreateReactor(identifier);
            var graph = new DependencyGraph(nonexistant);
            
            //Act
            var reactor = ReactorRegistry.Instance.GetReactor(graph);

            //Assert
            Assert.IsNull(reactor);
        }

        [TestMethod]
        public void GetReactor_GivenProvidedGraphIsNull_ReturnsNull()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            string identifier = "R1";
            ReactorRegistry.Instance.CreateReactor(identifier);
            IDependencyGraph graph = null;

            //Act
            var reactor = ReactorRegistry.Instance.GetReactor(graph);

            //Assert
            Assert.IsNull(reactor);
        }

        #endregion
    }
}
