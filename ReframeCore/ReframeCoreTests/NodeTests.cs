using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeCoreExamples.E00;
using ReframeCore;
using ReframeCore.Exceptions;

namespace ReframeCoreTests
{
    [TestClass]
    public class NodeTests
    {
        [TestMethod]
        public void NodeCreation_GivenCorrectArguments_CreatesSourceNode()
        {
            //Arrange
            Building00 building = new Building00();
            string memberName = "Width";

            //Act
            INode widthNode = new Node(building, memberName);

            //Assert
            Assert.IsInstanceOfType(widthNode, typeof(INode));
        }

        [TestMethod]
        public void NodeCreation_CreatedSourceNode_HoldsProperData()
        {
            //Arrange
            Building00 building = new Building00();
            string memberName = "Width";

            //Act
            INode widthNode = new Node(building, memberName);

            //Assert
            Assert.IsTrue(widthNode.OwnerObject == building && widthNode.MemberName == memberName);
        }

        [TestMethod]
        public void NodeCreation_SourceNodeUpdateInvoke_DoesNotBreak()
        {
            //Arrange
            Building00 building = new Building00();
            string memberName = "Width";

            try
            {
                //Act
                INode widthNode = new Node(building, memberName);
            }
            catch (Exception ex)
            {
                //Assert
                Assert.Fail("Expected no exception, instead got: " + ex.Message);
            }
        }

        [TestMethod]
        public void NodeCreation_GivenCorrectArguments_CreatesNonSourceNode()
        {
            //Arrange
            Building00 building = new Building00();
            string memberName = "Area";
            string updateMethodName = "Update_Area";

            //Act
            INode widthNode = new Node(building, memberName, updateMethodName);

            //Assert
            Assert.IsInstanceOfType(widthNode, typeof(INode));
        }

        [TestMethod]
        public void NodeCreation_GivenNullObject_ThrowsException()
        {
            //Arrange
            Building00 building00 = null;
            string memberName = "Width";

            //Act & Assert
            Assert.ThrowsException<ReactiveNodeException>(() => new Node(building00, memberName));
        }

        [TestMethod]
        public void NodeCreation_GivenNonExistantMemberName_ThrowsException()
        {
            //Arrange
            Building00 building00 = new Building00();
            string memberName = "NonexistantMember";

            //Act & Assert
            Assert.ThrowsException<ReactiveNodeException>(() => new Node(building00, memberName));
        }

        [TestMethod]
        public void NodeCreation_CreatedReactiveNode_ObtainedIdentifier()
        {
            //Arrange
            Building00 building00 = new Building00();
            string memberName = "Width";

            //Act
            INode node = new Node(building00, memberName);

            //Assert
            Assert.AreNotEqual(default(int), node.Identifier);
        }

        [TestMethod]
        public void HasSameIdentifier_NodesPointingAtTheSameObjectAndMember_ReturnsTrue()
        {
            //Arrange
            Building00 building00 = new Building00();
            string memberName = "Width";

            INode node1 = new Node(building00, memberName);
            INode node2 = new Node(building00, memberName);

            //Act
            bool same = node1.HasSameIdentifier(node2);

            //Assert
            Assert.IsTrue(same);
        }

        [TestMethod]
        public void HasSameIdentifier_NodesPointingAtTheSameObjectAndMember1_ReturnsTrue()
        {
            //Arrange
            Building00 building00 = new Building00();
            string memberName = "Width";

            INode node1 = new Node(building00, memberName);

            //Act
            bool same = node1.HasSameIdentifier(building00, memberName);

            //Assert
            Assert.IsTrue(same);
        }

        [TestMethod]
        public void HasSameIdentifier_NodesPointingAtDifferentObjectAndMember_ReturnsFalse()
        {
            //Arrange
            Building00 building00 = new Building00();
            string memberName = "Width";
            string memberName2 = "Length";

            INode node1 = new Node(building00, memberName);
            INode node2 = new Node(building00, memberName2);

            //Act
            bool same = node1.HasSameIdentifier(node2);

            //Assert
            Assert.IsFalse(same);
        }

        [TestMethod]
        public void HasSameIdentifier_NodesPointingAtDifferentObjectAndMember1_ReturnsFalse()
        {
            //Arrange
            Building00 building00 = new Building00();
            string memberName = "Width";
            string memberName2 = "Length";

            INode node1 = new Node(building00, memberName);

            //Act
            bool same = node1.HasSameIdentifier(building00, memberName2);

            //Assert
            Assert.IsFalse(same);
        }

        [TestMethod]
        public void AddPredecessor_GivenAnotherValidNode_ReturnsTrue()
        {
            //Arrange
            Building00 building00 = new Building00();
            string propertyName = "Area";
            string updateMethodName = "UpdateArea";

            string predecessorPropertyName = "Width";

            INode node = new Node(building00, propertyName, updateMethodName);
            INode predecessorNode = new Node(building00, predecessorPropertyName);

            //Act
            bool added = node.AddPredecessor(predecessorNode);

            //Assert
            Assert.IsTrue(added);
        }

        [TestMethod]
        public void AddPredecessor_GivenNullNode_ThrowsException()
        {
            //Arrange
            Building00 building00 = new Building00();
            string propertyName = "Area";
            string updateMethodName = "UpdateArea";

            INode node = new Node(building00, propertyName, updateMethodName);
            INode predecessorNode = null;

            //Act & Assert
            Assert.ThrowsException<ReactiveNodeException>(() => node.AddPredecessor(predecessorNode));
        }

        [TestMethod]
        public void AddPredecessor_GivenSameNode_ThrowsException()
        {
            //Arrange
            Building00 building00 = new Building00();
            string propertyName = "Area";
            string updateMethodName = "UpdateArea";

            INode node = new Node(building00, propertyName, updateMethodName);
            INode predecessorNode = node;

            //Act & Assert
            Assert.ThrowsException<ReactiveNodeException>(() => node.AddPredecessor(predecessorNode));
        }

        [TestMethod]
        public void AddPredecessor_GivenAlreadyAddedNode_ReturnsFalse()
        {
            //Arrange
            Building00 building00 = new Building00();
            string propertyName = "Area";
            string updateMethodName = "UpdateArea";

            string predecessorPropertyName = "Width";

            INode node = new Node(building00, propertyName, updateMethodName);
            INode predecessorNode = new Node(building00, predecessorPropertyName);
            node.AddPredecessor(predecessorNode);

            //Act
            bool added = node.AddPredecessor(predecessorNode);

            //Assert
            Assert.IsFalse(added);
        }
    }
}
