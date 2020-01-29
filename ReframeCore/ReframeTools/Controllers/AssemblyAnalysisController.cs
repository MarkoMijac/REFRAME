using IPCClient;
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
        public AssemblyAnalysisController(FrmAssemblyAnalysisView view, FrmFilterAssemblyAnalysis filterView) : base(view, filterView)
        {
            CreateAnalysisGraph(View.ReactorIdentifier, AnalysisLevel.AssemblyLevel);
        }
    }
}
