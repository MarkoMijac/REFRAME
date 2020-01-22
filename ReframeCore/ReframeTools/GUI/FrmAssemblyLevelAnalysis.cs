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

namespace ReframeTools.GUI
{
    public partial class FrmAssemblyLevelAnalysis : FrmAnalysis
    {
        public FrmAssemblyLevelAnalysis(string reactorIdentifier)
            :base(reactorIdentifier)
        {
            InitializeComponent();
            AddColumns();
        }

        protected override void SetFormTitle()
        {
            Text = $"Assembly-level analysis for Reactor [{ReactorIdentifier}]";
            ShowDescription($"Assembly-level analysis for Reactor [{ReactorIdentifier}]");
        }

        public override void ShowTable(IEnumerable<IAnalysisNode> nodes)
        {
            dgvAnalysis.Rows.Clear();

            try
            {
                if (nodes != null)
                {
                    foreach (AssemblyAnalysisNode node in nodes)
                    {
                        dgvAnalysis.Rows.Add(new string[]
                        {
                        node.Identifier.ToString(),
                        node.Name,
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
            if (dgvAnalysis.Columns.Count == 0)
            {
                dgvAnalysis.Columns.Add("colIdentifier", "Identifier");
                dgvAnalysis.Columns.Add("colName", "Name");
                dgvAnalysis.Columns.Add("colDegree", "Degree");
                dgvAnalysis.Columns.Add("colInDegree", "In Degree");
                dgvAnalysis.Columns.Add("colOutDegree", "Out Degree");
            }
        }
    }
}
