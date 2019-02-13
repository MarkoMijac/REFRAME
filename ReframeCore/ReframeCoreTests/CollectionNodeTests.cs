using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeCore;
using ReframeCore.Exceptions;
using ReframeCore.Nodes;
using ReframeCore.ReactiveCollections;
using ReframeCoreExamples.E07;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCoreTests
{
    [TestClass]
    public class CollectionNodeTests
    {
        #region NodeCreation

        [TestMethod]
        public void NodeCreation_GivenCorrectArguments_CreatesSourceNode()
        {
            //Arrange
            Whole whole = new Whole { Name = "Whole 1"};

            //Act
            INode partsNode = new CollectionNode<Part>(whole.Parts, "A");

            //Assert
            Assert.IsInstanceOfType(partsNode, typeof(INode));
        }

        [TestMethod]
        public void NodeCreation_CreatedSourceNode_HoldsProperData()
        {
            //Arrange
            Whole whole = new Whole { Name = "Whole 1" };
            ReactiveCollection<Part> parts = whole.Parts;

            //Act
            INode partsNode = new CollectionNode<Part>(parts, "A");

            //Assert
            Assert.IsTrue(partsNode.OwnerObject == parts && partsNode.MemberName == "A");
        }

        [TestMethod]
        public void NodeCreation_SourceNodeUpdateInvoke_DoesNotBreak()
        {
            //Arrange
            Whole whole = new Whole { Name = "Whole 1" };

            try
            {
                //Act
                INode partsNode = new CollectionNode<Part>(whole.Parts, "A");
                partsNode.Update();
            }
            catch (Exception ex)
            {
                //Assert
                Assert.Fail("Expected no exception, instead got: " + ex.Message);
            }
        }

        [TestMethod]
        public void NodeCreation_GivenNullObject_ThrowsException()
        {
            //Arrange
            Whole whole = new Whole { Name = "Whole 1" };
            ReactiveCollection<Part> parts = null;

            //Act & Assert
            Assert.ThrowsException<ReactiveNodeException>(() => new CollectionNode<Part>(parts, "A"));
        }

        [TestMethod]
        public void NodeCreation_GivenEmptyReactiveCollection_CreatesNode()
        {
            //Arrange
            ReactiveCollection<Part> collection = new ReactiveCollection<Part>();

            //Act
            INode node = new CollectionNode<Part>(collection, "A");

            //Assert
            //Assert
            Assert.IsInstanceOfType(node, typeof(INode));
        }

        [TestMethod]
        public void NodeCreation_GivenNonExistantMemberName_ThrowsException()
        {
            //Arrange
            Whole whole = new Whole { Name = "Whole 1" };

            //Act & Assert
            Assert.ThrowsException<ReactiveNodeException>(() => new CollectionNode<Part>(whole.Parts, "AA"));
        }

        [TestMethod]
        public void NodeCreation_CreatedReactiveNode_ObtainedIdentifier()
        {
            //Arrange
            Whole whole = new Whole { Name = "Whole 1" };

            //Act
            INode partsNode = new CollectionNode<Part>(whole.Parts, "A");

            //Assert
            Assert.AreNotEqual(default(uint), partsNode.Identifier);
        }

        #endregion

        #region HasSameIdentifier

        [TestMethod]
        public void HasSameIdentifier_NodesPointingAtTheSameObjectAndMember_ReturnsTrue()
        {
            //Arrange
            Whole whole = new Whole();
            const string memberName = "A";
            INode node1 = new CollectionNode<Part>(whole.Parts, memberName);
            INode node2 = new CollectionNode<Part>(whole.Parts, memberName);

            //Act
            bool same = node1.HasSameIdentifier(node2);

            //Assert
            Assert.IsTrue(same);
        }

        [TestMethod]
        public void HasSameIdentifier_NodesPointingAtTheSameObjectAndMember1_ReturnsTrue()
        {
            //Arrange
            Whole whole = new Whole();
            const string memberName = "A";

            INode node1 = new CollectionNode<Part>(whole.Parts, memberName);

            //Act
            bool same = node1.HasSameIdentifier(whole.Parts, memberName);

            //Assert
            Assert.IsTrue(same);
        }

        [TestMethod]
        public void HasSameIdentifier_NodesPointingAtSameObjectAndDifferentMember_ReturnsFalse()
        {
            //Arrange
            Whole whole = new Whole();
            const string memberName = "A";
            const string memberName2 = "B";

            INode node1 = new CollectionNode<Part>(whole.Parts, memberName);
            INode node2 = new CollectionNode<Part>(whole.Parts, memberName2);

            //Act
            bool same = node1.HasSameIdentifier(node2);

            //Assert
            Assert.IsFalse(same);
        }

        [TestMethod]
        public void HasSameIdentifier_NodesPointingAtSameObjectAndDifferentMember1_ReturnsFalse()
        {
            //Arrange
            Whole whole = new Whole();
            const string memberName = "A";
            const string memberName2 = "B";

            INode node1 = new CollectionNode<Part>(whole.Parts, memberName);

            //Act
            bool same = node1.HasSameIdentifier(whole.Parts, memberName2);

            //Assert
            Assert.IsFalse(same);
        }

        [TestMethod]
        public void HasSameIdentifier_NodesPointingAtDifferentObjectAndSameMember_ReturnsFalse()
        {
            //Arrange
            Whole whole1 = new Whole();
            Whole whole2 = new Whole();
            const string memberName = "A";

            INode node1 = new CollectionNode<Part>(whole1.Parts, memberName);
            INode node2 = new CollectionNode<Part>(whole2.Parts, memberName);

            //Act
            bool same = node1.HasSameIdentifier(node2);

            //Assert
            Assert.IsFalse(same);
        }

        [TestMethod]
        public void HasSameIdentifier_NodesPointingAtDifferentObjectAndSameMember1_ReturnsFalse()
        {
            //Arrange
            Whole whole1 = new Whole();
            Whole whole2 = new Whole();
            const string memberName = "A";

            INode node1 = new CollectionNode<Part>(whole1.Parts, memberName);

            //Act
            bool same = node1.HasSameIdentifier(whole2.Parts, memberName);

            //Assert
            Assert.IsFalse(same);
        }

        #endregion

        #region AddPredecessor

        [TestMethod]
        public void AddPredecessor_GivenAnotherValidPropertyNode_ReturnsTrue()
        {
            //Arrange
            Whole whole = new Whole();
            string memberName = "A";

            INode node = new CollectionNode<Part>(whole.Parts, memberName);
            INode predecessorNode = new PropertyNode(whole, "A");

            //Act
            bool added = node.AddPredecessor(predecessorNode);

            //Assert
            Assert.IsTrue(added);
        }

        [TestMethod]
        public void AddPredecessor_GivenAnotherValidCollectionNode_ReturnsTrue()
        {
            //Arrange
            ReactiveCollection<Whole> wholes = new ReactiveCollection<Whole>();
            ReactiveCollection<Part> parts = new ReactiveCollection<Part>();
            string memberName = "A";

            INode node = new CollectionNode<Whole>(wholes, memberName);
            INode predecessorNode = new CollectionNode<Part>(parts, memberName);

            //Act
            bool added = node.AddPredecessor(predecessorNode);

            //Assert
            Assert.IsTrue(added);
        }

        [TestMethod]
        public void AddPredecessor_GivenNullNode_ThrowsException()
        {
            //Arrange
            Whole whole = new Whole();
            string memberName = "A";

            INode node = new CollectionNode<Part>(whole.Parts, memberName);
            INode predecessorNode = null;

            //Act&Assert
            Assert.ThrowsException<ReactiveNodeException>(() => node.AddPredecessor(predecessorNode));
        }

        [TestMethod]
        public void AddPredecessor_GivenSameNode_ThrowsException()
        {
            //Arrange
            Whole whole = new Whole();
            string memberName = "A";

            INode node = new CollectionNode<Part>(whole.Parts, memberName);
            INode predecessorNode = node;

            //Act & Assert
            Assert.ThrowsException<ReactiveNodeException>(() => node.AddPredecessor(predecessorNode));
        }

        [TestMethod]
        public void AddPredecessor_GivenAlreadyAddedNode_ReturnsFalse()
        {
            //Arrange
            Whole whole = new Whole();
            string memberName = "A";

            INode node = new CollectionNode<Part>(whole.Parts, memberName);
            INode predecessorNode = new PropertyNode(whole, "A");
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
            Whole whole = new Whole();
            string memberName = "A";

            INode node = new CollectionNode<Part>(whole.Parts, memberName);
            INode predecessorNode = new MethodNode(whole, "Update_A");

            //Act
            bool added = node.AddPredecessor(predecessorNode);

            //Assert
            Assert.IsTrue(added);
        }

        #endregion

        #region AddSuccessor

        [TestMethod]
        public void AddSuccessor_GivenAnotherValidPropertyNode_ReturnsTrue()
        {
            //Arrange
            Whole whole = new Whole();
            string memberName = "A";

            INode node = new CollectionNode<Part>(whole.Parts, memberName);
            INode successorNode = new PropertyNode(whole, "A");

            //Act
            bool added = node.AddSuccessor(successorNode);

            //Assert
            Assert.IsTrue(added);
        }

        [TestMethod]
        public void AddSuccessor_GivenAnotherValidCollectionNode_ReturnsTrue()
        {
            //Arrange
            ReactiveCollection<Whole> wholes = new ReactiveCollection<Whole>();
            ReactiveCollection<Part> parts = new ReactiveCollection<Part>();
            string memberName = "A";

            INode node = new CollectionNode<Whole>(wholes, memberName);
            INode successorNode = new CollectionNode<Part>(parts, memberName);

            //Act
            bool added = node.AddSuccessor(successorNode);

            //Assert
            Assert.IsTrue(added);
        }

        [TestMethod]
        public void AddSuccessor_GivenNullNode_ThrowsException()
        {
            //Arrange
            Whole whole = new Whole();
            string memberName = "A";

            INode node = new CollectionNode<Part>(whole.Parts, memberName);
            INode successorNode = null;

            //Act&Assert
            Assert.ThrowsException<ReactiveNodeException>(() => node.AddSuccessor(successorNode));
        }

        [TestMethod]
        public void AddSuccessor_GivenSameNode_ThrowsException()
        {
            //Arrange
            Whole whole = new Whole();
            string memberName = "A";

            INode node = new CollectionNode<Part>(whole.Parts, memberName);
            INode successorNode = node;

            //Act & Assert
            Assert.ThrowsException<ReactiveNodeException>(() => node.AddSuccessor(successorNode));
        }

        [TestMethod]
        public void AddSuccessor_GivenAlreadyAddedNode_ReturnsFalse()
        {
            //Arrange
            Whole whole = new Whole();
            string memberName = "A";

            INode node = new CollectionNode<Part>(whole.Parts, memberName);
            INode successorNode = new PropertyNode(whole, "A");
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
            Whole whole = new Whole();
            string memberName = "A";

            INode node = new CollectionNode<Part>(whole.Parts, memberName);
            INode successorNode = new MethodNode(whole, "Update_A");

            //Act
            bool added = node.AddSuccessor(successorNode);

            //Assert
            Assert.IsTrue(added);
        }

        #endregion

        #region HasPredecessor

        [TestMethod]
        public void HasPredecessor_GivenNodeAddedAsSuccessor_ReturnsTrue()
        {
            //Arrange
            Whole whole = new Whole();
            string memberName = "A";
            string memberName2 = "A";

            INode successorNode = new CollectionNode<Part>(whole.Parts, memberName);
            INode predecessorNode = new PropertyNode(whole, memberName2);
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
            Whole whole = new Whole();
            string memberName = "A";
            string memberName2 = "A";

            INode successorNode = new CollectionNode<Part>(whole.Parts, memberName);
            INode predecessorNode = new PropertyNode(whole, memberName2);

            //Act
            bool hasPredecessor = successorNode.HasPredecessor(predecessorNode);

            //Assert
            Assert.IsFalse(hasPredecessor);
        }

        [TestMethod]
        public void HasPredecessor_GivenNodeRemovedAsSuccessor_ReturnsFalse()
        {
            //Arrange
            Whole whole = new Whole();
            string memberName = "A";
            string memberName2 = "A";

            INode successorNode = new CollectionNode<Part>(whole.Parts, memberName);
            INode predecessorNode = new PropertyNode(whole, memberName2);
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
            Whole whole = new Whole();
            string memberName = "A";
            string memberName2 = "Update_A";

            INode successorNode = new CollectionNode<Part>(whole.Parts, memberName);
            INode predecessorNode = new MethodNode(whole, memberName2);
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
            Whole whole = new Whole();
            string memberName = "A";
            string memberName2 = "A";

            INode predecessorNode = new CollectionNode<Part>(whole.Parts, memberName);
            INode successorNode = new PropertyNode(whole, memberName2);
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
            Whole whole = new Whole();
            string memberName = "A";
            string memberName2 = "A";

            INode predecessorNode = new CollectionNode<Part>(whole.Parts, memberName);
            INode successorNode = new PropertyNode(whole, memberName2);

            //Act
            bool hasSuccessor = predecessorNode.HasSuccessor(successorNode);

            //Assert
            Assert.IsFalse(hasSuccessor);
        }

        [TestMethod]
        public void HasSuccessor_GivenNodeRemovedAsPredecessor_ReturnsFalse()
        {
            //Arrange
            Whole whole = new Whole();
            string memberName = "A";
            string memberName2 = "A";

            INode predecessorNode = new CollectionNode<Part>(whole.Parts, memberName);
            INode successorNode = new PropertyNode(whole, memberName2);
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
            Whole whole = new Whole();
            string memberName = "A";
            string memberName2 = "Update_A";

            INode predecessorNode = new CollectionNode<Part>(whole.Parts, memberName);
            INode successorNode = new MethodNode(whole, memberName2);
            successorNode.AddPredecessor(predecessorNode);

            //Act
            bool hasSuccessor = predecessorNode.HasSuccessor(successorNode);

            //Assert
            Assert.IsTrue(hasSuccessor);
        }

        #endregion

        #region RemovePredecessor

        [TestMethod]
        public void RemovePredecessor_GivenValidPredecessor_ReturnsTrue()
        {
            //Arrange
            Whole whole = new Whole();
            string memberName = "A";
            string memberName2 = "A";

            INode node = new CollectionNode<Part>(whole.Parts, memberName);
            INode predecessorNode = new PropertyNode(whole, memberName2);
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
            Whole whole = new Whole();
            string memberName = "A";
            string memberName2 = "A";

            INode node = new CollectionNode<Part>(whole.Parts, memberName);
            INode predecessorNode = new PropertyNode(whole, memberName2);

            //Act
            bool removed = node.RemovePredecessor(predecessorNode);

            //Assert
            Assert.IsFalse(removed);
        }

        [TestMethod]
        public void RemovePredecessor_GivenNullPredecessor_ReturnsFalse()
        {
            //Arrange
            Whole whole = new Whole();
            string memberName = "A";

            INode node = new CollectionNode<Part>(whole.Parts, memberName);
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
            Whole whole = new Whole();
            string memberName = "A";
            string memberName2 = "Update_A";

            INode node = new CollectionNode<Part>(whole.Parts, memberName);
            INode predecessorNode = new MethodNode(whole, memberName2);
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
            Whole whole = new Whole();
            string memberName = "A";
            string memberName2 = "A";

            INode node = new CollectionNode<Part>(whole.Parts, memberName);
            INode successorNode = new PropertyNode(whole, memberName2);
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
            Whole whole = new Whole();
            string memberName = "A";
            string memberName2 = "A";

            INode node = new CollectionNode<Part>(whole.Parts, memberName);
            INode successorNode = new PropertyNode(whole, memberName2);

            //Act
            bool removed = node.RemoveSuccessor(successorNode);

            //Assert
            Assert.IsFalse(removed);
        }

        [TestMethod]
        public void RemoveSuccessor_GivenNullSuccessor_ReturnsFalse()
        {
            //Arrange
            Whole whole = new Whole();
            string memberName = "A";
            string memberName2 = "A";

            INode node = new CollectionNode<Part>(whole.Parts, memberName);
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
            Whole whole = new Whole();
            string memberName = "A";
            string memberName2 = "Update_A";

            INode node = new CollectionNode<Part>(whole.Parts, memberName);
            INode successorNode = new MethodNode(whole, memberName2);
            node.AddSuccessor(successorNode);

            //Act
            bool removed = node.RemoveSuccessor(successorNode);

            //Assert
            Assert.IsTrue(removed);
        }

        #endregion
    }
}
