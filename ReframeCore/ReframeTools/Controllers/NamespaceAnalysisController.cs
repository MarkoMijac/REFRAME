using ReframeAnalyzer.Graph;
using ReframeTools.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeTools.Controllers
{
    public class NamespaceAnalysisController : AnalysisController
    {
        public NamespaceAnalysisController(FrmNamespaceAnalysisView view, FrmFilterNamespaceAnalysis filterView) : base(view, filterView)
        {
            CreateAnalysisGraph(View.ReactorIdentifier, AnalysisLevel.NamespaceLevel);
        }
    }
}
