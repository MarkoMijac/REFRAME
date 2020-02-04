using ReframeAnalyzer;
using ReframeAnalyzer.Graph;
using ReframeClient;
using ReframeTools.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeTools.Controllers
{
    public class UpdateProcessController
    {
        protected FrmUpdateProcessInfo View { get; set; }
        public IAnalysisGraph AnalysisGraph { get; set; }
        public IEnumerable<IAnalysisNode> AnalysisNodes { get; set; }
        public Analyzer Analyzer { get; set; } = new Analyzer();
        public AnalysisGraphFactory GraphFactory { get; set; } = new AnalysisGraphFactory();

        public UpdateProcessController(FrmUpdateProcessInfo form)
        {
            View = form;
        }

        private void CreateAnalysisGraph(string reactorIdentifier)
        {
            var pipeClient = new ReframePipeClient();
            string xmlSource = pipeClient.GetUpdateInfo(reactorIdentifier);
            AnalysisGraph = GraphFactory.CreateGraph(xmlSource, AnalysisLevel.ObjectMemberLevel);
        }
    }
}
