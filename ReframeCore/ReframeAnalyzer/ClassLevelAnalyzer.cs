﻿using ReframeCore;
using ReframeCore.Nodes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ReframeCore.Factories;
using ReframeAnalyzer.Graph;
using ReframeAnalyzer.Xml;

namespace ReframeAnalyzer
{
    public class ClassLevelAnalyzer : GraphAnalyzer
    {
        public ClassLevelAnalyzer()
            :base()
        {

        }

        public ClassLevelAnalyzer(IDependencyGraph graph)
            :base(graph)
        {

        }

        public override IAnalysisGraph CreateGraph(string source)
        {
            return new ClassAnalysisGraph(source);
        }

        #region Methods

        protected override void CreateAnalysisGraph()
        {
            _analysisGraph = new ClassAnalysisGraph();
            _analysisGraph.InitializeGraph(_dependencyGraph);
        }

        #endregion
    }
}
