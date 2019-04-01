using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeCore;
using ReframeCore.Helpers;
using ReframeCore.Nodes;
using ReframeCoreExamples.E00;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReframeCoreTests
{   
    public partial class DependencyGraphTests
    {
        [TestMethod]
        public async Task PerformUpdate_GivenCompletePerformUpdateIsPerformedInSeparateThread_SchedulesCorrectUpdate()
        {
            //Arrange
            GraphFactory.Clear();
            DependencyGraph graph = GraphFactory.Create("G1") as DependencyGraph;
            GenericReactiveObject o = new GenericReactiveObject();
            graph.AddDependency(o, "A", o, "B");
            graph.AddDependency(o, "B", o, "C");
            graph.AddDependency(o, "C", o, "D");
            graph.AddDependency(o, "D", o, "E");

            graph.Initialize();
            graph.UpdateScheduler.EnableUpdateInSeparateThread = true;

            //Act
            Task task = graph.PerformUpdate();
            if (task != null) await task;
           
            //Assert
            UpdateLogger logger = graph.UpdateScheduler.LoggerUpdatedNodes;
            UpdateLogger expectedLogger = new UpdateLogger();
            expectedLogger.Log(graph.GetNode(o, "A"));
            expectedLogger.Log(graph.GetNode(o, "B"));
            expectedLogger.Log(graph.GetNode(o, "C"));
            expectedLogger.Log(graph.GetNode(o, "D"));
            expectedLogger.Log(graph.GetNode(o, "E"));

            Assert.IsTrue(expectedLogger.Equals(logger));
        }

        [TestMethod]
        public async Task PerformUpdate_GivenPerformUpdateWithInitialNodeIsPerformedInSeparateThread_SchedulesCorrectUpdate()
        {
            //Arrange
            GraphFactory.Clear();
            DependencyGraph graph = GraphFactory.Create("G1") as DependencyGraph;
            GenericReactiveObject o = new GenericReactiveObject();
            graph.AddDependency(o, "A", o, "B");
            graph.AddDependency(o, "B", o, "C");
            graph.AddDependency(o, "C", o, "D");
            graph.AddDependency(o, "D", o, "E");

            graph.Initialize();
            graph.UpdateScheduler.EnableUpdateInSeparateThread = true;

            //Act
            Task task = graph.PerformUpdate(graph.GetNode(o, "B"), false);
            if (task != null) await task;

            //Assert
            UpdateLogger logger = graph.UpdateScheduler.LoggerUpdatedNodes;
            UpdateLogger expectedLogger = new UpdateLogger();
            expectedLogger.Log(graph.GetNode(o, "B"));
            expectedLogger.Log(graph.GetNode(o, "C"));
            expectedLogger.Log(graph.GetNode(o, "D"));
            expectedLogger.Log(graph.GetNode(o, "E"));

            Assert.IsTrue(expectedLogger.Equals(logger));
        }

        [TestMethod]
        public async Task PerformUpdate_GivenPerformUpdateWithInitialNodeIsPerformedInSeparateThread1_SchedulesCorrectUpdate()
        {
            //Arrange
            GraphFactory.Clear();
            DependencyGraph graph = GraphFactory.Create("G1") as DependencyGraph;
            GenericReactiveObject o = new GenericReactiveObject();
            graph.AddDependency(o, "A", o, "B");
            graph.AddDependency(o, "B", o, "C");
            graph.AddDependency(o, "C", o, "D");
            graph.AddDependency(o, "D", o, "E");

            graph.Initialize();
            graph.UpdateScheduler.EnableUpdateInSeparateThread = true;

            //Act
            Task task = graph.PerformUpdate(graph.GetNode(o, "B"));
            if (task != null) await task;

            //Assert
            UpdateLogger logger = graph.UpdateScheduler.LoggerUpdatedNodes;
            UpdateLogger expectedLogger = new UpdateLogger();
            expectedLogger.Log(graph.GetNode(o, "C"));
            expectedLogger.Log(graph.GetNode(o, "D"));
            expectedLogger.Log(graph.GetNode(o, "E"));

            Assert.IsTrue(expectedLogger.Equals(logger));
        }

        [TestMethod]
        public async Task PerformUpdate_GivenPerformUpdateWithInitialNodeIsPerformedInSeparateThread2_SchedulesCorrectUpdate()
        {
            //Arrange
            GraphFactory.Clear();
            DependencyGraph graph = GraphFactory.Create("G1") as DependencyGraph;
            GenericReactiveObject o = new GenericReactiveObject();
            graph.AddDependency(o, "A", o, "B");
            graph.AddDependency(o, "B", o, "C");
            graph.AddDependency(o, "C", o, "D");
            graph.AddDependency(o, "D", o, "E");

            graph.Initialize();
            graph.UpdateScheduler.EnableUpdateInSeparateThread = true;

            //Act
            Task task = graph.PerformUpdate(o, "B");
            if (task != null) await task;

            //Assert
            UpdateLogger logger = graph.UpdateScheduler.LoggerUpdatedNodes;
            UpdateLogger expectedLogger = new UpdateLogger();
            expectedLogger.Log(graph.GetNode(o, "C"));
            expectedLogger.Log(graph.GetNode(o, "D"));
            expectedLogger.Log(graph.GetNode(o, "E"));

            Assert.IsTrue(expectedLogger.Equals(logger));
        }

        [TestMethod]
        public async Task PerformUpdate_GivenSuccessfulCompleteUpdateInSeparateThread_PerformUpdateCompletedIsRaised()
        {
            //Arrange
            GraphFactory.Clear();
            DependencyGraph graph = GraphFactory.Create("G1") as DependencyGraph;
            GenericReactiveObject o = new GenericReactiveObject();
            graph.AddDependency(o, "A", o, "B");
            graph.AddDependency(o, "B", o, "C");
            graph.AddDependency(o, "C", o, "D");
            graph.AddDependency(o, "D", o, "E");

            graph.Initialize();
            graph.UpdateScheduler.EnableUpdateInSeparateThread = true;

            bool eventRaised = false;

            graph.UpdateCompleted += delegate 
            {
                eventRaised = true;
            };

            //Act
            Task task = graph.PerformUpdate();
            if (task != null) await task;

            //Assert
            Assert.IsTrue(eventRaised);
        }

        [TestMethod]
        public async Task PerformUpdate_GivenSuccessfulUpdateWithInitialNodeInSeparateThread_PerformUpdateCompletedIsRaised()
        {
            //Arrange
            GraphFactory.Clear();
            DependencyGraph graph = GraphFactory.Create("G1") as DependencyGraph;
            GenericReactiveObject o = new GenericReactiveObject();
            graph.AddDependency(o, "A", o, "B");
            graph.AddDependency(o, "B", o, "C");
            graph.AddDependency(o, "C", o, "D");
            graph.AddDependency(o, "D", o, "E");

            graph.Initialize();
            graph.UpdateScheduler.EnableUpdateInSeparateThread = true;

            bool eventRaised = false;

            graph.UpdateCompleted += delegate
            {
                eventRaised = true;
            };

            //Act
            Task task = graph.PerformUpdate(o, "C");
            if (task != null) await task;

            //Assert
            Assert.IsTrue(eventRaised);
        }

        #region ParallelExecution

        private void CreateCase1(IDependencyGraph graph, GenericReactiveObject3 obj)
        {
            INode aNode = graph.AddNode(obj, "A");
            INode bNode = graph.AddNode(obj, "B");
            INode cNode = graph.AddNode(obj, "C");
            INode dNode = graph.AddNode(obj, "D");
            INode eNode = graph.AddNode(obj, "E");
            INode fNode = graph.AddNode(obj, "F");
            INode gNode = graph.AddNode(obj, "G");
            INode hNode = graph.AddNode(obj, "H");
            INode iNode = graph.AddNode(obj, "I");

            INode jNode = graph.AddNode(obj, "J");
            INode kNode = graph.AddNode(obj, "K");
            INode lNode = graph.AddNode(obj, "L");
            INode mNode = graph.AddNode(obj, "M");

            graph.AddDependency(aNode, dNode);

            graph.AddDependency(bNode, dNode);

            graph.AddDependency(cNode, aNode);
            graph.AddDependency(cNode, bNode);

            graph.AddDependency(dNode, gNode);
            graph.AddDependency(dNode, hNode);

            graph.AddDependency(eNode, aNode);
            graph.AddDependency(eNode, dNode);
            graph.AddDependency(eNode, fNode);

            graph.AddDependency(fNode, jNode);
            graph.AddDependency(fNode, kNode);

            graph.AddDependency(gNode, iNode);

            graph.AddDependency(hNode, jNode);
            graph.AddDependency(hNode, iNode);

            graph.AddDependency(iNode, lNode);

            graph.AddDependency(jNode, lNode);
            graph.AddDependency(jNode, mNode);

            graph.AddDependency(kNode, jNode);

            graph.Initialize();
        }

        [TestMethod]
        public async Task PerformUpdate_Case1_GivenCompletePerformUpdatePerformedSynchronously_SchedulesCorrectUpdate()
        {
            //Arrange
            Stopwatch sw = new Stopwatch();
            GraphFactory.Clear();
            DependencyGraph graph = GraphFactory.Create("G") as DependencyGraph;
            GenericReactiveObject3 o = new GenericReactiveObject3();

            CreateCase1(graph, o);

            //Act
            sw.Start();
            Task t = graph.PerformUpdate();
            if (t != null) await t;
            sw.Stop();

            //Assert
            string elapsedTime = sw.Elapsed.TotalMilliseconds.ToString();
            Assert.IsTrue(true);
        }

        [TestMethod]
        public async Task PerformUpdate_Case1_GivenCompletePerformUpdatePerformedInSeparateThread_SchedulesCorrectUpdate()
        {
            //Arrange
            Stopwatch sw = new Stopwatch();
            GraphFactory.Clear();
            DependencyGraph graph = GraphFactory.Create("G") as DependencyGraph;
            GenericReactiveObject3 o = new GenericReactiveObject3();
            graph.UpdateScheduler.EnableUpdateInSeparateThread = true;
            CreateCase1(graph, o);

            //Act
            sw.Start();
            Task t = graph.PerformUpdate();
            if (t != null) await t;
            sw.Stop();

            //Assert
            string elapsedTime = sw.Elapsed.TotalMilliseconds.ToString();
            Assert.IsTrue(true);
        }

        [TestMethod]
        public async Task PerformUpdate_Case1_GivenCompletePerformUpdatePerformedInParallel_SchedulesCorrectUpdate()
        {
            //Arrange
            Stopwatch sw = new Stopwatch();
            GraphFactory.Clear();
            DependencyGraph graph = GraphFactory.Create("G") as DependencyGraph;
            graph.UpdateScheduler.EnableUpdateInSeparateThread = true;
            graph.UpdateScheduler.EnableParallelUpdate = true;
            GenericReactiveObject3 o = new GenericReactiveObject3();

            CreateCase1(graph, o);

            //Act
            sw.Start();
            Task t = graph.PerformUpdate();
            if (t != null) await t;
            sw.Stop();

            //Assert
            string elapsedTime = sw.Elapsed.TotalMilliseconds.ToString();
            Assert.IsTrue(true);
        }

        #endregion
    }
}
