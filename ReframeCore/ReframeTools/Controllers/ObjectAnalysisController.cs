﻿using ReframeAnalyzer.Graph;
using ReframeTools.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeTools.Controllers
{
    public class ObjectAnalysisController : AnalysisController
    {
        public ObjectAnalysisController(FrmObjectLevelAnalysis form) : base(form)
        {
            CreateAnalysisGraph(Form.ReactorIdentifier, AnalysisLevel.ObjectLevel);
        }
    }
}
