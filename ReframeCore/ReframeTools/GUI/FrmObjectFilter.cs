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
    public partial class FrmObjectFilter : FrmAnalysisFilter
    {
        private ObjectAnalysisFilter _objectFilter;
        public FrmObjectFilter()
        {
            InitializeComponent();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Initialize()
        {
            Filter = new ObjectAnalysisFilter(OriginalNodes);
            _objectFilter = Filter as ObjectAnalysisFilter;
        }

        private void LoadNodes()
        {
            LoadAssemblyNodes();
            LoadNamespaceNodes();
        }

        private void LoadAssemblyNodes()
        {
            FillListBox(clbAssemblyNodes, _objectFilter.AssemblyFilterOption);
        }

        private void LoadNamespaceNodes()
        {
            FillListBox(clbNamespaceNodes, _objectFilter.NamespaceFilterOption);
        }

        private void LoadClassNodes(IAnalysisNode namespaceNode)
        {
            if (namespaceNode == null) return;

            FillListBox(clbClassNodes, _objectFilter.ClassFilterOption, node => node.Parent.Identifier == namespaceNode.Identifier);
        }

        private void LoadObjectNodes(IAnalysisNode classNode)
        {
            if (classNode == null) return;

            FillListBox(clbObjectNodes, _objectFilter.ObjectFilterOption, n=>n.Parent.Identifier == classNode.Identifier);
        }

        private void FrmOptions_Load(object sender, EventArgs e)
        {
            Initialize();
            LoadNodes();
        }

        private void btnSelecteAllAssemblies_Click(object sender, EventArgs e)
        {
            _objectFilter.AssemblyFilterOption.SelectNodes();
            LoadAssemblyNodes();
        }

        private void btnDeselectAllAssemblies_Click(object sender, EventArgs e)
        {
            _objectFilter.AssemblyFilterOption.DeselectNodes();
            LoadAssemblyNodes();
        }

        private void btnSelectAllNamespaces_Click(object sender, EventArgs e)
        {
            _objectFilter.NamespaceFilterOption.SelectNodes();
            LoadNamespaceNodes();

            RefreshClassNodes();
            RefreshObjectNodes();
        }

        private void RefreshObjectNodes()
        {
            var selectedClassNode = clbClassNodes.SelectedItem as IAnalysisNode;
            LoadObjectNodes(selectedClassNode);
            EnableObjectNodes(_objectFilter.ClassFilterOption.IsSelected(selectedClassNode));
        }

        private void btnDeselectAllNamespaces_Click(object sender, EventArgs e)
        {
            _objectFilter.NamespaceFilterOption.DeselectNodes();
            LoadNamespaceNodes();

            RefreshClassNodes();
            RefreshObjectNodes();
        }

        private void RefreshClassNodes()
        {
            var selectedNamespaceNode = clbNamespaceNodes.SelectedItem as IAnalysisNode;
            LoadClassNodes(selectedNamespaceNode);
            EnableClassNodes(_objectFilter.NamespaceFilterOption.IsSelected(selectedNamespaceNode));
        }

        private void btnSelectAllClasses_Click(object sender, EventArgs e)
        {
            var namespaceNode = clbNamespaceNodes.SelectedItem as IAnalysisNode;
            var namespacePredicate = new Predicate<IAnalysisNode>(n => n.Parent.Identifier == namespaceNode.Identifier);
            _objectFilter.ClassFilterOption.SelectNodes(namespacePredicate);
            LoadClassNodes(namespaceNode);

            RefreshObjectNodes();
        }

        private void btnDeselectAllClasses_Click(object sender, EventArgs e)
        {
            var namespaceNode = clbNamespaceNodes.SelectedItem as IAnalysisNode;
            var namespacePredicate = new Predicate<IAnalysisNode>(n => n.Parent.Identifier == namespaceNode.Identifier);
            _objectFilter.ClassFilterOption.DeselectNodes(namespacePredicate);
            LoadClassNodes(namespaceNode);

            RefreshObjectNodes();
        }

        private void btnSelectAllObjects_Click(object sender, EventArgs e)
        {
            var classNode = clbClassNodes.SelectedItem as IAnalysisNode;
            var classPredicate = new Predicate<IAnalysisNode>(n => n.Parent.Identifier == classNode.Identifier);
            _objectFilter.ObjectFilterOption.SelectNodes(classPredicate);
            LoadObjectNodes(classNode);
        }

        private void btnDeselectAllObjects_Click(object sender, EventArgs e)
        {
            var classNode = clbClassNodes.SelectedItem as IAnalysisNode;
            var classPredicate = new Predicate<IAnalysisNode>(n => n.Parent.Identifier == classNode.Identifier);
            _objectFilter.ObjectFilterOption.DeselectNodes(classPredicate);
            LoadObjectNodes(classNode);
        }

        private void clbAssemblyNodes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            CheckListBoxItem(clbAssemblyNodes, _objectFilter.AssemblyFilterOption, e);
        }

        private void clbNamespaceNodes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            CheckListBoxItem(clbNamespaceNodes, _objectFilter.NamespaceFilterOption, e);
            RefreshClassNodes();
            RefreshObjectNodes();
        }

        private void clbClassNodes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            CheckListBoxItem(clbClassNodes, _objectFilter.ClassFilterOption, e);
            RefreshObjectNodes();
        }

        private void clbObjectNodes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            CheckListBoxItem(clbObjectNodes, _objectFilter.ObjectFilterOption, e);
        }

        private void clbNamespaceNodes_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshClassNodes();
            RefreshObjectNodes();
        }

        private  void EnableClassNodes(bool enable)
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
