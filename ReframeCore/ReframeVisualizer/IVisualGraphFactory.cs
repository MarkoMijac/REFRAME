﻿using ReframeAnalyzer.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeVisualizer
{
    public interface IVisualGraphFactory
    {
        IVisualGraph CreateGraph(string reactorIdentifier, IEnumerable<IAnalysisNode> analysisNodes);
    }
}
