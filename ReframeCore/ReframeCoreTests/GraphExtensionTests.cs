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
    }
}
