﻿using ReframeAnalyzer.Graph;
using ReframeTools.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeTools.Controllers
{
    public class ObjectVisualizationController : VisualizationController
    {
        public ObjectVisualizationController(FrmObjectLevelAnalysis form) : base(form)
        {
            CreateAnalysisGraph(form.ReactorIdentifier, AnalysisLevel.ObjectLevel);
        }
    }
}
