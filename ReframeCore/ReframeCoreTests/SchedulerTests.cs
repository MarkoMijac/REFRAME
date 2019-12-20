using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeCore;
using ReframeCore.Factories;
using ReframeCore.Helpers;
using ReframeCore.Nodes;
using ReframeCoreExamples.E00;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCoreTests
{
    [TestClass]
    public class SchedulerTests
    {
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
        }

        private NodeLog CreateExpectedLogger_FullGraphUpdate(IDependencyGraph graph, GenericReactiveObject obj)
        {
            NodeLog logger = new NodeLog();

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
            var graph = new DependencyGraph("G");
            GenericReactiveObject o = new GenericReactiveObject();

            CreateCase(graph, o);
            ISorter sorter = new DFS_Sorter();
            var scheduler = new Scheduler(graph, sorter);
            scheduler.SkipUpdateIfInitialNodeNotChanged = false;

            //Act
            IList<INode> nodesForUpdate = scheduler.GetNodesForUpdate(graph.GetNode(o, "A"), true);

            //Assert
            NodeLog actualNodesToUpdate = scheduler.NodeLog;
            NodeLog expectedNodesToUpdate = CreateExpectedLogger_FullGraphUpdate(graph, o);

            Assert.AreEqual(expectedNodesToUpdate, actualNodesToUpdate);
        }

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
        }

        [TestMethod]
        public void GetNodesForUpdate_Case1_CompleteGraph_GivesCorrectNodeLayers()
        {
            //Arrange
            var graph = new DependencyGraph("G");
            GenericReactiveObject o = new GenericReactiveObject();

            CreateCase1(graph, o);
            var scheduler = new Scheduler(graph, new DFS_Sorter());

            //Act
            IList<INode> nodesForUpdate = scheduler.GetNodesForUpdate();
            scheduler.GetLayersOfNodesForUpdate(nodesForUpdate);

            //Assert
            Assert.AreEqual(0, graph.GetNode(o, "L").Layer);
            Assert.AreEqual(0, graph.GetNode(o, "M").Layer);

            Assert.AreEqual(1, graph.GetNode(o, "I").Layer);
            Assert.AreEqual(1, graph.GetNode(o, "J").Layer);

            Assert.AreEqual(2, graph.GetNode(o, "G").Layer);
            Assert.AreEqual(2, graph.GetNode(o, "H").Layer);
            Assert.AreEqual(2, graph.GetNode(o, "K").Layer);

            Assert.AreEqual(3, graph.GetNode(o, "F").Layer);
            Assert.AreEqual(3, graph.GetNode(o, "D").Layer);

            Assert.AreEqual(4, graph.GetNode(o, "B").Layer);
            Assert.AreEqual(4, graph.GetNode(o, "A").Layer);

            Assert.AreEqual(5, graph.GetNode(o, "C").Layer);
            Assert.AreEqual(5, graph.GetNode(o, "E").Layer);
        }

        [TestMethod]
        public void GetNodesForUpdate_Case1_InitialNode_GivesCorrectNodeLayers()
        {
            //Arrange
            var graph = new DependencyGraph("G");
            GenericReactiveObject o = new GenericReactiveObject();

            CreateCase1(graph, o);
            var scheduler = new Scheduler(graph, new DFS_Sorter());

            //Act
            IList<INode> nodesForUpdate = scheduler.GetNodesForUpdate();
            scheduler.GetLayersOfNodesForUpdate(nodesForUpdate);

            //Assert
            Assert.AreEqual(0, graph.GetNode(o, "L").Layer);
            Assert.AreEqual(0, graph.GetNode(o, "M").Layer);

            Assert.AreEqual(1, graph.GetNode(o, "I").Layer);
            Assert.AreEqual(1, graph.GetNode(o, "J").Layer);

            Assert.AreEqual(2, graph.GetNode(o, "G").Layer);
            Assert.AreEqual(2, graph.GetNode(o, "H").Layer);

            Assert.AreEqual(3, graph.GetNode(o, "D").Layer);
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
        }

        [TestMethod]
        public void GetNodesForUpdate_Case2_CompleteGraph_GivesCorrectNodeLayers()
        {
            //Arrange
            var graph = new DependencyGraph("G");
            GenericReactiveObject o = new GenericReactiveObject();

            CreateCase2(graph, o);
            var scheduler = new Scheduler(graph, new DFS_Sorter());

            //Act
            IList<INode> nodesForUpdate = scheduler.GetNodesForUpdate();
            scheduler.GetLayersOfNodesForUpdate(nodesForUpdate);

            //Assert
            Assert.AreEqual(0, graph.GetNode(o, "E").Layer);
            Assert.AreEqual(0, graph.GetNode(o, "I").Layer);

            Assert.AreEqual(1, graph.GetNode(o, "D").Layer);
            Assert.AreEqual(1, graph.GetNode(o, "H").Layer);

            Assert.AreEqual(2, graph.GetNode(o, "C").Layer);
            Assert.AreEqual(2, graph.GetNode(o, "G").Layer);

            Assert.AreEqual(3, graph.GetNode(o, "B").Layer);
            Assert.AreEqual(3, graph.GetNode(o, "F").Layer);

            Assert.AreEqual(4, graph.GetNode(o, "A").Layer);
        }

        [TestMethod]
        public void GetNodesForUpdate_TriggeredNodeValueNotChangedAndSkippingUpdateEnabled_ShouldBeZeroNodesForUpdate()
        {
            //Arrange
            var graph = new DependencyGraph("G");
            GenericReactiveObject o = new GenericReactiveObject();

            CreateCase(graph, o);
            var scheduler = new Scheduler(graph, new DFS_Sorter());
            scheduler.SkipUpdateIfInitialNodeNotChanged = true;

            //Act
            scheduler.GetNodesForUpdate(graph.GetNode(o, "A"), true);

            //Assert
            NodeLog actualNodesToUpdate = scheduler.NodeLog;
            NodeLog expectedNodesToUpdate = new NodeLog();

            Assert.AreEqual(expectedNodesToUpdate, actualNodesToUpdate);
        }

        [TestMethod]
        public void GetNodesForUpdate_TriggeredNodeValueChangedAndSkippingUpdateNotEnabled_SchedulerCorrectUpdateOrder()
        {
            //Arrange
            var graph = new DependencyGraph("G");
            GenericReactiveObject o = new GenericReactiveObject();

            CreateCase(graph, o);
            var scheduler = new Scheduler(graph, new DFS_Sorter());
            scheduler.SkipUpdateIfInitialNodeNotChanged = false;

            //Act
            o.A = 5;
            scheduler.GetNodesForUpdate(graph.GetNode(o, "A"), true);

            //Assert
            NodeLog actualNodesToUpdate = scheduler.NodeLog;
            NodeLog expectedNodesToUpdate = CreateExpectedLogger_FullGraphUpdate(graph, o);

            Assert.AreEqual(expectedNodesToUpdate, actualNodesToUpdate);
        }

        [TestMethod]
        public void GetNodesForUpdate_TriggeredNodeValueChangedAndSkippingUpdateEnabled_SchedulerCorrectUpdateOrder()
        {
            //Arrange
            var graph = new DependencyGraph("G");
            GenericReactiveObject o = new GenericReactiveObject();

            CreateCase(graph, o);
            var scheduler = new Scheduler(graph, new DFS_Sorter());
            scheduler.SkipUpdateIfInitialNodeNotChanged = true;

            //Act
            o.A = 5;
            scheduler.GetNodesForUpdate(graph.GetNode(o, "A"), true);

            //Assert
            NodeLog actualNodesToUpdate = scheduler.NodeLog;
            NodeLog expectedNodesToUpdate = CreateExpectedLogger_FullGraphUpdate(graph, o);

            Assert.AreEqual(expectedNodesToUpdate, actualNodesToUpdate);
        }
    }
}
