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
    public class UpdateProcessAnalysisController
    {
        protected FrmUpdateProcessInfo View { get; set; }
        public IAnalysisGraph AnalysisGraph { get; set; }
        public IEnumerable<IAnalysisNode> AnalysisNodes { get; set; }
        public Analyzer Analyzer { get; set; } = new Analyzer();
        public AnalysisGraphFactory GraphFactory { get; set; } = new AnalysisGraphFactory();

        public UpdateProcessAnalysisController(FrmUpdateProcessInfo form)
        {
            View = form;
            CreateAnalysisGraph(View.ReactorIdentifier);
        }

        private void CreateAnalysisGraph(string reactorIdentifier)
        {
            var pipeClient = new ReframePipeClient();
            string xmlUpdateInfo = pipeClient.GetUpdateInfo(reactorIdentifier);

            AnalysisGraph = GraphFactory.CreateGraph(xmlUpdateInfo);
            AnalysisNodes = AnalysisGraph.Nodes;
        }

        public void ShowGraph()
        {
            if (View != null)
            {
                View.ShowAnalysis(AnalysisGraph);
            }
        }
    }
}
