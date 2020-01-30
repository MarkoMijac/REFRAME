using ReframeVisualizer;
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
    public partial class FrmVisualizationOptions : Form
    {
        public IVisualGraph VisualGraph { get; set; }

        public FrmVisualizationOptions(IVisualGraph visualGraph)
        {
            InitializeComponent();
            VisualGraph = visualGraph;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            VisualGraph.VisualizationOptions.GroupingLevel = DetermineGroupingLevel();
            Close();
        }

        private void FillGroupingLevels()
        {
            cmbGroupLevel.Items.Clear();
            VisualizationOptions options = VisualGraph.VisualizationOptions;
            foreach (var level in options.AllowedGroupingLevels)
            {
                cmbGroupLevel.Items.Add(level);
            }

            cmbGroupLevel.SelectedIndex = 0;
        }

        private GroupingLevel DetermineGroupingLevel()
        {
            return (GroupingLevel)cmbGroupLevel.SelectedItem;
        }

        private void FrmVisualizationOptions_Load(object sender, EventArgs e)
        {
            FillGroupingLevels();
        }
    }
}
