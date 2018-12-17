using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeCore;
using ReframeCore.Exceptions;
using ReframeCoreExamples.E00;
using ReframeCore.Helpers;

namespace ReframeCoreTests
{
    [TestClass]
    public class DependencyGraphTests
    {
        [TestMethod]
        public void AddNode_GivenNullObject_ThrowsException()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            INode node = null;

            //Act&Assert
            Assert.ThrowsException<NodeNullReferenceException>(()=>graph.AddNode(node));
        }

        [TestMethod]
        public void AddNode_GivenValidNode_ReturnsTrue()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();
            string memberName = "Width";
            INode node = new Node(building, memberName);

            //Act
            bool added = graph.AddNode(node);

            //Assert
            Assert.IsTrue(added);
        }

        [TestMethod]
        public void AddNode_GivenAlreadyAddedNode_ReturnsFalse()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();
            string memberName = "Width";
            INode node = new Node(building, memberName);
            graph.AddNode(node);

            //Act
            bool added = graph.AddNode(node);

            //Assert
            Assert.IsFalse(added);
        }

        [TestMethod]
        public void AddNode1_GivenValidArguments_ReturnsTrue()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();
            string memberName = "Width";

            //Act
            bool added = graph.AddNode(building, memberName);

            //Assert
            Assert.IsTrue(added);
        }

        [TestMethod]
        public void AddNode1_GivenInvalidArguments1_ThrowsException()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = null;
            string memberName = "Width";

            //Act&Assert
            Assert.ThrowsException<ReactiveNodeException>(() => graph.AddNode(building, memberName));
        }

        [TestMethod]
        public void AddNode1_GivenInvalidArguments2_ThrowsException()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();
            string memberName = "Width_Inv";

            //Act&Assert
            Assert.ThrowsException<ReactiveNodeException>(() => graph.AddNode(building, memberName));
        }

        [TestMethod]
        public void AddNode1_GivenAlreadyAddedNode_ReturnsFalse()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();
            string memberName = "Width";
            INode node = new Node(building, memberName);
            graph.AddNode(node);

            //Act
            bool added = graph.AddNode(building, memberName);

            //Assert
            Assert.IsFalse(added);
        }

        [TestMethod]
        public void AddNode2_GivenInvalidArguments_ThrowsException()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = null;
            string memberName = "Area";
            string updateMethodName = "Update_Area";

            //Act&Assert
            Assert.ThrowsException<ReactiveNodeException>(() => graph.AddNode(building, memberName, updateMethodName));
        }

        [TestMethod]
        public void AddNode2_GivenInvalidArguments2_ThrowsException()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();
            string memberName = "Area_Inv";
            string updateMethodName = "Update_Area";

            //Act&Assert
            Assert.ThrowsException<ReactiveNodeException>(() => graph.AddNode(building, memberName, updateMethodName));
        }

        [TestMethod]
        public void ContainsNode_GivenAddedNode_ReturnsTrue()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();
            string memberName = "Width";
            INode node = new Node(building, memberName);
            graph.AddNode(node);

            //Act
            bool contains = graph.ContainsNode(node);

            //Assert
            Assert.IsTrue(contains);
        }

        [TestMethod]
        public void ContainsNode_GivenNotAddedNode_ReturnsFalse()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();
            string memberName = "Width";
            INode node = new Node(building, memberName);

            //Act
            bool contains = graph.ContainsNode(node);

            //Assert
            Assert.IsFalse(contains);
        }

        [TestMethod]
        public void ContainsNode1_GivenAddedNodeReturnsTrue()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();
            string memberName = "Width";
            INode node = new Node(building, memberName);
            graph.AddNode(node);

            //Act
            bool contains = graph.ContainsNode(building, memberName);

            //Assert
            Assert.IsTrue(contains);
        }

        [TestMethod]
        public void ContainsNode1_GivenNotAddedNode_ReturnsFalse()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();
            string memberName = "Width";
            INode node = new Node(building, memberName);

            //Act
            bool contains = graph.ContainsNode(building, memberName);

            //Assert
            Assert.IsFalse(contains);
        }

        [TestMethod]
        public void GetNode_GivenSuchNodeExists_ReturnsNode()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();
            string memberName = "Width";
            graph.AddNode(building, memberName);

            //Act
            INode node = graph.GetNode(building, memberName);

            //Assert
            Assert.IsNotNull(node);
        }

        [TestMethod]
        public void GetNode_GivenSuchNodeExists_ReturnsCorrectNode()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();
            string memberName = "Width";
            graph.AddNode(building, memberName);

            //Act
            INode node = graph.GetNode(building, memberName);

            //Assert
            Assert.IsTrue(node.OwnerObject == building && node.MemberName == memberName);
        }

        [TestMethod]
        public void GetNode_GivenSuchNodeDoesNotExist_ReturnsNull()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();
            string memberName = "Width";

            //Act
            INode node = graph.GetNode(building, memberName);

            //Assert
            Assert.IsNull(node);
        }

        [TestMethod]
        public void GetNode_GivenInvalidData_ReturnsNull()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = null;
            string memberName = "";

            //Act
            INode node = graph.GetNode(building, memberName);

            //Assert
            Assert.IsNull(node);
        }

        [TestMethod]
        public void GetNode1_GivenSuchNodeExists_ReturnsCorrectNode()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();
            string memberName = "Width";
            INode node = new Node(building, memberName);
            graph.AddNode(node);

            //Act
            INode n = graph.GetNode(node);

            //Assert
            Assert.AreEqual(n, node);
        }

        [TestMethod]
        public void GetNode1_GivenSuchNodeDoesNotExist_ReturnsNull()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();
            string memberName = "Width";
            INode node = new Node(building, memberName);

            //Act
            INode n = graph.GetNode(node);

            //Assert
            Assert.IsNull(n);
        }

        [TestMethod]
        public void GetNode1_GivenInvalidArguments_ThrowsException()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            INode node = null;

            //Act&Assert
            Assert.ThrowsException<NodeNullReferenceException>(()=> graph.GetNode(node));
        }

        [TestMethod]
        public void RemoveNode_GivenInvalidArguments_ThrowsException()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            INode node = null;

            //Act&Assert
            Assert.ThrowsException<NodeNullReferenceException>(()=> graph.RemoveNode(node));
        }

        [TestMethod]
        public void RemoveNode_GivenNotAddedNode_ReturnsFalse()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();
            string memberName = "Width";
            INode node = new Node(building, memberName);

            //Act
            bool removed = graph.RemoveNode(node);

            //Assert
            Assert.IsFalse(removed);
        }

        [TestMethod]
        public void RemoveNode_GivenNodeHasPredecessors_ThrowsException()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();
            string memberName = "Width";
            string memberName2 = "Area";
            INode predecessorNode = new Node(building, memberName);
            INode successorNode = new Node(building, memberName2);
            graph.AddNode(predecessorNode);
            graph.AddNode(successorNode);

            successorNode.AddPredecessor(predecessorNode);

            //Act&Assert
            Assert.ThrowsException<ReactiveNodeException>(()=> graph.RemoveNode(successorNode));
        }

        [TestMethod]
        public void RemoveNode_GivenNodeHasSuccessors_ThrowsException()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();
            string memberName = "Width";
            string memberName2 = "Area";
            INode predecessorNode = new Node(building, memberName);
            INode successorNode = new Node(building, memberName2);
            graph.AddNode(predecessorNode);
            graph.AddNode(successorNode);

            predecessorNode.AddSuccessor(successorNode);

            //Act&Assert
            Assert.ThrowsException<ReactiveNodeException>(() => graph.RemoveNode(predecessorNode));
        }

        [TestMethod]
        public void RemoveNode_GivenAddedNodeWithoutPredecessorsAndSuccessors_ReturnsTrue()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();
            string memberName = "Width";
            INode node = new Node(building, memberName);
            graph.AddNode(node);

            //Act
            bool removed = graph.RemoveNode(node);

            //Assert
            Assert.IsTrue(removed);
        }

        [TestMethod]
        public void AddDependency_GivenValidAddedNodes_DependencyAdded()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building00 = new Building00();
            string memberName = "Width";
            string memberName2 = "Area";

            INode predecessor = new Node(building00, memberName);
            INode successor = new Node(building00, memberName2);

            graph.AddNode(predecessor);
            graph.AddNode(successor);

            //Act
            graph.AddDependency(predecessor, successor);

            //Assert
            Assert.IsTrue(predecessor.HasSuccessor(successor) && successor.HasPredecessor(predecessor));

        }

        [TestMethod]
        public void AddDependency_GivenNullNodes_ThrowsException()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");

            INode predecessor = null;
            INode successor = null;

            //Act&Assert
            Assert.ThrowsException<NodeNullReferenceException>(()=> graph.AddDependency(predecessor, successor));
        }

        [TestMethod]
        public void AddDependency_GivenValidButNotAddedNodes_NodesAdded()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building00 = new Building00();
            string memberName = "Width";
            string memberName2 = "Area";

            INode predecessor = new Node(building00, memberName);
            INode successor = new Node(building00, memberName2);

            //Act
            graph.AddDependency(predecessor, successor);

            //Assert
            bool predecessorAdded = graph.GetNode(predecessor) != null;
            bool successorAdded = graph.GetNode(successor) != null;

            Assert.IsTrue(predecessorAdded == true && successorAdded==true);
        }

        [TestMethod]
        public void AddDependency_GivenValidButNotAddedNodes_DependencyAdded()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building00 = new Building00();
            string memberName = "Width";
            string memberName2 = "Area";

            INode predecessor = new Node(building00, memberName);
            INode successor = new Node(building00, memberName2);

            //Act
            graph.AddDependency(predecessor, successor);

            //Assert
            Assert.IsTrue(predecessor.HasSuccessor(successor) && successor.HasPredecessor(predecessor));
        }

        [TestMethod]
        public void RemoveDependency_GivenNullNodes_ThrowsException()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");

            INode predecessor = null;
            INode successor = null;

            //Act&Assert
            Assert.ThrowsException<NodeNullReferenceException>(() => graph.RemoveDependency(predecessor, successor));
        }

        [TestMethod]
        public void RemoveDependency_GivenValidButNotAddedNodes_ThrowsException()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building00 = new Building00();
            string memberName = "Width";
            string memberName2 = "Area";

            INode predecessor = new Node(building00, memberName);
            INode successor = new Node(building00, memberName2);

            //Act&Assert
            Assert.ThrowsException<ReactiveDependencyException>(() => graph.RemoveDependency(predecessor, successor));
        }

        [TestMethod]
        public void RemoveDependency_GivenValidAddedNodesButNoDependency_ReturnsFalse()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building00 = new Building00();
            string memberName = "Width";
            string memberName2 = "Area";

            INode predecessor = new Node(building00, memberName);
            INode successor = new Node(building00, memberName2);

            graph.AddNode(predecessor);
            graph.AddNode(successor);

            //Act
            bool removed = graph.RemoveDependency(predecessor, successor);

            //Assert
            Assert.IsFalse(removed);
        }

        [TestMethod]
        public void RemoveDependency_GivenValidAddedNodesWithDependency_ReturnsTrue()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building00 = new Building00();
            string memberName = "Width";
            string memberName2 = "Area";

            INode predecessor = new Node(building00, memberName);
            INode successor = new Node(building00, memberName2);

            graph.AddDependency(predecessor, successor);

            //Act
            bool removed = graph.RemoveDependency(predecessor, successor);

            //Assert
            Assert.IsTrue(removed);
        }

        [TestMethod]
        public void RemoveUnusedNodes_GivenEmptyGraph_ReturnsZero()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");

            //Act
            int numOfNodes = graph.RemoveUnusedNodes();

            //Assert
            Assert.AreEqual(numOfNodes, 0);
        }

        [TestMethod]
        public void RemoveUnusedNodes_GivenNoUnusedNodes_ReturnsZero()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building00 = new Building00();
            string memberName = "Width";
            string memberName2 = "Area";

            INode predecessor = new Node(building00, memberName);
            INode successor = new Node(building00, memberName2);
            graph.AddDependency(predecessor, successor);

            //Act
            int numOfNodes = graph.RemoveUnusedNodes();

            //Assert
            Assert.AreEqual(numOfNodes, 0);
        }

        [TestMethod]
        public void RemoveUnusedNodes_GivenOneUnusedNode_ReturnsOne()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building00 = new Building00();
            string memberName = "Width";
            string memberName2 = "Area";
            string memberName3 = "Length";

            INode predecessor = new Node(building00, memberName);
            INode successor = new Node(building00, memberName2);
            graph.AddDependency(predecessor, successor);
            graph.AddNode(building00, memberName3);

            //Act
            int numOfNodes = graph.RemoveUnusedNodes();

            //Assert
            Assert.AreEqual(numOfNodes, 1);
        }

        [TestMethod]
        public void RemoveUnusedNodes_GivenThreeUnusedNode_ReturnsThree()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building00 = new Building00();
            string memberName = "Width";
            string memberName2 = "Area";
            string memberName3 = "Length";

            INode predecessor = new Node(building00, memberName);
            INode successor = new Node(building00, memberName2);
            graph.AddNode(predecessor);
            graph.AddNode(successor);
            graph.AddNode(building00, memberName3);

            //Act
            int numOfNodes = graph.RemoveUnusedNodes();

            //Assert
            Assert.AreEqual(numOfNodes, 3);
        }

        [TestMethod]
        public void PerformUpdate_GivenNotInitializedGraph_ThrowsException()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();

            INode widthNode = new Node(building, "Width");
            INode lengthNode = new Node(building, "Length");
            INode areaNode = new Node(building, "Area", "Update_Area");
            INode heightNode = new Node(building, "Height");
            INode volumeNode = new Node(building, "Volume");

            graph.AddDependency(widthNode, areaNode);
            graph.AddDependency(lengthNode, areaNode);
            graph.AddDependency(areaNode, volumeNode);
            graph.AddDependency(heightNode, volumeNode);

            INode initialNode = widthNode;

            //Act&Assert
            Assert.ThrowsException<DependencyGraphException>(()=> graph.PerformUpdate(initialNode));
        }

        [TestMethod]
        public void PerformUpdate_GivenInitialNodeIsNull_ThrowsException()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();

            INode widthNode = new Node(building, "Width");
            INode lengthNode = new Node(building, "Length");
            INode areaNode = new Node(building, "Area", "Update_Area");
            INode heightNode = new Node(building, "Height");
            INode volumeNode = new Node(building, "Volume");

            graph.AddDependency(widthNode, areaNode);
            graph.AddDependency(lengthNode, areaNode);
            graph.AddDependency(areaNode, volumeNode);
            graph.AddDependency(heightNode, volumeNode);

            graph.Initialize();

            INode initialNode = null;

            //Act&Assert
            Assert.ThrowsException<NodeNullReferenceException>(() => graph.PerformUpdate(initialNode));
        }

        [TestMethod]
        public void PerformUpdate_GivenNonExistingInitialNode_ThrowsException()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();

            INode widthNode = new Node(building, "Width");
            INode lengthNode = new Node(building, "Length");
            INode areaNode = new Node(building, "Area", "Update_Area");
            INode heightNode = new Node(building, "Height");
            INode volumeNode = new Node(building, "Volume");

            INode consumption = new Node(building, "Consumption");

            graph.AddDependency(widthNode, areaNode);
            graph.AddDependency(lengthNode, areaNode);
            graph.AddDependency(areaNode, volumeNode);
            graph.AddDependency(heightNode, volumeNode);

            graph.Initialize();

            INode initialNode = consumption;

            //Act&Assert
            Assert.ThrowsException<NodeNullReferenceException>(() => graph.PerformUpdate(initialNode));
        }

        #region PerformUpdate CASE 1

        /*
         * Simple dependency graph with 8 nodes and 8 reactive dependencies. All nodes and dependencies are from the same object.
         * Dependencies are arrange so that there would be a glitch if there was no topological order.
         */

        [TestMethod]
        public void PerformUpdate_Case1_GivenWidthAsInitialNode_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();

            CreateCase1(graph, building);
            INode initialNode = graph.GetNode(building, "Width");

            //Act
            graph.PerformUpdate(initialNode);

            //Assert
            Logger actualLogger = graph.Logger;
            Logger expectedLogger = CreateExpectedLogger_Case1_GivenWidthOrLengthAsInitialNode(graph, building);

            Assert.AreEqual(expectedLogger.GetNodesToUpdate(), actualLogger.GetNodesToUpdate());
        }

        [TestMethod]
        public void PerformUpdate_Case1_GivenLengthAsInitialNode_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();

            CreateCase1(graph, building);
            INode initialNode = graph.GetNode(building, "Length");

            //Act
            graph.PerformUpdate(initialNode);

            //Assert
            Logger actualLogger = graph.Logger;
            Logger expectedLogger = CreateExpectedLogger_Case1_GivenWidthOrLengthAsInitialNode(graph, building);

            Assert.AreEqual(expectedLogger.GetNodesToUpdate(), actualLogger.GetNodesToUpdate());
        }

        [TestMethod]
        public void PerformUpdate_Case1_GivenHeightAsInitialNode_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();

            CreateCase1(graph, building);
            INode initialNode = graph.GetNode(building, "Height");

            //Act
            graph.PerformUpdate(initialNode);

            //Assert
            Logger actualLogger = graph.Logger;
            Logger expectedLogger = CreateExpectedLogger_Case1_GivenHeightAsInitialNode(graph, building);

            Assert.AreEqual(expectedLogger.GetNodesToUpdate(), actualLogger.GetNodesToUpdate());
        }

        [TestMethod]
        public void PerformUpdate_Case1_GivenConsumptionAsInitialNode_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();

            CreateCase1(graph, building);
            INode initialNode = graph.GetNode(building, "Consumption");

            //Act
            graph.PerformUpdate(initialNode);

            //Assert
            Logger actualLogger = graph.Logger;
            Logger expectedLogger = CreateExpectedLogger_Case1_GivenConsumptionAsInitialNode(graph, building);

            Assert.AreEqual(expectedLogger.GetNodesToUpdate(), actualLogger.GetNodesToUpdate());
        }

        [TestMethod]
        public void PerformUpdate_Case1_GivenAreaAsInitialNode_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();

            CreateCase1(graph, building);
            INode initialNode = graph.GetNode(building, "Area");

            //Act
            graph.PerformUpdate(initialNode);

            //Assert
            Logger actualLogger = graph.Logger;
            Logger expectedLogger = CreateExpectedLogger_Case1_AreaAsInitialNode(graph, building);

            Assert.AreEqual(expectedLogger.GetNodesToUpdate(), actualLogger.GetNodesToUpdate());
        }

        [TestMethod]
        public void PerformUpdate_Case1_GivenTotalConsumptionPer_m3AsInitialNode_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();

            CreateCase1(graph, building);
            INode initialNode = graph.GetNode(building, "TotalConsumptionPer_m3");

            //Act
            graph.PerformUpdate(initialNode);

            //Assert
            Logger actualLogger = graph.Logger;

            Assert.AreEqual("", actualLogger.GetNodesToUpdate());
        }

        [TestMethod]
        public void PerformUpdate_Case1_GivenWidthAsInitialNode_GivesCorrectResults()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();

            CreateCase1(graph, building);
            INode initialNode = graph.GetNode(building, "Width");

            //Act
            graph.PerformUpdate(initialNode);

            //Assert
            Assert.IsTrue(building.Area == 90 && building.Volume == 360 && building.TotalConsumption == 1800 && building.TotalConsumptionPer_m3 == 5);
        }

        [TestMethod]
        public void PerformUpdate_Case1_ChangingLength_GivesCorrectResults()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();

            CreateCase1(graph, building);
            INode initialNode = graph.GetNode(building, "Width");
            graph.PerformUpdate(initialNode);

            building.Length = 10;
            initialNode = graph.GetNode(building, "Length");

            //Act
            graph.PerformUpdate(initialNode);

            //Assert
            Assert.IsTrue(building.Area == 100 && building.Volume == 400 && building.TotalConsumption == 2000 && building.TotalConsumptionPer_m3 == 5);
        }

        private void CreateCase1(DependencyGraph graph, Building00 building)
        {
            building.Width = 10;
            building.Length = 9;
            building.Height = 4;
            building.Consumption = 20;

            INode widthNode = new Node(building, "Width");
            INode lengthNode = new Node(building, "Length");
            INode areaNode = new Node(building, "Area", "Update_Area");
            INode heightNode = new Node(building, "Height");
            INode volumeNode = new Node(building, "Volume", "Update_Volume");
            INode consumptionNode = new Node(building, "Consumption");
            INode totalConsumptionNode = new Node(building, "TotalConsumption", "Update_TotalConsumption");
            INode totalConsumptionPer_m3 = new Node(building, "TotalConsumptionPer_m3", "Update_TotalConsumptionPer_m3");

            graph.AddDependency(widthNode, areaNode);
            graph.AddDependency(lengthNode, areaNode);
            graph.AddDependency(areaNode, volumeNode);
            graph.AddDependency(areaNode, totalConsumptionNode);
            graph.AddDependency(heightNode, volumeNode);
            graph.AddDependency(consumptionNode, totalConsumptionNode);
            graph.AddDependency(totalConsumptionNode, totalConsumptionPer_m3);
            graph.AddDependency(volumeNode, totalConsumptionPer_m3);

            graph.Initialize();
        }

        private Logger CreateExpectedLogger_Case1_GivenWidthOrLengthAsInitialNode(DependencyGraph graph, Building00 building00)
        {
            Logger logger = new Logger();

            logger.LogNodeToUpdate(graph.GetNode(building00, "Area"));
            logger.LogNodeToUpdate(graph.GetNode(building00, "TotalConsumption"));
            logger.LogNodeToUpdate(graph.GetNode(building00, "Volume"));
            logger.LogNodeToUpdate(graph.GetNode(building00, "TotalConsumptionPer_m3"));

            return logger;
        }

        private Logger CreateExpectedLogger_Case1_GivenHeightAsInitialNode(DependencyGraph graph, Building00 building)
        {
            Logger logger = new Logger();

            logger.LogNodeToUpdate(graph.GetNode(building, "Volume"));
            logger.LogNodeToUpdate(graph.GetNode(building, "TotalConsumptionPer_m3"));

            return logger;
        }

        private Logger CreateExpectedLogger_Case1_GivenConsumptionAsInitialNode(DependencyGraph graph, Building00 building)
        {
            Logger logger = new Logger();

            logger.LogNodeToUpdate(graph.GetNode(building, "TotalConsumption"));
            logger.LogNodeToUpdate(graph.GetNode(building, "TotalConsumptionPer_m3"));

            return logger;
        }

        private Logger CreateExpectedLogger_Case1_AreaAsInitialNode(DependencyGraph graph, Building00 building00)
        {
            Logger logger = new Logger();

            logger.LogNodeToUpdate(graph.GetNode(building00, "TotalConsumption"));
            logger.LogNodeToUpdate(graph.GetNode(building00, "Volume"));
            logger.LogNodeToUpdate(graph.GetNode(building00, "TotalConsumptionPer_m3"));

            return logger;
        }

        #endregion
    }
}
