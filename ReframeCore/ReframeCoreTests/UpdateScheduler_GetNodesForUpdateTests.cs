using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeCore;
using ReframeCoreExamples.E00;
using ReframeCore.Nodes;
using ReframeCore.Helpers;
using System.Collections.Generic;
using System.Linq;
using ReframeCore.Factories;

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
            UpdateLogger actualNodesToUpdate = scheduler.LoggerNodesForUpdate;
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
            UpdateLogger actualNodesToUpdate = scheduler.LoggerNodesForUpdate;
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
            UpdateLogger actualNodesToUpdate = scheduler.LoggerNodesForUpdate;
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
            UpdateLogger actualNodesToUpdate = scheduler.LoggerNodesForUpdate;
            UpdateLogger expectedNodesToUpdate = CreateExpectedLogger_FullGraphUpdate(graph, o);

            Assert.AreEqual(expectedNodesToUpdate, actualNodesToUpdate);
        }

        #endregion

        #region DetermineNodeLevel

        private void CreateCase1(IDependencyGraph graph, GenericReactiveObject obj)
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

            INode jNode = graph.AddNode(obj, "J");
            INode kNode = graph.AddNode(obj, "K");
            INode lNode = graph.AddNode(obj, "L");
            INode mNode = graph.AddNode(obj, "M");

            graph.AddDependency(aNode, dNode);

            graph.AddDependency(bNode, dNode);

            graph.AddDependency(cNode, aNode);
            graph.AddDependency(cNode, bNode);

            graph.AddDependency(dNode, gNode);
            graph.AddDependency(dNode, hNode);

            graph.AddDependency(eNode, aNode);
            graph.AddDependency(eNode, dNode);
            graph.AddDependency(eNode, fNode);

            graph.AddDependency(fNode, jNode);
            graph.AddDependency(fNode, kNode);

            graph.AddDependency(gNode, iNode);

            graph.AddDependency(hNode, jNode);
            graph.AddDependency(hNode, iNode);

            graph.AddDependency(iNode, lNode);

            graph.AddDependency(jNode, lNode);
            graph.AddDependency(jNode, mNode);

            graph.AddDependency(kNode, jNode);

            graph.Initialize();
        }

        [TestMethod]
        public void GetNodesForUpdate_Case1_CompleteGraph_GivesCorrectNodeLevels()
        {
            //Arrange
            GraphFactory.Clear();
            var graph = GraphFactory.Create("G");
            GenericReactiveObject o = new GenericReactiveObject();

            CreateCase1(graph, o);
            UpdateScheduler scheduler = (graph as DependencyGraph).UpdateScheduler;

            //Act
            scheduler.GetNodesForUpdate();

            //Assert
            Assert.AreEqual(0, graph.GetNode(o, "L").Level);
            Assert.AreEqual(0, graph.GetNode(o, "M").Level);

            Assert.AreEqual(1, graph.GetNode(o, "I").Level);
            Assert.AreEqual(1, graph.GetNode(o, "J").Level);

            Assert.AreEqual(2, graph.GetNode(o, "G").Level);
            Assert.AreEqual(2, graph.GetNode(o, "H").Level);
            Assert.AreEqual(2, graph.GetNode(o, "K").Level);

            Assert.AreEqual(3, graph.GetNode(o, "F").Level);
            Assert.AreEqual(3, graph.GetNode(o, "D").Level);

            Assert.AreEqual(4, graph.GetNode(o, "B").Level);
            Assert.AreEqual(4, graph.GetNode(o, "A").Level);

            Assert.AreEqual(5, graph.GetNode(o, "C").Level);
            Assert.AreEqual(5, graph.GetNode(o, "E").Level);
        }

        [TestMethod]
        public void GetNodesForUpdate_Case1_InitialNode_GivesCorrectNodeLevels()
        {
            //Arrange
            GraphFactory.Clear();
            var graph = GraphFactory.Create("G");
            GenericReactiveObject o = new GenericReactiveObject();

            CreateCase1(graph, o);
            UpdateScheduler scheduler = (graph as DependencyGraph).UpdateScheduler;

            //Act
            scheduler.GetNodesForUpdate();

            //Assert
            Assert.AreEqual(0, graph.GetNode(o, "L").Level);
            Assert.AreEqual(0, graph.GetNode(o, "M").Level);

            Assert.AreEqual(1, graph.GetNode(o, "I").Level);
            Assert.AreEqual(1, graph.GetNode(o, "J").Level);

            Assert.AreEqual(2, graph.GetNode(o, "G").Level);
            Assert.AreEqual(2, graph.GetNode(o, "H").Level);

            Assert.AreEqual(3, graph.GetNode(o, "D").Level);
        }

        private void CreateCase2(IDependencyGraph graph, GenericReactiveObject obj)
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

        [TestMethod]
        public void GetNodesForUpdate_Case2_CompleteGraph_GivesCorrectNodeLevels()
        {
            //Arrange
            GraphFactory.Clear();
            var graph = GraphFactory.Create("G");
            GenericReactiveObject o = new GenericReactiveObject();

            CreateCase2(graph, o);
            UpdateScheduler scheduler = (graph as DependencyGraph).UpdateScheduler;

            //Act
            scheduler.GetNodesForUpdate();

            //Assert
            Assert.AreEqual(0, graph.GetNode(o, "E").Level);
            Assert.AreEqual(0, graph.GetNode(o, "I").Level);

            Assert.AreEqual(1, graph.GetNode(o, "D").Level);
            Assert.AreEqual(1, graph.GetNode(o, "H").Level);

            Assert.AreEqual(2, graph.GetNode(o, "C").Level);
            Assert.AreEqual(2, graph.GetNode(o, "G").Level);

            Assert.AreEqual(3, graph.GetNode(o, "B").Level);
            Assert.AreEqual(3, graph.GetNode(o, "F").Level);

            Assert.AreEqual(4, graph.GetNode(o, "A").Level);
        }

        #endregion
    }
}
