using EnvDTE;
using IPCClient;
using Microsoft.VisualStudio.GraphModel;
using ReframeAnalyzer;
using ReframeAnalyzer.Graph;
using ReframeTools.GUI;
using ReframeVisualizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReframeTools.Controllers
{
    public class ClassVisualizationController : VisualizationController
    {
        public ClassVisualizationController(FrmClassAnalysisView view, FrmVisualizationOptions frmOptions) : base(view, frmOptions)
        {
            CreateAnalysisGraph(View.ReactorIdentifier, AnalysisLevel.ClassLevel);
        }
    }
}
