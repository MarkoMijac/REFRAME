﻿using ReframeAnalyzer.Graph;
using ReframeVisualizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualizerDGML.Graphs;

namespace VisualizerDGML.Factories
{
    public class ClassGraphFactoryDGML : IVisualGraphFactory
    {
        public IVisualGraph CreateGraph(string reactorIdentifier, IEnumerable<IAnalysisNode> analysisNodes)
        {
            return new ClassVisualGraphDGML(reactorIdentifier, analysisNodes);
        }
    }
}