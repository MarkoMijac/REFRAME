using ReframeAnalyzer.Graph;
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
        public ObjectVisualizationController(FrmObjectAnalysisView view) : base(view)
        {
            CreateAnalysisGraph(View.ReactorIdentifier, AnalysisLevel.ObjectLevel);
        }
    }
}
