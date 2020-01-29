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
    public partial class FrmFilterClassAnalysis : FrmFilterAnalysis
    {
        public FrmFilterClassAnalysis() : base()
        {
            InitializeComponent();
        }

        protected override void Initialize()
        {
            base.Initialize();
            Level = AnalysisLevel.ClassLevel;
            Filter = new ClassAnalysisFilter(OriginalNodes);
            
            rbClassLevel.Checked = true;
            HandleCheckListsVisibility();
            LoadAssemblyNodes();
            LoadNamespaceNodes();
        }

        private void HandleCheckListsVisibility()
        {
            clbAssemblyNodes.Enabled = true;
            clbNamespaceNodes.Enabled = true;
            clbClassNodes.Enabled = false;
            clbObjectNodes.Enabled = false;
        }

        private void LoadAssemblyNodes()
        {
            var classFilter = Filter as ClassAnalysisFilter;
            clbAssemblyNodes.Items.Clear();
            List<IAnalysisNode> assemblyNodes = classFilter.GetAvailableAssemblyNodes();

            foreach (AssemblyAnalysisNode assemblyNode in assemblyNodes)
            {
                bool checkedItem = classFilter.IsSelected(assemblyNode);
                clbAssemblyNodes.Items.Add(assemblyNode, checkedItem);
            }
        }

        private void LoadNamespaceNodes()
        {
            var classFilter = Filter as ClassAnalysisFilter;
            clbNamespaceNodes.Items.Clear();
            List<IAnalysisNode> namespaceNodes = classFilter.GetAvailableNamespaceNodes();
            foreach (NamespaceAnalysisNode namespaceNode in namespaceNodes)
            {
                bool checkedItem = classFilter.IsSelected(namespaceNode);
                clbNamespaceNodes.Items.Add(namespaceNode, checkedItem);
            }
        }

        private void clbAssemblyNodes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            var classFilter = Filter as ClassAnalysisFilter;

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

        private void clbNamespaceNodes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            var classFilter = Filter as ClassAnalysisFilter;
            NamespaceAnalysisNode namespaceNode = clbNamespaceNodes.SelectedItem as NamespaceAnalysisNode;
            if (e.NewValue == CheckState.Checked)
            {
                classFilter.SelectNamespaceNode(namespaceNode);
            }
            else if (e.NewValue == CheckState.Unchecked)
            {
                classFilter.DeselectNamespaceNode(namespaceNode);
            }
        }

        protected override void btnApply_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
