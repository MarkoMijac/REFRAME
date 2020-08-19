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
    public partial class FrmClassFilter : FrmAnalysisFilter
    {
        private ClassAnalysisFilter _classFilter;
        public FrmClassFilter()
        {
            InitializeComponent();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Initialize()
        {
            Filter = new ClassAnalysisFilter(OriginalNodes);
            _classFilter = Filter as ClassAnalysisFilter;
        }

        private void LoadNodes()
        {
            LoadAssemblyNodes();
            LoadNamespaceNodes();
        }

        private void LoadAssemblyNodes()
        {
            FillListBoxes(clbAssemblyNodes, _classFilter.AssemblyFilterOption);
        }

        private void LoadNamespaceNodes()
        {
            FillListBoxes(clbNamespaceNodes, _classFilter.NamespaceFilterOption);
        }

        private void LoadClassNodes(IAnalysisNode namespaceNode)
        {
            if (namespaceNode == null) return;
            FillListBoxes(clbClassNodes, _classFilter.ClassFilterOption, node => node.Parent.Identifier == namespaceNode.Identifier);
        }

        private void FrmOptions_Load(object sender, EventArgs e)
        {
            Initialize();
            LoadNodes();
        }

        private void btnSelecteAllAssemblies_Click(object sender, EventArgs e)
        {
            _classFilter.AssemblyFilterOption.SelectNodes();
            LoadAssemblyNodes();
        }

        private void btnDeselectAllAssemblies_Click(object sender, EventArgs e)
        {
            _classFilter.AssemblyFilterOption.DeselectNodes();
            LoadAssemblyNodes();
        }

        private void btnSelectAllNamespaces_Click(object sender, EventArgs e)
        {
            _classFilter.NamespaceFilterOption.SelectNodes();
            LoadNamespaceNodes();

            RefreshClassNodes();
        }

        private void btnDeselectAllNamespaces_Click(object sender, EventArgs e)
        {
            _classFilter.NamespaceFilterOption.DeselectNodes();
            LoadNamespaceNodes();

            RefreshClassNodes();
        }

        private void RefreshClassNodes()
        {
            var selectedNamespaceNode = clbNamespaceNodes.SelectedItem as IAnalysisNode;
            LoadClassNodes(selectedNamespaceNode);
            EnableClassNodes(_classFilter.NamespaceFilterOption.IsSelected(selectedNamespaceNode));
        }

        private void EnableClassNodes(bool enable)
        {
            clbClassNodes.Enabled = enable;
            btnSelectAllClasses.Enabled = enable;
            btnDeselectAllClasses.Enabled = enable;
        }

        private void btnSelectAllClasses_Click(object sender, EventArgs e)
        {
            var namespaceNode = clbNamespaceNodes.SelectedItem as IAnalysisNode;
            _classFilter.ClassFilterOption.SelectNodes(n => n.Parent.Identifier == namespaceNode.Identifier);
            LoadClassNodes(namespaceNode);
        }

        private void btnDeselectAllClasses_Click(object sender, EventArgs e)
        {
            var namespaceNode = clbNamespaceNodes.SelectedItem as IAnalysisNode;
            _classFilter.ClassFilterOption.DeselectNodes(n => n.Parent.Identifier == namespaceNode.Identifier);
            LoadClassNodes(namespaceNode);
        }

        private void clbAssemblyNodes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            CheckListBoxItem(clbAssemblyNodes, _classFilter.AssemblyFilterOption, e);
        }

        private void clbNamespaceNodes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            CheckListBoxItem(clbNamespaceNodes, _classFilter.NamespaceFilterOption, e);
            RefreshClassNodes();
        }

        private void clbClassNodes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            CheckListBoxItem(clbClassNodes, _classFilter.ClassFilterOption, e);
        }
       
        private void clbNamespaceNodes_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshClassNodes();
        }
    }
}
