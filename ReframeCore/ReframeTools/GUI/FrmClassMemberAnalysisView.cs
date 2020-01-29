﻿using ReframeAnalyzer.Graph;
using ReframeTools.Controllers;
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
    public partial class FrmClassMemberAnalysisView : FrmAnalysisView
    {
        public FrmClassMemberAnalysisView(string reactorIdentifier) : base(reactorIdentifier)
        {
            InitializeComponent();
            AnalysisController = new ClassMemberAnalysisController(this, new FrmFilterClassMemberAnalysis());
        }

        public override void ShowAnalysis(IEnumerable<IAnalysisNode> nodes)
        {
            base.ShowAnalysis(nodes);
            try
            {
                if (nodes != null)
                {
                    foreach (ClassMemberAnalysisNode node in nodes)
                    {
                        dgvNodes.Rows.Add(new string[]
                        {
                        node.Identifier.ToString(),
                        node.Name,
                        node.NodeType,
                        node.OwnerClass.Identifier.ToString(),
                        node.OwnerClass.Name,
                        node.Degree.ToString(),
                        node.InDegree.ToString(),
                        node.OutDegree.ToString()
                        });
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        protected override void AddColumns()
        {
            if (dgvNodes.Columns.Count == 0)
            {
                dgvNodes.Columns.Add("colIdentifier", "Identifier");
                dgvNodes.Columns.Add("colName", "Name");
                dgvNodes.Columns.Add("colNodeType", "NodeType");
                dgvNodes.Columns.Add("colClassIdentifier", "ClassIdentifier");
                dgvNodes.Columns.Add("colClassName", "ClassName");
                dgvNodes.Columns.Add("colDegree", "Degree");
                dgvNodes.Columns.Add("colInDegree", "In Degree");
                dgvNodes.Columns.Add("colOutDegree", "Out Degree");
            }
        }
    }
}