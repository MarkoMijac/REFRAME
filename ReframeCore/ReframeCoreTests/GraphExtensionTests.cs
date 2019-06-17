using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeCore;
using ReframeCoreExamples.E00;
using ReframeCore.FluentAPI;

namespace ReframeCoreTests
{
    [TestClass]
    public class GraphExtensionTests
    {
        #region Let

        [TestMethod]
        public void Let_GivenGraphIsNull_ThrowsException()
        {
            //Arrange
            GraphFactory.Clear();
            IDependencyGraph graph = null;
            GenericReactiveObject3 obj = new GenericReactiveObject3();

            //Act&Assert
            Assert.ThrowsException<FluentException>(() => graph.Let(()=>obj.A));
        }

        [TestMethod]
        public void Let_GivenOwnerObjectIsNull_ThrowsException()
        {
            //Arrange
            GraphFactory.Clear();
            IDependencyGraph graph = GraphFactory.Create("G");
            GenericReactiveObject3 obj = null;

            //Act&Assert
            Assert.ThrowsException<FluentException>(() => graph.Let(() => obj.A));
        }

        [TestMethod]
        public void Let_GivenNestedOwnerObjectIsNull_ThrowsException()
        {
            //Arrange
            GraphFactory.Clear();
            IDependencyGraph graph = GraphFactory.Create("G");
            GenericReactiveObject3 obj = new GenericReactiveObject3();
            obj.NestedObject = null;

            //Act&Assert
            Assert.ThrowsException<FluentException>(() => graph.Let(() => obj.NestedObject.A));
        }

        [TestMethod]
        public void Let_GivenMemberNameEmpty_ThrowsException()
        {
            //Arrange
            GraphFactory.Clear();
            IDependencyGraph graph = GraphFactory.Create("G");
            GenericReactiveObject3 obj = null;

            //Act&Assert
            Assert.ThrowsException<FluentException>(() => graph.Let(() => obj));
        }

        [TestMethod]
        public void Let_GivenValidGraphAndProperty_ReturnsTransferObject()
        {
            //Arrange
            GraphFactory.Clear();
            var graph = GraphFactory.Create("G");
            GenericReactiveObject3 obj = new GenericReactiveObject3();

            //Act
            TransferObject to = graph.Let(() => obj.A);

            //Assert
            Assert.IsNotNull(to);
        }

        [TestMethod]
        public void Let_GivenValidPropertyNode_ReturnsCorrectTransferObject()
        {
            //Arrange
            GraphFactory.Clear();
            var graph = GraphFactory.Create("G");
            GenericReactiveObject3 obj = new GenericReactiveObject3();

            //Act
            TransferObject to = graph.Let(() => obj.A);

            //Assert
            Assert.IsTrue(to.Graph == graph && to.Nodes.Exists(n => n.OwnerObject == obj && n.MemberName == "A"));
        }

        [TestMethod]
        public void Let_GivenMultipleValidPropertyNodes_ReturnsCorrectTransferObject()
        {
            //Arrange
            GraphFactory.Clear();
            var graph = GraphFactory.Create("G");
            GenericReactiveObject obj1 = new GenericReactiveObject();
            GenericReactiveObject2 obj2 = new GenericReactiveObject2();
            GenericReactiveObject3 obj3 = new GenericReactiveObject3();

            //Act
            TransferObject to = graph.Let(() => obj1.A, () => obj2.B, () => obj3.C);

            //Assert
            Assert.IsTrue(to.Graph == graph
                && to.Nodes.Exists(n => n.OwnerObject == obj1 && n.MemberName == "A")
                && to.Nodes.Exists(n => n.OwnerObject == obj2 && n.MemberName == "B")
                && to.Nodes.Exists(n => n.OwnerObject == obj3 && n.MemberName == "C"));
        }

        [TestMethod]
        public void Let_GivenValidGraphAndMethod_ReturnsTransferObject()
        {
            //Arrange
            GraphFactory.Clear();
            var graph = GraphFactory.Create("G");
            GenericReactiveObject3 obj = new GenericReactiveObject3();

            //Act
            TransferObject to = graph.Let(() => obj.Update_A());

            //Assert
            Assert.IsNotNull(to);
        }

        [TestMethod]
        public void Let_GivenValidMethodNode_ReturnsCorrectTransferObject()
        {
            //Arrange
            GraphFactory.Clear();
            var graph = GraphFactory.Create("G");
            GenericReactiveObject3 obj = new GenericReactiveObject3();

            //Act
            TransferObject to = graph.Let(() => obj.Update_A());

            //Assert
            Assert.IsTrue(to.Graph == graph && to.Nodes.Exists(n => n.OwnerObject == obj && n.MemberName == "Update_A"));
        }

        [TestMethod]
        public void Let_GivenMultipleValidMethodNodes_ReturnsCorrectTransferObject()
        {
            //Arrange
            GraphFactory.Clear();
            var graph = GraphFactory.Create("G");
            GenericReactiveObject obj1 = new GenericReactiveObject();
            GenericReactiveObject2 obj2 = new GenericReactiveObject2();
            GenericReactiveObject3 obj3 = new GenericReactiveObject3();

            //Act
            TransferObject to = graph.Let(() => obj1.Update_A(), () => obj2.Update_F(), () => obj3.Update_A());

            //Assert
            Assert.IsTrue(to.Graph == graph
                && to.Nodes.Exists(n => n.OwnerObject == obj1 && n.MemberName == "Update_A")
                && to.Nodes.Exists(n => n.OwnerObject == obj2 && n.MemberName == "Update_F")
                && to.Nodes.Exists(n => n.OwnerObject == obj3 && n.MemberName == "Update_A"));
        }

        #endregion
    }
}
