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
    public partial class FrmFilterNamespaceAnalysis : FrmFilterAnalysis
    {
        public FrmFilterNamespaceAnalysis() : base()
        {
            InitializeComponent();
        }

        protected override void Initialize()
        {
            base.Initialize();
            Level = AnalysisLevel.NamespaceLevel;
            Filter = new NamespaceAnalysisFilter(OriginalNodes);
            rbNamespaceLevel.Checked = true;
        }

        protected override void HandleCheckListsVisibility()
        {
            base.HandleCheckListsVisibility();
            MakeAssemblyNodesVisible(true);
            MakeNamespaceNodesVisible(true);
            MakeClassNodesVisible(false);
            MakeObjectNodesVisible(false);

            int offset = 130 * 2;

            gbFilterByParent.Size = new Size(gbFilterByParent.Size.Width, gbFilterByParent.Size.Height - offset);
            btnApply.Location = new Point(btnApply.Location.X, btnApply.Location.Y - offset);
            this.Size = new Size(this.Size.Width, this.Size.Height - offset);
        }

        protected override void LoadNodes()
        {
            LoadNamespaceNodes();
        }
    }
}
