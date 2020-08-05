﻿using ReframeAnalyzer;
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

        protected override void Initialize()
        {
            Level = AnalysisLevel.AssemblyLevel;
            var filterFactory = new AnalysisFilterFactory();
            Filter = filterFactory.CreateFilter(OriginalNodes, AnalysisLevel.AssemblyLevel);
        }

        protected override void LoadNodes()
        {
            LoadAssemblyNodes();
        }

        protected void LoadAssemblyNodes()
        {
            FillListBoxes(clbAssemblyNodes, Filter.GetAvailableAssemblyNodes());
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

        protected virtual void clbAssemblyNodes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            CheckListBoxItem(clbAssemblyNodes, e);
        }
    }
}