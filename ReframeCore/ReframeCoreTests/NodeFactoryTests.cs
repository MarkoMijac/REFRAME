using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeCoreExamples.E00;
using ReframeCore.Nodes;
using ReframeCore;
using ReframeCore.Exceptions;
using ReframeCoreExamples.E02;
using ReframeCoreExamples.E07;
using ReframeCore.ReactiveCollections;

namespace ReframeCoreTests
{
    [TestClass]
    public class NodeFactoryTests
    {
        private NodeFactory defaultFactory = new NodeFactory();

        #region PropertyNode

        [TestMethod]
        public void CreateNode_GivenPropertyOwnerObjectIsNull_ThrowsException()
        {
            //Arrange
            Building00 building00 = null;
            string memberName = "Width";

            //Act & Assert
            Assert.ThrowsException<ReactiveNodeException>(() => defaultFactory.CreateNode(building00, memberName));
        }

        [TestMethod]
        public void CreateNode_GivenWrongPropertyName_ThrowsException()
        {
            //Arrange
            Building00 building00 = new Building00();
            string memberName = "Width_Wrong";

            //Act & Assert
            Assert.ThrowsException<ReactiveNodeException>(() => defaultFactory.CreateNode(building00, memberName));
        }

        [TestMethod]
        public void CreateNode_GivenWrongUpdateMethodIsProvided_ThrowsException()
        {
            //Arrange
            Building00 building00 = new Building00();
            string propertyName = "Area";
            string updateMethodName = "Update_Area_Wrong";

            //Act & Assert
            Assert.ThrowsException<ReactiveNodeException>(() => defaultFactory.CreateNode(building00, propertyName, updateMethodName));
        }

        [TestMethod]
        public void CreateNode_GivenCorrectUpdateMethodIsProvided_ReturnsPropertyNodeWithUpdateMethod()
        {
            //Arrange
            Building00 building = new Building00();
            string propertyName = "Area";
            string updateMethodName = "Update_Area";

            //Act
            PropertyNode node = defaultFactory.CreateNode(building, propertyName, updateMethodName) as PropertyNode;

            //Assert
            Assert.IsTrue(node.OwnerObject == building
                && node.MemberName == propertyName
                && node.UpdateMethod.Method.Name == updateMethodName);
        }

        [TestMethod]
        public void CreateNode_GivenUpdateMethodIsNotProvidedAndDefaultOneIsEnabledAndExisting_ReturnsPropertyNodeWithDefaultUpdateMethod()
        {
            //Arrange
            NodeFactory nodeFactory = new NodeFactory();
            nodeFactory.Settings.UseDefaultUpdateMethodNames = true; //This is default option
            Building00 building = new Building00();
            string propertyName = "Area";

            //Act
            PropertyNode node = nodeFactory.CreateNode(building, propertyName) as PropertyNode;

            //Assert
            Assert.IsTrue(node.OwnerObject == building
                && node.MemberName == propertyName
                && node.UpdateMethod.Method.Name == nodeFactory.Settings.GenerateDefaultUpdateMethodName(propertyName));
        }

        [TestMethod]
        public void CreateNode_GivenUpdateMethodIsNotProvidedAndDefaultOneIsDisabled_ReturnsPropertyNodeWithoutUpdateMethod()
        {
            //Arrange
            NodeFactory nodeFactory = new NodeFactory();
            nodeFactory.Settings.UseDefaultUpdateMethodNames = false; //This is default option
            Building00 building = new Building00();
            string propertyName = "Area";

            //Act
            PropertyNode node = nodeFactory.CreateNode(building, propertyName) as PropertyNode;

            //Assert
            Assert.IsTrue(node.OwnerObject == building
                && node.MemberName == propertyName
                && node.UpdateMethod == null);
        }

        [TestMethod]
        public void CreateNode_GivenUpdateMethodIsNotProvidedAndDefaultOneIsEnabledButNotExisting_ReturnsPropertyNodeWithoutUpdateMethod()
        {
            //Arrange
            NodeFactory nodeFactory = new NodeFactory();
            nodeFactory.Settings.UseDefaultUpdateMethodNames = true; //This is default option
            Building00 building = new Building00();
            string propertyName = "Width";

            //Act
            PropertyNode node = nodeFactory.CreateNode(building, propertyName) as PropertyNode;

            //Assert
            Assert.IsTrue(node.OwnerObject == building
                && node.MemberName == propertyName
                && node.UpdateMethod==null);
        }

        #endregion

        #region MethodNode

        [TestMethod]
        public void CreateNode_GivenMethodOwnerObjectIsNull_ThrowsException()
        {
            //Arrange
            Building00 building02 = null;
            string methodName = "Update_Area";

            //Act & Assert
            Assert.ThrowsException<ReactiveNodeException>(() => defaultFactory.CreateNode(building02, methodName));
        }

        [TestMethod]
        public void CreateNode_GivenIncorrectMethodName_ThrowsException()
        {
            //Arrange
            Building02 building = new Building02();
            string methodName = "Update_Area_Wrong";

            //Act & Assert
            Assert.ThrowsException<ReactiveNodeException>(() => defaultFactory.CreateNode(building, methodName));
        }

        [TestMethod]
        public void CreateNode_GivenCorrectArguments_ReturnsMethodNode()
        {
            //Arrange
            Building02 building = new Building02();
            string methodName = "Update_Area";

            //Act
            INode updateAreaNode = defaultFactory.CreateNode(building, methodName);

            //Assert
            Assert.IsInstanceOfType(updateAreaNode, typeof(MethodNode));
        }

        [TestMethod]
        public void CreateNode_GivenCorrectArguments_ReturnsMethodnoNodeWithCorrectData()
        {
            //Arrange
            Building02 building = new Building02();
            string memberName = "Update_Area";

            //Act
            MethodNode updateAreaNode = defaultFactory.CreateNode(building, memberName) as MethodNode;

            //Assert
            Assert.IsTrue(updateAreaNode.OwnerObject == building
                && updateAreaNode.MemberName == memberName
                && updateAreaNode.UpdateMethod.Method.Name == memberName);
        }

        #endregion

        #region CollectionNode
        
        [TestMethod]
        public void CreateNode_GivenCorrectArguments_ReturnsCollectionNode()
        {
            //Arrange
            Whole whole = new Whole { Name = "Whole 1" };

            //Act
            CollectionNode partsNode = defaultFactory.CreateNode(whole.Parts, "A") as CollectionNode;

            //Assert
            Assert.IsInstanceOfType(partsNode, typeof(CollectionNode));
        }
        
        [TestMethod]
        public void CreateNode_CreatedCollectionNode_HoldsProperData()
        {
            //Arrange
            Whole whole = new Whole { Name = "Whole 1" };
            ReactiveCollection<Part> parts = whole.Parts;

            //Act
            CollectionNode partsNode = defaultFactory.CreateNode(parts, "A") as CollectionNode;

            //Assert
            Assert.IsTrue(partsNode.OwnerObject == parts && partsNode.MemberName == "A");
        }

        
        [TestMethod]
        public void CreateNode_SourceNodeUpdateInvoke_DoesNotBreak()
        {
            //Arrange
            Whole whole = new Whole { Name = "Whole 1" };

            try
            {
                //Act
                CollectionNode partsNode = defaultFactory.CreateNode(whole.Parts, "A") as CollectionNode;
                partsNode.Update();
            }
            catch (Exception ex)
            {
                //Assert
                Assert.Fail("Expected no exception, instead got: " + ex.Message);
            }
        }

        [TestMethod]
        public void CreateNode_GivenNullObject_ThrowsException()
        {
            //Arrange
            Whole whole = new Whole { Name = "Whole 1" };
            ReactiveCollection<Part> parts = null;

            //Act & Assert
            Assert.ThrowsException<ReactiveNodeException>(() => defaultFactory.CreateNode(parts, "A") as CollectionNode);
        }

        [TestMethod]
        public void CreateNode_GivenEmptyReactiveCollection_CreatesNode()
        {
            //Arrange
            ReactiveCollection<Part> collection = new ReactiveCollection<Part>();

            //Act
            CollectionNode node = defaultFactory.CreateNode(collection, "A") as CollectionNode;

            //Assert
            //Assert
            Assert.IsInstanceOfType(node, typeof(INode));
        }

        [TestMethod]
        public void CreateNode_GivenNonExistantMemberName_ThrowsException()
        {
            //Arrange
            Whole whole = new Whole { Name = "Whole 1" };

            //Act & Assert
            Assert.ThrowsException<ReactiveNodeException>(() => defaultFactory.CreateNode(whole.Parts, "AA") as CollectionNode);
        }
        
        #endregion
    }
}
