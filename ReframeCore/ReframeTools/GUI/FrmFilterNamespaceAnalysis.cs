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
            var filterFactory = new AnalysisFilterFactory();
            Filter = filterFactory.CreateFilter(OriginalNodes, AnalysisLevel.NamespaceLevel);
            rbNamespaceLevel.Checked = true;
        }

        protected override void HandleCheckListsVisibility()
        {
            base.HandleCheckListsVisibility();
            MakeAssemblyNodesVisible(true);
            MakeNamespaceNodesVisible(true);
            MakeClassNodesVisible(false);
            EnableClassNodes(false);
            MakeObjectNodesVisible(false);
            EnableObjectNodes(false);

            int offset = 130 * 2;
            AdjustFormSize(offset);
        }

        protected override void LoadNodes()
        {
            LoadNamespaceNodes();
        }
    }
}
