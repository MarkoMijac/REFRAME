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
    public partial class FrmAssemblyFilter : FrmAnalysisFilter
    {
        private AssemblyAnalysisFilter _assemblyFilter;
        public FrmAssemblyFilter()
        {
            InitializeComponent();
        }

        private void FrmOptions_Load(object sender, EventArgs e)
        {
            Initialize();
            LoadNodes();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Initialize()
        {
            Filter = new AssemblyAnalysisFilter(OriginalNodes);
            _assemblyFilter = Filter as AssemblyAnalysisFilter;
        }

        private void LoadNodes()
        {
            LoadAssemblyNodes();
        }

        private void LoadAssemblyNodes()
        {
            FillListBoxes(clbAssemblyNodes, _assemblyFilter.AssemblyFilterOption);
        }

        private void btnSelecteAllAssemblies_Click(object sender, EventArgs e)
        {
            _assemblyFilter.AssemblyFilterOption.SelectNodes();
            LoadAssemblyNodes();
        }

        private void btnDeselectAllAssemblies_Click(object sender, EventArgs e)
        {
            _assemblyFilter.AssemblyFilterOption.DeselectNodes();
            LoadAssemblyNodes();
        }

        private void clbAssemblyNodes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            CheckListBoxItem(clbAssemblyNodes, _assemblyFilter.AssemblyFilterOption, e);
        }
    }
}
