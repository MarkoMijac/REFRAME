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
            FillListBoxes(clbAssemblyNodes, _classMemberFilter.GetAvailableAssemblyNodes());
        }

        private void LoadNamespaceNodes()
        {
            FillListBoxes(clbNamespaceNodes, _classMemberFilter.GetAvailableNamespaceNodes());
        }

        private void LoadClassNodes(IAnalysisNode namespaceNode)
        {
            List<IAnalysisNode> classNodes = new List<IAnalysisNode>();
            if (namespaceNode != null)
            {
                classNodes = _classMemberFilter.GetAvailableClassNodes(namespaceNode);
            }

            FillListBoxes(clbClassNodes, classNodes);
        }

        private void FrmOptions_Load(object sender, EventArgs e)
        {
            Initialize();
            LoadNodes();
        }

        private void btnSelecteAllAssemblies_Click(object sender, EventArgs e)
        {
            _classMemberFilter.SelectAllAssemblyNodes();
            LoadAssemblyNodes();
        }

        private void btnDeselectAllAssemblies_Click(object sender, EventArgs e)
        {
            _classMemberFilter.DeselectAllAssemblyNodes();
            LoadAssemblyNodes();
        }

        private void btnSelectAllNamespaces_Click(object sender, EventArgs e)
        {
            _classMemberFilter.SelectAllNamespaceNodes();
            LoadNamespaceNodes();

            RefreshClassNodes();
        }

        private void btnDeselectAllNamespaces_Click(object sender, EventArgs e)
        {
            _classMemberFilter.DeselectAllNamespaceNodes();
            LoadNamespaceNodes();

            RefreshClassNodes();
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
            _classMemberFilter.SelectAllNamespaceClasses(selectedNamespaceNode);
            LoadClassNodes(selectedNamespaceNode);
        }

        private void btnDeselectAllClasses_Click(object sender, EventArgs e)
        {
            var selectedNamespaceNode = clbNamespaceNodes.SelectedItem as IAnalysisNode;
            _classMemberFilter.DeselectAllNamespaceClasses(selectedNamespaceNode);
            LoadClassNodes(selectedNamespaceNode);;
        }

        private void clbAssemblyNodes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            CheckListBoxItem(clbAssemblyNodes, e);
        }

        private void clbNamespaceNodes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            CheckListBoxItem(clbNamespaceNodes, e);
            RefreshClassNodes();
        }

        private void clbClassNodes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            CheckListBoxItem(clbClassNodes, e);
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
