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
            var filterFactory = new AnalysisFilterFactory();
            Filter = filterFactory.CreateFilter(OriginalNodes, AnalysisLevel.AssemblyLevel);
            rbAssemblyLevel.Checked = true;
        }

        protected override void HandleCheckListsVisibility()
        {
            base.HandleCheckListsVisibility();
            MakeAssemblyNodesVisible(true);
            
            MakeNamespaceNodesVisible(false);
            MakeClassNodesVisible(false);
            EnableClassNodes(false);
            MakeObjectNodesVisible(false);
            EnableObjectNodes(false);

            int offset = 130 * 3;
            AdjustFormSize(offset);
        }

        protected override void LoadNodes()
        {
            LoadAssemblyNodes();
        }
    }
}
