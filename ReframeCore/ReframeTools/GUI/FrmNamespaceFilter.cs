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

        private void Initialize()
        {
            Filter = new NamespaceAnalysisFilter(OriginalNodes);
            _namespaceFilter = Filter as NamespaceAnalysisFilter;
        }

        private void LoadNodes()
        {
            LoadNamespaceNodes();
        }

        protected void LoadAssemblyNodes()
        {
            FillListBox(clbAssemblyNodes, _namespaceFilter.AssemblyFilterOption);
        }

        protected void LoadNamespaceNodes()
        {
            FillListBox(clbNamespaceNodes, _namespaceFilter.NamespaceFilterOption);
        }

        private void FrmOptions_Load(object sender, EventArgs e)
        {
            Initialize();
            LoadNodes();
        }

        private void btnSelecteAllAssemblies_Click(object sender, EventArgs e)
        {
            _namespaceFilter.AssemblyFilterOption.SelectNodes();
            LoadAssemblyNodes();
        }

        private void btnDeselectAllAssemblies_Click(object sender, EventArgs e)
        {
            _namespaceFilter.AssemblyFilterOption.DeselectNodes();
            LoadAssemblyNodes();
        }

        private void btnSelectAllNamespaces_Click(object sender, EventArgs e)
        {
            _namespaceFilter.NamespaceFilterOption.SelectNodes();
            LoadNamespaceNodes();
        }

        private void btnDeselectAllNamespaces_Click(object sender, EventArgs e)
        {
            _namespaceFilter.NamespaceFilterOption.DeselectNodes();
            LoadNamespaceNodes();
        }

        protected virtual void clbAssemblyNodes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            CheckListBoxItem(clbAssemblyNodes, _namespaceFilter.AssemblyFilterOption, e);
        }

        protected virtual void clbNamespaceNodes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            CheckListBoxItem(clbNamespaceNodes, _namespaceFilter.NamespaceFilterOption, e);
        }
    }
}
