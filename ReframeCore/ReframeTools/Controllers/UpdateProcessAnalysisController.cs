using ReframeAnalyzer;
using ReframeAnalyzer.Graph;
using ReframeClient;
using ReframeTools.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReframeTools.Controllers
{
    public class UpdateProcessAnalysisController
    {
        protected FrmFilterAnalysis FilterView { get; set; }
        protected FrmUpdateProcessInfo View { get; set; }
        public IAnalysisGraph AnalysisGraph { get; set; }
        public IEnumerable<IAnalysisNode> AnalysisNodes { get; set; }
        public Analyzer Analyzer { get; set; } = new Analyzer();

        public UpdateProcessAnalysisController(FrmUpdateProcessInfo form, FrmFilterAnalysis frmFilter = null)
        {
            View = form;
            FilterView = frmFilter;
            CreateAnalysisGraph(View.ReactorIdentifier);
        }

        private void CreateAnalysisGraph(string reactorIdentifier)
        {
            var pipeClient = new ReframePipeClient();
            string xmlUpdateInfo = pipeClient.GetUpdateInfo(reactorIdentifier);
            string xmlSource = pipeClient.GetReactor(reactorIdentifier);

            var objectMemberFactory = new ObjectMemberAnalysisGraphFactory();
            var objectMemberAnalysisGraph = objectMemberFactory.CreateGraph(xmlSource);
            var factory = new UpdateAnalysisGraphFactory(objectMemberAnalysisGraph);

            AnalysisGraph = factory.CreateGraph(xmlUpdateInfo);
            AnalysisNodes = AnalysisGraph.Nodes;
        }

        private void ShowGraph(IAnalysisGraph analysisGraph, IEnumerable<IAnalysisNode> nodes)
        {
            if (View != null)
            {
                View.ShowAnalysis(analysisGraph, nodes);
            }
        }

        private IEnumerable<IAnalysisNode> GetFilteredNodes(IEnumerable<IAnalysisNode> originalNodes)
        {
            IEnumerable<IAnalysisNode> filteredNodes;
            if (FilterView != null)
            {
                FilterView.OriginalNodes = originalNodes;
                FilterView.ShowDialog();

                var filter = FilterView.Filter;
                filteredNodes = filter.Apply();
            }
            else
            {
                filteredNodes = originalNodes;
            }

            return filteredNodes;
        }

        public void ShowUpdateAnalysis()
        {
            try
            {
                var originalNodes = AnalysisGraph.Nodes;
                AnalysisNodes = GetFilteredNodes(originalNodes);
                ShowGraph(AnalysisGraph, AnalysisNodes);
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        protected void DisplayError(Exception e)
        {
            MessageBox.Show($"Unable to fetch and display data! Error:{e.Message}!");
        }
    }
}
