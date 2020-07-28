﻿using System.Collections.Generic;

namespace ReframeAnalyzer.Graph
{
    public class ClassAnalysisGraph : AnalysisGraph
    {
        public ClassAnalysisGraph(string identifier, AnalysisLevel level)
        {
            Identifier = identifier;
            AnalysisLevel = level;
        }
    }
}
