using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ReframeAnalyzer.Graph;
using ReframeTools.Controllers;

namespace ReframeTools.GUI
{
    public partial class FrmUpdateProcessInfo : Form
    {
        public string ReactorIdentifier { get; set; }

        public FrmUpdateProcessInfo(string reactorIdentifier)
        {
            InitializeComponent();
            ReactorIdentifier = reactorIdentifier;
            AddColumns();
        }

        public void ShowAnalysis(IEnumerable<IAnalysisNode> analysisNodes)
        {
            dgvUpdateInfo.Rows.Clear();

            try
            {
                if (analysisNodes != null)
                {
                    foreach (UpdateAnalysisNode node in analysisNodes)
                    {
                        dgvUpdateInfo.Rows.Add(new string[]
                            {
                                node.Identifier.ToString(),
                                node.Name,
                                node.NodeType.ToString(),
                                node.UpdateLayer.ToString(),
                                node.UpdateStartedAt,
                                node.UpdateCompletedAt,
                                node.UpdateDuration,
                                node.Degree.ToString(),
                                node.InDegree.ToString(),
                                node.OutDegree.ToString()
                            });
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void AddColumns()
        {
            if (dgvUpdateInfo.Columns.Count == 0)
            {
                dgvUpdateInfo.Columns.Add("colIdentifier", "Identifier");
                dgvUpdateInfo.Columns.Add("colName", "Name");
                dgvUpdateInfo.Columns.Add("colNodeType", "Node Type");
                dgvUpdateInfo.Columns.Add("colUpdateLayer", "Update Layer");
                dgvUpdateInfo.Columns.Add("colStartedAt", "Started At");
                dgvUpdateInfo.Columns.Add("colCompletedAt", "Completed At");
                dgvUpdateInfo.Columns.Add("colDuration", "Duration");
                dgvUpdateInfo.Columns.Add("colDegree", "Degree");
                dgvUpdateInfo.Columns.Add("colInDegree", "In Degree");
                dgvUpdateInfo.Columns.Add("colOutDegree", "Out Degree");
            }
        }

        private void FrmUpdateProcessInfo_Load(object sender, EventArgs e)
        {
            UpdateProcessAnalysisController controller = new UpdateProcessAnalysisController(this);
            controller.ShowGraph();
        }
    }
}
