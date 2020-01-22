﻿using IPCClient;
using ReframeAnalyzer;
using ReframeAnalyzer.Graph;
using ReframeTools.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReframeTools.Controllers
{
    public class AssemblyAnalysisController : AnalysisController
    {
        public AssemblyAnalysisController(FrmAssemblyLevelAnalysis form) : base(form)
        {
            CreateAnalysisGraph(Form.ReactorIdentifier, AnalysisLevel.AssemblyLevel);
        }

        
    }
}
