using ReframeAnalyzer;
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
    public partial class FrmFilterAnalysis : Form
    {
        public IEnumerable<IAnalysisNode> OriginalNodes { get; set; }

        public AnalysisLevel Level { get; set; }

        public IAnalysisFilter Filter { get; protected set; }

        public FrmFilterAnalysis()
        {
            InitializeComponent();
        }

        protected virtual void btnApply_Click(object sender, EventArgs e)
        {

        }

        protected virtual void Initialize()
        {
            
        }

        private void FrmOptions_Load(object sender, EventArgs e)
        {
            Initialize();
        }
    }
}
