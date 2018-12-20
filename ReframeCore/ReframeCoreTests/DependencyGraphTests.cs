using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeCore;
using ReframeCore.Exceptions;
using ReframeCoreExamples.E00;
using ReframeCore.Helpers;
using ReframeCoreExamples.E01;

namespace ReframeCoreTests
{
    [TestClass]
    public class DependencyGraphTests
    {
        #region AddNode

        [TestMethod]
        public void AddNode_GivenNullObject_ThrowsException()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            INode node = null;

            //Act&Assert
            Assert.ThrowsException<NodeNullReferenceException>(() => graph.AddNode(node));
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

        #endregion

        #region ContainsNode

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

        #endregion

        #region GetNode

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
            Assert.ThrowsException<NodeNullReferenceException>(() => graph.GetNode(node));
        }

        #endregion

        #region RemoveNode

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

        #endregion

        #region AddDependency

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
        public void AddDependency_GivenNodesFromDifferentObjects_DependencyAdded()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Apartment01 apartment01 = new Apartment01 { Name = "AP-01" };

            AdditionalPart01 basement = new AdditionalPart01 { Name = "Basement" };
            apartment01.Basement = basement;

            INode apartmentTotalArea = new Node(apartment01, "TotalArea");
            INode basementArea = new Node(basement, "Area");

            //Act
            graph.AddDependency(basementArea, apartmentTotalArea);

            //Assert
            Assert.IsTrue(apartmentTotalArea.HasPredecessor(basementArea) && basementArea.HasSuccessor(apartmentTotalArea));
        }

        #endregion

        #region RemoveDependency

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
            Apartment01 apartment01 = new Apartment01 { Name = "AP-01" };

            AdditionalPart01 basement = new AdditionalPart01 { Name = "Basement" };
            apartment01.Basement = basement;

            INode apartmentTotalArea = new Node(apartment01, "TotalArea");
            INode basementArea = new Node(basement, "Area");

            graph.AddDependency(basementArea, apartmentTotalArea);

            //Act
            bool removed = graph.RemoveDependency(basementArea, apartmentTotalArea);

            //Assert
            Assert.IsTrue(removed);
        }

        [TestMethod]
        public void RemoveDependency_GivenNodesFromDifferentObjectsWithDependency_ReturnsTrue()
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

        #endregion

        #region RemoveUnusedNodes

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

        #endregion

        #region PerformUpdate GENERAL

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

        #endregion

        #region PerformUpdate CASE 1

        /*
         * Simple dependency graph with 8 nodes and 8 reactive dependencies. All nodes and dependencies are from the same object.
         * Dependencies are arranged so that there would be a glitch if there was no topological order.
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

        [TestMethod]
        public void PerformUpdate_Case1_GivenNoInitialNode_SchedulesCorrectUpdateOrderOfAllNodes()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();

            CreateCase1(graph, building);

            //Act
            graph.PerformUpdate();

            //Assert
            Logger actualLogger = graph.Logger;
            Logger expectedLogger = CreateExpectedLogger_Case1_GivenNoInitialNode(graph, building);

            Assert.AreEqual(expectedLogger.GetNodesToUpdate(), actualLogger.GetNodesToUpdate());
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

        private Logger CreateExpectedLogger_Case1_GivenNoInitialNode(DependencyGraph graph, Building00 building)
        {
            Logger logger = new Logger();

            logger.LogNodeToUpdate(graph.GetNode(building, "Consumption"));
            logger.LogNodeToUpdate(graph.GetNode(building, "Height"));
            logger.LogNodeToUpdate(graph.GetNode(building, "Length"));
            logger.LogNodeToUpdate(graph.GetNode(building, "Width"));
            logger.LogNodeToUpdate(graph.GetNode(building, "Area"));
            logger.LogNodeToUpdate(graph.GetNode(building, "TotalConsumption"));
            logger.LogNodeToUpdate(graph.GetNode(building, "Volume"));
            logger.LogNodeToUpdate(graph.GetNode(building, "TotalConsumptionPer_m3"));

            return logger;
        }

        #endregion

        #region PerformUpdate CASE2

        /*
         * Simple dependency graph with nodes from three objects of two different classes.
         */

        private void CreateCase2(DependencyGraph graph, Apartment01 apartment)
        {
            apartment.Basement = new AdditionalPart01 { Name = "Basement"};
            apartment.Balcony = new AdditionalPart01 { Name = "Balcony" };

            apartment.Width = 10;
            apartment.Length = 7;
            apartment.Height = 2.5;
            apartment.Consumption = 6;

            apartment.Basement.Width = 4;
            apartment.Basement.Length = 2;
            apartment.Basement.UtilCoeff = 0.6;

            apartment.Balcony.Width = 1;
            apartment.Balcony.Length = 3;
            apartment.Balcony.UtilCoeff = 0.5;

            INode balconyWidthNode = new Node(apartment.Balcony, "Width");
            INode balconyLengthNode = new Node(apartment.Balcony, "Length");
            INode balconyAreaNode = new Node(apartment.Balcony, "Area", "Update_Area");

            graph.AddDependency(balconyWidthNode, balconyAreaNode);
            graph.AddDependency(balconyLengthNode, balconyAreaNode);

            INode basementWidthNode = new Node(apartment.Basement, "Width");
            INode basementLengthNode = new Node(apartment.Basement, "Length");
            INode basementAreaNode = new Node(apartment.Basement, "Area", "Update_Area");

            graph.AddDependency(basementWidthNode, basementAreaNode);
            graph.AddDependency(basementLengthNode, basementAreaNode);

            INode widthNode = new Node(apartment, "Width");
            INode lengthNode = new Node(apartment, "Length");
            INode heatedAreaNode = new Node(apartment, "HeatedArea", "Update_HeatedArea");

            graph.AddDependency(widthNode, heatedAreaNode);
            graph.AddDependency(lengthNode, heatedAreaNode);

            INode heightNode = new Node(apartment, "Height");
            INode heatedVolumeNode = new Node(apartment, "HeatedVolume", "Update_HeatedVolume");

            graph.AddDependency(heightNode, heatedVolumeNode);
            graph.AddDependency(heatedAreaNode, heatedVolumeNode);

            INode consumptionNode = new Node(apartment, "Consumption");
            INode totalConsumptionNode = new Node(apartment, "TotalConsumption", "Update_TotalConsumption");

            graph.AddDependency(consumptionNode, totalConsumptionNode);
            graph.AddDependency(heatedVolumeNode, totalConsumptionNode);

            INode totalAreaNode = new Node(apartment, "TotalArea", "Update_TotalArea");
            INode balconyUtilCoeffNode = new Node(apartment.Balcony, "UtilCoeff");
            INode basementUtilCoeffNode = new Node(apartment.Basement, "UtilCoeff");

            graph.AddDependency(heatedAreaNode, totalAreaNode);
            graph.AddDependency(balconyAreaNode, totalAreaNode);
            graph.AddDependency(basementAreaNode, totalAreaNode);
            graph.AddDependency(balconyUtilCoeffNode, totalAreaNode);
            graph.AddDependency(basementUtilCoeffNode, totalAreaNode);

            graph.Initialize();
        }

        private Logger CreateExpectedLogger_Case2_GivenNoInitialNode(DependencyGraph graph, Apartment01 apartment)
        {
            Logger logger = new Logger();

            logger.LogNodeToUpdate(graph.GetNode(apartment.Basement, "UtilCoeff"));
            logger.LogNodeToUpdate(graph.GetNode(apartment.Balcony, "UtilCoeff"));
            logger.LogNodeToUpdate(graph.GetNode(apartment, "Consumption"));
            logger.LogNodeToUpdate(graph.GetNode(apartment, "Height"));
            logger.LogNodeToUpdate(graph.GetNode(apartment, "Length"));
            logger.LogNodeToUpdate(graph.GetNode(apartment, "Width"));
            logger.LogNodeToUpdate(graph.GetNode(apartment, "HeatedArea"));
            logger.LogNodeToUpdate(graph.GetNode(apartment, "HeatedVolume"));
            logger.LogNodeToUpdate(graph.GetNode(apartment, "TotalConsumption"));
            logger.LogNodeToUpdate(graph.GetNode(apartment.Basement, "Length"));
            logger.LogNodeToUpdate(graph.GetNode(apartment.Basement, "Width"));
            logger.LogNodeToUpdate(graph.GetNode(apartment.Basement, "Area"));
            logger.LogNodeToUpdate(graph.GetNode(apartment.Balcony, "Length"));
            logger.LogNodeToUpdate(graph.GetNode(apartment.Balcony, "Width"));
            logger.LogNodeToUpdate(graph.GetNode(apartment.Balcony, "Area"));
            logger.LogNodeToUpdate(graph.GetNode(apartment, "TotalArea"));

            return logger;
        }

        private Logger CreateExpectedLogger_Case2_GivenWidthAsInitialNode(DependencyGraph graph, Apartment01 apartment)
        {
            Logger logger = new Logger();

            logger.LogNodeToUpdate(graph.GetNode(apartment, "HeatedArea"));
            logger.LogNodeToUpdate(graph.GetNode(apartment, "TotalArea"));
            logger.LogNodeToUpdate(graph.GetNode(apartment, "HeatedVolume"));
            logger.LogNodeToUpdate(graph.GetNode(apartment, "TotalConsumption"));

            return logger;
        }

        private Logger CreateExpectedLogger_Case2_GivenBalconyWidthAsInitialNode(DependencyGraph graph, Apartment01 apartment)
        {
            Logger logger = new Logger();

            logger.LogNodeToUpdate(graph.GetNode(apartment.Balcony, "Area"));
            logger.LogNodeToUpdate(graph.GetNode(apartment, "TotalArea"));

            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case2_GivenNoInitialNode_SchedulesCorrectUpdateOrderOfAllNodes()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Apartment01 apartment = new Apartment01 { Name = "Apartment 01"};

            CreateCase2(graph, apartment);

            //Act
            graph.PerformUpdate();

            //Assert
            Logger actualLogger = graph.Logger;
            Logger expectedLogger = CreateExpectedLogger_Case2_GivenNoInitialNode(graph, apartment);

            Assert.AreEqual(actualLogger.GetNodesToUpdate(), expectedLogger.GetNodesToUpdate());
        }

        [TestMethod]
        public void PerformUpdate_Case2_GivenNoInitialNode_GivesCorrectResults()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Apartment01 apartment = new Apartment01 { Name = "Apartment 01" };

            CreateCase2(graph, apartment);

            //Act
            graph.PerformUpdate();

            //Assert
            Assert.IsTrue(
                apartment.Basement.Area == 8 &&
                apartment.Balcony.Area == 3 &&
                apartment.HeatedArea == 70 &&
                apartment.TotalArea == 76.3 &&
                apartment.HeatedVolume == 175 &&
                apartment.TotalConsumption == 1050
                );
        }

        [TestMethod]
        public void PerformUpdate_Case2_GivenApartmentWidthAsInitialNode_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Apartment01 apartment = new Apartment01 { Name = "Apartment 01" };

            CreateCase2(graph, apartment);
            INode initialNode = graph.GetNode(apartment, "Width");

            //Act
            graph.PerformUpdate(initialNode);

            //Assert
            Logger actualLogger = graph.Logger;
            Logger expectedLogger = CreateExpectedLogger_Case2_GivenWidthAsInitialNode(graph, apartment);

            Assert.AreEqual(actualLogger.GetNodesToUpdate(), expectedLogger.GetNodesToUpdate());
        }

        [TestMethod]
        public void PerformUpdate_Case2_GivenBalconyWidthAsInitialNode_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Apartment01 apartment = new Apartment01 { Name = "Apartment 01" };

            CreateCase2(graph, apartment);
            INode initialNode = graph.GetNode(apartment.Balcony, "Width");

            //Act
            graph.PerformUpdate(initialNode);

            //Assert
            Logger actualLogger = graph.Logger;
            Logger expectedLogger = CreateExpectedLogger_Case2_GivenBalconyWidthAsInitialNode(graph, apartment);

            Assert.AreEqual(actualLogger.GetNodesToUpdate(), expectedLogger.GetNodesToUpdate());
        }

        [TestMethod]
        public void PerformUpdate_Case2_ChangingApartmentLength_GivesCorrectResults()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Apartment01 apartment = new Apartment01 { Name = "Apartment 01" };

            CreateCase2(graph, apartment);
            graph.PerformUpdate();

            //Act
            INode initialNode = graph.GetNode(apartment, "Length");
            apartment.Length = 8;
            graph.PerformUpdate(initialNode);

            //Assert
            //Assert
            Assert.IsTrue(
                apartment.Basement.Area == 8 &&
                apartment.Balcony.Area == 3 &&
                apartment.HeatedArea == 80 &&
                apartment.TotalArea == 86.3 &&
                apartment.HeatedVolume == 200 &&
                apartment.TotalConsumption == 1200
                );
        }

        #endregion
    }
}
