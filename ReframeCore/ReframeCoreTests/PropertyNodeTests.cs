using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeCoreExamples.E00;
using ReframeCore;
using ReframeCore.Exceptions;
using ReframeCore.Nodes;
using ReframeCore.Helpers;
using ReframeCore.Factories;

namespace ReframeCoreTests
{
    [TestClass]
    public class PropertyNodeTests
    {
        #region UpdateInvoke

        [TestMethod]
        public void NodeCreation_GivenMethodNameInsteadOfPropertyName()
        {
            //Arrange
            Building00 building00 = new Building00();
            string propertyName = "Update_Area";

            //Act & Assert
            Assert.ThrowsException<ReactiveNodeException>(() => new PropertyNode(building00, propertyName));
        }

        [TestMethod]
        public void NodeCreation_SourceNodeUpdateInvoke_DoesNotBreak()
        {
            //Arrange
            NodeFactory nodeFactory = new StandardNodeFactory();
            Building00 building = new Building00();
            string memberName = "Width";

            try
            {
                //Act
                PropertyNode widthNode = nodeFactory.CreateNode(building, memberName) as PropertyNode;
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
            NodeFactory nodeFactory = new StandardNodeFactory();
            Building00 building00 = new Building00();
            string memberName = "Width";

            //Act
            PropertyNode node = nodeFactory.CreateNode(building00, memberName) as PropertyNode;

            //Assert
            Assert.AreNotEqual(default(uint), node.Identifier);
        }

        [TestMethod]
        public void HasSameIdentifier_NodesPointingAtTheSameObjectAndMember_ReturnsTrue()
        {
            //Arrange
            NodeFactory nodeFactory = new StandardNodeFactory();
            Building00 building00 = new Building00();
            string memberName = "Width";

            PropertyNode node1 = nodeFactory.CreateNode(building00, memberName) as PropertyNode;
            PropertyNode node2 = nodeFactory.CreateNode(building00, memberName) as PropertyNode;

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
            Building00 building00 = new Building00();
            string memberName = "Width";

            PropertyNode node1 = nodeFactory.CreateNode(building00, memberName) as PropertyNode;

            //Act
            bool same = node1.HasSameIdentifier(building00, memberName);

            //Assert
            Assert.IsTrue(same);
        }

        [TestMethod]
        public void HasSameIdentifier_NodesPointingAtSameObjectAndDifferentMember_ReturnsFalse()
        {
            //Arrange
            NodeFactory nodeFactory = new StandardNodeFactory();
            Building00 building00 = new Building00();
            string memberName = "Width";
            string memberName2 = "Length";

            PropertyNode node1 = nodeFactory.CreateNode(building00, memberName) as PropertyNode;
            PropertyNode node2 = nodeFactory.CreateNode(building00, memberName2) as PropertyNode;

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
            Building00 building00 = new Building00();
            string memberName = "Width";
            string memberName2 = "Length";

            PropertyNode node1 = nodeFactory.CreateNode(building00, memberName) as PropertyNode;

            //Act
            bool same = node1.HasSameIdentifier(building00, memberName2);

            //Assert
            Assert.IsFalse(same);
        }

        [TestMethod]
        public void HasSameIdentifier_NodesPointingAtDifferentObjectAndSameMember_ReturnsFalse()
        {
            //Arrange
            NodeFactory nodeFactory = new StandardNodeFactory();
            Building00 building1 = new Building00();
            Building00 building2 = new Building00();

            string memberName = "Width";

            PropertyNode node1 = nodeFactory.CreateNode(building1, memberName) as PropertyNode;
            PropertyNode node2 = nodeFactory.CreateNode(building2, memberName) as PropertyNode;

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
            Building00 building1 = new Building00();
            Building00 building2 = new Building00();

            string memberName = "Width";

            PropertyNode node1 = nodeFactory.CreateNode(building1, memberName) as PropertyNode;

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
            Building00 building = new Building00();
            string memberName = "Width";

            PropertyNode node = nodeFactory.CreateNode(building, memberName) as PropertyNode;

            //Act
            bool same = node.HasSameIdentifier(null, "Width");

            //Assert
            Assert.IsFalse(same);
        }

        [TestMethod]
        public void HasSameIdentifier_EmptyMemberNameProvided_ReturnsFalse()
        {
            //Arrange
            NodeFactory nodeFactory = new StandardNodeFactory();
            Building00 building = new Building00();
            string memberName = "Width";

            PropertyNode node = nodeFactory.CreateNode(building, memberName) as PropertyNode;

            //Act
            bool same = node.HasSameIdentifier(new Building00(), "");

            //Assert
            Assert.IsFalse(same);
        }

        [TestMethod]
        public void GetIdentifier_GivenObjectChangesItsState_IdentifierRemainsTheSame()
        {
            //Arrange
            Building00 b = new Building00();
            NodeFactory nodeFactory = new StandardNodeFactory();

            b.Width = 100;
            INode node = nodeFactory.CreateNode(b, "Width");

            //Act
            PrivateObject privateObject1 = new PrivateObject(node, new PrivateType(typeof(Node)));
            uint identifier1 = (uint)privateObject1.Invoke("GenerateIdentifier");

            b.Width = 2000;
            INode node2 = nodeFactory.CreateNode(b, "Width");
            PrivateObject privateObject2 = new PrivateObject(node2, new PrivateType(typeof(Node)));
            uint identifier2 = (uint)privateObject2.Invoke("GenerateIdentifier");

            //Assert
            Assert.AreEqual(identifier1, identifier2);
        }

        #endregion

        #region AddPredecessor

        [TestMethod]
        public void AddPredecessor_GivenAnotherValidNode_ReturnsTrue()
        {
            //Arrange
            NodeFactory nodeFactory = new StandardNodeFactory();
            Building00 building00 = new Building00();
            string propertyName = "Area";
            string updateMethodName = "Update_Area";

            string predecessorPropertyName = "Width";

            PropertyNode node = nodeFactory.CreateNode(building00, propertyName, updateMethodName) as PropertyNode;
            PropertyNode predecessorNode = nodeFactory.CreateNode(building00, predecessorPropertyName) as PropertyNode;

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
            Building00 building00 = new Building00();
            string propertyName = "Area";
            string updateMethodName = "Update_Area";

            PropertyNode node = nodeFactory.CreateNode(building00, propertyName, updateMethodName) as PropertyNode;
            PropertyNode predecessorNode = null;

            //Act & Assert
            Assert.ThrowsException<ReactiveNodeException>(() => node.AddPredecessor(predecessorNode));
        }

        [TestMethod]
        public void AddPredecessor_GivenSameNode_ThrowsException()
        {
            //Arrange
            NodeFactory nodeFactory = new StandardNodeFactory();
            Building00 building00 = new Building00();
            string propertyName = "Area";
            string updateMethodName = "Update_Area";

            INode node = nodeFactory.CreateNode(building00, propertyName, updateMethodName);
            INode predecessorNode = nodeFactory.CreateNode(building00, propertyName, updateMethodName);

            //Act & Assert
            Assert.ThrowsException<ReactiveNodeException>(() => node.AddPredecessor(predecessorNode));
        }

        [TestMethod]
        public void AddPredecessor_GivenAlreadyAddedNode_ReturnsFalse()
        {
            //Arrange
            NodeFactory nodeFactory = new StandardNodeFactory();
            Building00 building00 = new Building00();
            string propertyName = "Area";
            string updateMethodName = "Update_Area";

            string predecessorPropertyName = "Width";

            PropertyNode node = nodeFactory.CreateNode(building00, propertyName, updateMethodName) as PropertyNode;
            PropertyNode predecessorNode = nodeFactory.CreateNode(building00, predecessorPropertyName) as PropertyNode;
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
            NodeFactory nodeFactory = new StandardNodeFactory();
            Building00 building00 = new Building00();

            PropertyNode node = nodeFactory.CreateNode(building00, "Volume", "Update_Volume") as PropertyNode;
            MethodNode predecessorNode = nodeFactory.CreateNode(building00, "Update_Area") as MethodNode;

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
            NodeFactory nodeFactory = new StandardNodeFactory();
            Building00 building00 = new Building00();
            string propertyName = "Area";
            string updateMethodName = "Update_Area";

            string predecessorPropertyName = "Width";

            PropertyNode successorNode = nodeFactory.CreateNode(building00, propertyName, updateMethodName) as PropertyNode;
            PropertyNode predecessorNode = nodeFactory.CreateNode(building00, predecessorPropertyName) as PropertyNode;
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
            Building00 building00 = new Building00();
            string propertyName = "Area";
            string updateMethodName = "Update_Area";

            string predecessorPropertyName = "Width";

            PropertyNode successorNode = nodeFactory.CreateNode(building00, propertyName, updateMethodName) as PropertyNode;
            PropertyNode predecessorNode = nodeFactory.CreateNode(building00, predecessorPropertyName) as PropertyNode;

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
            Building00 building00 = new Building00();
            string propertyName = "Area";
            string updateMethodName = "Update_Area";

            string predecessorPropertyName = "Width";

            PropertyNode successorNode = nodeFactory.CreateNode(building00, propertyName, updateMethodName) as PropertyNode;
            PropertyNode predecessorNode = nodeFactory.CreateNode(building00, predecessorPropertyName) as PropertyNode;
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
            NodeFactory nodeFactory = new StandardNodeFactory();
            Building00 building00 = new Building00();

            PropertyNode predecessorNode = nodeFactory.CreateNode(building00, "Volume", "Update_Volume") as PropertyNode;
            MethodNode successorNode = nodeFactory.CreateNode(building00, "Update_Area") as MethodNode;
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
            NodeFactory nodeFactory = new StandardNodeFactory();
            Building00 building00 = new Building00();
            string propertyName = "Area";
            string updateMethodName = "Update_Area";

            string predecessorPropertyName = "Width";

            PropertyNode successorNode = nodeFactory.CreateNode(building00, propertyName, updateMethodName) as PropertyNode;
            PropertyNode predecessorNode = nodeFactory.CreateNode(building00, predecessorPropertyName) as PropertyNode;
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
            Building00 building00 = new Building00();
            string propertyName = "Area";
            string updateMethodName = "Update_Area";

            string predecessorPropertyName = "Width";

            PropertyNode successorNode = nodeFactory.CreateNode(building00, propertyName, updateMethodName) as PropertyNode;
            PropertyNode predecessorNode = nodeFactory.CreateNode(building00, predecessorPropertyName) as PropertyNode;

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
            Building00 building00 = new Building00();
            string propertyName = "Area";
            string updateMethodName = "Update_Area";

            string predecessorPropertyName = "Width";

            PropertyNode successorNode = nodeFactory.CreateNode(building00, propertyName, updateMethodName) as PropertyNode;
            PropertyNode predecessorNode = nodeFactory.CreateNode(building00, predecessorPropertyName) as PropertyNode;
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
            NodeFactory nodeFactory = new StandardNodeFactory();
            Building00 building00 = new Building00();

            PropertyNode successorNode = nodeFactory.CreateNode(building00, "Area", "Update_Area") as PropertyNode;
            MethodNode predecessorNode = nodeFactory.CreateNode(building00, "Update_Volume") as MethodNode;
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
            NodeFactory nodeFactory = new StandardNodeFactory();
            Building00 building00 = new Building00();
            string propertyName = "Width";

            string successorPropertyName = "Area";
            string successorUpdateMethodName = "Update_Area";

            PropertyNode node = nodeFactory.CreateNode(building00, propertyName) as PropertyNode;
            PropertyNode successorNode = nodeFactory.CreateNode(building00, successorPropertyName, successorUpdateMethodName) as PropertyNode;

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
            Building00 building00 = new Building00();
            string propertyName = "Width";

            PropertyNode node = nodeFactory.CreateNode(building00, propertyName) as PropertyNode;
            PropertyNode successorNode = null;

            //Act & Assert
            Assert.ThrowsException<ReactiveNodeException>(() => node.AddSuccessor(successorNode));
        }

        [TestMethod]
        public void AddSuccessor_GivenSameNode_ThrowsException()
        {
            //Arrange
            NodeFactory nodeFactory = new StandardNodeFactory();
            Building00 building00 = new Building00();
            string propertyName = "Width";

            PropertyNode node = nodeFactory.CreateNode(building00, propertyName) as PropertyNode;
            PropertyNode successorNode = node;

            //Act & Assert
            Assert.ThrowsException<ReactiveNodeException>(() => node.AddSuccessor(successorNode));
        }

        [TestMethod]
        public void AddSuccessor_GivenAlreadyAddedNode_ReturnsFalse()
        {
            //Arrange
            NodeFactory nodeFactory = new StandardNodeFactory();
            Building00 building00 = new Building00();
            string propertyName = "Width";

            string successorPropertyName = "Area";
            string successorUpdateMethodName = "Update_Area";

            PropertyNode node = nodeFactory.CreateNode(building00, propertyName) as PropertyNode;
            PropertyNode successorNode = nodeFactory.CreateNode(building00, successorPropertyName, successorUpdateMethodName) as PropertyNode;
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
            NodeFactory nodeFactory = new StandardNodeFactory();
            Building00 building00 = new Building00();

            MethodNode node = nodeFactory.CreateNode(building00, "Update_Area") as MethodNode;
            PropertyNode successorNode = nodeFactory.CreateNode(building00, "Volume", "Update_Volume") as PropertyNode;

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
            NodeFactory nodeFactory = new StandardNodeFactory();
            Building00 building00 = new Building00();
            string propertyName = "Area";
            string updateMethodName = "Update_Area";

            string predecessorPropertyName = "Width";

            PropertyNode node = nodeFactory.CreateNode(building00, propertyName, updateMethodName) as PropertyNode;
            PropertyNode predecessorNode = nodeFactory.CreateNode(building00, predecessorPropertyName) as PropertyNode;
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
            Building00 building00 = new Building00();
            string propertyName = "Area";
            string updateMethodName = "Update_Area";

            string predecessorPropertyName = "Width";

            PropertyNode node = nodeFactory.CreateNode(building00, propertyName, updateMethodName) as PropertyNode;
            PropertyNode predecessorNode = nodeFactory.CreateNode(building00, predecessorPropertyName) as PropertyNode;

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
            Building00 building00 = new Building00();
            string propertyName = "Area";
            string updateMethodName = "Update_Area";

            PropertyNode node = nodeFactory.CreateNode(building00, propertyName, updateMethodName) as PropertyNode;
            PropertyNode predecessorNode = null;

            //Act
            bool removed = node.RemovePredecessor(predecessorNode);

            //Assert
            Assert.IsFalse(removed);
        }

        [TestMethod]
        public void RemovePredecessor_GivenValidMethodNodePredecessor_ReturnsTrue()
        {
            //Arrange
            NodeFactory nodeFactory = new StandardNodeFactory();
            Building00 building00 = new Building00();

            PropertyNode node = nodeFactory.CreateNode(building00, "Volume", "Update_Volume") as PropertyNode;
            MethodNode predecessorNode = nodeFactory.CreateNode(building00, "Update_Area") as MethodNode;
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
            NodeFactory nodeFactory = new StandardNodeFactory();
            Building00 building00 = new Building00();
            string propertyName = "Width";
            

            string successorPropertyName = "Area";
            string updateMethodName = "Update_Area";

            PropertyNode node = nodeFactory.CreateNode(building00, propertyName) as PropertyNode;
            PropertyNode successorNode = nodeFactory.CreateNode(building00, successorPropertyName, updateMethodName) as PropertyNode;
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
            Building00 building00 = new Building00();
            string propertyName = "Width";


            string successorPropertyName = "Area";
            string updateMethodName = "Update_Area";

            PropertyNode node = nodeFactory.CreateNode(building00, propertyName) as PropertyNode;
            PropertyNode successorNode = nodeFactory.CreateNode(building00, successorPropertyName, updateMethodName) as PropertyNode;

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
            Building00 building00 = new Building00();
            string propertyName = "Width";

            PropertyNode node = nodeFactory.CreateNode(building00, propertyName) as PropertyNode;
            PropertyNode successorNode = null;

            //Act
            bool removed = node.RemoveSuccessor(successorNode);

            //Assert
            Assert.IsFalse(removed);
        }

        [TestMethod]
        public void RemoveSuccessor_GivenValidMethodNodeSuccessor_ReturnsTrue()
        {
            //Arrange
            NodeFactory nodeFactory = new StandardNodeFactory();
            Building00 building00 = new Building00();

            PropertyNode node = nodeFactory.CreateNode(building00, "Width") as PropertyNode;
            MethodNode successorNode = nodeFactory.CreateNode(building00, "Update_Area") as MethodNode;
            node.AddSuccessor(successorNode);

            //Act
            bool removed = node.RemoveSuccessor(successorNode);

            //Assert
            Assert.IsTrue(removed);
        }

        #endregion

        #region IsValueChanged

        [TestMethod]
        public void IsValueChanged_GivenValueIsChanged_ReturnsTrue()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("R1");
            Building00 building = new Building00() { Width = 10 };
            INode node = new PropertyNode(building, "Width");
            reactor.AddNode(node);
            reactor.PerformUpdate(node);

            //Act
            building.Width = 12;
            reactor.PerformUpdate(node);

            //Assert
            Assert.IsTrue(node.IsTriggered());
        }

        [TestMethod]
        public void IsValueChanged_GivenValueIsSame_ReturnsFalse()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("R1");
            Building00 building = new Building00() { Width = 10 };
            INode node = new PropertyNode(building, "Width");
            reactor.AddNode(node);
            reactor.PerformUpdate(node);

            //Act
            building.Width = 10;
            reactor.PerformUpdate(node);

            //Assert
            Assert.IsFalse(node.IsTriggered());
        }

        [TestMethod]
        public void IsValueChanged_GivenNoLaterActionMade_ReturnsFalse()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("R1");
            Building00 building = new Building00() { Width = 10 };
            INode node = new PropertyNode(building, "Width");

            reactor.AddNode(node);
            reactor.PerformUpdate(node);

            //Act
            reactor.PerformUpdate(node);

            //Assert
            Assert.IsFalse(node.IsTriggered());
        }

        [TestMethod]
        public void IsValueChanged_GivenNoInitialValueSet_ReturnsTrue()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("R1");
            Building00 building = new Building00() { };
            INode node = new PropertyNode(building, "Width");

            reactor.AddNode(node);
            reactor.PerformUpdate(node);

            //Act
            building.Width = 10;
            reactor.PerformUpdate(node);

            //Assert
            Assert.IsTrue(node.IsTriggered());
        }

        [TestMethod]
        public void IsValueChanged_GivenTwoSameSuccessiveInputs_ReturnsFalse()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("R1");
            Building00 building = new Building00() { };
            INode node = new PropertyNode(building, "Width");

            reactor.AddNode(node);

            //Act
            building.Width = 10;
            reactor.PerformUpdate(node);
            building.Width = 10;
            reactor.PerformUpdate(node);

            //Assert
            Assert.IsFalse(node.IsTriggered());
        }

        [TestMethod]
        public void IsValueChanged_GivenTwoDifferentSuccessiveInputs_ReturnsTrue()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("R1");
            Building00 building = new Building00() { };
            INode node = new PropertyNode(building, "Width");
            reactor.AddNode(node);

            //Act
            building.Width = 10;
            reactor.PerformUpdate(node);
            building.Width = 12;
            reactor.PerformUpdate(node);

            //Assert
            Assert.IsTrue(node.IsTriggered());
        }

        #endregion
    }
}
