using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeCoreExamples.E02;
using ReframeCore;
using ReframeCore.Exceptions;

namespace ReframeCoreTests
{
    /// <summary>
    /// Summary description for MethodNodeTests
    /// </summary>
    [TestClass]
    public class MethodNodeTests
    {
        #region NodeCreation

        [TestMethod]
        public void NodeCreation_GivenCorrectArguments_CreatesMethodNode()
        {
            //Arrange
            Building02 building = new Building02();
            string memberName = "Update_Area";

            //Act
            INode updateAreaNode = new MethodNode(building, memberName);

            //Assert
            Assert.IsInstanceOfType(updateAreaNode, typeof(INode));
        }

        [TestMethod]
        public void NodeCreation_CreatedMethodNode_HoldsProperData()
        {
            //Arrange
            Building02 building = new Building02();
            string memberName = "Update_Area";

            //Act
            MethodNode updateAreaNode = new MethodNode(building, memberName);

            //Assert
            Assert.IsTrue(updateAreaNode.OwnerObject == building 
                && updateAreaNode.MemberName == memberName
                && updateAreaNode.UpdateMethod.Method.Name == memberName);
        }

        [TestMethod]
        public void NodeCreation_MethodNodeUpdateInvoke_DoesNotBreak()
        {
            //Arrange
            Building02 building = new Building02();
            string memberName = "Update_Area";

            try
            {
                //Act
                INode updateAreaNode = new MethodNode(building, memberName);
                updateAreaNode.Update();
            }
            catch (Exception ex)
            {
                //Assert
                Assert.Fail("Expected no exception, instead got:" + ex.Message);
            }
        
        }

        [TestMethod]
        public void NodeCreation_GivenNullObject_ThrowsException()
        {
            //Arrange
            Building02 building02 = null;
            string updateAreaNode = "Update_Area";

            //Act & Assert
            Assert.ThrowsException<ReactiveNodeException>(() => new MethodNode(building02, updateAreaNode));
        }

        [TestMethod]
        public void NodeCreation_GivenNonExistantMemberName_ThrowsException()
        {
            //Arrange
            Building02 building02 = new Building02();
            string memberName = "NonexistantMember";

            //Act & Assert
            Assert.ThrowsException<ReactiveNodeException>(() => new MethodNode(building02, memberName));
        }

        [TestMethod]
        public void NodeCreation_CreatedMethodNode_ObtainedIdentifier()
        {
            //Arrange
            Building02 building02 = new Building02();
            string memberName = "Update_Area";

            //Act
            INode node = new MethodNode(building02, memberName);

            //Assert
            Assert.AreNotEqual(default(uint), node.Identifier);
        }



        #endregion

        #region HasSameIdentifier

        [TestMethod]
        public void HasSameIdentifier_NodesPointingAtTheSameObjectAndMember_ReturnsTrue()
        {
            //Arrange
            Building02 building = new Building02();
            string memberName = "Update_Area";

            INode node1 = new MethodNode(building, memberName);
            INode node2 = new MethodNode(building, memberName);

            //Act
            bool same = node1.HasSameIdentifier(node2);

            //Assert
            Assert.IsTrue(same);
        }

        [TestMethod]
        public void HasSameIdentifier_NodesPointingAtTheSameObjectAndMember1_ReturnsTrue()
        {
            //Arrange
            Building02 building = new Building02();
            string memberName = "Update_Area";

            INode node1 = new MethodNode(building, memberName);

            //Act
            bool same = node1.HasSameIdentifier(building, memberName);

            //Assert
            Assert.IsTrue(same);
        }

        [TestMethod]
        public void HasSameIdentifier_NodesPointingAtSameObjectAndDifferentMember_ReturnsFalse()
        {
            //Arrange
            Building02 building = new Building02();
            string memberName = "Update_Area";
            string memberName2 = "Update_Volume";

            INode node1 = new MethodNode(building, memberName);
            INode node2 = new MethodNode(building, memberName2);

            //Act
            bool same = node1.HasSameIdentifier(node2);

            //Assert
            Assert.IsFalse(same);
        }

        [TestMethod]
        public void HasSameIdentifier_NodesPointingAtSameObjectAndDifferentMember1_ReturnsFalse()
        {
            //Arrange
            Building02 building = new Building02();
            string memberName = "Update_Area";
            string memberName2 = "Update_Volume";

            INode node1 = new MethodNode(building, memberName);

            //Act
            bool same = node1.HasSameIdentifier(building, memberName2);

            //Assert
            Assert.IsFalse(same);
        }

        [TestMethod]
        public void HasSameIdentifier_NodesPointingAtDifferentObjectAndSameMember_ReturnsFalse()
        {
            //Arrange
            Building02 building1 = new Building02();
            Building02 building2 = new Building02();

            string memberName = "Update_Area";

            INode node1 = new MethodNode(building1, memberName);
            INode node2 = new MethodNode(building2, memberName);

            //Act
            bool same = node1.HasSameIdentifier(node2);

            //Assert
            Assert.IsFalse(same);
        }

        [TestMethod]
        public void HasSameIdentifier_NodesPointingAtDifferentObjectAndSameMember1_ReturnsFalse()
        {
            //Arrange
            Building02 building1 = new Building02();
            Building02 building2 = new Building02();

            string memberName = "Update_Area";

            INode node1 = new MethodNode(building1, memberName);

            //Act
            bool same = node1.HasSameIdentifier(building2, memberName);

            //Assert
            Assert.IsFalse(same);
        }

        #endregion

        #region AddPredecessor

        [TestMethod]
        public void AddPredecessor_GivenAnotherValidMethodNode_ReturnsTrue()
        {
            //Arrange
            Building02 building = new Building02();

            INode node = new MethodNode(building, "Update_Volume");
            INode predecessorNode = new MethodNode(building, "Update_Area");

            //Act
            bool added = node.AddPredecessor(predecessorNode);

            //Assert
            Assert.IsTrue(added);
        }

        [TestMethod]
        public void AddPredecessor_GivenNullNode_ThrowsException()
        {
            //Arrange
            Building02 building = new Building02();

            INode node = new MethodNode(building, "Update_Area");
            INode predecessorNode = null;

            //Act & Assert
            Assert.ThrowsException<ReactiveNodeException>(() => node.AddPredecessor(predecessorNode));
        }

        [TestMethod]
        public void AddPredecessor_GivenSameNode_ThrowsException()
        {
            //Arrange
            Building02 building = new Building02();

            INode node = new MethodNode(building, "Update_Area");
            INode predecessorNode = node;

            //Act & Assert
            Assert.ThrowsException<ReactiveNodeException>(() => node.AddPredecessor(predecessorNode));
        }

        [TestMethod]
        public void AddPredecessor_GivenAlreadyAddedNode_ReturnsFalse()
        {
            //Arrange
            Building02 building = new Building02();

            INode node = new MethodNode(building, "Update_Volume");
            INode predecessorNode = new MethodNode(building, "Update_Area");
            node.AddPredecessor(predecessorNode);

            //Act
            bool added = node.AddPredecessor(predecessorNode);

            //Assert
            Assert.IsFalse(added);
        }

        #endregion

        #region HasPredecessor

        [TestMethod]
        public void HasPredecessor_GivenNodeAddedAsSuccessor_ReturnsTrue()
        {
            //Arrange
            Building02 building = new Building02();

            INode successorNode = new MethodNode(building, "Update_Volume");
            INode predecessorNode = new MethodNode(building, "Update_Area");
            predecessorNode.AddSuccessor(successorNode);

            //Act
            bool hasPredecessor = successorNode.HasPredecessor(predecessorNode);

            //Assert
            Assert.IsTrue(hasPredecessor);
        }

        [TestMethod]
        public void HasPredecessor_GivenNodeNotAddedAsSuccessor_ReturnsFalse()
        {
            //Arrange
            Building02 building = new Building02();

            INode successorNode = new MethodNode(building, "Update_Volume");
            INode predecessorNode = new MethodNode(building, "Update_Area");

            //Act
            bool hasPredecessor = successorNode.HasPredecessor(predecessorNode);

            //Assert
            Assert.IsFalse(hasPredecessor);
        }

        [TestMethod]
        public void HasPredecessor_GivenNodeRemovedAsSuccessor_ReturnsFalse()
        {
            //Arrange
            Building02 building = new Building02();

            INode successorNode = new MethodNode(building, "Update_Volume");
            INode predecessorNode = new MethodNode(building, "Update_Area");
            predecessorNode.AddSuccessor(successorNode);
            predecessorNode.RemoveSuccessor(successorNode);

            //Act
            bool hasPredecessor = successorNode.HasPredecessor(predecessorNode);

            //Assert
            Assert.IsFalse(hasPredecessor);
        }

        #endregion

        #region HasSuccessor

        [TestMethod]
        public void HasSuccessor_GivenNodeAddedAsPredecessor_ReturnsTrue()
        {
            //Arrange
            Building02 building = new Building02();

            INode successorNode = new MethodNode(building, "Update_Volume");
            INode predecessorNode = new MethodNode(building, "Update_Area");
            successorNode.AddPredecessor(predecessorNode);

            //Act
            bool hasSuccessor = predecessorNode.HasSuccessor(successorNode);

            //Assert
            Assert.IsTrue(hasSuccessor);
        }

        [TestMethod]
        public void HasSuccessor_GivenNodeNotAddedAsPredecessor_ReturnsFalse()
        {
            //Arrange
            Building02 building = new Building02();

            INode successorNode = new MethodNode(building, "Update_Volume");
            INode predecessorNode = new MethodNode(building, "Update_Area");

            //Act
            bool hasSuccessor = predecessorNode.HasSuccessor(successorNode);

            //Assert
            Assert.IsFalse(hasSuccessor);
        }

        [TestMethod]
        public void HasSuccessor_GivenNodeRemovedAsPredecessor_ReturnsFalse()
        {
            //Arrange
            Building02 building = new Building02();

            INode successorNode = new MethodNode(building, "Update_Volume");
            INode predecessorNode = new MethodNode(building, "Update_Area");
            successorNode.AddPredecessor(predecessorNode);
            successorNode.RemovePredecessor(predecessorNode);

            //Act
            bool hasSuccessor = predecessorNode.HasSuccessor(successorNode);

            //Assert
            Assert.IsFalse(hasSuccessor);
        }

        #endregion

        #region AddSuccessor

        [TestMethod]
        public void AddSuccessor_GivenAnotherValidNode_ReturnsTrue()
        {
            //Arrange
            Building02 building = new Building02();

            INode node = new MethodNode(building, "Update_Volume");
            INode successorNode = new MethodNode(building, "Update_Area");

            //Act
            bool added = node.AddSuccessor(successorNode);

            //Assert
            Assert.IsTrue(added);
        }

        [TestMethod]
        public void AddSuccessor_GivenNullNode_ThrowsException()
        {
            //Arrange
            Building02 building = new Building02();

            INode node = new MethodNode(building, "Update_Area");
            INode successorNode = null;

            //Act & Assert
            Assert.ThrowsException<ReactiveNodeException>(() => node.AddSuccessor(successorNode));
        }

        [TestMethod]
        public void AddSuccessor_GivenSameNode_ThrowsException()
        {
            //Arrange
            Building02 building = new Building02();

            INode node = new MethodNode(building, "Update_Area");
            INode successorNode = node;

            //Act & Assert
            Assert.ThrowsException<ReactiveNodeException>(() => node.AddSuccessor(successorNode));
        }

        [TestMethod]
        public void AddSuccessor_GivenAlreadyAddedNode_ReturnsFalse()
        {
            //Arrange
            Building02 building = new Building02();

            INode node = new MethodNode(building, "Update_Area");
            INode successorNode = new MethodNode(building, "Update_Volume");
            node.AddSuccessor(successorNode);

            //Act
            bool added = node.AddSuccessor(successorNode);

            //Assert
            Assert.IsFalse(added);
        }

        #endregion

        #region RemovePredecessor

        [TestMethod]
        public void RemovePredecessor_GivenValidPredecessor_ReturnsTrue()
        {
            //Arrange
            Building02 building = new Building02();

            INode node = new MethodNode(building, "Update_Area");
            INode predecessorNode = new MethodNode(building, "Update_Volume");
            node.AddPredecessor(predecessorNode);

            //Act
            bool removed = node.RemovePredecessor(predecessorNode);

            //Assert
            Assert.IsTrue(removed);
        }

        [TestMethod]
        public void RemovePredecessor_GivenNonexistingPredecessor_ReturnsFalse()
        {
            //Arrange
            Building02 building = new Building02();

            INode node = new MethodNode(building, "Update_Area");
            INode predecessorNode = new MethodNode(building, "Update_Volume");

            //Act
            bool removed = node.RemovePredecessor(predecessorNode);

            //Assert
            Assert.IsFalse(removed);
        }

        [TestMethod]
        public void RemovePredecessor_GivenNullPredecessor_ReturnsFalse()
        {
            //Arrange
            Building02 building = new Building02();

            INode node = new MethodNode(building, "Update_Area");
            INode predecessorNode = null;

            //Act
            bool removed = node.RemovePredecessor(predecessorNode);

            //Assert
            Assert.IsFalse(removed);
        }

        #endregion

        #region RemoveSuccessor

        [TestMethod]
        public void RemoveSuccessor_GivenValidSuccessor_ReturnsTrue()
        {
            //Arrange
            Building02 building = new Building02();

            INode node = new MethodNode(building, "Update_Area");
            INode successorNode = new MethodNode(building, "Update_Volume");
            node.AddSuccessor(successorNode);

            //Act
            bool removed = node.RemoveSuccessor(successorNode);

            //Assert
            Assert.IsTrue(removed);
        }

        [TestMethod]
        public void RemoveSuccessor_GivenNonexistingSuccessor_ReturnsFalse()
        {
            //Arrange
            Building02 building = new Building02();

            INode node = new MethodNode(building, "Update_Area");
            INode successorNode = new MethodNode(building, "Update_Volume");

            //Act
            bool removed = node.RemoveSuccessor(successorNode);

            //Assert
            Assert.IsFalse(removed);
        }

        [TestMethod]
        public void RemoveSuccessor_GivenNullSuccessor_ReturnsFalse()
        {
            //Arrange
            Building02 building = new Building02();

            INode node = new MethodNode(building, "Update_Area");
            INode successorNode = null;

            //Act
            bool removed = node.RemoveSuccessor(successorNode);

            //Assert
            Assert.IsFalse(removed);
        }

        #endregion
    }
}
