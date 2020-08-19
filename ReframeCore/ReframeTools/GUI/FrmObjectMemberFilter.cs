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
    public partial class FrmObjectMemberFilter : FrmAnalysisFilter
    {
        private ObjectMemberAnalysisFilter _objectMemberFilter;
        public FrmObjectMemberFilter()
        {
            InitializeComponent();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Initialize()
        {
            _objectMemberFilter = new ObjectMemberAnalysisFilter(OriginalNodes);
            Filter = _objectMemberFilter;
        }

        private void LoadNodes()
        {
            LoadAssemblyNodes();
            LoadNamespaceNodes();
        }

        private void LoadAssemblyNodes()
        {
            FillListBoxes(clbAssemblyNodes, _objectMemberFilter.AssemblyFilterOption);
        }

        private void LoadNamespaceNodes()
        {
            FillListBoxes(clbNamespaceNodes, _objectMemberFilter.NamespaceFilterOption);
        }

        private void LoadClassNodes(IAnalysisNode namespaceNode)
        {
            if (namespaceNode == null) return;

            FillListBoxes(clbClassNodes, _objectMemberFilter.ClassFilterOption, n => n.Parent.Identifier == namespaceNode.Identifier);
        }

        private void LoadObjectNodes(IAnalysisNode classNode)
        {
            if (classNode == null) return;

            FillListBoxes(clbObjectNodes, _objectMemberFilter.ObjectFilterOption, n => n.Parent.Identifier == classNode.Identifier);
        }

        private void FrmOptions_Load(object sender, EventArgs e)
        {
            Initialize();
            LoadNodes();
        }

        private void btnSelecteAllAssemblies_Click(object sender, EventArgs e)
        {
            _objectMemberFilter.AssemblyFilterOption.SelectNodes();
            LoadAssemblyNodes();
        }

        private void btnDeselectAllAssemblies_Click(object sender, EventArgs e)
        {
            _objectMemberFilter.AssemblyFilterOption.DeselectNodes();
            LoadAssemblyNodes();
        }

        private void btnSelectAllNamespaces_Click(object sender, EventArgs e)
        {
            _objectMemberFilter.NamespaceFilterOption.SelectNodes();
            LoadNamespaceNodes();

            RefreshClassNodes();
            RefreshObjectNodes();
        }

        private void RefreshObjectNodes()
        {
            var selectedClassNode = clbClassNodes.SelectedItem as IAnalysisNode;
            LoadObjectNodes(selectedClassNode);
            EnableObjectNodes(_objectMemberFilter.ClassFilterOption.IsSelected(selectedClassNode));
        }

        private void btnDeselectAllNamespaces_Click(object sender, EventArgs e)
        {
            _objectMemberFilter.NamespaceFilterOption.DeselectNodes();
            LoadNamespaceNodes();

            RefreshClassNodes();
            RefreshObjectNodes();
        }

        private void RefreshClassNodes()
        {
            var selectedNamespaceNode = clbNamespaceNodes.SelectedItem as IAnalysisNode;
            LoadClassNodes(selectedNamespaceNode);
            EnableClassNodes(_objectMemberFilter.NamespaceFilterOption.IsSelected(selectedNamespaceNode));
        }

        private void btnSelectAllClasses_Click(object sender, EventArgs e)
        {
            var namespaceNode = clbNamespaceNodes.SelectedItem as IAnalysisNode;
            _objectMemberFilter.ClassFilterOption.SelectNodes(n => n.Parent.Identifier == namespaceNode.Identifier);
            LoadClassNodes(namespaceNode);

            RefreshObjectNodes();
        }

        private void btnDeselectAllClasses_Click(object sender, EventArgs e)
        {
            var namespaceNode = clbNamespaceNodes.SelectedItem as IAnalysisNode;
            _objectMemberFilter.ClassFilterOption.DeselectNodes(n => n.Parent.Identifier == namespaceNode.Identifier);
            LoadClassNodes(namespaceNode);

            RefreshObjectNodes();
        }

        private void btnSelectAllObjects_Click(object sender, EventArgs e)
        {
            var classNode = clbClassNodes.SelectedItem as IAnalysisNode;
            _objectMemberFilter.ObjectFilterOption.SelectNodes(n => n.Parent.Identifier == classNode.Identifier);
            LoadObjectNodes(classNode);
        }

        private void btnDeselectAllObjects_Click(object sender, EventArgs e)
        {
            var classNode = clbClassNodes.SelectedItem as IAnalysisNode;
            _objectMemberFilter.ObjectFilterOption.DeselectNodes(n => n.Parent.Identifier == classNode.Identifier);
            LoadObjectNodes(classNode);
        }

        private void clbAssemblyNodes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            CheckListBoxItem(clbAssemblyNodes, _objectMemberFilter.AssemblyFilterOption, e);
        }

        private void clbNamespaceNodes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            CheckListBoxItem(clbNamespaceNodes, _objectMemberFilter.NamespaceFilterOption, e);
            RefreshClassNodes();
            RefreshObjectNodes();
        }

        private void clbClassNodes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            CheckListBoxItem(clbClassNodes, _objectMemberFilter.ClassFilterOption, e);
            RefreshObjectNodes();
        }

        private void clbObjectNodes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            CheckListBoxItem(clbObjectNodes, _objectMemberFilter.ObjectFilterOption, e);
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
