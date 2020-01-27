using ReframeAnalyzer.Graph;
using ReframeTools.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeTools.Controllers
{
    public class AssemblyVisualizationController : VisualizationController
    {
        public AssemblyVisualizationController(FrmAssemblyAnalysisView view, FrmVisualizationOptions frmOptions) : base(view, frmOptions)
        {
            CreateAnalysisGraph(View.ReactorIdentifier, AnalysisLevel.AssemblyLevel);
        }
    }
}
