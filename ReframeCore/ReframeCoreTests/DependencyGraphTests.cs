using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeCore;
using ReframeCore.Exceptions;
using ReframeCoreExamples.E00;
using ReframeCore.Helpers;
using ReframeCoreExamples.E01;
using ReframeCore.Nodes;

namespace ReframeCoreTests
{
    [TestClass]
    public partial class DependencyGraphTests
    {
        #region AddNode

        #region public bool AddNode(INode node)

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
        public void AddNode_GivenValidNonExistingPropertyNode_ReturnsAddedNode()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();
            string memberName = "Width";
            INode node = new PropertyNode(building, memberName);

            //Act
            INode addedNode = graph.AddNode(node);

            //Assert
            Assert.IsNotNull(addedNode);
        }

        [TestMethod]
        public void AddNode_GivenValidNonExistingMethodNode_ReturnsAddedNode()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();
            string memberName = "Update_Area";
            INode node = new MethodNode(building, memberName);

            //Act
            INode addedNode = graph.AddNode(node);

            //Assert
            Assert.IsNotNull(addedNode);
        }

        [TestMethod]
        public void AddNode_GivenAlreadyAddedPropertyNode_ReturnsNull()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();
            string memberName = "Width";
            INode node = new PropertyNode(building, memberName);
            graph.AddNode(node);

            //Act
            INode addedNode = graph.AddNode(node);

            //Assert
            Assert.IsNull(addedNode);
        }

        [TestMethod]
        public void AddNode_GivenAlreadyAddedMethodNode_ReturnsNull()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();
            string memberName = "Update_Area";
            INode node = new MethodNode(building, memberName);
            graph.AddNode(node);

            //Act
            INode addedNode = graph.AddNode(node);

            //Assert
            Assert.IsNull(addedNode);
        }

        #endregion

        #region public INode AddNode(object ownerObject, string memberName)

        [TestMethod]
        public void AddNode1_GivenValidArguments_ReturnsPropertyNode()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();
            string memberName = "Width";

            //Act
            INode addedNode = graph.AddNode(building, memberName);

            //Assert
            Assert.IsInstanceOfType(addedNode, typeof(PropertyNode));
        }

        [TestMethod]
        public void AddNode1_GivenValidArguments_ReturnsCorrectPropertyNode()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();
            string memberName = "Width";

            //Act
            PropertyNode addedNode = graph.AddNode(building, memberName) as PropertyNode;

            //Assert
            Assert.IsTrue(addedNode.OwnerObject == building && addedNode.MemberName == memberName);
        }

        [TestMethod]
        public void AddNode1_GivenValidArguments_ReturnsPropertyNodeWithoutUpdateMethod()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();
            string memberName = "Width";

            //Act
            PropertyNode addedNode = graph.AddNode(building, memberName) as PropertyNode;

            //Assert
            Assert.IsTrue(addedNode.UpdateMethod == null);
        }

        [TestMethod]
        public void AddNode1_GivenValidArgumentsAndDefaultUpdateMethodNames_ReturnsPropertyNodeWithUpdateMethod()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            graph.Settings.UseDefaultUpdateMethodNames = true;

            Building00 building = new Building00();
            string memberName = "Area";

            //Act
            PropertyNode addedNode = graph.AddNode(building, memberName) as PropertyNode;

            //Assert
            Assert.IsTrue(addedNode.UpdateMethod != null
                && addedNode.UpdateMethod.Method.Name == graph.Settings.UpdateMethodNamePrefix + memberName);
        }

        [TestMethod]
        public void AddNode1_GivenValidArgumentsAndNoDefaultUpdateMethodNames_ReturnsPropertyNodeWithoutUpdateMethod()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            graph.Settings.UseDefaultUpdateMethodNames = false;

            Building00 building = new Building00();
            string memberName = "Area";

            //Act
            PropertyNode addedNode = graph.AddNode(building, memberName) as PropertyNode;

            //Assert
            //Assert
            Assert.IsTrue(addedNode.OwnerObject == building && addedNode.MemberName == memberName && addedNode.UpdateMethod == null);
        }

        [TestMethod]
        public void AddNode1_GivenInvalidOwnerObject_ThrowsException()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = null;
            string memberName = "Width";

            //Act&Assert
            Assert.ThrowsException<ReactiveNodeException>(() => graph.AddNode(building, memberName));
        }

        [TestMethod]
        public void AddNode1_GivenInvalidMemberName_ThrowsException()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();
            string memberName = "Width_Inv";

            //Act&Assert
            Assert.ThrowsException<ReactiveNodeException>(() => graph.AddNode(building, memberName));
        }

        [TestMethod]
        public void AddNode1_GivenValidArguments_ReturnsMethodNode()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();
            string memberName = "Update_Area";

            //Act
            INode addedNode = graph.AddNode(building, memberName);

            //Assert
            Assert.IsInstanceOfType(addedNode, typeof(MethodNode));
        }

        #endregion

        #region public INode AddNode(object ownerObject, string memberName, string updateMethodName)

        [TestMethod]
        public void AddNode2_GivenValidArguments_ReturnsPropertyNode()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();
            string memberName = "Area";
            string updateMethodName = "Update_Area";

            //Act
            INode addedNode = graph.AddNode(building, memberName, updateMethodName);

            //Assert
            Assert.IsInstanceOfType(addedNode, typeof(PropertyNode));
        }

        [TestMethod]
        public void AddNode2_GivenValidArguments_ReturnsMethodNode()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();
            string memberName = "Update_Area";
            string updateMethodName = "Update_Area";

            //Act
            INode addedNode = graph.AddNode(building, memberName, updateMethodName);

            //Assert
            Assert.IsInstanceOfType(addedNode, typeof(MethodNode));
        }

        [TestMethod]
        public void AddNode2_GivenValidArguments_ReturnsCorrectPropertyNode()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();
            string memberName = "Area";
            string updateMethodName = "Update_Area";

            //Act
            PropertyNode addedNode = graph.AddNode(building, memberName, updateMethodName) as PropertyNode;

            //Assert
            Assert.IsTrue(addedNode.OwnerObject == building 
                && addedNode.MemberName == memberName
                && addedNode.UpdateMethod != null
                && addedNode.UpdateMethod.Method.Name == updateMethodName);
        }

        [TestMethod]
        public void AddNode2_GivenValidArguments_ReturnsCorrectMethodNode()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();
            string memberName = "Update_Area";
            string updateMethodName = "Update_Area";

            //Act
            MethodNode addedNode = graph.AddNode(building, memberName, updateMethodName) as MethodNode;

            //Assert
            Assert.IsTrue(addedNode.OwnerObject == building
                && addedNode.MemberName == memberName
                && addedNode.UpdateMethod != null
                && addedNode.UpdateMethod.Method.Name == updateMethodName);
        }

        [TestMethod]
        public void AddNode2_GivenNonExistingUpdateMethodName_ReturnsPropertyNodeWithoutUpdateMethod()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();
            string memberName = "Width";
            string updateMethodName = "Update_Width";

            //Act
            PropertyNode addedNode = graph.AddNode(building, memberName, updateMethodName) as PropertyNode;

            //Assert
            Assert.IsTrue(addedNode.UpdateMethod == null);
        }

        [TestMethod]
        public void AddNode2_GivenInvalidOwnerObject_ThrowsException()
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
        public void AddNode2_GivenInvalidMemberName_ThrowsException()
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
        public void AddNode2_GivenAlreadyAddedNode_ReturnsExistingNode()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();
            string memberName = "Area";
            string updateMethodName = "Update_Area";

            //Act
            INode addedNode = graph.AddNode(building, memberName, updateMethodName);
            INode additionalNode = graph.AddNode(building, memberName, updateMethodName);

            //Assert
            Assert.AreEqual(addedNode, additionalNode);
        }

        #endregion

        #endregion

        #region ContainsNode

        [TestMethod]
        public void ContainsNode_GivenAddedPropertyNode_ReturnsTrue()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();
            string memberName = "Width";
            INode node = new PropertyNode(building, memberName);
            graph.AddNode(node);

            //Act
            bool contains = graph.ContainsNode(node);

            //Assert
            Assert.IsTrue(contains);
        }

        [TestMethod]
        public void ContainsNode_GivenAddedMethodNode_ReturnsTrue()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();
            string memberName = "Update_Area";
            INode node = new MethodNode(building, memberName);
            graph.AddNode(node);

            //Act
            bool contains = graph.ContainsNode(node);

            //Assert
            Assert.IsTrue(contains);
        }

        [TestMethod]
        public void ContainsNode_GivenNotAddedPropertyNode_ReturnsFalse()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();
            string memberName = "Width";
            INode node = new PropertyNode(building, memberName);

            //Act
            bool contains = graph.ContainsNode(node);

            //Assert
            Assert.IsFalse(contains);
        }

        [TestMethod]
        public void ContainsNode_GivenNotAddedMethodNode_ReturnsFalse()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();
            string memberName = "Update_Area";
            INode node = new MethodNode(building, memberName);

            //Act
            bool contains = graph.ContainsNode(node);

            //Assert
            Assert.IsFalse(contains);
        }

        [TestMethod]
        public void ContainsNode1_GivenAddedPropertyNodeReturnsTrue()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();
            string memberName = "Width";
            INode node = new PropertyNode(building, memberName);
            graph.AddNode(node);

            //Act
            bool contains = graph.ContainsNode(building, memberName);

            //Assert
            Assert.IsTrue(contains);
        }

        [TestMethod]
        public void ContainsNode1_GivenAddedMethodNodeReturnsTrue()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();
            string memberName = "Update_Area";
            INode node = new MethodNode(building, memberName);
            graph.AddNode(node);

            //Act
            bool contains = graph.ContainsNode(building, memberName);

            //Assert
            Assert.IsTrue(contains);
        }

        [TestMethod]
        public void ContainsNode1_GivenNotAddedPropertyNode_ReturnsFalse()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();
            string memberName = "Width";
            INode node = new PropertyNode(building, memberName);

            //Act
            bool contains = graph.ContainsNode(building, memberName);

            //Assert
            Assert.IsFalse(contains);
        }

        [TestMethod]
        public void ContainsNode1_GivenNotAddedMethodNode_ReturnsFalse()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();
            string memberName = "Width";
            INode node = new MethodNode(building, memberName);

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
            INode node = new PropertyNode(building, memberName);
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
            INode node = new PropertyNode(building, memberName);

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
            INode node = new PropertyNode(building, memberName);

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
            INode predecessorNode = new PropertyNode(building, memberName);
            INode successorNode = new PropertyNode(building, memberName2);
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
            INode predecessorNode = new PropertyNode(building, memberName);
            INode successorNode = new PropertyNode(building, memberName2);
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
            INode node = new PropertyNode(building, memberName);
            graph.AddNode(node);

            //Act
            bool removed = graph.RemoveNode(node);

            //Assert
            Assert.IsTrue(removed);
        }

        #endregion

        #region AddDependency

        #region public void AddDependency(INode predecessor, INode successor)

        [TestMethod]
        public void AddDependency_GivenNullNodes_ThrowsException()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");

            INode predecessor = null;
            INode successor = null;

            //Act&Assert
            Assert.ThrowsException<NodeNullReferenceException>(() => graph.AddDependency(predecessor, successor));
        }

        [TestMethod]
        public void AddDependency_GivenValidButNotAddedNodes_NodesAdded()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building00 = new Building00();
            string memberName = "Width";
            string memberName2 = "Area";

            INode predecessor = new PropertyNode(building00, memberName);
            INode successor = new PropertyNode(building00, memberName2);

            //Act
            graph.AddDependency(predecessor, successor);

            //Assert
            bool predecessorAdded = graph.GetNode(predecessor) != null;
            bool successorAdded = graph.GetNode(successor) != null;

            Assert.IsTrue(predecessorAdded == true && successorAdded == true);
        }

        [TestMethod]
        public void AddDependency_GivenValidButNotAddedNodes_DependencyAdded()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building00 = new Building00();
            string memberName = "Width";
            string memberName2 = "Area";

            INode predecessor = new PropertyNode(building00, memberName);
            INode successor = new PropertyNode(building00, memberName2);

            //Act
            graph.AddDependency(predecessor, successor);

            //Assert
            Assert.IsTrue(predecessor.HasSuccessor(successor) && successor.HasPredecessor(predecessor));
        }

        [TestMethod]
        public void AddDependency_GivenValidAddedPropertyNodes_DependencyAdded()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building00 = new Building00();
            string memberName = "Width";
            string memberName2 = "Area";

            INode predecessor = new PropertyNode(building00, memberName);
            INode successor = new PropertyNode(building00, memberName2);

            graph.AddNode(predecessor);
            graph.AddNode(successor);

            //Act
            graph.AddDependency(predecessor, successor);

            //Assert
            Assert.IsTrue(predecessor.HasSuccessor(successor) && successor.HasPredecessor(predecessor));
        }

        [TestMethod]
        public void AddDependency_GivenValidAddedMethodNodes_DependencyAdded()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building00 = new Building00();
            string memberName = "Update_Area";
            string memberName2 = "Update_Volume";

            INode predecessor = new MethodNode(building00, memberName);
            INode successor = new MethodNode(building00, memberName2);

            graph.AddNode(predecessor);
            graph.AddNode(successor);

            //Act
            graph.AddDependency(predecessor, successor);

            //Assert
            Assert.IsTrue(predecessor.HasSuccessor(successor) && successor.HasPredecessor(predecessor));
        }

        [TestMethod]
        public void AddDependency_GivenValidAddedPropertyAndMethodNodes_DependencyAdded()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building00 = new Building00();
            string memberName = "Area";
            string memberName2 = "Update_Volume";

            INode predecessor = new PropertyNode(building00, memberName);
            INode successor = new MethodNode(building00, memberName2);

            graph.AddNode(predecessor);
            graph.AddNode(successor);

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

            INode apartmentTotalArea = new PropertyNode(apartment01, "TotalArea");
            INode basementArea = new PropertyNode(basement, "Area");

            //Act
            graph.AddDependency(basementArea, apartmentTotalArea);

            //Assert
            Assert.IsTrue(apartmentTotalArea.HasPredecessor(basementArea) && basementArea.HasSuccessor(apartmentTotalArea));
        }

        #endregion

        #region public void AddDependency(object predecessorObject, string predecessorMember, object successorObject, string successorMember)

        [TestMethod]
        public void AddDependency1_GivenValidAddedPropertyNodes_DependencyAdded()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();
            string predecessorMember = "Width";
            string successorMember = "Area";

            INode predecessor = graph.AddNode(building, predecessorMember);
            INode successor = graph.AddNode(building, successorMember);

            //Act
            graph.AddDependency(building, predecessorMember, building, successorMember);

            //Assert
            Assert.IsTrue(predecessor.HasSuccessor(successor) && successor.HasPredecessor(predecessor));
        }

        [TestMethod]
        public void AddDependency1_GivenValidAddedMethodNodes_DependencyAdded()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();
            string predecessorMember = "Update_Area";
            string successorMember = "Update_Volume";

            INode predecessor = graph.AddNode(building, predecessorMember);
            INode successor = graph.AddNode(building, successorMember);

            //Act
            graph.AddDependency(building, predecessorMember, building, successorMember);

            //Assert
            Assert.IsTrue(predecessor.HasSuccessor(successor) && successor.HasPredecessor(predecessor));
        }

        [TestMethod]
        public void AddDependency1_GivenValidAddedPropertyAndMethodNodes_DependencyAdded()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();
            string predecessorMember = "Area";
            string successorMember = "Update_Volume";

            INode predecessor = graph.AddNode(building, predecessorMember);
            INode successor = graph.AddNode(building, successorMember);

            //Act
            graph.AddDependency(building, predecessorMember, building, successorMember);

            //Assert
            Assert.IsTrue(predecessor.HasSuccessor(successor) && successor.HasPredecessor(predecessor));
        }

        [TestMethod]
        public void AddDependency1_GivenNullNodes_ThrowsException()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");

            object predecessorObject = null;
            string predecessorMember = "";
            object successorObject = new Building00();
            string successorMember = "Width";

            //Act&Assert
            Assert.ThrowsException<NodeNullReferenceException>(() => graph.AddDependency(predecessorObject, predecessorMember, successorObject, successorMember));
        }

        [TestMethod]
        public void AddDependency1_GivenValidButNotAddedNodes_DependencyAdded()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();
            string predecessorMember = "Width";
            string successorMember = "Area";

            //Act
            graph.AddDependency(building, predecessorMember, building, successorMember);

            //Assert
            INode predecessor = graph.GetNode(building, predecessorMember);
            INode successor = graph.GetNode(building, successorMember);
            Assert.IsTrue(predecessor.HasSuccessor(successor) && successor.HasPredecessor(predecessor));
        }

        #endregion
        #region public void AddDependency(INode predecessor, List<INode> successors)

        [TestMethod]
        public void AddDependency2_GivenNullNodes_ThrowsException()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");

            INode predecessor = null;
            List<INode> successors = null;

            //Act&Assert
            Assert.ThrowsException<NodeNullReferenceException>(() => graph.AddDependency(predecessor, successors));
        }

        [TestMethod]
        public void AddDependency2_GivenValidButNotAddedNodes_NodesAdded()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            GenericReactiveObject owner = new GenericReactiveObject();

            //Act
            INode predecessor = new PropertyNode(owner, "A");
            List<INode> successors = new List<INode>();
            successors.Add(new PropertyNode(owner, "B"));
            successors.Add(new PropertyNode(owner, "C"));
            successors.Add(new PropertyNode(owner, "D"));

            graph.AddDependency(predecessor, successors);

            //Assert
            bool predecessorAdded = graph.GetNode(owner, "A") != null;
            bool successorsAdded = graph.GetNode(owner, "B") != null && graph.GetNode(owner, "D") != null && graph.GetNode(owner, "D") != null;

            Assert.IsTrue(predecessorAdded == true && successorsAdded == true);
        }

        [TestMethod]
        public void AddDependency2_GivenValidNodes_DependenciesAdded()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            GenericReactiveObject owner = new GenericReactiveObject();

            //Act
            INode predecessor = graph.AddNode(owner, "A");
            List<INode> successors = new List<INode>();
            successors.Add(graph.AddNode(owner, "B"));
            successors.Add(graph.AddNode(owner, "C"));
            successors.Add(graph.AddNode(owner, "D"));

            graph.AddDependency(predecessor, successors);

            //Assert
            Assert.IsTrue(
                predecessor.HasSuccessor(successors[0]) &&
                predecessor.HasSuccessor(successors[1]) &&
                predecessor.HasSuccessor(successors[2]) &&
                successors[0].HasPredecessor(predecessor) &&
                successors[1].HasPredecessor(predecessor) &&
                successors[2].HasPredecessor(predecessor)
                );
        }

        #endregion

        #region public void AddDependency(List<INode> predecessors, INode successor)

        [TestMethod]
        public void AddDependency3_GivenNullNodes_ThrowsException()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");

            List<INode> predecessors = null;
            INode successor = null;
            

            //Act&Assert
            Assert.ThrowsException<NodeNullReferenceException>(() => graph.AddDependency(predecessors, successor));
        }

        [TestMethod]
        public void AddDependency3_GivenValidButNotAddedNodes_NodesAdded()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            GenericReactiveObject owner = new GenericReactiveObject();

            //Act
            INode successor = new PropertyNode(owner, "D");
            List<INode> predecessors = new List<INode>();
            predecessors.Add(new PropertyNode(owner, "A"));
            predecessors.Add(new PropertyNode(owner, "B"));
            predecessors.Add(new PropertyNode(owner, "C"));

            graph.AddDependency(predecessors, successor);

            //Assert
            bool successorAdded = graph.GetNode(owner, "D") != null;
            bool predecessorsAdded = graph.GetNode(owner, "A") != null && graph.GetNode(owner, "B") != null && graph.GetNode(owner, "C") != null;

            Assert.IsTrue(predecessorsAdded == true && successorAdded == true);
        }

        [TestMethod]
        public void AddDependency32_GivenValidNodes_DependenciesAdded()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            GenericReactiveObject owner = new GenericReactiveObject();

            //Act
            INode successor = new PropertyNode(owner, "D");
            List<INode> predecessors = new List<INode>();
            predecessors.Add(new PropertyNode(owner, "A"));
            predecessors.Add(new PropertyNode(owner, "B"));
            predecessors.Add(new PropertyNode(owner, "C"));

            graph.AddDependency(predecessors, successor);

            //Assert
            Assert.IsTrue(
                successor.HasPredecessor(predecessors[0]) &&
                successor.HasPredecessor(predecessors[1]) &&
                successor.HasPredecessor(predecessors[2]) &&
                predecessors[0].HasSuccessor(successor) &&
                predecessors[1].HasSuccessor(successor) &&
                predecessors[2].HasSuccessor(successor)
                );
        }

        #endregion

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

            INode predecessor = new PropertyNode(building00, memberName);
            INode successor = new PropertyNode(building00, memberName2);

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

            INode predecessor = new PropertyNode(building00, memberName);
            INode successor = new PropertyNode(building00, memberName2);

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

            INode apartmentTotalArea = new PropertyNode(apartment01, "TotalArea");
            INode basementArea = new PropertyNode(basement, "Area");

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

            INode predecessor = new PropertyNode(building00, memberName);
            INode successor = new PropertyNode(building00, memberName2);

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

            INode predecessor = new PropertyNode(building00, memberName);
            INode successor = new PropertyNode(building00, memberName2);
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

            INode predecessor = new PropertyNode(building00, memberName);
            INode successor = new PropertyNode(building00, memberName2);
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

            INode predecessor = new PropertyNode(building00, memberName);
            INode successor = new PropertyNode(building00, memberName2);
            graph.AddNode(predecessor);
            graph.AddNode(successor);
            graph.AddNode(building00, memberName3);

            //Act
            int numOfNodes = graph.RemoveUnusedNodes();

            //Assert
            Assert.AreEqual(numOfNodes, 3);
        }

        #endregion
    }
}
