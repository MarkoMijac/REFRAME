using ReframeAnalyzer.Graph;
using ReframeTools.Controllers;
using ReframeVisualizer.Factories;
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
    public partial class FrmAssemblyAnalysisView : FrmAnalysisView
    {
        public FrmAssemblyAnalysisView(string reactorIdentifier) : base(reactorIdentifier)
        {
            InitializeComponent();
        }

        public override void ShowAnalysis(IEnumerable<IAnalysisNode> nodes)
        {
            base.ShowAnalysis(nodes);
            try
            {
                if (nodes != null)
                {
                    foreach (var node in nodes)
                    {
                        dgvNodes.Rows.Add(new string[]
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

        protected override void AddColumns()
        {
            if (dgvNodes.Columns.Count == 0)
            {
                dgvNodes.Columns.Add("colIdentifier", "Identifier");
                dgvNodes.Columns.Add("colName", "Name");
                dgvNodes.Columns.Add("colDegree", "Degree");
                dgvNodes.Columns.Add("colInDegree", "In Degree");
                dgvNodes.Columns.Add("colOutDegree", "Out Degree");
            }
        }

        protected override AnalysisController CreateAnalysisController()
        {
            AssemblyAnalysisGraphFactory factory = new AssemblyAnalysisGraphFactory();
            return new AnalysisController(this, factory, new FrmAssemblyFilter());
        }

        protected override VisualizationController CreateVisualizationController()
        {
            var factory = new AssemblyGraphFactoryDGML();
            return new VisualizationController(ReactorIdentifier, factory);
        }
    }
}
