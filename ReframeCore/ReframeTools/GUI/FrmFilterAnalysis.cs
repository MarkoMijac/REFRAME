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

            foreach (var assemblyNode in assemblyNodes)
            {
                bool checkedItem = Filter.IsSelected(assemblyNode);
                clbAssemblyNodes.Items.Add(assemblyNode, checkedItem);
            }
        }

        protected void LoadNamespaceNodes()
        {
            clbNamespaceNodes.Items.Clear();
            List<IAnalysisNode> namespaceNodes = Filter.GetAvailableNamespaceNodes();
            foreach (var namespaceNode in namespaceNodes)
            {
                bool checkedItem = Filter.IsSelected(namespaceNode);
                clbNamespaceNodes.Items.Add(namespaceNode, checkedItem);
            }
        }

        protected void LoadClassNodes(IAnalysisNode namespaceNode)
        {
            clbClassNodes.Items.Clear();
            if (namespaceNode != null)
            {
                List<IAnalysisNode> classNodes = Filter.GetAvailableClassNodes(namespaceNode);
                foreach (var classNode in classNodes)
                {
                    bool checkedItem = Filter.IsSelected(classNode);
                    clbClassNodes.Items.Add(classNode, checkedItem);
                }
            }
        }

        protected void LoadObjectNodes(IAnalysisNode classNode)
        {
            clbObjectNodes.Items.Clear();
            if (classNode != null)
            {
                List<IAnalysisNode> objectNodes = Filter.GetAvailableObjectNodes(classNode);
                foreach (var objectNode in objectNodes)
                {
                    bool checkedItem = Filter.IsSelected(objectNode);
                    clbObjectNodes.Items.Add(objectNode, checkedItem);
                }
            }
        }

        private void FrmOptions_Load(object sender, EventArgs e)
        {
            Initialize();
            HandleCheckListsVisibility();
            LoadNodes();
        }

        protected void AdjustFormSize(int verticalOffset)
        {
            if (verticalOffset == 0) return;

            gbFilterByParent.Size = new Size(gbFilterByParent.Size.Width, gbFilterByParent.Size.Height - verticalOffset);
            btnApply.Location = new Point(btnApply.Location.X, btnApply.Location.Y - verticalOffset);
            this.Size = new Size(this.Size.Width, this.Size.Height - verticalOffset);
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

            RefreshClassNodes();
            RefreshObjectNodes();
        }

        private void RefreshObjectNodes()
        {
            var selectedClassNode = clbClassNodes.SelectedItem as IAnalysisNode;
            LoadObjectNodes(selectedClassNode);
            EnableObjectNodes(Filter.IsSelected(selectedClassNode));
        }

        protected virtual void btnDeselectAllNamespaces_Click(object sender, EventArgs e)
        {
            Filter.DeselectAllNamespaceNodes();
            LoadNamespaceNodes();

            RefreshClassNodes();
            RefreshObjectNodes();
        }

        private void RefreshClassNodes()
        {
            var selectedNamespaceNode = clbNamespaceNodes.SelectedItem as IAnalysisNode;
            LoadClassNodes(selectedNamespaceNode);
            EnableClassNodes(Filter.IsSelected(selectedNamespaceNode));
        }

        protected virtual void btnSelectAllClasses_Click(object sender, EventArgs e)
        {
            var selectedNamespaceNode = clbNamespaceNodes.SelectedItem as IAnalysisNode;
            Filter.SelectAllNamespaceClasses(selectedNamespaceNode);
            LoadClassNodes(selectedNamespaceNode);

            RefreshObjectNodes();
        }

        protected virtual void btnDeselectAllClasses_Click(object sender, EventArgs e)
        {
            var selectedNamespaceNode = clbNamespaceNodes.SelectedItem as IAnalysisNode;
            Filter.DeselectAllNamespaceClasses(selectedNamespaceNode);
            LoadClassNodes(selectedNamespaceNode);

            RefreshObjectNodes();
        }

        protected virtual void btnSelectAllObjects_Click(object sender, EventArgs e)
        {
            var selectedClassNode = clbClassNodes.SelectedItem as IAnalysisNode;
            Filter.SelectAllClassObjects(selectedClassNode);
            LoadObjectNodes(selectedClassNode);
        }

        protected virtual void btnDeselectAllObjects_Click(object sender, EventArgs e)
        {
            var selectedClassNode = clbClassNodes.SelectedItem as IAnalysisNode;
            Filter.DeselectAllClassObjects(selectedClassNode);
            LoadObjectNodes(selectedClassNode);
        }

        protected virtual void clbAssemblyNodes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            IAnalysisNode assemblyNode = clbAssemblyNodes.SelectedItem as IAnalysisNode;
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
            var namespaceNode = clbNamespaceNodes.SelectedItem as IAnalysisNode;
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
            var classNode = clbClassNodes.SelectedItem as IAnalysisNode;
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
            var objectNode = clbObjectNodes.SelectedItem as IAnalysisNode;
            if (objectNode != null)
            {
                if (e.NewValue == CheckState.Checked)
                {
                    Filter.SelectNode(objectNode);
                }
                else if (e.NewValue == CheckState.Unchecked)
                {
                    Filter.DeselectNode(objectNode);
                }
            }
        }

        protected virtual void clbAssemblyNodes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected virtual void clbNamespaceNodes_SelectedIndexChanged(object sender, EventArgs e)
        {
            var namespaceNode = clbNamespaceNodes.SelectedItem as IAnalysisNode;
            LoadClassNodes(namespaceNode);
            EnableClassNodes(Filter.IsSelected(namespaceNode));

            var classNode = clbClassNodes.SelectedItem as IAnalysisNode;
            if (classNode != null)
            {
                LoadObjectNodes(classNode);
                EnableObjectNodes(Filter.IsSelected(classNode));
            }
        }

        protected void EnableClassNodes(bool enable)
        {
            clbClassNodes.Enabled = enable;
            btnSelectAllClasses.Enabled = enable;
            btnDeselectAllClasses.Enabled = enable;
        }

        protected void EnableObjectNodes(bool enable)
        {
            clbObjectNodes.Enabled = enable;
            btnSelectAllObjects.Enabled = enable;
            btnDeselectAllObjects.Enabled = enable;
        }

        protected virtual void clbClassNodes_SelectedIndexChanged(object sender, EventArgs e)
        {
            var classNode = clbClassNodes.SelectedItem as IAnalysisNode;
            LoadObjectNodes(classNode);
            EnableObjectNodes(Filter.IsSelected(classNode));

        }
    }
}
