﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.GraphModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeAnalyzer.Nodes;
using VisualizerDGML.Graphs;

namespace VisualizerDGMLTests.Graphs
{
    [TestClass]
    public class NamespaceVisualGraphDGML_Tests
    {
        [TestMethod]
        public void GetGraph_GivenEmptyListOfNodes_ReturnsEmptyDgmlGraph()
        {
            //Arrange
            var visualGraph = new NamespaceVisualGraphDGML("G1", new List<IAnalysisNode>());
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
            var visualGraph = new NamespaceVisualGraphDGML("G1", VisualizationTestHelper.GetNamespaceAnalysisNodes());
            var privateVisualGraph = new PrivateObject(visualGraph, new PrivateType(typeof(VisualGraphDGML)));

            //Act
            Graph dgmlGraph = privateVisualGraph.Invoke("GetGraph") as Graph;

            //Assert
            Assert.IsTrue(dgmlGraph != null
                && dgmlGraph.Nodes.Count == 3
                && dgmlGraph.DocumentSchema.Properties.Count == 5
                && dgmlGraph.Links.Count == 2);
        }
    }
}
