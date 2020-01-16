using IPCClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReframeToolsGUI.Controllers
{
    public class ClassAnalysisController
    {
        private FrmClassAnalysis _form;
        public ClassAnalysisController(FrmClassAnalysis form)
        {
            _form = form;
        }

        public void ShowEntireGraph()
        {
            string xmlSource = FetchEntireGraphXml();
            _form.ShowXMLSource(xmlSource);
            _form.ShowTable(xmlSource);
        }

        private string FetchEntireGraphXml()
        {
            try
            {
                return ClientQueries.GetClassAnalysisGraph(_form.ReactorIdentifier);
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to fetch analysis data!");
                return "";
            }
        }

        internal void ShowSourceNodes()
        {
            string xmlSource = ClientQueries.GetClassAnalysisGraphSourceNodes(_form.ReactorIdentifier);
            _form.ShowXMLSource(xmlSource);
            _form.ShowTable(xmlSource);
        }

        internal void ShowSinkNodes()
        {
            string xmlSource = ClientQueries.GetClassAnalysisGraphSinkNodes(_form.ReactorIdentifier);
            _form.ShowXMLSource(xmlSource);
            _form.ShowTable(xmlSource);
        }

        internal void ShowLeafNodes()
        {
            string xmlSource = ClientQueries.GetClassAnalysisGraphLeafNodes(_form.ReactorIdentifier);
            _form.ShowXMLSource(xmlSource);
            _form.ShowTable(xmlSource);
        }

        internal void ShowOrphanNodes()
        {
            string xmlSource = ClientQueries.GetClassAnalysisGraphOrphanNodes(_form.ReactorIdentifier);
            _form.ShowXMLSource(xmlSource);
            _form.ShowTable(xmlSource);
        }

        internal void ShowIntermediaryNodes()
        {
            string xmlSource = ClientQueries.GetClassAnalysisGraphIntermediaryNodes(_form.ReactorIdentifier);
            _form.ShowXMLSource(xmlSource);
            _form.ShowTable(xmlSource);
        }

        internal void ShowPredecessors()
        {
            string nodeId = _form.GetSelectedNodeIdentifier();
            string xmlSource = ClientQueries.GetClassAnalysisGraphPredecessorNodes(_form.ReactorIdentifier, nodeId);

            _form.ShowXMLSource(xmlSource);
            _form.ShowTable(xmlSource);
        }

        internal void ShowSuccessors()
        {
            string nodeId = _form.GetSelectedNodeIdentifier();
            string xmlSource = ClientQueries.GetClassAnalysisGraphSuccessorNodes(_form.ReactorIdentifier, nodeId);

            _form.ShowXMLSource(xmlSource);
            _form.ShowTable(xmlSource);
        }

        internal void ShowNeighbours()
        {
            string nodeId = _form.GetSelectedNodeIdentifier();
            string xmlSource = ClientQueries.GetClassAnalysisGraphNeighbourNodes(_form.ReactorIdentifier, nodeId);

            _form.ShowXMLSource(xmlSource);
            _form.ShowTable(xmlSource);
        }
    }
}
