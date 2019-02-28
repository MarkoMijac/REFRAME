using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeCore.ReactiveCollections;
using ReframeCoreExamples.E07;
using System.Collections.Generic;
using ReframeCore.Exceptions;

namespace ReframeCoreTests
{
    [TestClass]
    public class ReactiveCollectionTests
    {
        #region Add

        [TestMethod]
        public void Add_GivenValidObject_AddsObjectToCollection()
        {
            //Arrange
            ReactiveCollection<Part> collection = new ReactiveCollection<Part>();
            int initialCount = collection.Count;

            //Act
            collection.Add(new Part());

            //Assert
            Assert.AreEqual(initialCount + 1, collection.Count);
        }

        [TestMethod]
        public void Add_GivenInvalidObject_ThrowsException()
        {
            //Arrange
            ReactiveCollection<Whole> collection = new ReactiveCollection<Whole>();

            //Act&Assert
            Assert.ThrowsException<ReactiveCollectionException>(() => collection.Add(new Whole()));
        }

        [TestMethod]
        public void Add_GivenNullObject_ThrowsException()
        {
            //Arrange
            ReactiveCollection<Whole> collection = new ReactiveCollection<Whole>();

            //Act&Assert
            Assert.ThrowsException<ReactiveCollectionException>(() => collection.Add(null));
        }

        #endregion
    }
}
