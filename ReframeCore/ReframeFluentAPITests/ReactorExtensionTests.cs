using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeCore.Factories;
using ReframeCore;
using ReframeCoreExamples.E00;
using ReframeCoreExamples.E07;
using ReframeCore.Nodes;
using System.Collections.Generic;
using ReframeCore.Exceptions;
using ReframeCoreFluentAPI;

namespace ReframeFluentAPITests
{
    [TestClass]
    public class ReactorExtensionTests
    {
        #region Let

        [TestMethod]
        public void Let_GivenReactorIsNull_ThrowsException()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            IReactor reactor = null;
            GenericReactiveObject3 obj = new GenericReactiveObject3();

            //Act&Assert
            Assert.ThrowsException<FluentException>(() => reactor.Let(() => obj.A));
        }

        [TestMethod]
        public void Let_GivenOwnerObjectIsNull_ThrowsException()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            IReactor reactor = ReactorRegistry.Instance.CreateReactor("R1");
            GenericReactiveObject3 obj = null;

            //Act&Assert
            Assert.ThrowsException<FluentException>(() => reactor.Let(() => obj.A));
        }

        [TestMethod]
        public void Let_GivenNestedOwnerObjectIsNull_ThrowsException()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            IReactor reactor = ReactorRegistry.Instance.CreateReactor("R1");
            GenericReactiveObject3 obj = new GenericReactiveObject3();
            obj.NestedObject = null;

            //Act&Assert
            Assert.ThrowsException<FluentException>(() => reactor.Let(() => obj.NestedObject.A));
        }

        [TestMethod]
        public void Let_GivenMemberNameEmpty_ThrowsException()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            IReactor reactor = ReactorRegistry.Instance.CreateReactor("R1");
            GenericReactiveObject3 obj = null;

            //Act&Assert
            Assert.ThrowsException<FluentException>(() => reactor.Let(() => obj));
        }

        [TestMethod]
        public void Let_GivenValidReactorAndPropertyNode_ReturnsTransferParameter()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("R1");
            GenericReactiveObject3 obj = new GenericReactiveObject3();

            //Act
            TransferParameter to = reactor.Let(() => obj.A);

            //Assert
            Assert.IsNotNull(to);
        }

        [TestMethod]
        public void Let_GivenValidPropertyNode_ReturnsCorrectTransferParameter()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("R");
            GenericReactiveObject3 obj = new GenericReactiveObject3();

            //Act
            TransferParameter to = reactor.Let(() => obj.A);

            //Assert
            Assert.IsTrue(to.Reactor == reactor && to.Successors.Exists(n => n.OwnerObject == obj && n.MemberName == "A"));
        }

        [TestMethod]
        public void Let_GivenMultipleValidPropertyNodes_ReturnsCorrectTransferParameter()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("R1");
            GenericReactiveObject obj1 = new GenericReactiveObject();
            GenericReactiveObject2 obj2 = new GenericReactiveObject2();
            GenericReactiveObject3 obj3 = new GenericReactiveObject3();

            //Act
            TransferParameter to = reactor.Let(() => obj1.A, () => obj2.B, () => obj3.C);

            //Assert
            Assert.IsTrue(to.Reactor == reactor
                && to.Successors.Exists(n => n.OwnerObject == obj1 && n.MemberName == "A")
                && to.Successors.Exists(n => n.OwnerObject == obj2 && n.MemberName == "B")
                && to.Successors.Exists(n => n.OwnerObject == obj3 && n.MemberName == "C"));
        }

        [TestMethod]
        public void Let_GivenValidReactorAndMethodNode_ReturnsTransferParameter()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("R1");
            GenericReactiveObject3 obj = new GenericReactiveObject3();

            //Act
            TransferParameter to = reactor.Let(() => obj.Update_A());

            //Assert
            Assert.IsNotNull(to);
        }

        [TestMethod]
        public void Let_GivenValidMethodNode_ReturnsCorrectTransferParameter()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("R1");
            GenericReactiveObject3 obj = new GenericReactiveObject3();

            //Act
            TransferParameter to = reactor.Let(() => obj.Update_A());

            //Assert
            Assert.IsTrue(to.Reactor == reactor && to.Successors.Exists(n => n.OwnerObject == obj && n.MemberName == "Update_A"));
        }

        [TestMethod]
        public void Let_GivenMultipleValidMethodNodes_ReturnsCorrectTransferParameter()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("R1");
            GenericReactiveObject obj1 = new GenericReactiveObject();
            GenericReactiveObject2 obj2 = new GenericReactiveObject2();
            GenericReactiveObject3 obj3 = new GenericReactiveObject3();

            //Act
            TransferParameter to = reactor.Let(() => obj1.Update_A(), () => obj2.Update_F(), () => obj3.Update_A());

            //Assert
            Assert.IsTrue(to.Reactor == reactor
                && to.Successors.Exists(n => n.OwnerObject == obj1 && n.MemberName == "Update_A")
                && to.Successors.Exists(n => n.OwnerObject == obj2 && n.MemberName == "Update_F")
                && to.Successors.Exists(n => n.OwnerObject == obj3 && n.MemberName == "Update_A"));
        }

        #endregion

        #region LetCollection

        [TestMethod]
        public void LetCollection_GivenReactorIsNull_ThrowsException()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            IReactor reactor = null;
            Whole whole = new Whole();

            //Act&Assert
            Assert.ThrowsException<FluentException>(() => reactor.Let(whole.Parts, () => whole.Parts[0].A));
        }

        [TestMethod]
        public void LetCollection_GivenCollectionIsNull_ThrowsException()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            IReactor reactor = ReactorRegistry.Instance.CreateReactor("R");
            Whole whole = new Whole();
            whole.Parts = null;

            //Act&Assert
            Assert.ThrowsException<FluentException>(() => reactor.Let(whole.Parts, () => whole.Parts[0].A));
        }

        [TestMethod]
        public void LetCollection_GivenValidPropertyNode_ReturnsCorrectTransferParameter()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("R");
            Whole obj = new Whole();

            //Act
            TransferParameter to = reactor.Let(obj.Parts, () => obj.Parts[0].A);

            //Assert
            Assert.IsTrue(to != null && to.Reactor == reactor && to.Successors.Exists(n => n.OwnerObject == obj.Parts && n.MemberName == "A"));
        }

        [TestMethod]
        public void LetCollection_GivenValidMethodNode_ReturnsCorrectTransferParameter()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("R");
            Whole obj = new Whole();

            //Act
            TransferParameter to = reactor.Let(obj.Parts, () => obj.Parts[0].Update_A());

            //Assert
            Assert.IsTrue(to != null && to.Reactor == reactor && to.Successors.Exists(n => n.OwnerObject == obj.Parts && n.MemberName == "Update_A"));
        }

        #endregion

        #region DependOn

        [TestMethod]
        [ExpectedException(typeof(FluentException))]
        public void DependOn_GivenTransferParameterIsNull_ThrowsException()
        {
            //Arrange
            TransferParameter tp = null;
            GenericReactiveObject obj = new GenericReactiveObject();

            //Act&Assert
            tp.DependOn(() => obj.B);
        }

        [TestMethod]
        [ExpectedException(typeof(FluentException))]
        public void DependOn_GivenReactorIsNull_ThrowsException()
        {
            //Arrange
            IReactor reactor = null;
            NodeFactory factory = new StandardNodeFactory();
            GenericReactiveObject obj = new GenericReactiveObject();

            List<INode> successors = new List<INode>();
            successors.Add(factory.CreateNode(obj, "A"));

            TransferParameter tp = new TransferParameter(reactor, successors);

            //Act&Assert
            tp.DependOn(() => obj.B);
        }

        [TestMethod]
        [ExpectedException(typeof(FluentException))]
        public void DependOn_GivenSuccessorsListIsNull_ThrowsException()
        {
            //Arrange
            IReactor reactor = null;
            NodeFactory factory = new StandardNodeFactory();
            GenericReactiveObject obj = new GenericReactiveObject();

            List<INode> successors = null;

            TransferParameter tp = new TransferParameter(reactor, successors);

            //Act&Assert
            tp.DependOn(() => obj.B);
        }

        [TestMethod]
        [ExpectedException(typeof(FluentException))]
        public void DependOn_GivenSuccessorsListIsEmpty_ThrowsException()
        {
            //Arrange
            IReactor reactor = null;
            NodeFactory factory = new StandardNodeFactory();
            GenericReactiveObject obj = new GenericReactiveObject();

            List<INode> successors = new List<INode>();

            TransferParameter tp = new TransferParameter(reactor, successors);

            //Act&Assert
            tp.DependOn(() => obj.B);
        }

        [TestMethod]
        [ExpectedException(typeof(FluentException))]
        public void DependOn_GivenOwnerObjectIsNull_ThrowsException()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            IReactor reactor = ReactorRegistry.Instance.CreateReactor("R");
            GenericReactiveObject obj1 = new GenericReactiveObject();
            GenericReactiveObject obj2 = null;

            //Act&Assert
            reactor.Let(() => obj1.A).DependOn(() => obj2.B);
        }

        [TestMethod]
        [ExpectedException(typeof(FluentException))]
        public void DependOn_GivenNestedOwnerObjectIsNull_ThrowsException()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            IReactor reactor = ReactorRegistry.Instance.CreateReactor("R");
            GenericReactiveObject3 obj = new GenericReactiveObject3();
            obj.NestedObject = null;

            //Act&Assert
            reactor.Let(() => obj.A).DependOn(() => obj.NestedObject.B);
        }

        [TestMethod]
        [ExpectedException(typeof(FluentException))]
        public void DependOn_GivenMemberNameEmpty_ThrowsException()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            IReactor reactor = ReactorRegistry.Instance.CreateReactor("R");
            GenericReactiveObject obj = new GenericReactiveObject();

            //Act&Assert
            reactor.Let(() => obj.A).DependOn(() => obj);
        }

        [TestMethod]
        public void DependOn_GivenValidPropertyPredecessor_AddsDependency()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            IReactor reactor = ReactorRegistry.Instance.CreateReactor("R");
            GenericReactiveObject obj = new GenericReactiveObject();

            //Act
            reactor.Let(() => obj.A).DependOn(() => obj.B);

            //Assert
            INode successor = reactor.GetNode(obj, "A");
            INode predecessor = reactor.GetNode(obj, "B");

            Assert.IsTrue(reactor.ContainsDependency(predecessor, successor));
        }

        [TestMethod]
        public void DependOn_GivenValidNestedPropertyPredecessor_AddsDependency()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            IReactor reactor = ReactorRegistry.Instance.CreateReactor("R");
            GenericReactiveObject3 obj = new GenericReactiveObject3();
            obj.NestedObject = new GenericReactiveObject();

            //Act
            reactor.Let(() => obj.A).DependOn(() => obj.NestedObject.B);

            //Assert
            INode successor = reactor.GetNode(obj, "A");
            INode predecessor = reactor.GetNode(obj.NestedObject, "B");

            Assert.IsTrue(reactor.ContainsDependency(predecessor, successor));
        }

        [TestMethod]
        public void DependOn_GivenMultipleValidPropertyPredecessors_AddsDependencies()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            IReactor graph = ReactorRegistry.Instance.CreateReactor("R");
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
            ReactorRegistry.Instance.Clear();
            IReactor reactor = ReactorRegistry.Instance.CreateReactor("R");
            GenericReactiveObject obj = new GenericReactiveObject();

            //Act
            reactor.Let(() => obj.Update_A()).DependOn(() => obj.Update_B());

            //Assert
            INode successor = reactor.GetNode(obj, "Update_A");
            INode predecessor = reactor.GetNode(obj, "Update_B");

            Assert.IsTrue(reactor.ContainsDependency(predecessor, successor));
        }

        #endregion

        #region DependOnCollection

        [TestMethod]
        [ExpectedException(typeof(FluentException))]
        public void DependOnCollection_GivenTransferParameterIsNull_ThrowsException()
        {
            //Arrange
            TransferParameter tp = null;
            Whole whole = new Whole();

            //Act&Assert
            tp.DependOn(whole.Parts, () => whole.Parts[0].A);
        }

        [TestMethod]
        [ExpectedException(typeof(FluentException))]
        public void DependOnCollection_GivenReactorIsNull_ThrowsException()
        {
            //Arrange
            IReactor reactor = null;
            NodeFactory factory = new StandardNodeFactory();
            Whole whole = new Whole();

            List<INode> successors = new List<INode>();
            successors.Add(factory.CreateNode(whole, "A"));

            TransferParameter tp = new TransferParameter(reactor, successors);

            //Act&Assert
            tp.DependOn(whole.Parts, () => whole.Parts[0].A);
        }

        [TestMethod]
        [ExpectedException(typeof(FluentException))]
        public void DependOnCollection_GivenSuccessorsListIsNull_ThrowsException()
        {
            //Arrange
            IReactor reactor = null;
            NodeFactory factory = new StandardNodeFactory();
            Whole whole = new Whole();

            List<INode> successors = null;

            TransferParameter tp = new TransferParameter(reactor, successors);

            //Act&Assert
            tp.DependOn(whole.Parts, () => whole.Parts[0].B);
        }

        [TestMethod]
        [ExpectedException(typeof(FluentException))]
        public void DependOnCollection_GivenSuccessorsListIsEmpty_ThrowsException()
        {
            //Arrange
            IReactor reactor = null;
            Whole whole = new Whole();

            List<INode> successors = new List<INode>();

            TransferParameter tp = new TransferParameter(reactor, successors);

            //Act&Assert
            tp.DependOn(whole.Parts, () => whole.Parts[0].B);
        }

        [TestMethod]
        [ExpectedException(typeof(FluentException))]
        public void DependOnCollection_GivenCollectionIsNull_ThrowsException()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("R");
            Whole whole = new Whole();
            whole.Parts = null;

            //Act
            reactor.Let(() => whole.A).DependOn(whole.Parts, () => whole.Parts[0].A);
        }

        [TestMethod]
        public void DependOnCollection_GivenValidCollectionPropertyPredecessor_AddsDependency()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("R");
            Whole whole = new Whole();

            //Act
            reactor.Let(() => whole.A).DependOn(whole.Parts, () => whole.Parts[0].A);

            //Assert
            INode predecessor = reactor.GetNode(whole.Parts, "A");
            INode successor = reactor.GetNode(whole, "A");
            Assert.IsTrue(reactor.ContainsDependency(predecessor, successor));
        }

        [TestMethod]
        public void DependOnCollection_GivenValidCollectionMethodPredecessor_AddsDependency()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("R");
            Whole whole = new Whole();

            //Act
            reactor.Let(() => whole.Update_A()).DependOn(whole.Parts, () => whole.Parts[0].Update_A());

            //Assert
            INode predecessor = reactor.GetNode(whole.Parts, "Update_A");
            INode successor = reactor.GetNode(whole, "Update_A");
            Assert.IsTrue(reactor.ContainsDependency(predecessor, successor));
        }

        #endregion

        #region Update

        [TestMethod]
        public void Update_GivenProvidedReactorIsNull_ThrowsException()
        {
            //Arrange
            IReactor reactor = null;
            Building00 b = new Building00();

            //Act&Assert
            Assert.ThrowsException<ReactorException>(() => b.Update(reactor, nameof(b.Width)));
        }

        [TestMethod]
        public void Update_GivenProvidedOwnerIsNull_ThrowsException()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("R1");

            Building00 b = null;

            //Act&Assert
            Assert.ThrowsException<NodeNullReferenceException>(() => b.Update(reactor, nameof(b.Width)));
        }

        [TestMethod]
        public void Update_GivenProvidedNodeIsNotPartOfGraph_ThrowsException()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("R1");
            Building00 b = new Building00();

            //Act&Asset
            Assert.ThrowsException<NodeNullReferenceException>(() => b.Update(reactor, nameof(b.Width)));
        }

        [TestMethod]
        public void Update_GivenProvidedNodeIsPartOfGraph_PerformsUpdate()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("R1");
            Building00 b = new Building00();
            NodeFactory nodeFactory = new StandardNodeFactory();
            INode width = nodeFactory.CreateNode(b, nameof(b.Width));
            INode area = nodeFactory.CreateNode(b, nameof(b.Area));

            bool updatePerformed = false;
            reactor.AddDependency(width, area);
            reactor.UpdateCompleted += delegate
            {
                updatePerformed = true;
            };

            //Act
            b.Update(reactor, nameof(b.Width));

            //Assert
            Assert.IsTrue(updatePerformed);
        }

        [TestMethod]
        public void Update1_GivenProvidedNodeIsPartOfGraph_PerformsUpdate()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("R1");
            Building00 b = new Building00();
            NodeFactory nodeFactory = new StandardNodeFactory();
            INode width = nodeFactory.CreateNode(b, nameof(b.Width));
            INode area = nodeFactory.CreateNode(b, nameof(b.Area));

            bool updatePerformed = false;
            reactor.AddDependency(width, area);
            reactor.UpdateCompleted += delegate
            {
                updatePerformed = true;
            };

            //Act
            reactor.Update(b, nameof(b.Width));

            //Assert
            Assert.IsTrue(updatePerformed);
        }

        [TestMethod]
        public void Update2_GivenProvidedNodeIsPartOfGraph_PerformsUpdate()
        {
            //Arrange
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("R1");
            Building00 b = new Building00();
            NodeFactory nodeFactory = new StandardNodeFactory();
            INode width = nodeFactory.CreateNode(b, nameof(b.Width));
            INode area = nodeFactory.CreateNode(b, nameof(b.Area));

            bool updatePerformed = false;
            reactor.AddDependency(width, area);
            reactor.UpdateCompleted += delegate
            {
                updatePerformed = true;
            };

            //Act
            b.Update(nameof(b.Width));

            //Assert
            Assert.IsTrue(updatePerformed);
        }

        #endregion
    }
}
