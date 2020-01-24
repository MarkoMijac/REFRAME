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
    public class ClassAnalysisController : AnalysisController
    {
        public ClassAnalysisController(FrmClassAnalysisView view) : base(view)
        {
            CreateAnalysisGraph(View.ReactorIdentifier, AnalysisLevel.ClassLevel);
        }
    }
}
