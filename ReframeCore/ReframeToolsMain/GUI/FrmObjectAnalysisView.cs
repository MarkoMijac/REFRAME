﻿using ReframeAnalyzer.GraphFactories;
using ReframeAnalyzer.Nodes;
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
using VisualizerDGML.Factories;
using VisualizerDGML.Utilities;

namespace ReframeTools.GUI
{
    public partial class FrmObjectAnalysisView : FrmAnalysisView
    {
        public FrmObjectAnalysisView(string reactorIdentifier) : base(reactorIdentifier)
        {
            InitializeComponent();
        }

        public override void ShowAnalysis(IEnumerable<IAnalysisNode> nodes)
        {
            base.ShowAnalysis(nodes);
            try
            {
                if (nodes != null)
                {
                    foreach (var node in nodes)
                    {
                        dgvNodes.Rows.Add(new string[]
                        {
                        node.Identifier.ToString(),
                        node.Name,
                        node.Parent.Name,
                        node.Parent.Parent.Name,
                        node.Parent.Parent2.Name,
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
                dgvNodes.Columns.Add("colClass", "Class");
                dgvNodes.Columns.Add("colNamespace", "Namespace");
                dgvNodes.Columns.Add("colAssembly", "Assembly");
                dgvNodes.Columns.Add("colDegree", "Degree");
                dgvNodes.Columns.Add("colInDegree", "In Degree");
                dgvNodes.Columns.Add("colOutDegree", "Out Degree");
            }
        }

        protected override AnalysisController CreateAnalysisController()
        {
            var factory = new ObjectAnalysisGraphFactory();
            return new AnalysisController(this, factory, new FrmObjectFilter());
        }

        protected override VisualizationController CreateVisualizationController()
        {
            var factory = new ObjectGraphFactoryDGML();
            var fileCreator = new DGMLFileCreator2();
            return new VisualizationController(ReactorIdentifier, factory, fileCreator);
        }
    }
}
