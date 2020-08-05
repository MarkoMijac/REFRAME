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
    public partial class FrmObjectFilter : Form, IFilterForm
    {
        public IEnumerable<IAnalysisNode> OriginalNodes { get; set; }

        public AnalysisLevel Level { get; set; }

        public IAnalysisFilter Filter { get; set; }

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
            Level = AnalysisLevel.ObjectLevel;
            var filterFactory = new AnalysisFilterFactory();
            Filter = filterFactory.CreateFilter(OriginalNodes, AnalysisLevel.ObjectLevel);
        }

        private void LoadNodes()
        {
            LoadAssemblyNodes();
            LoadNamespaceNodes();
        }

        private void LoadAssemblyNodes()
        {
            clbAssemblyNodes.Items.Clear();
            List<IAnalysisNode> assemblyNodes = Filter.GetAvailableAssemblyNodes();

            foreach (var assemblyNode in assemblyNodes)
            {
                bool checkedItem = Filter.IsSelected(assemblyNode);
                clbAssemblyNodes.Items.Add(assemblyNode, checkedItem);
            }
        }

        private void LoadNamespaceNodes()
        {
            clbNamespaceNodes.Items.Clear();
            List<IAnalysisNode> namespaceNodes = Filter.GetAvailableNamespaceNodes();
            foreach (var namespaceNode in namespaceNodes)
            {
                bool checkedItem = Filter.IsSelected(namespaceNode);
                clbNamespaceNodes.Items.Add(namespaceNode, checkedItem);
            }
        }

        private void LoadClassNodes(IAnalysisNode namespaceNode)
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

        private void LoadObjectNodes(IAnalysisNode classNode)
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
            LoadNodes();
        }

        private void btnSelecteAllAssemblies_Click(object sender, EventArgs e)
        {
            Filter.SelectAllAssemblyNodes();
            LoadAssemblyNodes();
        }

        private void btnDeselectAllAssemblies_Click(object sender, EventArgs e)
        {
            Filter.DeselectAllAssemblyNodes();
            LoadAssemblyNodes();
        }

        private void btnSelectAllNamespaces_Click(object sender, EventArgs e)
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

        private void btnDeselectAllNamespaces_Click(object sender, EventArgs e)
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

        private void btnSelectAllClasses_Click(object sender, EventArgs e)
        {
            var selectedNamespaceNode = clbNamespaceNodes.SelectedItem as IAnalysisNode;
            Filter.SelectAllNamespaceClasses(selectedNamespaceNode);
            LoadClassNodes(selectedNamespaceNode);

            RefreshObjectNodes();
        }

        private void btnDeselectAllClasses_Click(object sender, EventArgs e)
        {
            var selectedNamespaceNode = clbNamespaceNodes.SelectedItem as IAnalysisNode;
            Filter.DeselectAllNamespaceClasses(selectedNamespaceNode);
            LoadClassNodes(selectedNamespaceNode);

            RefreshObjectNodes();
        }

        private void btnSelectAllObjects_Click(object sender, EventArgs e)
        {
            var selectedClassNode = clbClassNodes.SelectedItem as IAnalysisNode;
            Filter.SelectAllClassObjects(selectedClassNode);
            LoadObjectNodes(selectedClassNode);
        }

        private void btnDeselectAllObjects_Click(object sender, EventArgs e)
        {
            var selectedClassNode = clbClassNodes.SelectedItem as IAnalysisNode;
            Filter.DeselectAllClassObjects(selectedClassNode);
            LoadObjectNodes(selectedClassNode);
        }

        private void clbAssemblyNodes_ItemCheck(object sender, ItemCheckEventArgs e)
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

        private void clbClassNodes_ItemCheck(object sender, ItemCheckEventArgs e)
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
            LoadObjectNodes(classNode);
        }

        private void clbObjectNodes_ItemCheck(object sender, ItemCheckEventArgs e)
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

        private void clbNamespaceNodes_SelectedIndexChanged(object sender, EventArgs e)
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

        private  void EnableClassNodes(bool enable)
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
