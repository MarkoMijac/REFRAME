using ReframeTools.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReframeTools.Controllers
{
    public abstract class GUIController
    {
        protected FrmAnalysis _form;

        public GUIController(FrmAnalysis frmAnalysis)
        {
            _form = frmAnalysis;
        }

        protected virtual void ShowGraph(string xmlGraph, string analysisDescription = "")
        {
            _form.ShowXMLSource(xmlGraph);
            _form.ShowTable(xmlGraph);
            _form.ShowDescription(analysisDescription);
        }

        protected void DisplayError(Exception e)
        {
            MessageBox.Show($"Unable to fetch and display data! Error:{e.Message}!");
        }

        public abstract void ShowEntireGraph();
        public abstract void ShowSourceNodes();
        public abstract void ShowSinkNodes();
        public abstract void ShowLeafNodes();
        public abstract void ShowOrphanNodes();
        public abstract void ShowIntermediaryNodes();
        public abstract void ShowPredecessorNodes();
        public abstract void ShowSuccessorNodes();
        public abstract void ShowNeighbourNodes();
    }
}
