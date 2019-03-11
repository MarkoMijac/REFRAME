using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeCore;
using ReframeCoreExamples.E00;
using ReframeCore.Nodes;
using ReframeCore.Helpers;

namespace ReframeCoreTests
{
    [TestClass]
    public class UpdateScheduler_GetNodesForUpdateTests
    {
        #region Skipping update tests

        private void CreateCase(IDependencyGraph graph, GenericReactiveObject obj)
        {
            INode aNode = graph.AddNode(obj, "A");
            INode bNode = graph.AddNode(obj, "B");
            INode cNode = graph.AddNode(obj, "C");
            INode dNode = graph.AddNode(obj, "D");
            INode eNode = graph.AddNode(obj, "E");
            INode fNode = graph.AddNode(obj, "F");
            INode gNode = graph.AddNode(obj, "G");
            INode hNode = graph.AddNode(obj, "H");
            INode iNode = graph.AddNode(obj, "I");

            graph.AddDependency(aNode, bNode);
            graph.AddDependency(bNode, cNode);
            graph.AddDependency(cNode, dNode);
            graph.AddDependency(dNode, eNode);

            graph.AddDependency(aNode, fNode);
            graph.AddDependency(fNode, gNode);
            graph.AddDependency(gNode, hNode);
            graph.AddDependency(hNode, iNode);

            graph.Initialize();
        }

        private UpdateLogger CreateExpectedLogger_FullGraphUpdate(IDependencyGraph graph, GenericReactiveObject obj)
        {
            UpdateLogger logger = new UpdateLogger();

            logger.Log(graph.GetNode(obj, "F"));
            logger.Log(graph.GetNode(obj, "G"));
            logger.Log(graph.GetNode(obj, "H"));
            logger.Log(graph.GetNode(obj, "I"));

            logger.Log(graph.GetNode(obj, "B"));
            logger.Log(graph.GetNode(obj, "C"));
            logger.Log(graph.GetNode(obj, "D"));
            logger.Log(graph.GetNode(obj, "E"));
            
            return logger;
        }

        [TestMethod]
        public void GetNodesForUpdate_TriggeredNodeValueNotChangedAndSkippingUpdateNotEnabled_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            GraphFactory.Clear();
            var graph = GraphFactory.Create("G");
            GenericReactiveObject o = new GenericReactiveObject();

            CreateCase(graph, o);
            UpdateScheduler scheduler = (graph as DependencyGraph).UpdateScheduler;
            scheduler.EnableSkippingUpdateIfInitialNodeValueNotChanged = false;

            //Act
            scheduler.GetNodesForUpdate(graph.GetNode(o, "A"), true);

            //Assert
            UpdateLogger actualNodesToUpdate = scheduler.Logger;
            UpdateLogger expectedNodesToUpdate = CreateExpectedLogger_FullGraphUpdate(graph, o);

            Assert.AreEqual(expectedNodesToUpdate, actualNodesToUpdate);
        }

        [TestMethod]
        public void GetNodesForUpdate_TriggeredNodeValueNotChangedAndSkippingUpdateEnabled_ShouldBeZeroNodesForUpdate()
        {
            //Arrange
            GraphFactory.Clear();
            var graph = GraphFactory.Create("G");
            GenericReactiveObject o = new GenericReactiveObject();

            CreateCase(graph, o);
            UpdateScheduler scheduler = (graph as DependencyGraph).UpdateScheduler;
            scheduler.EnableSkippingUpdateIfInitialNodeValueNotChanged = true;

            //Act
            scheduler.GetNodesForUpdate(graph.GetNode(o, "A"), true);

            //Assert
            UpdateLogger actualNodesToUpdate = scheduler.Logger;
            UpdateLogger expectedNodesToUpdate = new UpdateLogger();

            Assert.AreEqual(expectedNodesToUpdate, actualNodesToUpdate);
        }

        [TestMethod]
        public void GetNodesForUpdate_TriggeredNodeValueChangedAndSkippingUpdateNotEnabled_SchedulerCorrectUpdateOrder()
        {
            //Arrange
            GraphFactory.Clear();
            var graph = GraphFactory.Create("G");
            GenericReactiveObject o = new GenericReactiveObject();

            CreateCase(graph, o);
            UpdateScheduler scheduler = (graph as DependencyGraph).UpdateScheduler;
            scheduler.EnableSkippingUpdateIfInitialNodeValueNotChanged = false;

            //Act
            o.A = 5;
            scheduler.GetNodesForUpdate(graph.GetNode(o, "A"), true);

            //Assert
            UpdateLogger actualNodesToUpdate = scheduler.Logger;
            UpdateLogger expectedNodesToUpdate = CreateExpectedLogger_FullGraphUpdate(graph, o);

            Assert.AreEqual(expectedNodesToUpdate, actualNodesToUpdate);
        }

        [TestMethod]
        public void GetNodesForUpdate_TriggeredNodeValueChangedAndSkippingUpdateEnabled_SchedulerCorrectUpdateOrder()
        {
            //Arrange
            GraphFactory.Clear();
            var graph = GraphFactory.Create("G");
            GenericReactiveObject o = new GenericReactiveObject();

            CreateCase(graph, o);
            UpdateScheduler scheduler = (graph as DependencyGraph).UpdateScheduler;
            scheduler.EnableSkippingUpdateIfInitialNodeValueNotChanged = true;

            //Act
            o.A = 5;
            scheduler.GetNodesForUpdate(graph.GetNode(o, "A"), true);

            //Assert
            UpdateLogger actualNodesToUpdate = scheduler.Logger;
            UpdateLogger expectedNodesToUpdate = CreateExpectedLogger_FullGraphUpdate(graph, o);

            Assert.AreEqual(expectedNodesToUpdate, actualNodesToUpdate);
        }

        #endregion
    }
}
