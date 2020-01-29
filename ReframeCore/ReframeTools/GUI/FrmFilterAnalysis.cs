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
    public partial class FrmFilterAnalysis : Form
    {
        public IEnumerable<IAnalysisNode> OriginalNodes { get; set; }

        public AnalysisLevel Level { get; set; }

        public IAnalysisFilter Filter { get; protected set; }

        public FrmFilterAnalysis()
        {
            InitializeComponent();
        }

        protected virtual void btnApply_Click(object sender, EventArgs e)
        {
            Close();
        }

        protected virtual void Initialize() { }

        protected virtual void HandleCheckListsVisibility()
        {

        }

        protected void MakeAssemblyNodesVisible(bool visible)
        {
            clbAssemblyNodes.Visible = visible;
            btnSelecteAllAssemblies.Visible = visible;
            btnDeselectAllAssemblies.Visible = visible;
            lblAssemblies.Visible = visible;
        }

        protected void MakeNamespaceNodesVisible(bool visible)
        {
            clbNamespaceNodes.Visible = visible;
            btnSelectAllNamespaces.Visible = visible;
            btnDeselectAllNamespaces.Visible = visible;
            lblNamespaces.Visible = visible;
        }

        protected void MakeClassNodesVisible(bool visible)
        {
            clbClassNodes.Visible = visible;
            btnSelectAllClasses.Visible = visible;
            btnDeselectAllClasses.Visible = visible;
            lblClassNodes.Visible = visible;
        }

        protected void MakeObjectNodesVisible(bool visible)
        {
            clbObjectNodes.Visible = visible;
            btnSelectAllObjects.Visible = visible;
            btnDeselectAllObjects.Visible = visible;
            lblObjectNodes.Visible = visible;
        }

        protected virtual void LoadNodes()
        {
            
        }

        protected void LoadAssemblyNodes()
        {
            clbAssemblyNodes.Items.Clear();
            List<IAnalysisNode> assemblyNodes = Filter.GetAvailableAssemblyNodes();

            foreach (AssemblyAnalysisNode assemblyNode in assemblyNodes)
            {
                bool checkedItem = Filter.IsSelected(assemblyNode);
                clbAssemblyNodes.Items.Add(assemblyNode, checkedItem);
            }
        }

        protected void LoadNamespaceNodes()
        {
            clbNamespaceNodes.Items.Clear();
            List<IAnalysisNode> namespaceNodes = Filter.GetAvailableNamespaceNodes();
            foreach (NamespaceAnalysisNode namespaceNode in namespaceNodes)
            {
                bool checkedItem = Filter.IsSelected(namespaceNode);
                clbNamespaceNodes.Items.Add(namespaceNode, checkedItem);
            }
        }

        protected void LoadClassNodes(NamespaceAnalysisNode namespaceNode)
        {
            if (namespaceNode == null) return;

            clbClassNodes.Items.Clear();

            List<IAnalysisNode> classNodes = Filter.GetAvailableClassNodes(namespaceNode);
            foreach (ClassAnalysisNode classNode in classNodes)
            {
                bool checkedItem = Filter.IsSelected(classNode);
                clbClassNodes.Items.Add(classNode, checkedItem);
            }
        }

        private void FrmOptions_Load(object sender, EventArgs e)
        {
            Initialize();
            HandleCheckListsVisibility();
            LoadNodes();
        }

        protected virtual void btnSelecteAllAssemblies_Click(object sender, EventArgs e)
        {
            Filter.SelectAllAssemblyNodes();
            LoadAssemblyNodes();
        }

        protected virtual void btnDeselectAllAssemblies_Click(object sender, EventArgs e)
        {
            Filter.DeselectAllAssemblyNodes();
            LoadAssemblyNodes();
        }

        protected virtual void btnSelectAllNamespaces_Click(object sender, EventArgs e)
        {
            Filter.SelectAllNamespaceNodes();
            LoadNamespaceNodes();
        }

        protected virtual void btnDeselectAllNamespaces_Click(object sender, EventArgs e)
        {
            Filter.DeselectAllNamespaceNodes();
            LoadNamespaceNodes();
        }

        protected virtual void btnSelectAllClasses_Click(object sender, EventArgs e)
        {
            var selectedNamespaceNode = clbNamespaceNodes.SelectedItem as NamespaceAnalysisNode;
            Filter.SelectAllClassNodes(selectedNamespaceNode);
            LoadClassNodes(selectedNamespaceNode);
        }

        protected virtual void btnDeselectAllClasses_Click(object sender, EventArgs e)
        {
            var selectedNamespaceNode = clbNamespaceNodes.SelectedItem as NamespaceAnalysisNode;
            Filter.DeselectAllClassNodes(selectedNamespaceNode);
            LoadClassNodes(selectedNamespaceNode);
        }

        protected virtual void btnSelectAllObjects_Click(object sender, EventArgs e)
        {

        }

        protected virtual void btnDeselectAllObjects_Click(object sender, EventArgs e)
        {

        }

        protected virtual void clbAssemblyNodes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            AssemblyAnalysisNode assemblyNode = clbAssemblyNodes.SelectedItem as AssemblyAnalysisNode;
            if (e.NewValue == CheckState.Checked)
            {
                Filter.SelectNode(assemblyNode);
            }
            else if (e.NewValue == CheckState.Unchecked)
            {
                Filter.DeselectNode(assemblyNode);
            }
        }

        protected virtual void clbNamespaceNodes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            NamespaceAnalysisNode namespaceNode = clbNamespaceNodes.SelectedItem as NamespaceAnalysisNode;
            if (e.NewValue == CheckState.Checked)
            {
                Filter.SelectNode(namespaceNode);
            }
            else if (e.NewValue == CheckState.Unchecked)
            {
                Filter.DeselectNode(namespaceNode);
            }
        }

        protected virtual void clbClassNodes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            ClassAnalysisNode classNode = clbClassNodes.SelectedItem as ClassAnalysisNode;
            if (e.NewValue == CheckState.Checked)
            {
                Filter.SelectNode(classNode);
            }
            else if (e.NewValue == CheckState.Unchecked)
            {
                Filter.DeselectNode(classNode);
            }
        }

        protected virtual void clbObjectNodes_ItemCheck(object sender, ItemCheckEventArgs e)
        {

        }

        protected virtual void clbAssemblyNodes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected virtual void clbNamespaceNodes_SelectedIndexChanged(object sender, EventArgs e)
        {
            var namespaceNode = clbNamespaceNodes.SelectedItem as NamespaceAnalysisNode;
            LoadClassNodes(namespaceNode);
            EnableClassNodes(Filter.IsSelected(namespaceNode));
        }

        private void EnableClassNodes(bool enable)
        {
            clbClassNodes.Enabled = enable;
            btnSelectAllClasses.Enabled = enable;
            btnDeselectAllClasses.Enabled = enable;
        }

        protected virtual void clbClassNodes_SelectedIndexChanged(object sender, EventArgs e)
        {
            

        }
    }
}
