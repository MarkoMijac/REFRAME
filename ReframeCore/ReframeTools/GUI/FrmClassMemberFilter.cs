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
    public partial class FrmClassMemberFilter : FrmAnalysisFilter
    {

        private ClassMemberAnalysisFilter _classMemberFilter;
        public FrmClassMemberFilter()
        {
            InitializeComponent();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            Close();
        }

        protected override void Initialize()
        {
            Level = AnalysisLevel.ClassMemberLevel;
            Filter = new ClassMemberAnalysisFilter(OriginalNodes);
            _classMemberFilter = Filter as ClassMemberAnalysisFilter;
        }

        protected override void LoadNodes()
        {
            LoadAssemblyNodes();
            LoadNamespaceNodes();
        }

        private void LoadAssemblyNodes()
        {
            FillListBoxes(clbAssemblyNodes, _classMemberFilter.AssemblyFilterOption);
        }

        private void LoadNamespaceNodes()
        {
            FillListBoxes(clbNamespaceNodes, _classMemberFilter.NamespaceFilterOption);
        }

        private void LoadClassNodes(IAnalysisNode namespaceNode)
        {
            if (namespaceNode == null) return;

            FillListBoxes(clbClassNodes, _classMemberFilter.ClassFilterOption, n => n.Parent.Identifier == namespaceNode.Identifier);
        }

        private void FrmOptions_Load(object sender, EventArgs e)
        {
            Initialize();
            LoadNodes();
        }

        private void btnSelecteAllAssemblies_Click(object sender, EventArgs e)
        {
            _classMemberFilter.AssemblyFilterOption.SelectNodes();
            LoadAssemblyNodes();
        }

        private void btnDeselectAllAssemblies_Click(object sender, EventArgs e)
        {
            _classMemberFilter.AssemblyFilterOption.DeselectNodes();
            LoadAssemblyNodes();
        }

        private void btnSelectAllNamespaces_Click(object sender, EventArgs e)
        {
            _classMemberFilter.NamespaceFilterOption.SelectNodes();
            LoadNamespaceNodes();
            RefreshClassNodes();
        }

        private void btnDeselectAllNamespaces_Click(object sender, EventArgs e)
        {
            _classMemberFilter.NamespaceFilterOption.DeselectNodes();
            LoadNamespaceNodes();

            RefreshClassNodes();
        }

        private void RefreshClassNodes()
        {
            var selectedNamespaceNode = clbNamespaceNodes.SelectedItem as IAnalysisNode;
            LoadClassNodes(selectedNamespaceNode);
            EnableClassNodes(_classMemberFilter.NamespaceFilterOption.IsSelected(selectedNamespaceNode));
        }

        private void btnSelectAllClasses_Click(object sender, EventArgs e)
        {
            var namespaceNode = clbNamespaceNodes.SelectedItem as IAnalysisNode;
            _classMemberFilter.ClassFilterOption.SelectNodes(n => n.Parent.Identifier == namespaceNode.Identifier);
            LoadClassNodes(namespaceNode);
        }

        private void btnDeselectAllClasses_Click(object sender, EventArgs e)
        {
            var namespaceNode = clbNamespaceNodes.SelectedItem as IAnalysisNode;
            _classMemberFilter.ClassFilterOption.DeselectNodes(n => n.Parent.Identifier == namespaceNode.Identifier);
            LoadClassNodes(namespaceNode);;
        }

        private void clbAssemblyNodes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            CheckListBoxItem(clbAssemblyNodes, _classMemberFilter.AssemblyFilterOption, e);
        }

        private void clbNamespaceNodes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            CheckListBoxItem(clbNamespaceNodes, _classMemberFilter.NamespaceFilterOption, e);
            RefreshClassNodes();
        }

        private void clbClassNodes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            CheckListBoxItem(clbClassNodes, _classMemberFilter.ClassFilterOption, e);
        }


        private void clbNamespaceNodes_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshClassNodes();
        }

        private void EnableClassNodes(bool enable)
        {
            clbClassNodes.Enabled = enable;
            btnSelectAllClasses.Enabled = enable;
            btnDeselectAllClasses.Enabled = enable;
        }
    }
}
