using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using ReframeCore;
using ReframeCoreExamples.E00;
using ReframeCore.Helpers;

namespace ReframeCoreTests
{
    public partial class UpdaterTests
    {
        [TestMethod]
        public async Task PerformUpdate_GivenPerformUpdateWithInitialNodeIsPerformedInSeparateThread1_SchedulesCorrectUpdate()
        {
            //Arrange
            DependencyGraph graph = new DependencyGraph("G1");
            GenericReactiveObject o = new GenericReactiveObject();
            graph.AddDependency(o, "A", o, "B");
            graph.AddDependency(o, "B", o, "C");
            graph.AddDependency(o, "C", o, "D");
            graph.AddDependency(o, "D", o, "E");
            graph.Initialize();

            Updater updater = CreateUpdater(graph);
            updater.EnableUpdateInSeparateThread = true;

            //Act
            Task task = updater.PerformUpdate(graph.GetNode(o, "B"));
            if (task != null) await task;

            //Assert
            UpdateLogger logger = updater.NodeLog;
            UpdateLogger expectedLogger = new UpdateLogger();
            expectedLogger.Log(graph.GetNode(o, "C"));
            expectedLogger.Log(graph.GetNode(o, "D"));
            expectedLogger.Log(graph.GetNode(o, "E"));

            Assert.IsTrue(expectedLogger.Equals(logger));
        }


    }
}
