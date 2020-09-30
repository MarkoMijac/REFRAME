using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ReframeAnalyzer.Graph;
using ReframeTools.Controllers;
using VisualizerDGML.Factories;
using VisualizerDGML.Utilities;

namespace ReframeTools.GUI
{
    public partial class FrmUpdateProcessInfo : Form
    {
        public string ReactorIdentifier { get; set; }
        private UpdateProcessAnalysisController _controller;

        public FrmUpdateProcessInfo(string reactorIdentifier)
        {
            InitializeComponent();
            ReactorIdentifier = reactorIdentifier;
            rbtnAllNodes.Checked = true;
            _controller = new UpdateProcessAnalysisController(this, new FrmUpdateFilter());
            AddColumns();
        }

        private void FrmUpdateProcessInfo_Load(object sender, EventArgs e)
        {
            _controller.ShowUpdateAnalysis();
        }

        private void AddColumns()
        {
            if (dgvUpdateInfo.Columns.Count == 0)
            {
                dgvUpdateInfo.Columns.Add("colIdentifier", "Identifier");
                dgvUpdateInfo.Columns.Add("colName", "Name");
                dgvUpdateInfo.Columns.Add("colNodeType", "Node Type");
                dgvUpdateInfo.Columns.Add("colCurrentValue", "Current Value");
                dgvUpdateInfo.Columns.Add("colPreviousValue", "Previous Value");
                dgvUpdateInfo.Columns.Add("colUpdateOrder", "Update Order");
                dgvUpdateInfo.Columns.Add("colUpdateLayer", "Update Layer");
                dgvUpdateInfo.Columns.Add("colStartedAt", "Started At");
                dgvUpdateInfo.Columns.Add("colCompletedAt", "Completed At");
                dgvUpdateInfo.Columns.Add("colDuration", "Duration");
                dgvUpdateInfo.Columns.Add("colDegree", "Degree");
                dgvUpdateInfo.Columns.Add("colInDegree", "In Degree");
                dgvUpdateInfo.Columns.Add("colOutDegree", "Out Degree");
            }
        }

        private void btnVisualize_Click(object sender, EventArgs e)
        {
            var factory = new UpdateGraphFactoryDGML();
            var fileCreator = new DGMLFileCreator(SolutionServices.Solution);
            VisualizationController visualizationController = new VisualizationController(ReactorIdentifier, factory,  fileCreator);
            visualizationController.Visualize(_controller.AnalysisNodes);
        }

        private void ShowOnlyRowsWithNoDifferences()
        {
            foreach (DataGridViewRow row in dgvUpdateInfo.Rows)
            {
                if (ValueChanged(row) == false)
                {
                    row.Visible = true;
                }
                else
                {
                    row.Visible = false;
                }
            }
        }

        private void ShowOnlyRowsWithDifferences()
        {
            foreach (DataGridViewRow row in dgvUpdateInfo.Rows)
            {
                if (ValueChanged(row) == true)
                {
                    row.Visible = true;
                }
                else
                {
                    row.Visible = false;
                }
            }
        }

        private void ShowAllRows()
        {
            foreach (DataGridViewRow row in dgvUpdateInfo.Rows)
            {
                row.Visible = true;
            }
        }

        private bool ValueChanged(DataGridViewRow row)
        {
            bool changed = true;

            if (row.Cells["colCurrentValue"].Value.ToString() == row.Cells["colPreviousValue"].Value.ToString())
            {
                changed = false;
            }

            return changed;
        }

        private void rbtnAllNodes_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnAllNodes.Checked == true)
            {
                ShowAllRows();
            }
        }

        private void rbtnOnlyNodesWithDifferences_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnOnlyNodesWithDifferences.Checked == true)
            {
                ShowOnlyRowsWithDifferences();
            }
        }

        private void rbtnOnlyNodesWithNoDifferences_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnOnlyNodesWithNoDifferences.Checked == true)
            {
                ShowOnlyRowsWithNoDifferences();
            }
        }
    }
}
