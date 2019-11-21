using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeCore;
using ReframeCoreExamples.E00;
using ReframeCore.FluentAPI;
using ReframeCore.Nodes;
using System.Collections.Generic;
using ReframeCoreExamples.E07;
using ReframeCore.ReactiveCollections;
using ReframeCore.Exceptions;
using ReframeCore.Factories;

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
            GraphRegistry.Clear();
            IDependencyGraph graph = null;
            GenericReactiveObject3 obj = new GenericReactiveObject3();

            //Act&Assert
            Assert.ThrowsException<FluentException>(() => graph.Let(()=>obj.A));
        }

        [TestMethod]
        public void Let_GivenOwnerObjectIsNull_ThrowsException()
        {
            //Arrange
            GraphRegistry.Clear();
            IDependencyGraph graph = GraphRegistry.Create("G");
            GenericReactiveObject3 obj = null;

            //Act&Assert
            Assert.ThrowsException<FluentException>(() => graph.Let(() => obj.A));
        }

        [TestMethod]
        public void Let_GivenNestedOwnerObjectIsNull_ThrowsException()
        {
            //Arrange
            GraphRegistry.Clear();
            IDependencyGraph graph = GraphRegistry.Create("G");
            GenericReactiveObject3 obj = new GenericReactiveObject3();
            obj.NestedObject = null;

            //Act&Assert
            Assert.ThrowsException<FluentException>(() => graph.Let(() => obj.NestedObject.A));
        }

        [TestMethod]
        public void Let_GivenMemberNameEmpty_ThrowsException()
        {
            //Arrange
            GraphRegistry.Clear();
            IDependencyGraph graph = GraphRegistry.Create("G");
            GenericReactiveObject3 obj = null;

            //Act&Assert
            Assert.ThrowsException<FluentException>(() => graph.Let(() => obj));
        }

        [TestMethod]
        public void Let_GivenValidGraphAndProperty_ReturnsTransferObject()
        {
            //Arrange
            GraphRegistry.Clear();
            var graph = GraphRegistry.Create("G");
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
            GraphRegistry.Clear();
            var graph = GraphRegistry.Create("G");
            GenericReactiveObject3 obj = new GenericReactiveObject3();

            //Act
            TransferObject to = graph.Let(() => obj.A);

            //Assert
            Assert.IsTrue(to.Graph == graph && to.Successors.Exists(n => n.OwnerObject == obj && n.MemberName == "A"));
        }

        [TestMethod]
        public void Let_GivenMultipleValidPropertyNodes_ReturnsCorrectTransferObject()
        {
            //Arrange
            GraphRegistry.Clear();
            var graph = GraphRegistry.Create("G");
            GenericReactiveObject obj1 = new GenericReactiveObject();
            GenericReactiveObject2 obj2 = new GenericReactiveObject2();
            GenericReactiveObject3 obj3 = new GenericReactiveObject3();

            //Act
            TransferObject to = graph.Let(() => obj1.A, () => obj2.B, () => obj3.C);

            //Assert
            Assert.IsTrue(to.Graph == graph
                && to.Successors.Exists(n => n.OwnerObject == obj1 && n.MemberName == "A")
                && to.Successors.Exists(n => n.OwnerObject == obj2 && n.MemberName == "B")
                && to.Successors.Exists(n => n.OwnerObject == obj3 && n.MemberName == "C"));
        }

        [TestMethod]
        public void Let_GivenValidGraphAndMethod_ReturnsTransferObject()
        {
            //Arrange
            GraphRegistry.Clear();
            var graph = GraphRegistry.Create("G");
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
            GraphRegistry.Clear();
            var graph = GraphRegistry.Create("G");
            GenericReactiveObject3 obj = new GenericReactiveObject3();

            //Act
            TransferObject to = graph.Let(() => obj.Update_A());

            //Assert
            Assert.IsTrue(to.Graph == graph && to.Successors.Exists(n => n.OwnerObject == obj && n.MemberName == "Update_A"));
        }

        [TestMethod]
        public void Let_GivenMultipleValidMethodNodes_ReturnsCorrectTransferObject()
        {
            //Arrange
            GraphRegistry.Clear();
            var graph = GraphRegistry.Create("G");
            GenericReactiveObject obj1 = new GenericReactiveObject();
            GenericReactiveObject2 obj2 = new GenericReactiveObject2();
            GenericReactiveObject3 obj3 = new GenericReactiveObject3();

            //Act
            TransferObject to = graph.Let(() => obj1.Update_A(), () => obj2.Update_F(), () => obj3.Update_A());

            //Assert
            Assert.IsTrue(to.Graph == graph
                && to.Successors.Exists(n => n.OwnerObject == obj1 && n.MemberName == "Update_A")
                && to.Successors.Exists(n => n.OwnerObject == obj2 && n.MemberName == "Update_F")
                && to.Successors.Exists(n => n.OwnerObject == obj3 && n.MemberName == "Update_A"));
        }

        #endregion

        #region Let Collection

        [TestMethod]
        public void LetCollection_GivenGraphIsNull_ThrowsException()
        {
            //Arrange
            GraphRegistry.Clear();
            IDependencyGraph graph = null;
            Whole whole = new Whole();

            //Act&Assert
            Assert.ThrowsException<FluentException>(() => graph.Let(whole.Parts, () => whole.Parts[0].A));
        }

        [TestMethod]
        public void LetCollection_GivenCollectionIsNull_ThrowsException()
        {
            //Arrange
            GraphRegistry.Clear();
            IDependencyGraph graph = GraphRegistry.Create("G");
            Whole whole = new Whole();
            whole.Parts = null;

            //Act&Assert
            Assert.ThrowsException<FluentException>(() => graph.Let(whole.Parts, () => whole.Parts[0].A));
        }

        [TestMethod]
        public void LetCollection_GivenValidPropertyNode_ReturnsCorrectTransferObject()
        {
            //Arrange
            GraphRegistry.Clear();
            var graph = GraphRegistry.Create("G");
            Whole obj = new Whole();

            //Act
            TransferObject to = graph.Let(obj.Parts, () => obj.Parts[0].A);

            //Assert
            Assert.IsTrue(to != null && to.Graph == graph && to.Successors.Exists(n => n.OwnerObject == obj.Parts && n.MemberName == "A"));
        }

        [TestMethod]
        public void LetCollection_GivenValidMethodNode_ReturnsCorrectTransferObject()
        {
            //Arrange
            GraphRegistry.Clear();
            var graph = GraphRegistry.Create("G");
            Whole obj = new Whole();

            //Act
            TransferObject to = graph.Let(obj.Parts, () => obj.Parts[0].Update_A());

            //Assert
            Assert.IsTrue(to != null && to.Graph == graph && to.Successors.Exists(n => n.OwnerObject == obj.Parts && n.MemberName == "Update_A"));
        }

        #endregion

        #region DependOn

        [TestMethod]
        [ExpectedException(typeof(FluentException))]
        public void DependOn_GivenTransferObjectIsNull_ThrowsException()
        {
            //Arrange
            TransferObject transferObject = null;
            GenericReactiveObject obj = new GenericReactiveObject();

            //Act&Assert
            transferObject.DependOn(() => obj.B);
        }

        [TestMethod]
        [ExpectedException(typeof(FluentException))]
        public void DependOn_GivenGraphIsNull_ThrowsException()
        {
            //Arrange
            IDependencyGraph graph = null;
            NodeFactory factory = new StandardNodeFactory();
            GenericReactiveObject obj = new GenericReactiveObject();
            
            List<INode> successors = new List<INode>();
            successors.Add(factory.CreateNode(obj, "A"));

            TransferObject transferObject = new TransferObject(graph, successors);
            
            //Act&Assert
            transferObject.DependOn(() => obj.B);
        }

        [TestMethod]
        [ExpectedException(typeof(FluentException))]
        public void DependOn_GivenSuccessorsListIsNull_ThrowsException()
        {
            //Arrange
            IDependencyGraph graph = null;
            NodeFactory factory = new StandardNodeFactory();
            GenericReactiveObject obj = new GenericReactiveObject();

            List<INode> successors = null;

            TransferObject transferObject = new TransferObject(graph, successors);

            //Act&Assert
            transferObject.DependOn(() => obj.B);
        }

        [TestMethod]
        [ExpectedException(typeof(FluentException))]
        public void DependOn_GivenSuccessorsListIsEmpty_ThrowsException()
        {
            //Arrange
            IDependencyGraph graph = null;
            NodeFactory factory = new StandardNodeFactory();
            GenericReactiveObject obj = new GenericReactiveObject();

            List<INode> successors = new List<INode>();

            TransferObject transferObject = new TransferObject(graph, successors);

            //Act&Assert
            transferObject.DependOn(() => obj.B);
        }

        [TestMethod]
        [ExpectedException(typeof(FluentException))]
        public void DependOn_GivenOwnerObjectIsNull_ThrowsException()
        {
            //Arrange
            GraphRegistry.Clear();
            IDependencyGraph graph = GraphRegistry.Create("G");
            GenericReactiveObject obj1 = new GenericReactiveObject();
            GenericReactiveObject obj2 = null;

            //Act&Assert
            graph.Let(() => obj1.A).DependOn(() => obj2.B);
        }

        [TestMethod]
        [ExpectedException(typeof(FluentException))]
        public void DependOn_GivenNestedOwnerObjectIsNull_ThrowsException()
        {
            //Arrange
            GraphRegistry.Clear();
            IDependencyGraph graph = GraphRegistry.Create("G");
            GenericReactiveObject3 obj = new GenericReactiveObject3();
            obj.NestedObject = null;

            //Act&Assert
            graph.Let(() => obj.A).DependOn(() => obj.NestedObject.B);
        }

        [TestMethod]
        [ExpectedException(typeof(FluentException))]
        public void DependOn_GivenMemberNameEmpty_ThrowsException()
        {
            //Arrange
            GraphRegistry.Clear();
            IDependencyGraph graph = GraphRegistry.Create("G");
            GenericReactiveObject obj = new GenericReactiveObject();

            //Act&Assert
            graph.Let(() => obj.A).DependOn(() => obj);
        }

        [TestMethod]
        public void DependOn_GivenValidPropertyPredecessor_AddsDependency()
        {
            //Arrange
            GraphRegistry.Clear();
            IDependencyGraph graph = GraphRegistry.Create("G");
            GenericReactiveObject obj = new GenericReactiveObject();

            //Act
            graph.Let(() => obj.A).DependOn(() => obj.B);

            //Assert
            INode successor = graph.GetNode(obj, "A");
            INode predecessor = graph.GetNode(obj, "B");

            Assert.IsTrue(graph.ContainsDependency(predecessor, successor));
        }

        [TestMethod]
        public void DependOn_GivenValidNestedPropertyPredecessor_AddsDependency()
        {
            //Arrange
            GraphRegistry.Clear();
            IDependencyGraph graph = GraphRegistry.Create("G");
            GenericReactiveObject3 obj = new GenericReactiveObject3();
            obj.NestedObject = new GenericReactiveObject();

            //Act
            graph.Let(() => obj.A).DependOn(() => obj.NestedObject.B);

            //Assert
            INode successor = graph.GetNode(obj, "A");
            INode predecessor = graph.GetNode(obj.NestedObject, "B");

            Assert.IsTrue(graph.ContainsDependency(predecessor, successor));
        }

        [TestMethod]
        public void DependOn_GivenMultipleValidPropertyPredecessors_AddsDependencies()
        {
            //Arrange
            GraphRegistry.Clear();
            IDependencyGraph graph = GraphRegistry.Create("G");
            GenericReactiveObject obj = new GenericReactiveObject();

            //Act
            graph.Let(() => obj.A).DependOn(() => obj.B, () => obj.C);

            //Assert
            INode successor = graph.GetNode(obj, "A");
            INode predecessor1 = graph.GetNode(obj, "B");
            INode predecessor2 = graph.GetNode(obj, "C");

            Assert.IsTrue(graph.ContainsDependency(predecessor1, successor)
                && graph.ContainsDependency(predecessor2, successor));
        }

        [TestMethod]
        public void DependOn_GivenValidMethodPredecessor_AddsDependency()
        {
            //Arrange
            GraphRegistry.Clear();
            IDependencyGraph graph = GraphRegistry.Create("G");
            GenericReactiveObject obj = new GenericReactiveObject();

            //Act
            graph.Let(() => obj.Update_A()).DependOn(() => obj.Update_B());

            //Assert
            INode successor = graph.GetNode(obj, "Update_A");
            INode predecessor = graph.GetNode(obj, "Update_B");

            Assert.IsTrue(graph.ContainsDependency(predecessor, successor));
        }

        #endregion

        #region DependOn Collection

        [TestMethod]
        [ExpectedException(typeof(FluentException))]
        public void DependOnCollection_GivenTransferObjectIsNull_ThrowsException()
        {
            //Arrange
            TransferObject transferObject = null;
            Whole whole = new Whole();

            //Act&Assert
            transferObject.DependOn(whole.Parts, () => whole.Parts[0].A);
        }

        [TestMethod]
        [ExpectedException(typeof(FluentException))]
        public void DependOnCollection_GivenGraphIsNull_ThrowsException()
        {
            //Arrange
            IDependencyGraph graph = null;
            NodeFactory factory = new StandardNodeFactory();
            Whole whole = new Whole();

            List<INode> successors = new List<INode>();
            successors.Add(factory.CreateNode(whole, "A"));

            TransferObject transferObject = new TransferObject(graph, successors);

            //Act&Assert
            transferObject.DependOn(whole.Parts, () => whole.Parts[0].A);
        }

        [TestMethod]
        [ExpectedException(typeof(FluentException))]
        public void DependOnCollection_GivenSuccessorsListIsNull_ThrowsException()
        {
            //Arrange
            IDependencyGraph graph = null;
            NodeFactory factory = new StandardNodeFactory();
            Whole whole = new Whole();

            List<INode> successors = null;

            TransferObject transferObject = new TransferObject(graph, successors);

            //Act&Assert
            transferObject.DependOn(whole.Parts, () => whole.Parts[0].B);
        }

        [TestMethod]
        [ExpectedException(typeof(FluentException))]
        public void DependOnCollection_GivenSuccessorsListIsEmpty_ThrowsException()
        {
            //Arrange
            IDependencyGraph graph = null;
            Whole whole = new Whole();

            List<INode> successors = new List<INode>();

            TransferObject transferObject = new TransferObject(graph, successors);

            //Act&Assert
            transferObject.DependOn(whole.Parts, () => whole.Parts[0].B);
        }

        [TestMethod]
        [ExpectedException(typeof(FluentException))]
        public void DependOnCollection_GivenCollectionIsNull_ThrowsException()
        {
            //Arrange
            GraphRegistry.Clear();
            var graph = GraphRegistry.Create("G");
            Whole whole = new Whole();
            whole.Parts = null;

            //Act
            graph.Let(() => whole.A).DependOn(whole.Parts, () => whole.Parts[0].A);
        }

        [TestMethod]
        public void DependOnCollection_GivenValidCollectionPropertyPredecessor_AddsDependency()
        {
            //Arrange
            GraphRegistry.Clear();
            var graph = GraphRegistry.Create("G");
            Whole whole = new Whole();

            //Act
            graph.Let(() => whole.A).DependOn(whole.Parts, () => whole.Parts[0].A);

            //Assert
            INode predecessor = graph.GetNode(whole.Parts, "A");
            INode successor = graph.GetNode(whole, "A");
            Assert.IsTrue(graph.ContainsDependency(predecessor, successor));
        }

        [TestMethod]
        public void DependOnCollection_GivenValidCollectionMethodPredecessor_AddsDependency()
        {
            //Arrange
            GraphRegistry.Clear();
            var graph = GraphRegistry.Create("G");
            Whole whole = new Whole();

            //Act
            graph.Let(() => whole.Update_A()).DependOn(whole.Parts, () => whole.Parts[0].Update_A());

            //Assert
            INode predecessor = graph.GetNode(whole.Parts, "Update_A");
            INode successor = graph.GetNode(whole, "Update_A");
            Assert.IsTrue(graph.ContainsDependency(predecessor, successor));
        }

        #endregion

        #region Update

        [TestMethod]
        public void Update_GivenProvidedGraphIsNull_ThrowsException()
        {
            //Arrange
            IDependencyGraph graph = null;
            Building00 b = new Building00();

            //Act&Assert
            Assert.ThrowsException<DependencyGraphException>(() => b.Update(graph, nameof(b.Width)));
        }

        [TestMethod]
        public void Update_GivenProvidedOwnerIsNull_ThrowsException()
        {
            //Arrange
            IDependencyGraph graph = GraphRegistry.GetDefault();
            Building00 b = null;

            //Act&Assert
            Assert.ThrowsException<NodeNullReferenceException>(() => b.Update(graph, nameof(b.Width)));
        }

        [TestMethod]
        public void Update_GivenProvidedNodeIsNotPartOfGraph_ThrowsException()
        {
            //Arrange
            IDependencyGraph graph = GraphRegistry.GetDefault();
            graph.Initialize();
            Building00 b = new Building00();

            //Act&Asset
            Assert.ThrowsException<NodeNullReferenceException>(() => b.Update(graph, nameof(b.Width)));
        }

        [TestMethod]
        public void Update_GivenProvideIsPartOfGraph_PerformsUpdate()
        {
            //Arrange
            IDependencyGraph graph = GraphRegistry.GetDefault();
            graph.Initialize();
            Building00 b = new Building00();
            NodeFactory nodeFactory = new StandardNodeFactory();
            INode width = nodeFactory.CreateNode(b, nameof(b.Width));
            INode area = nodeFactory.CreateNode(b, nameof(b.Area));

            bool updatePerformed = false;
            graph.AddDependency(width, area);
            graph.UpdateCompleted += delegate
              {
                  updatePerformed = true;
              };

            //Act
            b.Update(graph, nameof(b.Width));

            //Assert
            Assert.IsTrue(updatePerformed);
        }

        #endregion
    }
}
