using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.GraphModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeAnalyzer.Nodes;
using VisualizerDGML.Graphs;

namespace VisualizerDGMLTests.Graphs
{
    [TestClass]
    public class ObjectMemberVisualGraphDGML_Tests
    {
        [TestMethod]
        public void GetGraph_GivenEmptyListOfNodes_ReturnsEmptyDgmlGraph()
        {
            //Arrange
            var visualGraph = new ObjectMemberVisualGraphDGML("G1", new List<IAnalysisNode>());
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
            var visualGraph = new ObjectMemberVisualGraphDGML("G1", VisualizationTestHelper.GetObjectMemberAnalysisNodes());
            var privateVisualGraph = new PrivateObject(visualGraph, new PrivateType(typeof(VisualGraphDGML)));

            //Act
            Graph dgmlGraph = privateVisualGraph.Invoke("GetGraph") as Graph;
            string serizalize = visualGraph.SerializeGraph();
            //Assert
            Assert.IsTrue(dgmlGraph != null
                && dgmlGraph.Nodes.Count == 3
                && dgmlGraph.DocumentSchema.Properties.Count == 7
                && dgmlGraph.Links.Count == 2);
        }

        [TestMethod]
        public void GetGraph_GivenObjectLevelGrouping_GroupsExist()
        {
            //Arrange
            var visualGraph = new ObjectMemberVisualGraphDGML("G1", VisualizationTestHelper.GetObjectMemberAnalysisNodes());
            visualGraph.Options.ChosenGroupingLevel = ReframeVisualizer.GroupingLevel.ObjectLevel;
            var privateVisualGraph = new PrivateObject(visualGraph, new PrivateType(typeof(VisualGraphDGML)));

            //Act
            Graph dgmlGraph = privateVisualGraph.Invoke("GetGraph") as Graph;
            string serizalize = visualGraph.SerializeGraph();
            //Assert
            Assert.IsTrue(dgmlGraph != null && dgmlGraph.Groups.Count > 0);
        }

        [TestMethod]
        public void GetGraph_GivenClassLevelGrouping_GroupsExist()
        {
            //Arrange
            var visualGraph = new ObjectMemberVisualGraphDGML("G1", VisualizationTestHelper.GetObjectMemberAnalysisNodes());
            visualGraph.Options.ChosenGroupingLevel = ReframeVisualizer.GroupingLevel.ClassLevel;
            var privateVisualGraph = new PrivateObject(visualGraph, new PrivateType(typeof(VisualGraphDGML)));

            //Act
            Graph dgmlGraph = privateVisualGraph.Invoke("GetGraph") as Graph;
            string serizalize = visualGraph.SerializeGraph();
            //Assert
            Assert.IsTrue(dgmlGraph != null && dgmlGraph.Groups.Count > 0);
        }

        [TestMethod]
        public void GetGraph_GivenNamespaceLevelGrouping_GroupsExist()
        {
            //Arrange
            var visualGraph = new ObjectMemberVisualGraphDGML("G1", VisualizationTestHelper.GetObjectMemberAnalysisNodes());
            visualGraph.Options.ChosenGroupingLevel = ReframeVisualizer.GroupingLevel.NamespaceLevel;
            var privateVisualGraph = new PrivateObject(visualGraph, new PrivateType(typeof(VisualGraphDGML)));

            //Act
            Graph dgmlGraph = privateVisualGraph.Invoke("GetGraph") as Graph;
            string serizalize = visualGraph.SerializeGraph();
            //Assert
            Assert.IsTrue(dgmlGraph != null && dgmlGraph.Groups.Count > 0);
        }

        [TestMethod]
        public void GetGraph_GivenAssemblyLevelGrouping_GroupsExist()
        {
            //Arrange
            var visualGraph = new ObjectMemberVisualGraphDGML("G1", VisualizationTestHelper.GetObjectMemberAnalysisNodes());
            visualGraph.Options.ChosenGroupingLevel = ReframeVisualizer.GroupingLevel.AssemblyLevel;
            var privateVisualGraph = new PrivateObject(visualGraph, new PrivateType(typeof(VisualGraphDGML)));

            //Act
            Graph dgmlGraph = privateVisualGraph.Invoke("GetGraph") as Graph;
            string serizalize = visualGraph.SerializeGraph();
            //Assert
            Assert.IsTrue(dgmlGraph != null && dgmlGraph.Groups.Count > 0);
        }
    }
}
