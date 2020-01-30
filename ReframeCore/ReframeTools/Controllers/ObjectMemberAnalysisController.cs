using ReframeAnalyzer.Graph;
using ReframeTools.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeTools.Controllers
{
    public class ObjectMemberAnalysisController : AnalysisController
    {
        public ObjectMemberAnalysisController(FrmObjectMemberAnalysisView view, FrmFilterObjectMemberAnalysis filterView) : base(view, filterView)
        {
            CreateAnalysisGraph(View.ReactorIdentifier, AnalysisLevel.ObjectMemberLevel);
        }
    }
}
