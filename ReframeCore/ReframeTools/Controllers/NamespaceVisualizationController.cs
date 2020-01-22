using ReframeAnalyzer.Graph;
using ReframeTools.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeTools.Controllers
{
    public class NamespaceVisualizationController : VisualizationController
    {
        public NamespaceVisualizationController(FrmNamespaceLevelAnalysis form) : base(form)
        {
            CreateAnalysisGraph(form.ReactorIdentifier, AnalysisLevel.NamespaceLevel);
        }
    }
}
