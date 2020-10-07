using ReframeAnalyzer;
using ReframeAnalyzer.Filters;
using ReframeAnalyzer.Nodes;
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
    public partial class FrmUpdateFilter : FrmAnalysisFilter
    {
        private UpdateAnalysisFilter _updateFilter;
        public FrmUpdateFilter()
        {
            InitializeComponent();
        }

        protected virtual void btnApply_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Initialize()
        {
            _updateFilter = new UpdateAnalysisFilter(OriginalNodes);
            Filter = _updateFilter;
        }

        private void LoadNodes()
        {
            LoadAssemblyNodes();
            LoadNamespaceNodes();
        }

        private void LoadAssemblyNodes()
        {
            FillListBox(clbAssemblyNodes, _updateFilter.AssemblyFilterOption);
        }

        private void LoadNamespaceNodes()
        {
            FillListBox(clbNamespaceNodes, _updateFilter.NamespaceFilterOption);
        }

        private void LoadClassNodes(IAnalysisNode namespaceNode)
        {
            if (namespaceNode == null) return;

            FillListBox(clbClassNodes, _updateFilter.ClassFilterOption, node => node.Parent.Identifier == namespaceNode.Identifier);
        }

        private void LoadObjectNodes(IAnalysisNode classNode)
        {
            if (classNode == null) return;

            FillListBox(clbObjectNodes, _updateFilter.ObjectFilterOption, n => n.Parent.Identifier == classNode.Identifier);
        }

        private void FrmOptions_Load(object sender, EventArgs e)
        {
            Initialize();
            LoadNodes();
        }

        private void btnSelecteAllAssemblies_Click(object sender, EventArgs e)
        {
            _updateFilter.AssemblyFilterOption.SelectNodes();
            LoadAssemblyNodes();
        }

        private void btnDeselectAllAssemblies_Click(object sender, EventArgs e)
        {
            _updateFilter.AssemblyFilterOption.DeselectNodes();
            LoadAssemblyNodes();
        }

        private void btnSelectAllNamespaces_Click(object sender, EventArgs e)
        {
            _updateFilter.NamespaceFilterOption.SelectNodes();
            LoadNamespaceNodes();

            RefreshClassNodes();
            RefreshObjectNodes();
        }

        private void RefreshObjectNodes()
        {
            var classNode = clbClassNodes.SelectedItem as IAnalysisNode;
            LoadObjectNodes(classNode);
            EnableObjectNodes(_updateFilter.ClassFilterOption.IsSelected(classNode));
        }

        private void btnDeselectAllNamespaces_Click(object sender, EventArgs e)
        {
            _updateFilter.NamespaceFilterOption.DeselectNodes();
            LoadNamespaceNodes();

            RefreshClassNodes();
            RefreshObjectNodes();
        }

        private void RefreshClassNodes()
        {
            var namespaceNode = clbNamespaceNodes.SelectedItem as IAnalysisNode;
            LoadClassNodes(namespaceNode);
            EnableClassNodes(_updateFilter.NamespaceFilterOption.IsSelected(namespaceNode));
        }

        private void btnSelectAllClasses_Click(object sender, EventArgs e)
        {
            var namespaceNode = clbNamespaceNodes.SelectedItem as IAnalysisNode;

            _updateFilter.ClassFilterOption.SelectNodes(n => n.Parent.Identifier == namespaceNode.Identifier);
            LoadClassNodes(namespaceNode);

            RefreshObjectNodes();
        }

        private void btnDeselectAllClasses_Click(object sender, EventArgs e)
        {
            var namespaceNode = clbNamespaceNodes.SelectedItem as IAnalysisNode;
            _updateFilter.ClassFilterOption.DeselectNodes(n => n.Parent.Identifier == namespaceNode.Identifier);
            LoadClassNodes(namespaceNode);

            RefreshObjectNodes();
        }

        private void btnSelectAllObjects_Click(object sender, EventArgs e)
        {
            var classNode = clbClassNodes.SelectedItem as IAnalysisNode;
            _updateFilter.ObjectFilterOption.DeselectNodes(n => n.Parent.Identifier == classNode.Identifier);
            LoadObjectNodes(classNode);
        }

        private void btnDeselectAllObjects_Click(object sender, EventArgs e)
        {
            var classNode = clbClassNodes.SelectedItem as IAnalysisNode;
           _updateFilter.ObjectFilterOption.DeselectNodes(n => n.Parent.Identifier == classNode.Identifier);
            LoadObjectNodes(classNode);
        }

        private void clbAssemblyNodes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            CheckListBoxItem(clbAssemblyNodes, _updateFilter.AssemblyFilterOption, e);
        }

        private void clbNamespaceNodes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            CheckListBoxItem(clbNamespaceNodes, _updateFilter.NamespaceFilterOption, e);
            RefreshClassNodes();
            RefreshObjectNodes();
        }

        private void clbClassNodes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            CheckListBoxItem(clbClassNodes, _updateFilter.ClassFilterOption, e);
        }

        private void clbObjectNodes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            CheckListBoxItem(clbObjectNodes, _updateFilter.ObjectFilterOption, e);
        }

        private void clbNamespaceNodes_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshClassNodes();
            RefreshObjectNodes();
        }

        private void EnableClassNodes(bool enable)
        {
            clbClassNodes.Enabled = enable;
            btnSelectAllClasses.Enabled = enable;
            btnDeselectAllClasses.Enabled = enable;
        }

        private void EnableObjectNodes(bool enable)
        {
            clbObjectNodes.Enabled = enable;
            btnSelectAllObjects.Enabled = enable;
            btnDeselectAllObjects.Enabled = enable;
        }

        private void clbClassNodes_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshObjectNodes();
        }
    }
}
