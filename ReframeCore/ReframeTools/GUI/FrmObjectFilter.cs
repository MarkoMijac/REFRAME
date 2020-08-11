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

        protected override void Initialize()
        {
            Level = AnalysisLevel.ObjectLevel;
            Filter = new ObjectAnalysisFilter(OriginalNodes);
            _objectFilter = Filter as ObjectAnalysisFilter;
        }

        protected override void LoadNodes()
        {
            LoadAssemblyNodes();
            LoadNamespaceNodes();
        }

        private void LoadAssemblyNodes()
        {
            FillListBoxes(clbAssemblyNodes, _objectFilter.GetAvailableAssemblyNodes());
        }

        private void LoadNamespaceNodes()
        {
            FillListBoxes(clbNamespaceNodes, _objectFilter.GetAvailableNamespaceNodes());
        }

        private void LoadClassNodes(IAnalysisNode namespaceNode)
        {
            List<IAnalysisNode> classNodes = new List<IAnalysisNode>();
            if (namespaceNode != null)
            {
                classNodes = _objectFilter.GetAvailableClassNodes(namespaceNode);
            }

            FillListBoxes(clbClassNodes, classNodes);
        }

        private void LoadObjectNodes(IAnalysisNode classNode)
        {
            List<IAnalysisNode> objectNodes;
            if (classNode != null)
            {
                objectNodes = _objectFilter.GetAvailableObjectNodes(classNode);
            }
            else
            {
                objectNodes = new List<IAnalysisNode>();
            }

            FillListBoxes(clbObjectNodes, objectNodes);
        }

        private void FrmOptions_Load(object sender, EventArgs e)
        {
            Initialize();
            LoadNodes();
        }

        private void btnSelecteAllAssemblies_Click(object sender, EventArgs e)
        {
            _objectFilter.SelectAllAssemblyNodes();
            LoadAssemblyNodes();
        }

        private void btnDeselectAllAssemblies_Click(object sender, EventArgs e)
        {
            _objectFilter.DeselectAllAssemblyNodes();
            LoadAssemblyNodes();
        }

        private void btnSelectAllNamespaces_Click(object sender, EventArgs e)
        {
            _objectFilter.SelectAllNamespaceNodes();
            LoadNamespaceNodes();

            RefreshClassNodes();
            RefreshObjectNodes();
        }

        private void RefreshObjectNodes()
        {
            var selectedClassNode = clbClassNodes.SelectedItem as IAnalysisNode;
            LoadObjectNodes(selectedClassNode);
            EnableObjectNodes(_objectFilter.IsSelected(selectedClassNode));
        }

        private void btnDeselectAllNamespaces_Click(object sender, EventArgs e)
        {
            _objectFilter.DeselectAllNamespaceNodes();
            LoadNamespaceNodes();

            RefreshClassNodes();
            RefreshObjectNodes();
        }

        private void RefreshClassNodes()
        {
            var selectedNamespaceNode = clbNamespaceNodes.SelectedItem as IAnalysisNode;
            LoadClassNodes(selectedNamespaceNode);
            EnableClassNodes(_objectFilter.IsSelected(selectedNamespaceNode));
        }

        private void btnSelectAllClasses_Click(object sender, EventArgs e)
        {
            var selectedNamespaceNode = clbNamespaceNodes.SelectedItem as IAnalysisNode;
            _objectFilter.SelectAllNamespaceClasses(selectedNamespaceNode);
            LoadClassNodes(selectedNamespaceNode);

            RefreshObjectNodes();
        }

        private void btnDeselectAllClasses_Click(object sender, EventArgs e)
        {
            var selectedNamespaceNode = clbNamespaceNodes.SelectedItem as IAnalysisNode;
            _objectFilter.DeselectAllNamespaceClasses(selectedNamespaceNode);
            LoadClassNodes(selectedNamespaceNode);

            RefreshObjectNodes();
        }

        private void btnSelectAllObjects_Click(object sender, EventArgs e)
        {
            var selectedClassNode = clbClassNodes.SelectedItem as IAnalysisNode;
            _objectFilter.SelectAllClassObjects(selectedClassNode);
            LoadObjectNodes(selectedClassNode);
        }

        private void btnDeselectAllObjects_Click(object sender, EventArgs e)
        {
            var selectedClassNode = clbClassNodes.SelectedItem as IAnalysisNode;
            _objectFilter.DeselectAllClassObjects(selectedClassNode);
            LoadObjectNodes(selectedClassNode);
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
