using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeCore.ReactiveCollections;
using ReframeCoreExamples.E07;
using System.Collections.Generic;
using ReframeCore.Exceptions;
using ReframeCoreExamples.E08.E1;
using ReframeCore;
using ReframeCore.Factories;
using ReframeCore.Helpers;

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
            Part p = new Part();

            //Act
            collection.Add(p);

            //Assert
            Assert.IsTrue(collection.Contains(p));
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

        #region Remove

        [TestMethod]
        public void Remove_GivenNullObject_ReturnsFalse()
        {
            //Arrange
            ReactiveCollection<Part> collection = new ReactiveCollection<Part>();
            collection.Add(new Part());

            //Act
            bool removed = collection.Remove(null);

            //Assert
            Assert.IsFalse(removed);
        }

        [TestMethod]
        public void Remove_GivenNonExistingObject_ReturnsFalse()
        {
            //Arrange
            ReactiveCollection<Part> collection = new ReactiveCollection<Part>();
            collection.Add(new Part());

            //Act
            bool removed = collection.Remove(new Part());

            //Assert
            Assert.IsFalse(removed);
        }

        [TestMethod]
        public void Remove_GivenExistingObject_ReturnsTrue()
        {
            //Arrange
            ReactiveCollection<Part> collection = new ReactiveCollection<Part>();
            Part p = new Part();
            collection.Add(p);

            //Act
            bool removed = collection.Remove(p);

            //Assert
            Assert.IsTrue(removed);
        }

        #endregion

        #region Events

        [TestMethod]
        public void ItemAdded_GivenItemIsAdded_EventIsTriggered()
        {
            //Arrange
            List<Part> addedItems = new List<Part>();
            ReactiveCollection<Part> collection = new ReactiveCollection<Part>();

            collection.ItemAdded += delegate (object sender, ReactiveCollectionEventArgs<Part> e)
            {
                foreach (var item in e.AddedItems)
                {
                    addedItems.Add(item);
                }
            };

            //Act
            Part p1 = new Part { Name = "P1"};
            Part p2 = new Part { Name = "P2" };

            collection.Add(p1);
            collection.Add(p2);

            //Assert
            Assert.IsTrue(addedItems.Contains(p1) && addedItems.Contains(p2));
        }

        [TestMethod]
        public void ItemAdded_GivenItemIsRemoved_EventIsNotTriggered()
        {
            //Arrange
            List<Part> addedItems = new List<Part>();
            ReactiveCollection<Part> collection = new ReactiveCollection<Part>();

            Part p1 = new Part { Name = "P1" };
            Part p2 = new Part { Name = "P2" };

            collection.Add(p1);
            collection.Add(p2);

            collection.ItemAdded += delegate (object sender, ReactiveCollectionEventArgs<Part> e)
            {
                foreach (var item in e.AddedItems)
                {
                    addedItems.Add(item);
                }
            };

            //Act
            collection.Remove(p1);
            collection.Remove(p2);

            //Assert
            Assert.IsTrue(addedItems.Count == 0);
        }

        [TestMethod]
        public void ItemRemoved_GivenItemIsRemoved_EventIsTriggered()
        {
            //Arrange
            List<Part> removedItems = new List<Part>();
            ReactiveCollection<Part> collection = new ReactiveCollection<Part>();

            Part p1 = new Part { Name = "P1"};
            Part p2 = new Part { Name = "P2"};

            collection.Add(p1);
            collection.Add(p2);

            collection.ItemRemoved += delegate (object sender, ReactiveCollectionEventArgs<Part> e)
            {
                foreach (var item in e.RemovedItems)
                {
                    removedItems.Add(item);
                }
            };

            //Act
            collection.Remove(p1);
            collection.Remove(p2);

            //Assert
            Assert.IsTrue(removedItems.Contains(p1) == true && removedItems.Contains(p2) == true);
        }

        [TestMethod]
        public void ItemRemoved_GivenItemIsAdded_EventIsNotTriggered()
        {
            //Arrange
            List<Part> removedItems = new List<Part>();
            ReactiveCollection<Part> collection = new ReactiveCollection<Part>();

            collection.ItemRemoved += delegate (object sender, ReactiveCollectionEventArgs<Part> e)
            {
                foreach (var item in e.RemovedItems)
                {
                    removedItems.Add(item);
                }
            };

            //Act
            Part p1 = new Part { Name = "P1" };
            Part p2 = new Part { Name = "P2" };

            collection.Add(p1);
            collection.Add(p2);

            //Assert
            Assert.IsTrue(removedItems.Count == 0);
        }

        [TestMethod]
        public void CollectionChanged_GivenItemIsAdded_EventIsTriggered()
        {
            //Arrange
            List<Part> addedItems = new List<Part>();
            ReactiveCollection<Part> collection = new ReactiveCollection<Part>();

            collection.CollectionChanged += delegate (object sender, ReactiveCollectionEventArgs<Part> e)
            {
                foreach (var item in e.AddedItems)
                {
                    addedItems.Add(item);
                }
            };

            //Act
            Part p1 = new Part { Name = "P1" };
            Part p2 = new Part { Name = "P2" };

            collection.Add(p1);
            collection.Add(p2);

            //Assert
            Assert.IsTrue(addedItems.Contains(p1) && addedItems.Contains(p2));
        }

        [TestMethod]
        public void CollectionChanged_GivenItemIsRemoved_EventIsTriggered()
        {
            //Arrange
            List<Part> removedItems = new List<Part>();
            ReactiveCollection<Part> collection = new ReactiveCollection<Part>();

            Part p1 = new Part { Name = "P1" };
            Part p2 = new Part { Name = "P2" };

            collection.Add(p1);
            collection.Add(p2);

            collection.CollectionChanged += delegate (object sender, ReactiveCollectionEventArgs<Part> e)
            {
                foreach (var item in e.RemovedItems)
                {
                    removedItems.Add(item);
                }
            };

            //Act
            collection.Remove(p1);
            collection.Remove(p2);

            //Assert
            Assert.IsTrue(removedItems.Contains(p1) == true && removedItems.Contains(p2) == true);
        }

        [TestMethod]
        public void UpdateTriggered_GivenCollectionItemTriggeredEvent_EventIsTriggered()
        {
            //Arrange
            GraphRegistry.Instance.Clear();
            var graph = GraphRegistry.Instance.CreateGraph("GRAPH_CASE_8_1");
            var scheduler = new Scheduler(graph, new DFS_Sorter());
            var updater = new Updater(graph, scheduler);

            ReactiveCollection<Part_8_1> parts = new ReactiveCollection<Part_8_1>();

            parts.Add(new Part_8_1(updater) { Name = "P1" });
            parts.Add(new Part_8_1(updater) { Name = "P2" });

            graph.AddNode(parts, "A");
            graph.Initialize();

            

            bool eventTriggered = false;

            parts.UpdateTriggered += delegate (object sender, EventArgs e)
            {
                eventTriggered = true;
            };

            //Act
            parts[0].A = 3;

            //Assert
            Assert.IsTrue(eventTriggered);
        }

        #endregion
    }
}
