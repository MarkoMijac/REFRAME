using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeAnalyzer.Exceptions;
using ReframeAnalyzer.Graph;
using ReframeCore.Factories;
using ReframeExporter;

namespace ReframeAnalyzerTests.GraphFactories
{
    [TestClass]
    public class UpdateAnalysisGraphFactoryTests
    {
        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void CreateGraph_GivenObjectMemberGraphIsNull_ThrowsException()
        {
            //Arrange
            var factory = new UpdateAnalysisGraphFactory(null);

            //Act
            var analysisGraph = factory.CreateGraph("");
        }
    }
}
