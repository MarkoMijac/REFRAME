using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeAnalyzer.Exceptions;
using ReframeAnalyzer.Filters;
using ReframeAnalyzer.GraphFactories;
using ReframeAnalyzer.Nodes;
using ReframeCore;
using ReframeExporter;

namespace ReframeAnalyzerTests.Filters
{
    [TestClass]
    public class UpdateAnalysisFilterTests
    {
        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void Apply_GivenNullList_ThrowsException()
        {
            //Arrange
            var filter = new UpdateAnalysisFilter(null);

            //Act
            var filteredNodes = filter.Apply();
        }

        [TestMethod]
        public void Apply_GivenEmptyListOfOriginalNodes_ReturnsEmptyList()
        {
            //Arrange
            var originalNodes = new List<IAnalysisNode>();
            var filter = new UpdateAnalysisFilter(originalNodes);

            //Act
            var filteredNodes = filter.Apply();

            //Assert
            Assert.IsTrue(filteredNodes.Count == 0);
        }
    }
}
