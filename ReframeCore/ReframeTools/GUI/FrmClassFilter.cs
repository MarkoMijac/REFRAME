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
    public partial class FrmClassFilter : Form, IFilterForm
    {
        public IEnumerable<IAnalysisNode> OriginalNodes { get; set; }

        public AnalysisLevel Level { get; set; }

        public IAnalysisFilter Filter { get; set; }

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
            Level = AnalysisLevel.ClassLevel;
            var filterFactory = new AnalysisFilterFactory();
            Filter = filterFactory.CreateFilter(OriginalNodes, AnalysisLevel.ClassLevel);
        }

        private void LoadNodes()
        {
            LoadAssemblyNodes();
            LoadNamespaceNodes();
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

        private void FrmOptions_Load(object sender, EventArgs e)
        {
            Initialize();
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

            RefreshClassNodes();
        }

        protected virtual void btnDeselectAllNamespaces_Click(object sender, EventArgs e)
        {
            Filter.DeselectAllNamespaceNodes();
            LoadNamespaceNodes();

            RefreshClassNodes();
        }

        private void RefreshClassNodes()
        {
            var selectedNamespaceNode = clbNamespaceNodes.SelectedItem as IAnalysisNode;
            LoadClassNodes(selectedNamespaceNode);
        }

        protected virtual void btnSelectAllClasses_Click(object sender, EventArgs e)
        {
            var selectedNamespaceNode = clbNamespaceNodes.SelectedItem as IAnalysisNode;
            Filter.SelectAllNamespaceClasses(selectedNamespaceNode);
            LoadClassNodes(selectedNamespaceNode);
        }

        protected virtual void btnDeselectAllClasses_Click(object sender, EventArgs e)
        {
            var selectedNamespaceNode = clbNamespaceNodes.SelectedItem as IAnalysisNode;
            Filter.DeselectAllNamespaceClasses(selectedNamespaceNode);
            LoadClassNodes(selectedNamespaceNode);
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

        private void clbNamespaceNodes_ItemCheck(object sender, ItemCheckEventArgs e)
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

            LoadClassNodes(namespaceNode);
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

       
        protected virtual void clbNamespaceNodes_SelectedIndexChanged(object sender, EventArgs e)
        {
            var namespaceNode = clbNamespaceNodes.SelectedItem as IAnalysisNode;
            LoadClassNodes(namespaceNode);
        }
    }
}
