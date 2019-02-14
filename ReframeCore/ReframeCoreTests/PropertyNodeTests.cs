using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeCoreExamples.E00;
using ReframeCore;
using ReframeCore.Exceptions;
using ReframeCore.Nodes;

namespace ReframeCoreTests
{
    [TestClass]
    public class PropertyNodeTests
    {
        #region UpdateInvoke

        [TestMethod]
        public void NodeCreation_SourceNodeUpdateInvoke_DoesNotBreak()
        {
            //Arrange
            Building00 building = new Building00();
            string memberName = "Width";

            try
            {
                //Act
                INode widthNode = NodeFactory.CreateNode(building, memberName);
                widthNode.Update();
            }
            catch (Exception ex)
            {
                //Assert
                Assert.Fail("Expected no exception, instead got: " + ex.Message);
            }
        }

        #endregion

        #region Identifier check

        [TestMethod]
        public void NodeCreation_CreatedReactiveNode_ObtainedIdentifier()
        {
            //Arrange
            Building00 building00 = new Building00();
            string memberName = "Width";

            //Act
            INode node = NodeFactory.CreateNode(building00, memberName);

            //Assert
            Assert.AreNotEqual(default(uint), node.Identifier);
        }

        [TestMethod]
        public void HasSameIdentifier_NodesPointingAtTheSameObjectAndMember_ReturnsTrue()
        {
            //Arrange
            Building00 building00 = new Building00();
            string memberName = "Width";

            INode node1 = new PropertyNode(building00, memberName);
            INode node2 = new PropertyNode(building00, memberName);

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

            INode node1 = new PropertyNode(building00, memberName);

            //Act
            bool same = node1.HasSameIdentifier(building00, memberName);

            //Assert
            Assert.IsTrue(same);
        }

        [TestMethod]
        public void HasSameIdentifier_NodesPointingAtSameObjectAndDifferentMember_ReturnsFalse()
        {
            //Arrange
            Building00 building00 = new Building00();
            string memberName = "Width";
            string memberName2 = "Length";

            INode node1 = new PropertyNode(building00, memberName);
            INode node2 = new PropertyNode(building00, memberName2);

            //Act
            bool same = node1.HasSameIdentifier(node2);

            //Assert
            Assert.IsFalse(same);
        }

        [TestMethod]
        public void HasSameIdentifier_NodesPointingAtSameObjectAndDifferentMember1_ReturnsFalse()
        {
            //Arrange
            Building00 building00 = new Building00();
            string memberName = "Width";
            string memberName2 = "Length";

            INode node1 = new PropertyNode(building00, memberName);

            //Act
            bool same = node1.HasSameIdentifier(building00, memberName2);

            //Assert
            Assert.IsFalse(same);
        }

        [TestMethod]
        public void HasSameIdentifier_NodesPointingAtDifferentObjectAndSameMember_ReturnsFalse()
        {
            //Arrange
            Building00 building1 = new Building00();
            Building00 building2 = new Building00();

            string memberName = "Width";

            INode node1 = new PropertyNode(building1, memberName);
            INode node2 = new PropertyNode(building2, memberName);

            //Act
            bool same = node1.HasSameIdentifier(node2);

            //Assert
            Assert.IsFalse(same);
        }

        [TestMethod]
        public void HasSameIdentifier_NodesPointingAtDifferentObjectAndSameMember1_ReturnsFalse()
        {
            //Arrange
            Building00 building1 = new Building00();
            Building00 building2 = new Building00();

            string memberName = "Width";

            INode node1 = new PropertyNode(building1, memberName);

            //Act
            bool same = node1.HasSameIdentifier(building2, memberName);

            //Assert
            Assert.IsFalse(same);
        }

        #endregion

        #region AddPredecessor

        [TestMethod]
        public void AddPredecessor_GivenAnotherValidNode_ReturnsTrue()
        {
            //Arrange
            Building00 building00 = new Building00();
            string propertyName = "Area";
            string updateMethodName = "Update_Area";

            string predecessorPropertyName = "Width";

            INode node = new PropertyNode(building00, propertyName, updateMethodName);
            INode predecessorNode = new PropertyNode(building00, predecessorPropertyName);

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
            string updateMethodName = "Update_Area";

            INode node = new PropertyNode(building00, propertyName, updateMethodName);
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
            string updateMethodName = "Update_Area";

            INode node = new PropertyNode(building00, propertyName, updateMethodName);
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
            string updateMethodName = "Update_Area";

            string predecessorPropertyName = "Width";

            INode node = new PropertyNode(building00, propertyName, updateMethodName);
            INode predecessorNode = new PropertyNode(building00, predecessorPropertyName);
            node.AddPredecessor(predecessorNode);

            //Act
            bool added = node.AddPredecessor(predecessorNode);

            //Assert
            Assert.IsFalse(added);
        }

        [TestMethod]
        public void AddPredecessor_GivenValidMethodNode_ReturnsTrue()
        {
            //Arrange
            Building00 building00 = new Building00();


            INode node = new PropertyNode(building00, "Volume", "Update_Volume");
            INode predecessorNode = new MethodNode(building00, "Update_Area");

            //Act
            bool added = node.AddPredecessor(predecessorNode);

            //Assert
            Assert.IsTrue(added);
        }

        #endregion

        #region HasPredecessor

        [TestMethod]
        public void HasPredecessor_GivenNodeAddedAsSuccessor_ReturnsTrue()
        {
            //Arrange
            Building00 building00 = new Building00();
            string propertyName = "Area";
            string updateMethodName = "Update_Area";

            string predecessorPropertyName = "Width";

            INode successorNode = new PropertyNode(building00, propertyName, updateMethodName);
            INode predecessorNode = new PropertyNode(building00, predecessorPropertyName);
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
            Building00 building00 = new Building00();
            string propertyName = "Area";
            string updateMethodName = "Update_Area";

            string predecessorPropertyName = "Width";

            INode successorNode = new PropertyNode(building00, propertyName, updateMethodName);
            INode predecessorNode = new PropertyNode(building00, predecessorPropertyName);

            //Act
            bool hasPredecessor = successorNode.HasPredecessor(predecessorNode);

            //Assert
            Assert.IsFalse(hasPredecessor);
        }

        [TestMethod]
        public void HasPredecessor_GivenNodeRemovedAsSuccessor_ReturnsFalse()
        {
            //Arrange
            Building00 building00 = new Building00();
            string propertyName = "Area";
            string updateMethodName = "Update_Area";

            string predecessorPropertyName = "Width";

            INode successorNode = new PropertyNode(building00, propertyName, updateMethodName);
            INode predecessorNode = new PropertyNode(building00, predecessorPropertyName);
            predecessorNode.AddSuccessor(successorNode);
            predecessorNode.RemoveSuccessor(successorNode);

            //Act
            bool hasPredecessor = successorNode.HasPredecessor(predecessorNode);

            //Assert
            Assert.IsFalse(hasPredecessor);
        }

        [TestMethod]
        public void HasPredecessor_GivenMethodNodeAddedAsSuccessor_ReturnsTrue()
        {
            //Arrange
            Building00 building00 = new Building00();

            INode predecessorNode = new PropertyNode(building00, "Volume", "Update_Volume");
            INode successorNode = new MethodNode(building00, "Update_Area");
            predecessorNode.AddSuccessor(successorNode);

            //Act
            bool hasPredecessor = successorNode.HasPredecessor(predecessorNode);

            //Assert
            Assert.IsTrue(hasPredecessor);
        }

        #endregion

        #region HasSuccessor

        [TestMethod]
        public void HasSuccessor_GivenNodeAddedAsPredecessor_ReturnsTrue()
        {
            //Arrange
            Building00 building00 = new Building00();
            string propertyName = "Area";
            string updateMethodName = "Update_Area";

            string predecessorPropertyName = "Width";

            INode successorNode = new PropertyNode(building00, propertyName, updateMethodName);
            INode predecessorNode = new PropertyNode(building00, predecessorPropertyName);
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
            Building00 building00 = new Building00();
            string propertyName = "Area";
            string updateMethodName = "Update_Area";

            string predecessorPropertyName = "Width";

            INode successorNode = new PropertyNode(building00, propertyName, updateMethodName);
            INode predecessorNode = new PropertyNode(building00, predecessorPropertyName);

            //Act
            bool hasSuccessor = predecessorNode.HasSuccessor(successorNode);

            //Assert
            Assert.IsFalse(hasSuccessor);
        }

        [TestMethod]
        public void HasSuccessor_GivenNodeRemovedAsPredecessor_ReturnsFalse()
        {
            //Arrange
            Building00 building00 = new Building00();
            string propertyName = "Area";
            string updateMethodName = "Update_Area";

            string predecessorPropertyName = "Width";

            INode successorNode = new PropertyNode(building00, propertyName, updateMethodName);
            INode predecessorNode = new PropertyNode(building00, predecessorPropertyName);
            successorNode.AddPredecessor(predecessorNode);
            successorNode.RemovePredecessor(predecessorNode);

            //Act
            bool hasSuccessor = predecessorNode.HasSuccessor(successorNode);

            //Assert
            Assert.IsFalse(hasSuccessor);
        }

        [TestMethod]
        public void HasSuccessor_GivenMethodNodeAddedAsPredecessor_ReturnsTrue()
        {
            //Arrange
            Building00 building00 = new Building00();

            INode successorNode = new PropertyNode(building00, "Area", "Update_Area");
            INode predecessorNode = new MethodNode(building00, "Update_Volume");
            successorNode.AddPredecessor(predecessorNode);

            //Act
            bool hasSuccessor = predecessorNode.HasSuccessor(successorNode);

            //Assert
            Assert.IsTrue(hasSuccessor);
        }

        #endregion

        #region AddSuccessor

        [TestMethod]
        public void AddSuccessor_GivenAnotherValidNode_ReturnsTrue()
        {
            //Arrange
            Building00 building00 = new Building00();
            string propertyName = "Width";

            string successorPropertyName = "Area";
            string successorUpdateMethodName = "Update_Area";

            INode node = new PropertyNode(building00, propertyName);
            INode successorNode = new PropertyNode(building00, successorPropertyName, successorUpdateMethodName);

            //Act
            bool added = node.AddSuccessor(successorNode);

            //Assert
            Assert.IsTrue(added);
        }

        [TestMethod]
        public void AddSuccessor_GivenNullNode_ThrowsException()
        {
            //Arrange
            Building00 building00 = new Building00();
            string propertyName = "Width";

            INode node = new PropertyNode(building00, propertyName);
            INode successorNode = null;

            //Act & Assert
            Assert.ThrowsException<ReactiveNodeException>(() => node.AddSuccessor(successorNode));
        }

        [TestMethod]
        public void AddSuccessor_GivenSameNode_ThrowsException()
        {
            //Arrange
            Building00 building00 = new Building00();
            string propertyName = "Width";

            INode node = new PropertyNode(building00, propertyName);
            INode successorNode = node;

            //Act & Assert
            Assert.ThrowsException<ReactiveNodeException>(() => node.AddSuccessor(successorNode));
        }

        [TestMethod]
        public void AddSuccessor_GivenAlreadyAddedNode_ReturnsFalse()
        {
            //Arrange
            Building00 building00 = new Building00();
            string propertyName = "Width";

            string successorPropertyName = "Area";
            string successorUpdateMethodName = "Update_Area";

            INode node = new PropertyNode(building00, propertyName);
            INode successorNode = new PropertyNode(building00, successorPropertyName, successorUpdateMethodName);
            node.AddSuccessor(successorNode);

            //Act
            bool added = node.AddSuccessor(successorNode);

            //Assert
            Assert.IsFalse(added);
        }

        [TestMethod]
        public void AddSuccessor_GivenValidMethodNode_ReturnsTrue()
        {
            //Arrange
            Building00 building00 = new Building00();

            INode node = new MethodNode(building00, "Update_Area");
            INode successorNode = new PropertyNode(building00, "Volume", "Update_Volume");

            //Act
            bool added = node.AddSuccessor(successorNode);

            //Assert
            Assert.IsTrue(added);
        }

        #endregion

        #region RemovePredecessor

        [TestMethod]
        public void RemovePredecessor_GivenValidPredecessor_ReturnsTrue()
        {
            //Arrange
            Building00 building00 = new Building00();
            string propertyName = "Area";
            string updateMethodName = "Update_Area";

            string predecessorPropertyName = "Width";

            INode node = new PropertyNode(building00, propertyName, updateMethodName);
            INode predecessorNode = new PropertyNode(building00, predecessorPropertyName);
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
            Building00 building00 = new Building00();
            string propertyName = "Area";
            string updateMethodName = "Update_Area";

            string predecessorPropertyName = "Width";

            INode node = new PropertyNode(building00, propertyName, updateMethodName);
            INode predecessorNode = new PropertyNode(building00, predecessorPropertyName);

            //Act
            bool removed = node.RemovePredecessor(predecessorNode);

            //Assert
            Assert.IsFalse(removed);
        }

        [TestMethod]
        public void RemovePredecessor_GivenNullPredecessor_ReturnsFalse()
        {
            //Arrange
            Building00 building00 = new Building00();
            string propertyName = "Area";
            string updateMethodName = "Update_Area";

            INode node = new PropertyNode(building00, propertyName, updateMethodName);
            INode predecessorNode = null;

            //Act
            bool removed = node.RemovePredecessor(predecessorNode);

            //Assert
            Assert.IsFalse(removed);
        }

        [TestMethod]
        public void RemovePredecessor_GivenValidMethodNodePredecessor_ReturnsTrue()
        {
            //Arrange
            Building00 building00 = new Building00();

            INode node = new PropertyNode(building00, "Width", "Update_Width");
            INode predecessorNode = new MethodNode(building00, "Update_Area");
            node.AddPredecessor(predecessorNode);

            //Act
            bool removed = node.RemovePredecessor(predecessorNode);

            //Assert
            Assert.IsTrue(removed);
        }

        #endregion

        #region RemoveSuccessor

        [TestMethod]
        public void RemoveSuccessor_GivenValidSuccessor_ReturnsTrue()
        {
            //Arrange
            Building00 building00 = new Building00();
            string propertyName = "Width";
            

            string successorPropertyName = "Area";
            string updateMethodName = "Update_Area";

            INode node = new PropertyNode(building00, propertyName);
            INode successorNode = new PropertyNode(building00, successorPropertyName, updateMethodName);
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
            Building00 building00 = new Building00();
            string propertyName = "Width";


            string successorPropertyName = "Area";
            string updateMethodName = "Update_Area";

            INode node = new PropertyNode(building00, propertyName);
            INode successorNode = new PropertyNode(building00, successorPropertyName, updateMethodName);

            //Act
            bool removed = node.RemoveSuccessor(successorNode);

            //Assert
            Assert.IsFalse(removed);
        }

        [TestMethod]
        public void RemoveSuccessor_GivenNullSuccessor_ReturnsFalse()
        {
            //Arrange
            Building00 building00 = new Building00();
            string propertyName = "Width";

            INode node = new PropertyNode(building00, propertyName);
            INode successorNode = null;

            //Act
            bool removed = node.RemoveSuccessor(successorNode);

            //Assert
            Assert.IsFalse(removed);
        }

        [TestMethod]
        public void RemoveSuccessor_GivenValidMethodNodeSuccessor_ReturnsTrue()
        {
            //Arrange
            Building00 building00 = new Building00();

            INode node = new PropertyNode(building00, "Width");
            INode successorNode = new MethodNode(building00, "Update_Area");
            node.AddSuccessor(successorNode);

            //Act
            bool removed = node.RemoveSuccessor(successorNode);

            //Assert
            Assert.IsTrue(removed);
        }

        #endregion
    }
}
