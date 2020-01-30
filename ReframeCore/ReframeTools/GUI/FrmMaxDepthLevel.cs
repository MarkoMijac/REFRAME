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
    public partial class FrmMaxDepthLevel : Form
    {
        public int MaxDepthLevel { get; private set; }

        public FrmMaxDepthLevel()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            int selectedLevel;
            int.TryParse(cmbDepthLevel.SelectedItem.ToString(), out selectedLevel);

            MaxDepthLevel = selectedLevel;
            Close();
        }

        private void FrmMaxDepthLevel_Load(object sender, EventArgs e)
        {
            cmbDepthLevel.SelectedIndex = 0;
        }
    }
}
