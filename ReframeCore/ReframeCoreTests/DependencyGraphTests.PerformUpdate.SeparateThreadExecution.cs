using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeCore;
using ReframeCore.Helpers;
using ReframeCoreExamples.E00;
using System;
using System.Collections.Generic;
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
            graph.UpdateScheduler.PerformUpdateInSeparateThread = true;

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
            graph.UpdateScheduler.PerformUpdateInSeparateThread = true;

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
            graph.UpdateScheduler.PerformUpdateInSeparateThread = true;

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
            graph.UpdateScheduler.PerformUpdateInSeparateThread = true;

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
    }
}
