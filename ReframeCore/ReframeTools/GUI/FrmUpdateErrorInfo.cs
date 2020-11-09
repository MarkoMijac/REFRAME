using ReframeAnalyzer.Graph;
using ReframeTools.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReframeTools.GUI
{
    public partial class FrmUpdateErrorInfo : Form
    {
        private IUpdateGraph _updateGraph;
        public FrmUpdateErrorInfo(IUpdateGraph updateGraph)
        {
            InitializeComponent();
            _updateGraph = updateGraph;
        }

        private void FrmUpdateErrorInfo_Load(object sender, EventArgs e)
        {
            DisplayErrorInfo();
        }

        private void DisplayErrorInfo()
        {
            if (_updateGraph != null)
            {
                txtIdentifier.Text = _updateGraph.FailedNodeIdentifier.ToString();
                txtMemberName.Text = _updateGraph.FailedNodeName;
                txtOwner.Text = _updateGraph.FailedNodeOwner;
                txtException.Text = _updateGraph.SourceException;
                txtStackTrace.Text = _updateGraph.StackTrace;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
