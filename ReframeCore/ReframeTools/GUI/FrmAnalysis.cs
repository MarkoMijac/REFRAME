using IPCClient;
using ReframeAnalyzer.Graph;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ReframeTools.GUI
{
    public partial class FrmAnalysis : Form
    {
        public string ReactorIdentifier { get; set; }

        public FrmAnalysis()
        {
            InitializeComponent();
        }

        public FrmAnalysis(string reactorIdentifier)
            :this()
        {
            ReactorIdentifier = reactorIdentifier;
            SetFormTitle();
        }

        protected void FrmClassAnalysis_Load(object sender, EventArgs e)
        {
            
        }

        protected virtual void SetFormTitle()
        {
            Text = $"Graph analysis for reactor [{ReactorIdentifier}]";
            lblAnalysisDescription.Text = $"Graph analysis for reactor [{ReactorIdentifier}]";
        }

        public virtual void ShowXMLSource(string xmlSource)
        {
            rtxtXMLSource.Text = xmlSource;
        }

        public virtual void ShowTable(IEnumerable<IAnalysisNode> nodes)
        {
            
        }

        public virtual void ShowDescription(string description)
        {
            lblAnalysisDescription.Text = description;
        }

        public string GetSelectedNodeIdentifier()
        {
            string identifier = "";

            if (dgvAnalysis.Rows.Count > 0)
            {
                identifier = dgvAnalysis.CurrentRow.Cells[0].Value.ToString();
            }

            return identifier;
        }
    }
}
