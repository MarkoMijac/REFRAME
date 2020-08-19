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

        protected override void Initialize()
        {
            Level = AnalysisLevel.UpdateAnalysisLevel;
            _updateFilter = new UpdateAnalysisFilter(OriginalNodes);
            Filter = _updateFilter;
        }

        protected override void LoadNodes()
        {
            LoadAssemblyNodes();
            LoadNamespaceNodes();
        }

        private void LoadAssemblyNodes()
        {
            FillListBoxes(clbAssemblyNodes, _updateFilter.AssemblyFilterOption);
        }

        private void LoadNamespaceNodes()
        {
            FillListBoxes(clbNamespaceNodes, _updateFilter.NamespaceFilterOption);
        }

        private void LoadClassNodes(IAnalysisNode namespaceNode)
        {
            if (namespaceNode == null) return;

            FillListBoxes(clbClassNodes, _updateFilter.ClassFilterOption, node => node.Parent.Identifier == namespaceNode.Identifier);
        }

        private void LoadObjectNodes(IAnalysisNode classNode)
        {
            if (classNode == null) return;

            FillListBoxes(clbObjectNodes, _updateFilter.ObjectFilterOption, n => n.Parent.Identifier == classNode.Identifier);
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
            var selectedClassNode = clbClassNodes.SelectedItem as IAnalysisNode;
            LoadObjectNodes(selectedClassNode);
            EnableObjectNodes(_updateFilter.IsSelected(selectedClassNode));
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
            var selectedNamespaceNode = clbNamespaceNodes.SelectedItem as IAnalysisNode;
            LoadClassNodes(selectedNamespaceNode);
            EnableClassNodes(_updateFilter.IsSelected(selectedNamespaceNode));
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
            CheckListBoxItem(clbAssemblyNodes, e);
        }

        private void clbNamespaceNodes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            CheckListBoxItem(clbNamespaceNodes, e);
            RefreshClassNodes();
            RefreshObjectNodes();
        }

        private void clbClassNodes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            CheckListBoxItem(clbClassNodes, e);
        }

        private void clbObjectNodes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            CheckListBoxItem(clbObjectNodes, e);
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
