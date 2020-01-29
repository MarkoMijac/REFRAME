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
    public partial class FrmFilterClassMemberAnalysis : FrmFilterAnalysis
    {
        public FrmFilterClassMemberAnalysis() : base()
        {
            InitializeComponent();
        }

        protected override void Initialize()
        {
            base.Initialize();
            Level = AnalysisLevel.ClassMemberLevel;
            Filter = new ClassMemberAnalysisFilter(OriginalNodes);

            rbClassMemberLevel.Checked = true;
            HandleCheckListsVisibility();
            LoadAssemblyNodes();
            LoadNamespaceNodes();
        }

        private void HandleCheckListsVisibility()
        {
            clbAssemblyNodes.Enabled = true;
            clbNamespaceNodes.Enabled = true;
            clbClassNodes.Enabled = true;
            clbObjectNodes.Enabled = false;
        }

        private void LoadAssemblyNodes()
        {
            var classMemberFilter = Filter as ClassMemberAnalysisFilter;
            clbAssemblyNodes.Items.Clear();
            List<IAnalysisNode> assemblyNodes = classMemberFilter.GetAvailableAssemblyNodes();

            foreach (AssemblyAnalysisNode assemblyNode in assemblyNodes)
            {
                bool checkedItem = classMemberFilter.IsSelected(assemblyNode);
                clbAssemblyNodes.Items.Add(assemblyNode, checkedItem);
            }
        }

        private void LoadNamespaceNodes()
        {
            var classMemberFilter = Filter as ClassMemberAnalysisFilter;
            clbNamespaceNodes.Items.Clear();
            List<IAnalysisNode> namespaceNodes = classMemberFilter.GetAvailableNamespaceNodes();
            foreach (NamespaceAnalysisNode namespaceNode in namespaceNodes)
            {
                bool checkedItem = classMemberFilter.IsSelected(namespaceNode);
                clbNamespaceNodes.Items.Add(namespaceNode, checkedItem);
            }
        }

        private void LoadClassNodes(NamespaceAnalysisNode namespaceNode)
        {
            if (namespaceNode == null) return;

            var classMemberFilter = Filter as ClassMemberAnalysisFilter;
            clbClassNodes.Items.Clear();

            List<IAnalysisNode> classNodes = classMemberFilter.GetAvailableClassNodes(namespaceNode);
            foreach (ClassAnalysisNode classNode in classNodes)
            {
                bool checkedItem = classMemberFilter.IsSelected(classNode);
                clbClassNodes.Items.Add(classNode, checkedItem);
            }
        }

        private void clbNamespaceNodes_SelectedIndexChanged(object sender, EventArgs e)
        {
            var classFilter = Filter as ClassMemberAnalysisFilter;
            var namespaceNode = clbNamespaceNodes.SelectedItem as NamespaceAnalysisNode;
            LoadClassNodes(namespaceNode);
            clbClassNodes.Enabled = classFilter.IsSelected(namespaceNode);
        }

        private void clbClassNodes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            var classFilter = Filter as ClassMemberAnalysisFilter;
            ClassAnalysisNode classNode = clbClassNodes.SelectedItem as ClassAnalysisNode;
            if (e.NewValue == CheckState.Checked)
            {
                classFilter.SelectClassNode(classNode);
            }
            else if (e.NewValue == CheckState.Unchecked)
            {
                classFilter.DeselectClassNode(classNode);
            }
        }

        private void clbNamespaceNodes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            var classFilter = Filter as ClassMemberAnalysisFilter;
            NamespaceAnalysisNode namespaceNode = clbNamespaceNodes.SelectedItem as NamespaceAnalysisNode;
            if (e.NewValue == CheckState.Checked)
            {
                classFilter.SelectNamespaceNode(namespaceNode);
            }
            else if (e.NewValue == CheckState.Unchecked)
            {
                classFilter.DeselectNamespaceNode(namespaceNode);
            }

            LoadClassNodes(namespaceNode);
        }

        private void clbAssemblyNodes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            var classFilter = Filter as ClassMemberAnalysisFilter;

            AssemblyAnalysisNode assemblyNode = clbAssemblyNodes.SelectedItem as AssemblyAnalysisNode;
            if (e.NewValue == CheckState.Checked)
            {
                classFilter.SelectAssemblyNode(assemblyNode);
            }
            else if (e.NewValue == CheckState.Unchecked)
            {
                classFilter.DeselectAssemblyNode(assemblyNode);
            }
        }

        protected override void btnSelecteAllAssemblies_Click(object sender, EventArgs e)
        {
            var classFilter = Filter as ClassMemberAnalysisFilter;
            classFilter.SelectAllAssemblyNodes();
            LoadAssemblyNodes();
        }

        protected override void btnDeselectAllAssemblies_Click(object sender, EventArgs e)
        {
            var classFilter = Filter as ClassMemberAnalysisFilter;
            classFilter.DeselectAllAssemblyNodes();
            LoadAssemblyNodes();
        }

        protected override void btnSelectAllNamespaces_Click(object sender, EventArgs e)
        {
            var classFilter = Filter as ClassMemberAnalysisFilter;
            classFilter.SelectAllNamespaceNodes();
            LoadNamespaceNodes();
        }

        protected override void btnDeselectAllNamespaces_Click(object sender, EventArgs e)
        {
            var classFilter = Filter as ClassMemberAnalysisFilter;
            classFilter.DeselectAllNamespaceNodes();
            LoadNamespaceNodes();
        }

        protected override void btnSelectAllClasses_Click(object sender, EventArgs e)
        {
            var classFilter = Filter as ClassMemberAnalysisFilter;
            var selectedNamespaceNode = clbNamespaceNodes.SelectedItem as NamespaceAnalysisNode;
            classFilter.SelectAllClassNodes(selectedNamespaceNode);
            LoadClassNodes(selectedNamespaceNode);
        }

        protected override void btnDeselectAllClasses_Click(object sender, EventArgs e)
        {
            var classFilter = Filter as ClassMemberAnalysisFilter;
            var selectedNamespaceNode = clbNamespaceNodes.SelectedItem as NamespaceAnalysisNode;
            classFilter.DeselectAllClassNodes(selectedNamespaceNode);
            LoadClassNodes(selectedNamespaceNode);
        }
    }
}
