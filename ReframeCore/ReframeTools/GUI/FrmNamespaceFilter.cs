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
    public partial class FrmNamespaceFilter : FrmAnalysisFilter
    {
        private NamespaceAnalysisFilter _namespaceFilter;
        public FrmNamespaceFilter ()
        {
            InitializeComponent();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            Close();
        }

        protected override void Initialize()
        {
            Level = AnalysisLevel.NamespaceLevel;
            Filter = new NamespaceAnalysisFilter(OriginalNodes);
            _namespaceFilter = Filter as NamespaceAnalysisFilter;
        }

        protected override void LoadNodes()
        {
            LoadNamespaceNodes();
        }

        protected void LoadAssemblyNodes()
        {
            FillListBoxes(clbAssemblyNodes, _namespaceFilter.GetAvailableAssemblyNodes());
        }

        protected void LoadNamespaceNodes()
        {
            FillListBoxes(clbNamespaceNodes, _namespaceFilter.GetAvailableNamespaceNodes());
        }

        private void FrmOptions_Load(object sender, EventArgs e)
        {
            Initialize();
            LoadNodes();
        }

        private void btnSelecteAllAssemblies_Click(object sender, EventArgs e)
        {
            _namespaceFilter.SelectAllAssemblyNodes();
            LoadAssemblyNodes();
        }

        private void btnDeselectAllAssemblies_Click(object sender, EventArgs e)
        {
            _namespaceFilter.DeselectAllAssemblyNodes();
            LoadAssemblyNodes();
        }

        private void btnSelectAllNamespaces_Click(object sender, EventArgs e)
        {
            _namespaceFilter.SelectAllNamespaceNodes();
            LoadNamespaceNodes();
        }

        private void btnDeselectAllNamespaces_Click(object sender, EventArgs e)
        {
            _namespaceFilter.DeselectAllNamespaceNodes();
            LoadNamespaceNodes();
        }

        protected virtual void clbAssemblyNodes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            CheckListBoxItem(clbAssemblyNodes, e);
        }

        protected virtual void clbNamespaceNodes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            CheckListBoxItem(clbNamespaceNodes, e);
        }
    }
}
