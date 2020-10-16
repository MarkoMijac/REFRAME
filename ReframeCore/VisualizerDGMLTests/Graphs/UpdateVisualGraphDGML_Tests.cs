using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.GraphModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeAnalyzer.Nodes;
using VisualizerDGML.Graphs;

namespace VisualizerDGMLTests.Graphs
{
    [TestClass]
    public class UpdateVisualGraphDGML_Tests
    {
        [TestMethod]
        public void GetGraph_GivenEmptyListOfNodes_ReturnsEmptyDgmlGraph()
        {
            //Arrange
            var visualGraph = new UpdateVisualGraphDGML("G1", new List<IAnalysisNode>());
            var privateVisualGraph = new PrivateObject(visualGraph, new PrivateType(typeof(VisualGraphDGML)));

            //Act
            Graph dgmlGraph = privateVisualGraph.Invoke("GetGraph") as Graph;

            //Assert
            Assert.IsTrue(dgmlGraph != null
                && dgmlGraph.Nodes.Count == 0);
        }

        [TestMethod]
        public void GetGraph_SimpleScenario_ReturnsCorrectDgmlGraph()
        {
            //Arrange
            var analysisNodes = VisualizationTestHelper.GetUpdateAnalysisNodes();
            var visualGraph = new UpdateVisualGraphDGML("G1", analysisNodes);
            var privateVisualGraph = new PrivateObject(visualGraph, new PrivateType(typeof(VisualGraphDGML)));

            //Act
            Graph dgmlGraph = privateVisualGraph.Invoke("GetGraph") as Graph;

            //Assert
            Assert.IsTrue(dgmlGraph != null
                && dgmlGraph.Nodes.Count == 3
                && dgmlGraph.DocumentSchema.Properties.Count == 15
                && dgmlGraph.Links.Count == 2);
        }

        [TestMethod]
        public void GetGraph_GivenObjectLevelGrouping_GroupsExist()
        {
            //Arrange
            var analysisNodes = VisualizationTestHelper.GetUpdateAnalysisNodes();
            var visualGraph = new UpdateVisualGraphDGML("G1", analysisNodes);
            visualGraph.Options.ChosenGroupingLevel = ReframeVisualizer.GroupingLevel.ObjectLevel;
            var privateVisualGraph = new PrivateObject(visualGraph, new PrivateType(typeof(VisualGraphDGML)));

            //Act
            Graph dgmlGraph = privateVisualGraph.Invoke("GetGraph") as Graph;

            //Assert
            Assert.IsTrue(dgmlGraph != null && dgmlGraph.Groups.Count > 0);
        }

        [TestMethod]
        public void GetGraph_GivenClassLevelGrouping_GroupsExist()
        {
            //Arrange
            var analysisNodes = VisualizationTestHelper.GetUpdateAnalysisNodes();
            var visualGraph = new UpdateVisualGraphDGML("G1", analysisNodes);
            visualGraph.Options.ChosenGroupingLevel = ReframeVisualizer.GroupingLevel.ClassLevel;
            var privateVisualGraph = new PrivateObject(visualGraph, new PrivateType(typeof(VisualGraphDGML)));

            //Act
            Graph dgmlGraph = privateVisualGraph.Invoke("GetGraph") as Graph;

            //Assert
            Assert.IsTrue(dgmlGraph != null && dgmlGraph.Groups.Count > 0);
        }

        [TestMethod]
        public void GetGraph_GivenNamespaceLevelGrouping_GroupsExist()
        {
            //Arrange
            var analysisNodes = VisualizationTestHelper.GetUpdateAnalysisNodes();
            var visualGraph = new UpdateVisualGraphDGML("G1", analysisNodes);
            visualGraph.Options.ChosenGroupingLevel = ReframeVisualizer.GroupingLevel.NamespaceLevel;
            var privateVisualGraph = new PrivateObject(visualGraph, new PrivateType(typeof(VisualGraphDGML)));

            //Act
            Graph dgmlGraph = privateVisualGraph.Invoke("GetGraph") as Graph;

            //Assert
            Assert.IsTrue(dgmlGraph != null && dgmlGraph.Groups.Count > 0);
        }

        [TestMethod]
        public void GetGraph_GivenAssemblyLevelGrouping_GroupsExist()
        {
            //Arrange
            var analysisNodes = VisualizationTestHelper.GetUpdateAnalysisNodes();
            var visualGraph = new UpdateVisualGraphDGML("G1", analysisNodes);
            visualGraph.Options.ChosenGroupingLevel = ReframeVisualizer.GroupingLevel.AssemblyLevel;
            var privateVisualGraph = new PrivateObject(visualGraph, new PrivateType(typeof(VisualGraphDGML)));

            //Act
            Graph dgmlGraph = privateVisualGraph.Invoke("GetGraph") as Graph;

            //Assert
            Assert.IsTrue(dgmlGraph != null && dgmlGraph.Groups.Count > 0);
        }
    }
}
