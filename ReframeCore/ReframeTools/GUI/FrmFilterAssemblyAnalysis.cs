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
    public partial class FrmFilterAssemblyAnalysis : FrmFilterAnalysis
    {
        public FrmFilterAssemblyAnalysis() : base()
        {
            InitializeComponent();
        }

        protected override void Initialize()
        {
            base.Initialize();
            Level = AnalysisLevel.AssemblyLevel;
            Filter = new AssemblyAnalysisFilter(OriginalNodes);
            rbAssemblyLevel.Checked = true;
        }

        protected override void HandleCheckListsVisibility()
        {
            base.HandleCheckListsVisibility();
            MakeAssemblyNodesVisible(true);
            MakeNamespaceNodesVisible(false);
            MakeClassNodesVisible(false);
            MakeObjectNodesVisible(false);

            int offset = 130 * 3;

            gbFilterByParent.Size = new Size(gbFilterByParent.Size.Width, gbFilterByParent.Size.Height - offset);
            btnApply.Location = new Point(btnApply.Location.X, btnApply.Location.Y - offset);
            this.Size = new Size(this.Size.Width, this.Size.Height - offset);
        }

        protected override void LoadNodes()
        {
            LoadAssemblyNodes();
        }
    }
}
