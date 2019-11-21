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
using ReframeCoreExamples.E07;
using ReframeCore.ReactiveCollections;
using ReframeCore.Factories;

namespace ReframeCoreTests
{
    [TestClass]
    public partial class DependencyGraphTests
    {
        private NodeFactory nodeFactory = new StandardNodeFactory();

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
            PropertyNode node = nodeFactory.CreateNode(building, memberName) as PropertyNode;

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
            MethodNode node = nodeFactory.CreateNode(building, memberName) as MethodNode;

            //Act
            INode addedNode = graph.AddNode(node);

            //Assert
            Assert.IsNotNull(addedNode);
        }

        [TestMethod]
        public void AddNode_GivenValidCollectionPropertyNode_ReturnsAddedNode()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Whole whole = new Whole();
            string memberName = "A";
            CollectionPropertyNode node = nodeFactory.CreateNode(whole.Parts, memberName) as CollectionPropertyNode;

            //Act
            INode addedNode = graph.AddNode(node);

            //Assert
            Assert.IsInstanceOfType(addedNode, typeof(CollectionPropertyNode));
        }

        [TestMethod]
        public void AddNode_GivenValidCollectionMethodNode_ReturnsAddedNode()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Whole whole = new Whole();
            string memberName = "Update_A";
            CollectionMethodNode node = nodeFactory.CreateNode(whole.Parts, memberName) as CollectionMethodNode;

            //Act
            INode addedNode = graph.AddNode(node);

            //Assert
            Assert.IsInstanceOfType(addedNode, typeof(CollectionMethodNode));
        }

        [TestMethod]
        public void AddNode_GivenAlreadyAddedPropertyNode_ReturnsNull()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();
            string memberName = "Width";
            PropertyNode node = nodeFactory.CreateNode(building, memberName) as PropertyNode;
            graph.AddNode(node);

            //Act
            INode addedNode = graph.AddNode(node);

            //Assert
            Assert.IsNull(addedNode);
        }

        [TestMethod]
        public void AddNode_GivenAlreadyAddedPropertyNodeAsDifferentInstance_ReturnsNull()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();
            string memberName = "Width";
            INode node = nodeFactory.CreateNode(building, memberName);
            graph.AddNode(node);
            INode nodeToAdd = nodeFactory.CreateNode(building, memberName);

            //Act
            INode addedNode = graph.AddNode(nodeToAdd);

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
            MethodNode node = nodeFactory.CreateNode(building, memberName) as MethodNode;
            graph.AddNode(node);

            //Act
            INode addedNode = graph.AddNode(node);

            //Assert
            Assert.IsNull(addedNode);
        }

        [TestMethod]
        public void AddNode_GivenAlreadyAddedCollectionPropertyNode_ReturnsNull()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Whole whole = new Whole();
            string memberName = "A";
            CollectionPropertyNode node = nodeFactory.CreateNode(whole.Parts, memberName) as CollectionPropertyNode;
            graph.AddNode(node);

            //Act
            INode addedNode = graph.AddNode(node);

            //Assert
            Assert.IsNull(addedNode);
        }

        [TestMethod]
        public void AddNode_GivenAlreadyAddedCollectionMethodNode_ReturnsNull()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Whole whole = new Whole();
            string memberName = "Update_A";
            CollectionMethodNode node = nodeFactory.CreateNode(whole.Parts, memberName) as CollectionMethodNode;
            graph.AddNode(node);

            //Act
            INode addedNode = graph.AddNode(node);

            //Assert
            Assert.IsNull(addedNode);
        }

        [TestMethod]
        public void AddNode_GivenAddedNode_GraphIsRegisteredToNode()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();
            string memberName = "Update_Area";
            INode node = nodeFactory.CreateNode(building, memberName);

            //Act
            INode addedNode = graph.AddNode(node);

            //Assert
            Assert.IsTrue(addedNode.Graph == graph);
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

        [TestMethod]
        public void AddNode1_GivenValidArguments_ReturnsCollectionPropertyNode()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Whole whole = new Whole();
            string memberName = "A";

            //Act
            INode addedNode = graph.AddNode(whole.Parts, memberName);

            //Assert
            Assert.IsInstanceOfType(addedNode, typeof(CollectionPropertyNode));
        }

        [TestMethod]
        public void AddNode1_GivenValidArguments_ReturnsCollectionMethodNode()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Whole whole = new Whole();
            string memberName = "Update_A";

            //Act
            INode addedNode = graph.AddNode(whole.Parts, memberName);

            //Assert
            Assert.IsInstanceOfType(addedNode, typeof(CollectionMethodNode));
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
        public void AddNode1_GivenValidArguments_ReturnsCorrectMethodNode()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Whole whole = new Whole();
            string memberName = "A";

            //Act
            CollectionPropertyNode addedNode = graph.AddNode(whole.Parts, memberName) as CollectionPropertyNode;

            //Assert
            Assert.IsTrue(addedNode.OwnerObject == whole.Parts && addedNode.MemberName == memberName);
        }

        [TestMethod]
        public void AddNode1_GivenValidCollectionAndPropertyName_ReturnsCorrectCollectionPropertyNode()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Whole whole = new Whole();
            string memberName = "A";

            //Act
            CollectionPropertyNode addedNode = graph.AddNode(whole.Parts, memberName) as CollectionPropertyNode;

            //Assert
            Assert.IsTrue(addedNode.OwnerObject == whole.Parts && addedNode.MemberName == memberName);
        }

        [TestMethod]
        public void AddNode1_GivenValidCollectionAndMethodName_ReturnsCorrectCollectionMethodNode()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Whole whole = new Whole();
            string memberName = "Update_A";

            //Act
            CollectionMethodNode addedNode = graph.AddNode(whole.Parts, memberName) as CollectionMethodNode;

            //Assert
            Assert.IsTrue(addedNode.OwnerObject == whole.Parts && addedNode.MemberName == memberName);
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
            graph.NodeFactory.UseDefaultUpdateMethodNames = true;
            PrivateObject privateObject = new PrivateObject(graph.NodeFactory);

            Building00 building = new Building00();
            string memberName = "Area";

            //Act
            PropertyNode addedNode = graph.AddNode(building, memberName) as PropertyNode;

            //Assert
            string generatedUpdateMethodName = privateObject.Invoke("GenerateDefaultUpdateMethodName", memberName).ToString();
            Assert.IsTrue(addedNode.UpdateMethod != null
                && addedNode.UpdateMethod.Method.Name == generatedUpdateMethodName);
        }

        [TestMethod]
        public void AddNode1_GivenValidArgumentsAndNoDefaultUpdateMethodNames_ReturnsPropertyNodeWithoutUpdateMethod()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            graph.NodeFactory.UseDefaultUpdateMethodNames = false;

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
        public void AddNode1_GivenAddedNode_GraphIsRegisteredToNode()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();
            string memberName = "Update_Area";

            //Act
            INode addedNode = graph.AddNode(building, memberName);

            //Assert
            Assert.IsTrue(addedNode.Graph == graph);
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
        public void AddNode2_GivenNonExistingUpdateMethodName_ThrowsException()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();
            string memberName = "Width";
            string updateMethodName = "Update_Width";

            //Act&Assert
            Assert.ThrowsException<ReactiveNodeException>(() => graph.AddNode(building, memberName, updateMethodName));
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
        public void AddNode2_GivenWrongParameterOrder_ThrowsException()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();
            string memberName = "Area";
            string updateMethodName = "Update_Area";

            //Act&Assert
            Assert.ThrowsException<ReactiveNodeException>(() => graph.AddNode(building, updateMethodName, memberName));
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

        [TestMethod]
        public void AddNode2_GivenAddedNode_GraphIsRegisteredToNode()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();
            string memberName = "Area";
            string updateMethod = "Update_Area";

            //Act
            INode addedNode = graph.AddNode(building, memberName, updateMethod);

            //Assert
            Assert.IsTrue(addedNode.Graph == graph);
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
            PropertyNode node = nodeFactory.CreateNode(building, memberName) as PropertyNode;
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
            MethodNode node = nodeFactory.CreateNode(building, memberName) as MethodNode;
            graph.AddNode(node);

            //Act
            bool contains = graph.ContainsNode(node);

            //Assert
            Assert.IsTrue(contains);
        }

        [TestMethod]
        public void ContainsNode_GivenAddedCollectionPropertyNode_ReturnsTrue()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Whole whole = new Whole();
            string memberName = "A";
            CollectionPropertyNode node = nodeFactory.CreateNode(whole.Parts, memberName) as CollectionPropertyNode;
            graph.AddNode(node);

            //Act
            bool contains = graph.ContainsNode(node);

            //Assert
            Assert.IsTrue(contains);
        }

        [TestMethod]
        public void ContainsNode_GivenAddedCollectionMethodNode_ReturnsTrue()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Whole whole = new Whole();
            string memberName = "Update_A";
            CollectionMethodNode node = nodeFactory.CreateNode(whole.Parts, memberName) as CollectionMethodNode;
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
            PropertyNode node = nodeFactory.CreateNode(building, memberName) as PropertyNode;

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
            MethodNode node = nodeFactory.CreateNode(building, memberName) as MethodNode;

            //Act
            bool contains = graph.ContainsNode(node);

            //Assert
            Assert.IsFalse(contains);
        }

        [TestMethod]
        public void ContainsNode_GivenNotAddedCollectionPropertyNode_ReturnsFalse()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Whole whole = new Whole();
            string memberName = "A";
            CollectionPropertyNode node = nodeFactory.CreateNode(whole.Parts, memberName) as CollectionPropertyNode;

            //Act
            bool contains = graph.ContainsNode(node);

            //Assert
            Assert.IsFalse(contains);
        }

        [TestMethod]
        public void ContainsNode_GivenNotAddedCollectionMethodNode_ReturnsFalse()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Whole whole = new Whole();
            string memberName = "Update_A";
            CollectionMethodNode node = nodeFactory.CreateNode(whole.Parts, memberName) as CollectionMethodNode;

            //Act
            bool contains = graph.ContainsNode(node);

            //Assert
            Assert.IsFalse(contains);
        }

        [TestMethod]
        public void ContainsNode_GivenAddedPropertyNode_ReturnsProperInstanceOfPropertyNode()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();
            string memberName = "Width";
            INode node = nodeFactory.CreateNode(building, memberName);
            graph.AddNode(node);

            INode sameNode = nodeFactory.CreateNode(building, memberName);

            //Act
            bool contains = graph.ContainsNode(sameNode);

            //Assert
            Assert.IsTrue(contains);
        }

        [TestMethod]
        public void ContainsNode1_GivenAddedPropertyNodeReturnsTrue()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();
            string memberName = "Width";
            PropertyNode node = nodeFactory.CreateNode(building, memberName) as PropertyNode;
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
            MethodNode node = nodeFactory.CreateNode(building, memberName) as MethodNode;
            graph.AddNode(node);

            //Act
            bool contains = graph.ContainsNode(building, memberName);

            //Assert
            Assert.IsTrue(contains);
        }

        [TestMethod]
        public void ContainsNode1_GivenAddedCollectionPropertyNode_ReturnsTrue()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Whole whole = new Whole();
            string memberName = "A";
            CollectionPropertyNode node = nodeFactory.CreateNode(whole.Parts, memberName) as CollectionPropertyNode;
            graph.AddNode(node);

            //Act
            bool contains = graph.ContainsNode(whole.Parts, memberName);

            //Assert
            Assert.IsTrue(contains);
        }

        [TestMethod]
        public void ContainsNode1_GivenAddedCollectionMethodNode_ReturnsTrue()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Whole whole = new Whole();
            string memberName = "Update_A";
            CollectionMethodNode node = nodeFactory.CreateNode(whole.Parts, memberName) as CollectionMethodNode;
            graph.AddNode(node);

            //Act
            bool contains = graph.ContainsNode(whole.Parts, memberName);

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
            PropertyNode node = nodeFactory.CreateNode(building, memberName) as PropertyNode;

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
            string memberName = "Update_Area";
            MethodNode node = nodeFactory.CreateNode(building, memberName) as MethodNode;

            //Act
            bool contains = graph.ContainsNode(building, memberName);

            //Assert
            Assert.IsFalse(contains);
        }

        [TestMethod]
        public void ContainsNode1_GivenNotAddedCollectionPropertyNode_ReturnsFalse()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Whole whole = new Whole();
            string memberName = "A";
            CollectionPropertyNode node = nodeFactory.CreateNode(whole.Parts, memberName) as CollectionPropertyNode;

            //Act
            bool contains = graph.ContainsNode(whole.Parts, memberName);

            //Assert
            Assert.IsFalse(contains);
        }

        [TestMethod]
        public void ContainsNode1_GivenNotAddedCollectionMethodNode_ReturnsFalse()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Whole whole = new Whole();
            string memberName = "Update_A";
            CollectionMethodNode node = nodeFactory.CreateNode(whole.Parts, memberName) as CollectionMethodNode;

            //Act
            bool contains = graph.ContainsNode(whole.Parts, memberName);

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
        public void GetNode_GivenInvalidObjectAndEmptyGraph_ReturnsNull()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = null;
            string memberName = "Width";

            //Act
            INode node = graph.GetNode(building, memberName);

            //Assert
            Assert.IsNull(node);
        }

        [TestMethod]
        public void GetNode_GivenInvalidMemberNameAndEmptyGraph_ReturnsNull()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();
            string memberName = "WidthINV";

            //Act
            INode node = graph.GetNode(building, memberName);

            //Assert
            Assert.IsNull(node);
        }

        [TestMethod]
        public void GetNode_GivenInvalidObjectAndNonEmptyGraph_ReturnsNull()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            graph.AddNode(new Building00(), "Height");
            Building00 building = null;
            string memberName = "Width";

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
            PropertyNode node = nodeFactory.CreateNode(building, memberName) as PropertyNode;
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
            PropertyNode node = nodeFactory.CreateNode(building, memberName) as PropertyNode;

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

        [TestMethod]
        public void GetNode_GivenSuchNodeExistsButAsDifferentInstance_ReturnsInstanceFromDependencyGraph()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();
            string memberName = "Width";

            INode addedNode = nodeFactory.CreateNode(building, memberName);
            graph.AddNode(addedNode);

            INode sameNode = nodeFactory.CreateNode(building, memberName);

            //Act
            INode obtainedNode = graph.GetNode(sameNode);

            //Assert
            Assert.AreEqual(addedNode, obtainedNode);
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
        public void RemoveNode_GivenNotAddedNode_ThrowsException()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();
            string memberName = "Width";
            PropertyNode node = nodeFactory.CreateNode(building, memberName) as PropertyNode;

            //Act&Assert
            Assert.ThrowsException<NodeNullReferenceException>(() => graph.RemoveNode(node));
        }

        [TestMethod]
        public void RemoveNode_GivenNodeHasPredecessors_ThrowsException()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();
            string memberName = "Width";
            string memberName2 = "Area";
            PropertyNode predecessorNode = nodeFactory.CreateNode(building, memberName) as PropertyNode;
            PropertyNode successorNode = nodeFactory.CreateNode(building, memberName2) as PropertyNode;
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
            PropertyNode predecessorNode = nodeFactory.CreateNode(building, memberName) as PropertyNode;
            PropertyNode successorNode = nodeFactory.CreateNode(building, memberName2) as PropertyNode;
            graph.AddNode(predecessorNode);
            graph.AddNode(successorNode);

            predecessorNode.AddSuccessor(successorNode);

            //Act&Assert
            Assert.ThrowsException<ReactiveNodeException>(() => graph.RemoveNode(predecessorNode));
        }

        [TestMethod]
        public void RemoveNode_GivenRemovalIsForcedEvenIfNodeHasSuccessorsAndPredecessors_ReturnsTrue()
        {
            //Arrange
            GraphRegistry.Instance.Clear();
            var graph = GraphRegistry.Instance.CreateGraph("G1");
            GenericReactiveObject obj = new GenericReactiveObject();

            INode nodeA = nodeFactory.CreateNode(obj, "A");
            INode nodeB = nodeFactory.CreateNode(obj, "B");
            INode nodeC = nodeFactory.CreateNode(obj, "C");
            INode nodeD = nodeFactory.CreateNode(obj, "D");
            INode nodeE = nodeFactory.CreateNode(obj, "E");

            graph.AddDependency(nodeA, nodeC);
            graph.AddDependency(nodeB, nodeC);
            graph.AddDependency(nodeC, nodeD);
            graph.AddDependency(nodeC, nodeE);

            //Act
            bool removed = graph.RemoveNode(nodeC, true);

            //Assert
            Assert.IsTrue(removed);
            Assert.IsFalse(nodeA.Successors.Contains(nodeC));
            Assert.IsFalse(nodeB.Successors.Contains(nodeC));
            Assert.IsFalse(nodeD.Predecessors.Contains(nodeC));
            Assert.IsFalse(nodeE.Predecessors.Contains(nodeC));
        }

        [TestMethod]
        public void RemoveNode_GivenAddedNodeWithoutPredecessorsAndSuccessors_ReturnsTrue()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();
            string memberName = "Width";
            PropertyNode node = nodeFactory.CreateNode(building, memberName) as PropertyNode;
            graph.AddNode(node);

            //Act
            bool removed = graph.RemoveNode(node);

            //Assert
            Assert.IsTrue(removed);
        }

        [TestMethod]
        public void RemoveNode_GivenRemovedNode_GraphIsUnregisteredFromNode()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Building00 building = new Building00();
            string memberName = "Width";
            PropertyNode node = nodeFactory.CreateNode(building, memberName) as PropertyNode;
            graph.AddNode(node);

            //Act
            bool removed = graph.RemoveNode(node);

            //Assert
            Assert.IsTrue(node.Graph == null);
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

            PropertyNode predecessor = nodeFactory.CreateNode(building00, memberName) as PropertyNode;
            PropertyNode successor = nodeFactory.CreateNode(building00, memberName2) as PropertyNode;

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

            PropertyNode predecessor = nodeFactory.CreateNode(building00, memberName) as PropertyNode;
            PropertyNode successor = nodeFactory.CreateNode(building00, memberName2) as PropertyNode;

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

            PropertyNode predecessor = nodeFactory.CreateNode(building00, memberName) as PropertyNode;
            PropertyNode successor = nodeFactory.CreateNode(building00, memberName2) as PropertyNode;

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

            MethodNode predecessor = nodeFactory.CreateNode(building00, memberName) as MethodNode;
            MethodNode successor = nodeFactory.CreateNode(building00, memberName2) as MethodNode;

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

            PropertyNode predecessor = nodeFactory.CreateNode(building00, memberName) as PropertyNode;
            MethodNode successor = nodeFactory.CreateNode(building00, memberName2) as MethodNode;

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

            PropertyNode apartmentTotalArea = nodeFactory.CreateNode(apartment01, "TotalArea") as PropertyNode;
            PropertyNode basementArea = nodeFactory.CreateNode(basement, "Area") as PropertyNode;

            //Act
            graph.AddDependency(basementArea, apartmentTotalArea);

            //Assert
            Assert.IsTrue(apartmentTotalArea.HasPredecessor(basementArea) && basementArea.HasSuccessor(apartmentTotalArea));
        }

        [TestMethod]
        public void AddDependency_GivenDependencyAlreadyExists_ThrowsException()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Apartment01 apartment01 = new Apartment01 { Name = "AP-01" };

            AdditionalPart01 basement = new AdditionalPart01 { Name = "Basement" };
            apartment01.Basement = basement;

            PropertyNode apartmentTotalArea = nodeFactory.CreateNode(apartment01, "TotalArea") as PropertyNode;
            PropertyNode basementArea = nodeFactory.CreateNode(basement, "Area") as PropertyNode;
            graph.AddDependency(basementArea, apartmentTotalArea);

            //Act&Assert
            Assert.ThrowsException<ReactiveDependencyException>(() => graph.AddDependency(basementArea, apartmentTotalArea));
        }

        [TestMethod]
        public void AddDependency_GivenDirectCycleIsFormed_ThrowsException()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            Apartment01 apartment01 = new Apartment01 { Name = "AP-01" };

            AdditionalPart01 basement = new AdditionalPart01 { Name = "Basement" };
            apartment01.Basement = basement;

            PropertyNode apartmentTotalArea = nodeFactory.CreateNode(apartment01, "TotalArea") as PropertyNode;
            PropertyNode basementArea = nodeFactory.CreateNode(basement, "Area") as PropertyNode;
            graph.AddDependency(basementArea, apartmentTotalArea);

            //Act&Assert
            Assert.ThrowsException<ReactiveDependencyException>(() => graph.AddDependency(apartmentTotalArea, basementArea));
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
            PropertyNode predecessor = nodeFactory.CreateNode(owner, "A") as PropertyNode;
            List<INode> successors = new List<INode>();
            successors.Add(nodeFactory.CreateNode(owner, "B"));
            successors.Add(nodeFactory.CreateNode(owner, "C"));
            successors.Add(nodeFactory.CreateNode(owner, "D"));

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
            PropertyNode successor = nodeFactory.CreateNode(owner, "D") as PropertyNode;
            List<INode> predecessors = new List<INode>();
            predecessors.Add(nodeFactory.CreateNode(owner, "A"));
            predecessors.Add(nodeFactory.CreateNode(owner, "B"));
            predecessors.Add(nodeFactory.CreateNode(owner, "C"));

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
            PropertyNode successor = nodeFactory.CreateNode(owner, "D") as PropertyNode;
            List<INode> predecessors = new List<INode>();
            predecessors.Add(nodeFactory.CreateNode(owner, "A"));
            predecessors.Add(nodeFactory.CreateNode(owner, "B"));
            predecessors.Add(nodeFactory.CreateNode(owner, "C"));

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

            PropertyNode predecessor = nodeFactory.CreateNode(building00, memberName) as PropertyNode;
            PropertyNode successor = nodeFactory.CreateNode(building00, memberName2) as PropertyNode;

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

            PropertyNode predecessor = nodeFactory.CreateNode(building00, memberName) as PropertyNode;
            PropertyNode successor = nodeFactory.CreateNode(building00, memberName2) as PropertyNode;

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

            PropertyNode apartmentTotalArea = nodeFactory.CreateNode(apartment01, "TotalArea") as PropertyNode;
            PropertyNode basementArea = nodeFactory.CreateNode(basement, "Area") as PropertyNode;

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

            PropertyNode predecessor = nodeFactory.CreateNode(building00, memberName) as PropertyNode;
            PropertyNode successor = nodeFactory.CreateNode(building00, memberName2) as PropertyNode;

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

            PropertyNode predecessor = nodeFactory.CreateNode(building00, memberName) as PropertyNode;
            PropertyNode successor = nodeFactory.CreateNode(building00, memberName2) as PropertyNode;
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

            PropertyNode predecessor = nodeFactory.CreateNode(building00, memberName) as PropertyNode;
            PropertyNode successor = nodeFactory.CreateNode(building00, memberName2) as PropertyNode;
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

            PropertyNode predecessor = nodeFactory.CreateNode(building00, memberName) as PropertyNode;
            PropertyNode successor = nodeFactory.CreateNode(building00, memberName2) as PropertyNode;
            graph.AddNode(predecessor);
            graph.AddNode(successor);
            graph.AddNode(building00, memberName3);

            //Act
            int numOfNodes = graph.RemoveUnusedNodes();

            //Assert
            Assert.AreEqual(numOfNodes, 3);
        }

        #endregion

        #region RemoveNodesOfNonexistantObjects

        [TestMethod]
        public void RemoveNodesOfNonexistantObjects_GivenOwnerObjectsHaveStrongReferences_NoNodesAreRemoved()
        {
            //Arrange
            NodeFactory nodeFactory = new StandardNodeFactory();
            GenericReactiveObject obj1 = new GenericReactiveObject();
            GraphRegistry.Instance.Clear();
            var graph = GraphRegistry.Instance.CreateGraph("G1");

            INode nodeA = nodeFactory.CreateNode(obj1, "A");
            INode nodeB = nodeFactory.CreateNode(obj1, "B");
            INode nodeC = nodeFactory.CreateNode(obj1, "C");

            graph.AddNode(nodeA);
            graph.AddNode(nodeB);
            graph.AddNode(nodeC);

            //Act
            PrivateObject privateGraph = new PrivateObject(graph);
            int numOfRemoved = (int) privateGraph.Invoke("RemoveNodesOfNonexistantObjects");

            //Assert
            Assert.AreEqual(0, numOfRemoved);
            Assert.AreEqual(3, graph.Nodes.Count);
        }

        [TestMethod]
        public void RemoveNodesOfNonexistantObjects_GivenOwnerObjectDoesntHaveStrongReferences_NodesAreRemoved()
        {
            //Arrange
            NodeFactory nodeFactory = new StandardNodeFactory();
            GenericReactiveObject obj1 = new GenericReactiveObject();
            GraphRegistry.Instance.Clear();
            var graph = GraphRegistry.Instance.CreateGraph("G1");

            INode nodeA = nodeFactory.CreateNode(obj1, "A");
            INode nodeB = nodeFactory.CreateNode(obj1, "B");
            INode nodeC = nodeFactory.CreateNode(obj1, "Update_C");

            graph.AddNode(nodeA);
            graph.AddNode(nodeB);
            graph.AddNode(nodeC);

            obj1 = null;

            //Act
            PrivateObject privateGraph = new PrivateObject(graph);
            int numOfRemoved = (int)privateGraph.Invoke("RemoveNodesOfNonexistantObjects");

            //Assert
            Assert.AreEqual(3, numOfRemoved);
            Assert.AreEqual(0, graph.Nodes.Count);
        }

        [TestMethod]
        public void RemoveNodesOfNonexistantObjects_GivenMultipleOwnerObjectsDontHaveStrongReferences_NodesAreRemoved()
        {
            //Arrange
            NodeFactory nodeFactory = new StandardNodeFactory();
            GenericReactiveObject obj1 = new GenericReactiveObject();
            GenericReactiveObject obj2 = new GenericReactiveObject();
            GraphRegistry.Instance.Clear();
            var graph = GraphRegistry.Instance.CreateGraph("G1");

            INode node1A = nodeFactory.CreateNode(obj1, "A");
            INode node1B = nodeFactory.CreateNode(obj1, "B");
            INode node1C = nodeFactory.CreateNode(obj1, "C");

            INode node2A = nodeFactory.CreateNode(obj2, "A");
            INode node2B = nodeFactory.CreateNode(obj2, "B");
            INode node2C = nodeFactory.CreateNode(obj2, "C");

            graph.AddNode(node1A);
            graph.AddNode(node1B);
            graph.AddNode(node1C);

            graph.AddNode(node2A);
            graph.AddNode(node2B);
            graph.AddNode(node2C);

            obj1 = null;
            obj2 = null;

            //Act
            PrivateObject privateGraph = new PrivateObject(graph);
            int numOfRemoved = (int)privateGraph.Invoke("RemoveNodesOfNonexistantObjects");

            //Assert
            Assert.AreEqual(6, numOfRemoved);
            Assert.AreEqual(0, graph.Nodes.Count);
        }

        [TestMethod]
        public void RemoveNodesOfNonexistantObjects_GivenSomeOwnerObjectsDontHaveStrongReferences_NodesAreRemoved()
        {
            //Arrange
            NodeFactory nodeFactory = new StandardNodeFactory();
            GenericReactiveObject obj1 = new GenericReactiveObject();
            GenericReactiveObject obj2 = new GenericReactiveObject();
            GraphRegistry.Instance.Clear();
            var graph = GraphRegistry.Instance.CreateGraph("G1");

            INode node1A = nodeFactory.CreateNode(obj1, "A");
            INode node1B = nodeFactory.CreateNode(obj1, "B");
            INode node1C = nodeFactory.CreateNode(obj1, "C");

            INode node2A = nodeFactory.CreateNode(obj2, "A");
            INode node2B = nodeFactory.CreateNode(obj2, "B");
            INode node2C = nodeFactory.CreateNode(obj2, "C");

            graph.AddNode(node1A);
            graph.AddNode(node1B);
            graph.AddNode(node1C);

            graph.AddNode(node2A);
            graph.AddNode(node2B);
            graph.AddNode(node2C);

            graph.AddDependency(node1A, node2A);
            graph.AddDependency(node1B, node2B);
            graph.AddDependency(node1C, node2C);

            obj1 = null;

            //Act
            PrivateObject privateGraph = new PrivateObject(graph);
            int numOfRemoved = (int)privateGraph.Invoke("RemoveNodesOfNonexistantObjects");

            //Assert
            Assert.AreEqual(3, numOfRemoved);
            Assert.AreEqual(3, graph.Nodes.Count);
        }

        [TestMethod]
        public void RemoveNodesOfNonexistantObjects_GivenCollectionDoesntHaveStrongReferences_CollectionNodeIsRemoved()
        {
            //Arrange
            NodeFactory nodeFactory = new StandardNodeFactory();
            GraphRegistry.Instance.Clear();
            var graph = GraphRegistry.Instance.CreateGraph("G1");

            ReactiveCollection<GenericReactiveObject> reactiveCollection = new ReactiveCollection<GenericReactiveObject>();
            reactiveCollection.Add(new GenericReactiveObject());
            reactiveCollection.Add(new GenericReactiveObject());
            reactiveCollection.Add(new GenericReactiveObject());
            reactiveCollection.Add(new GenericReactiveObject());

            INode collNode = nodeFactory.CreateNode(reactiveCollection, "A");
            INode collNode2 = nodeFactory.CreateNode(reactiveCollection, "B");

            graph.AddNode(collNode);
            graph.AddNode(collNode2);

            reactiveCollection = null;

            //Act
            PrivateObject privateGraph = new PrivateObject(graph);
            int numOfRemoved = (int)privateGraph.Invoke("RemoveNodesOfNonexistantObjects");

            //Assert
            Assert.AreEqual(2, numOfRemoved);
            Assert.AreEqual(0, graph.Nodes.Count);
        }

        [TestMethod]
        public void RemoveNodesOfNonexistantObjects_GivenOwnerObjectBelongsToCollectionWithNoStrongReferences_NodesAreRemoved()
        {
            //Arrange
            NodeFactory nodeFactory = new StandardNodeFactory();
            GraphRegistry.Instance.Clear();
            var graph = GraphRegistry.Instance.CreateGraph("G1");

            List<GenericReactiveObject> list = new List<GenericReactiveObject>();
            list.Add(new GenericReactiveObject());
            list.Add(new GenericReactiveObject());
            list.Add(new GenericReactiveObject());

            INode node1A = nodeFactory.CreateNode(list[0], "A");
            INode node2A = nodeFactory.CreateNode(list[1], "A");
            INode node3A = nodeFactory.CreateNode(list[2], "A");

            graph.AddNode(node1A);
            graph.AddNode(node2A);
            graph.AddNode(node3A);

            list = null;

            //Act
            PrivateObject privateGraph = new PrivateObject(graph);
            int numOfRemoved = (int)privateGraph.Invoke("RemoveNodesOfNonexistantObjects");

            //Assert
            Assert.AreEqual(3, numOfRemoved);
            Assert.AreEqual(0, graph.Nodes.Count);
        }

        #endregion

        #region ContainsDependency

        [TestMethod]
        [ExpectedException(typeof(NodeNullReferenceException))]
        public void ContainsDependency_GivenPredecessorIsNull_ThrowsException()
        {
            //Arrange
            GraphRegistry.Instance.Clear();
            var graph = GraphRegistry.Instance.CreateGraph("G");
            GenericReactiveObject obj = new GenericReactiveObject();
            NodeFactory nodeFactory = new StandardNodeFactory();
            INode predecessor = null;
            INode successor = nodeFactory.CreateNode(obj, "A");

            //Act&Assert
            graph.ContainsDependency(predecessor, successor);
        }

        [TestMethod]
        [ExpectedException(typeof(NodeNullReferenceException))]
        public void ContainsDependency_GivenSuccessorIsNull_ThrowsException()
        {
            //Arrange
            GraphRegistry.Instance.Clear();
            var graph = GraphRegistry.Instance.CreateGraph("G");
            GenericReactiveObject obj = new GenericReactiveObject();
            NodeFactory nodeFactory = new StandardNodeFactory();
            INode predecessor = nodeFactory.CreateNode(obj, "A");
            INode successor = null;

            //Act&Assert
            graph.ContainsDependency(predecessor, successor);
        }

        [TestMethod]
        public void ContainsDependency_GivenNoSuchDependency_ReturnsFalse()
        {
            //Arrange
            GraphRegistry.Instance.Clear();
            var graph = GraphRegistry.Instance.CreateGraph("G");
            GenericReactiveObject obj = new GenericReactiveObject();
            NodeFactory nodeFactory = new StandardNodeFactory();
            INode predecessor = nodeFactory.CreateNode(obj, "A");
            INode successor = nodeFactory.CreateNode(obj, "B");

            //Act
            bool contains = graph.ContainsDependency(predecessor, successor);

            //Assert
            Assert.IsFalse(contains);
        }

        [TestMethod]
        public void ContainsDependency_GivenThereIsSuchDependency_ReturnsTrue()
        {
            //Arrange
            GraphRegistry.Instance.Clear();
            var graph = GraphRegistry.Instance.CreateGraph("G");
            GenericReactiveObject obj = new GenericReactiveObject();
            NodeFactory nodeFactory = new StandardNodeFactory();
            INode predecessor = nodeFactory.CreateNode(obj, "A");
            INode successor = nodeFactory.CreateNode(obj, "B");

            graph.AddDependency(predecessor, successor);

            //Act
            bool contains = graph.ContainsDependency(predecessor, successor);

            //Assert
            Assert.IsTrue(contains);
        }

        #endregion
    }
}
