using ReframeAnalyzer.Graph;
using ReframeTools.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeTools.Controllers
{
    public class ObjectMemberVisualizationController : VisualizationController
    {
        public ObjectMemberVisualizationController(FrmObjectMemberLevelAnalysis form) : base(form)
        {
            CreateAnalysisGraph(form.ReactorIdentifier, AnalysisLevel.ObjectMemberLevel);
        }
    }
}
