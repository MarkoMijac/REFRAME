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
    public partial class FrmFilterUpdateAnalysis : FrmFilterAnalysis
    {
        public FrmFilterUpdateAnalysis() : base()
        {
            InitializeComponent();
        }

        protected override void Initialize()
        {
            base.Initialize();
            Level = AnalysisLevel.UpdateAnalysisLevel;
            Filter = new UpdateAnalysisFilter(OriginalNodes);
        }

        protected override void HandleCheckListsVisibility()
        {
            base.HandleCheckListsVisibility();
            MakeAssemblyNodesVisible(true);
            MakeNamespaceNodesVisible(true);
            MakeClassNodesVisible(true);
            EnableClassNodes(false);
            MakeObjectNodesVisible(true);
            EnableObjectNodes(false);

            int offset = 0;
            AdjustFormSize(offset);
        }

        protected override void LoadNodes()
        {
            LoadAssemblyNodes();
            LoadNamespaceNodes();
        }

        protected override void clbNamespaceNodes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            base.clbNamespaceNodes_ItemCheck(sender, e);
            var namespaceNode = clbNamespaceNodes.SelectedItem as IAnalysisNode;
            LoadClassNodes(namespaceNode);
        }

        protected override void clbClassNodes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            base.clbClassNodes_ItemCheck(sender, e);
            var classNode = clbClassNodes.SelectedItem as IAnalysisNode;
            LoadObjectNodes(classNode);
        }
    }
}
