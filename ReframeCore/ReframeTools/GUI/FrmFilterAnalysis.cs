using ReframeAnalyzer;
using ReframeAnalyzer.Filters;
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
            Close();
        }

        protected virtual void Initialize()
        {
            
        }

        private void FrmOptions_Load(object sender, EventArgs e)
        {
            Initialize();
        }

        protected virtual void btnSelecteAllAssemblies_Click(object sender, EventArgs e)
        {

        }

        protected virtual void btnDeselectAllAssemblies_Click(object sender, EventArgs e)
        {

        }

        protected virtual void btnSelectAllNamespaces_Click(object sender, EventArgs e)
        {

        }

        protected virtual void btnDeselectAllNamespaces_Click(object sender, EventArgs e)
        {

        }

        protected virtual void btnSelectAllClasses_Click(object sender, EventArgs e)
        {

        }

        protected virtual void btnDeselectAllClasses_Click(object sender, EventArgs e)
        {

        }

        protected virtual void btnSelectAllObjects_Click(object sender, EventArgs e)
        {

        }

        protected virtual void btnDeselectAllObjects_Click(object sender, EventArgs e)
        {

        }
    }
}
