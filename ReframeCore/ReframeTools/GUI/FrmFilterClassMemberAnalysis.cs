﻿using ReframeAnalyzer.Filters;
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
            var filterFactory = new AnalysisFilterFactory();
            Filter = filterFactory.CreateFilter(OriginalNodes, AnalysisLevel.ClassMemberLevel);
            rbClassMemberLevel.Checked = true;
        }

        protected override void HandleCheckListsVisibility()
        {
            base.HandleCheckListsVisibility();
            MakeAssemblyNodesVisible(true);
            MakeNamespaceNodesVisible(true);
            MakeClassNodesVisible(true);
            EnableClassNodes(false);
            MakeObjectNodesVisible(false);
            EnableObjectNodes(false);

            int offset = 130;
            AdjustFormSize(offset);
        }

        protected override void LoadNodes()
        {
            LoadAssemblyNodes();
            LoadNamespaceNodes();
        }

        protected override void clbNamespaceNodes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            base.clbNamespaceNodes_ItemCheck(sender, e);

            var namespaceNode = clbNamespaceNodes.SelectedItem as NamespaceAnalysisNode;
            LoadClassNodes(namespaceNode);
        }
    }
}
