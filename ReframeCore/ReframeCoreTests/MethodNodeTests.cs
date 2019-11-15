using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeCoreExamples.E02;
using ReframeCore;
using ReframeCore.Exceptions;
using ReframeCore.Nodes;
using ReframeCoreExamples.E00;
using ReframeCore.Factories;

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
        public void NodeCreation_MethodNodeUpdateInvoke_DoesNotBreak()
        {
            //Arrange 
            NodeFactory nodeFactory = new StandardNodeFactory();
            Building02 building = new Building02();
            string memberName = "Update_Area";

            try
            {
                //Act
                MethodNode updateAreaNode = nodeFactory.CreateNode(building, memberName) as MethodNode;
                updateAreaNode.Update();
            }
            catch (Exception ex)
            {
                //Assert
                Assert.Fail("Expected no exception, instead got:" + ex.Message);
            }

        }

        [TestMethod]
        public void NodeCreation_GivenPropertyNameInsteadOfMethodName_ThrowsException()
        {
            //Arrange 
            NodeFactory nodeFactory = new StandardNodeFactory();
            Building02 building = new Building02();
            string memberName = "Area";

            //Act & Assert
            Assert.ThrowsException<ReactiveNodeException>(() => new MethodNode(building, memberName));
        }

        #endregion

        #region Identifier Check

        [TestMethod]
        public void NodeCreation_CreatedMethodNode_ObtainedIdentifier()
        {
            //Arrange 
            NodeFactory nodeFactory = new StandardNodeFactory();
            Building02 building02 = new Building02();
            string memberName = "Update_Area";

            //Act
            MethodNode node = nodeFactory.CreateNode(building02, memberName) as MethodNode;

            //Assert
            Assert.AreNotEqual(default(uint), node.Identifier);
        }

        #endregion

        #region HasSameIdentifier

        [TestMethod]
        public void HasSameIdentifier_NodesPointingAtTheSameObjectAndMember_ReturnsTrue()
        {
            //Arrange 
            NodeFactory nodeFactory = new StandardNodeFactory();
            Building02 building = new Building02();
            string memberName = "Update_Area";

            MethodNode node1 = nodeFactory.CreateNode(building, memberName) as MethodNode;
            MethodNode node2 = nodeFactory.CreateNode(building, memberName) as MethodNode;

            //Act
            bool same = node1.HasSameIdentifier(node2);

            //Assert
            Assert.IsTrue(same);
        }

        [TestMethod]
        public void HasSameIdentifier_NodesPointingAtTheSameObjectAndMember1_ReturnsTrue()
        {
            //Arrange 
            NodeFactory nodeFactory = new StandardNodeFactory();
            Building02 building = new Building02();
            string memberName = "Update_Area";

            MethodNode node1 = nodeFactory.CreateNode(building, memberName) as MethodNode;

            //Act
            bool same = node1.HasSameIdentifier(building, memberName);

            //Assert
            Assert.IsTrue(same);
        }

        [TestMethod]
        public void HasSameIdentifier_NodesPointingAtSameObjectAndDifferentMember_ReturnsFalse()
        {
            //Arrange 
            NodeFactory nodeFactory = new StandardNodeFactory();
            Building02 building = new Building02();
            string memberName = "Update_Area";
            string memberName2 = "Update_Volume";

            MethodNode node1 = nodeFactory.CreateNode(building, memberName) as MethodNode;
            MethodNode node2 = nodeFactory.CreateNode(building, memberName2) as MethodNode;

            //Act
            bool same = node1.HasSameIdentifier(node2);

            //Assert
            Assert.IsFalse(same);
        }

        [TestMethod]
        public void HasSameIdentifier_NodesPointingAtSameObjectAndDifferentMember1_ReturnsFalse()
        {
            //Arrange 
            NodeFactory nodeFactory = new StandardNodeFactory();
            Building02 building = new Building02();
            string memberName = "Update_Area";
            string memberName2 = "Update_Volume";

            MethodNode node1 = nodeFactory.CreateNode(building, memberName) as MethodNode;

            //Act
            bool same = node1.HasSameIdentifier(building, memberName2);

            //Assert
            Assert.IsFalse(same);
        }

        [TestMethod]
        public void HasSameIdentifier_NodesPointingAtDifferentObjectAndSameMember_ReturnsFalse()
        {
            //Arrange 
            NodeFactory nodeFactory = new StandardNodeFactory();
            Building02 building1 = new Building02();
            Building02 building2 = new Building02();

            string memberName = "Update_Area";

            MethodNode node1 = nodeFactory.CreateNode(building1, memberName) as MethodNode;
            MethodNode node2 = nodeFactory.CreateNode(building2, memberName) as MethodNode;

            //Act
            bool same = node1.HasSameIdentifier(node2);

            //Assert
            Assert.IsFalse(same);
        }

        [TestMethod]
        public void HasSameIdentifier_NodesPointingAtDifferentObjectAndSameMember1_ReturnsFalse()
        {
            //Arrange 
            NodeFactory nodeFactory = new StandardNodeFactory();
            Building02 building1 = new Building02();
            Building02 building2 = new Building02();

            string memberName = "Update_Area";

            MethodNode node1 = nodeFactory.CreateNode(building1, memberName) as MethodNode;

            //Act
            bool same = node1.HasSameIdentifier(building2, memberName);

            //Assert
            Assert.IsFalse(same);
        }

        [TestMethod]
        public void HasSameIdentifier_NullObjectProvided_ReturnsFalse()
        {
            //Arrange
            NodeFactory nodeFactory = new StandardNodeFactory();
            Building02 building = new Building02();
            string memberName = "Update_Area";

            MethodNode node = nodeFactory.CreateNode(building, memberName) as MethodNode;

            //Act
            bool same = node.HasSameIdentifier(null, "Update_Area");

            //Assert
            Assert.IsFalse(same);
        }

        [TestMethod]
        public void HasSameIdentifier_EmptyMemberNameProvided_ReturnsFalse()
        {
            //Arrange
            NodeFactory nodeFactory = new StandardNodeFactory();
            Building02 building = new Building02();
            string memberName = "Update_Area";

            MethodNode node = nodeFactory.CreateNode(building, memberName) as MethodNode;

            //Act
            bool same = node.HasSameIdentifier(new Building02(), "");

            //Assert
            Assert.IsFalse(same);
        }

        #endregion

        #region AddPredecessor

        [TestMethod]
        public void AddPredecessor_GivenAnotherValidMethodNode_ReturnsTrue()
        {
            //Arrange 
            NodeFactory nodeFactory = new StandardNodeFactory();
            Building02 building = new Building02();

            MethodNode node = nodeFactory.CreateNode(building, "Update_Volume") as MethodNode;
            MethodNode predecessorNode = nodeFactory.CreateNode(building, "Update_Area") as MethodNode;

            //Act
            bool added = node.AddPredecessor(predecessorNode);

            //Assert
            Assert.IsTrue(added);
        }

        [TestMethod]
        public void AddPredecessor_GivenAnotherValidPropertyNode_ReturnsTrue()
        {
            //Arrange 
            NodeFactory nodeFactory = new StandardNodeFactory();
            Building02 building = new Building02();

            MethodNode node = nodeFactory.CreateNode(building, "Update_Volume") as MethodNode;
            PropertyNode predecessorNode = nodeFactory.CreateNode(building, "Area", "Update_Area") as PropertyNode;

            //Act
            bool added = node.AddPredecessor(predecessorNode);

            //Assert
            Assert.IsTrue(added);
        }

        [TestMethod]
        public void AddPredecessor_GivenNullNode_ThrowsException()
        {
            //Arrange 
            NodeFactory nodeFactory = new StandardNodeFactory();
            Building02 building = new Building02();

            MethodNode node = nodeFactory.CreateNode(building, "Update_Area") as MethodNode;
            MethodNode predecessorNode = null;

            //Act & Assert
            Assert.ThrowsException<ReactiveNodeException>(() => node.AddPredecessor(predecessorNode));
        }

        [TestMethod]
        public void AddPredecessor_GivenSameNode_ThrowsException()
        {
            //Arrange 
            NodeFactory nodeFactory = new StandardNodeFactory();
            Building02 building = new Building02();

            MethodNode node = nodeFactory.CreateNode(building, "Update_Area") as MethodNode;
            MethodNode predecessorNode = node;

            //Act & Assert
            Assert.ThrowsException<ReactiveNodeException>(() => node.AddPredecessor(predecessorNode));
        }

        [TestMethod]
        public void AddPredecessor_GivenAlreadyAddedNode_ReturnsFalse()
        {
            //Arrange 
            NodeFactory nodeFactory = new StandardNodeFactory();
            Building02 building = new Building02();

            MethodNode node = nodeFactory.CreateNode(building, "Update_Volume") as MethodNode;
            MethodNode predecessorNode = nodeFactory.CreateNode(building, "Update_Area") as MethodNode;
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
            NodeFactory nodeFactory = new StandardNodeFactory();
            Building02 building = new Building02();

            MethodNode successorNode = nodeFactory.CreateNode(building, "Update_Volume") as MethodNode;
            MethodNode predecessorNode = nodeFactory.CreateNode(building, "Update_Area") as MethodNode;
            predecessorNode.AddSuccessor(successorNode);

            //Act
            bool hasPredecessor = successorNode.HasPredecessor(predecessorNode);

            //Assert
            Assert.IsTrue(hasPredecessor);
        }

        [TestMethod]
        public void HasPredecessor_GivenPropertyNodeAddedAsSuccessor_ReturnsTrue()
        {
            //Arrange 
            NodeFactory nodeFactory = new StandardNodeFactory();
            Building02 building = new Building02();

            PropertyNode successorNode = nodeFactory.CreateNode(building, "Volume", "Update_Volume") as PropertyNode;
            MethodNode predecessorNode = nodeFactory.CreateNode(building, "Update_Area") as MethodNode;
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
            NodeFactory nodeFactory = new StandardNodeFactory();
            Building02 building = new Building02();

            MethodNode successorNode = nodeFactory.CreateNode(building, "Update_Volume") as MethodNode;
            MethodNode predecessorNode = nodeFactory.CreateNode(building, "Update_Area") as MethodNode;

            //Act
            bool hasPredecessor = successorNode.HasPredecessor(predecessorNode);

            //Assert
            Assert.IsFalse(hasPredecessor);
        }

        [TestMethod]
        public void HasPredecessor_GivenNodeRemovedAsSuccessor_ReturnsFalse()
        {
            //Arrange 
            NodeFactory nodeFactory = new StandardNodeFactory();
            Building02 building = new Building02();

            MethodNode successorNode = nodeFactory.CreateNode(building, "Update_Volume") as MethodNode;
            MethodNode predecessorNode = nodeFactory.CreateNode(building, "Update_Area") as MethodNode;
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
            NodeFactory nodeFactory = new StandardNodeFactory();
            Building02 building = new Building02();

            MethodNode successorNode = nodeFactory.CreateNode(building, "Update_Volume") as MethodNode;
            MethodNode predecessorNode = nodeFactory.CreateNode(building, "Update_Area") as MethodNode;
            successorNode.AddPredecessor(predecessorNode);

            //Act
            bool hasSuccessor = predecessorNode.HasSuccessor(successorNode);

            //Assert
            Assert.IsTrue(hasSuccessor);
        }

        [TestMethod]
        public void HasSuccessor_GivenPropertyNodeAddedAsPredecessor_ReturnsTrue()
        {
            //Arrange 
            NodeFactory nodeFactory = new StandardNodeFactory();
            Building02 building = new Building02();

            MethodNode successorNode = nodeFactory.CreateNode(building, "Update_Volume") as MethodNode;
            PropertyNode predecessorNode = nodeFactory.CreateNode(building, "Area", "Update_Area") as PropertyNode;
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
            NodeFactory nodeFactory = new StandardNodeFactory();
            Building02 building = new Building02();

            MethodNode successorNode = nodeFactory.CreateNode(building, "Update_Volume") as MethodNode;
            MethodNode predecessorNode = nodeFactory.CreateNode(building, "Update_Area") as MethodNode;

            //Act
            bool hasSuccessor = predecessorNode.HasSuccessor(successorNode);

            //Assert
            Assert.IsFalse(hasSuccessor);
        }

        [TestMethod]
        public void HasSuccessor_GivenNodeRemovedAsPredecessor_ReturnsFalse()
        {
            //Arrange 
            NodeFactory nodeFactory = new StandardNodeFactory();
            Building02 building = new Building02();

            MethodNode successorNode = nodeFactory.CreateNode(building, "Update_Volume") as MethodNode;
            MethodNode predecessorNode = nodeFactory.CreateNode(building, "Update_Area") as MethodNode;
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
            NodeFactory nodeFactory = new StandardNodeFactory();
            Building02 building = new Building02();

            MethodNode node = nodeFactory.CreateNode(building, "Update_Volume") as MethodNode;
            MethodNode successorNode = nodeFactory.CreateNode(building, "Update_Area") as MethodNode;

            //Act
            bool added = node.AddSuccessor(successorNode);

            //Assert
            Assert.IsTrue(added);
        }

        [TestMethod]
        public void AddSuccessor_GivenValidPropertyNode_ReturnsTrue()
        {
            //Arrange 
            NodeFactory nodeFactory = new StandardNodeFactory();
            Building02 building = new Building02();

            MethodNode node = nodeFactory.CreateNode(building, "Update_Volume") as MethodNode;
            PropertyNode successorNode = nodeFactory.CreateNode(building, "Area", "Update_Area") as PropertyNode;

            //Act
            bool added = node.AddSuccessor(successorNode);

            //Assert
            Assert.IsTrue(added);
        }

        [TestMethod]
        public void AddSuccessor_GivenNullNode_ThrowsException()
        {
            //Arrange 
            NodeFactory nodeFactory = new StandardNodeFactory();
            Building02 building = new Building02();

            MethodNode node = nodeFactory.CreateNode(building, "Update_Area") as MethodNode;
            MethodNode successorNode = null;

            //Act & Assert
            Assert.ThrowsException<ReactiveNodeException>(() => node.AddSuccessor(successorNode));
        }

        [TestMethod]
        public void AddSuccessor_GivenSameNode_ThrowsException()
        {
            //Arrange 
            NodeFactory nodeFactory = new StandardNodeFactory();
            Building02 building = new Building02();

            MethodNode node = nodeFactory.CreateNode(building, "Update_Area") as MethodNode;
            MethodNode successorNode = node;

            //Act & Assert
            Assert.ThrowsException<ReactiveNodeException>(() => node.AddSuccessor(successorNode));
        }

        [TestMethod]
        public void AddSuccessor_GivenAlreadyAddedNode_ReturnsFalse()
        {
            //Arrange 
            NodeFactory nodeFactory = new StandardNodeFactory();
            Building02 building = new Building02();

            MethodNode node = nodeFactory.CreateNode(building, "Update_Area") as MethodNode;
            MethodNode successorNode = nodeFactory.CreateNode(building, "Update_Volume") as MethodNode;
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
            NodeFactory nodeFactory = new StandardNodeFactory();
            Building02 building = new Building02();

            MethodNode node = nodeFactory.CreateNode(building, "Update_Area") as MethodNode;
            MethodNode predecessorNode = nodeFactory.CreateNode(building, "Update_Volume") as MethodNode;
            node.AddPredecessor(predecessorNode);

            //Act
            bool removed = node.RemovePredecessor(predecessorNode);

            //Assert
            Assert.IsTrue(removed);
        }

        [TestMethod]
        public void RemovePredecessor_GivenValidPropertyNodePredecessor_ReturnsTrue()
        {
            //Arrange 
            NodeFactory nodeFactory = new StandardNodeFactory();
            Building02 building = new Building02();

            MethodNode node = nodeFactory.CreateNode(building, "Update_Area") as MethodNode;
            PropertyNode predecessorNode = nodeFactory.CreateNode(building, "Volume", "Update_Volume") as PropertyNode;
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
            NodeFactory nodeFactory = new StandardNodeFactory();
            Building02 building = new Building02();

            MethodNode node = nodeFactory.CreateNode(building, "Update_Area") as MethodNode;
            MethodNode predecessorNode = nodeFactory.CreateNode(building, "Update_Volume") as MethodNode;

            //Act
            bool removed = node.RemovePredecessor(predecessorNode);

            //Assert
            Assert.IsFalse(removed);
        }

        [TestMethod]
        public void RemovePredecessor_GivenNullPredecessor_ReturnsFalse()
        {
            //Arrange 
            NodeFactory nodeFactory = new StandardNodeFactory();
            Building02 building = new Building02();

            MethodNode node = nodeFactory.CreateNode(building, "Update_Area") as MethodNode;
            MethodNode predecessorNode = null;

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
            NodeFactory nodeFactory = new StandardNodeFactory();
            Building02 building = new Building02();

            MethodNode node = nodeFactory.CreateNode(building, "Update_Area") as MethodNode;
            MethodNode successorNode = nodeFactory.CreateNode(building, "Update_Volume") as MethodNode;
            node.AddSuccessor(successorNode);

            //Act
            bool removed = node.RemoveSuccessor(successorNode);

            //Assert
            Assert.IsTrue(removed);
        }

        [TestMethod]
        public void RemoveSuccessor_GivenValidPropertyNodeSuccessor_ReturnsTrue()
        {
            //Arrange 
            NodeFactory nodeFactory = new StandardNodeFactory();
            Building02 building = new Building02();

            MethodNode node = nodeFactory.CreateNode(building, "Update_Area") as MethodNode;
            PropertyNode successorNode = nodeFactory.CreateNode(building, "Volume", "Update_Volume") as PropertyNode;
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
            NodeFactory nodeFactory = new StandardNodeFactory();
            Building02 building = new Building02();

            MethodNode node = nodeFactory.CreateNode(building, "Update_Area") as MethodNode;
            MethodNode successorNode = nodeFactory.CreateNode(building, "Update_Volume") as MethodNode;

            //Act
            bool removed = node.RemoveSuccessor(successorNode);

            //Assert
            Assert.IsFalse(removed);
        }

        [TestMethod]
        public void RemoveSuccessor_GivenNullSuccessor_ReturnsFalse()
        {
            //Arrange 
            NodeFactory nodeFactory = new StandardNodeFactory();
            Building02 building = new Building02();

            MethodNode node = nodeFactory.CreateNode(building, "Update_Area") as MethodNode;
            MethodNode successorNode = null;

            //Act
            bool removed = node.RemoveSuccessor(successorNode);

            //Assert
            Assert.IsFalse(removed);
        }

        #endregion

        #region IsValueChanged

        [TestMethod]
        public void IsValueChanged_ReturnTrue()
        {
            //Arrange
            Building00 building = new Building00();
            MethodNode node = new MethodNode(building, "Update_Area");

            //Act
            bool isChanged = node.IsValueChanged();

            //Assert
            Assert.IsTrue(isChanged);
        }

        #endregion
    }
}
