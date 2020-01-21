﻿using ReframeTools.Controllers;
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
    public partial class FrmMain : Form
    {
        FrmReactors formReactors;

        Form currentForm = null;

        public FrmMain()
        {
            InitializeComponent();
        }

        private void displayReactorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            formReactors = new FrmReactors();
            DisplayForm(formReactors);
        }

        private void classlevelAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowClassLevelAnalysisForm();
        }

        private void ShowClassLevelAnalysisForm()
        {
            string reactorIdentifier = formReactors.GetSelectedReactorIdentifier();

            FrmClassLevelAnalysis form = new FrmClassLevelAnalysis(reactorIdentifier);
            DisplayForm(form);
        }

        private void showSourceNodesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var controller = new ClassAnalysisController(currentForm as FrmAnalysis);
            controller.ShowSourceNodes();
        }

        private void DisplayForm(Form form)
        {
            form.MdiParent = this;
            currentForm = form;
            ManageMenuVisibility();
            form.Show();
        }

        private void ManageMenuVisibility()
        {
            analysesToolStripMenuItem.Visible = currentForm is FrmAnalysis;
        }

        private void showEntireGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var controller = new ClassAnalysisController(currentForm as FrmAnalysis);
            controller.ShowEntireGraph();
        }

        private void showSinkNodesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var controller = new ClassAnalysisController(currentForm as FrmAnalysis);
            controller.ShowSinkNodes();
        }

        private void showLeafNodesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var controller = new ClassAnalysisController(currentForm as FrmAnalysis);
            controller.ShowLeafNodes();
        }

        private void showOrphanNodesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var controller = new ClassAnalysisController(currentForm as FrmAnalysis);
            controller.ShowOrphanNodes();
        }

        private void showIntermediaryNodesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var controller = new ClassAnalysisController(currentForm as FrmAnalysis);
            controller.ShowIntermediaryNodes();
        }

        private void showEntireGraphToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (currentForm is FrmAnalysis)
            {
                var controller = new ClassVisualizationController(currentForm as FrmAnalysis);
                controller.ShowEntireGraph();
            }
        }

        private void showPredecessorsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (currentForm is FrmAnalysis)
            {
                FrmAnalysis classAnalysisForm = currentForm as FrmAnalysis;
                ClassVisualizationController controller = new ClassVisualizationController(classAnalysisForm);
                controller.ShowPredecessorNodes();
            }
        }

        private void showPredecessorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var controller = new ClassAnalysisController(currentForm as FrmAnalysis);
            controller.ShowPredecessorNodes();
        }

        private void showSuccessorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var controller = new ClassAnalysisController(currentForm as FrmAnalysis);
            controller.ShowSuccessorNodes();
        }

        private void showNeighboursToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var controller = new ClassAnalysisController(currentForm as FrmAnalysis);
            controller.ShowNeighbourNodes();
        }

        private void showSuccessorsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (currentForm is FrmAnalysis)
            {
                FrmAnalysis classAnalysisForm = currentForm as FrmAnalysis;
                ClassVisualizationController controller = new ClassVisualizationController(classAnalysisForm);
                controller.ShowSuccessorNodes();
            }
        }

        private void showNeighboursToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (currentForm is FrmAnalysis)
            {
                FrmAnalysis classAnalysisForm = currentForm as FrmAnalysis;
                ClassVisualizationController controller = new ClassVisualizationController(classAnalysisForm);
                controller.ShowNeighbourNodes();
            }
        }

        private void displaySourceNodesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentForm is FrmAnalysis)
            {
                FrmAnalysis classAnalysisForm = currentForm as FrmAnalysis;
                ClassVisualizationController controller = new ClassVisualizationController(classAnalysisForm);
                controller.ShowSourceNodes();
            }
        }

        private void displaySinkNodesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentForm is FrmAnalysis)
            {
                FrmAnalysis classAnalysisForm = currentForm as FrmAnalysis;
                ClassVisualizationController controller = new ClassVisualizationController(classAnalysisForm);
                controller.ShowSinkNodes();
            }
        }

        private void displayLeafNodesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentForm is FrmAnalysis)
            {
                FrmAnalysis classAnalysisForm = currentForm as FrmAnalysis;
                ClassVisualizationController controller = new ClassVisualizationController(classAnalysisForm);
                controller.ShowLeafNodes();
            }
        }

        private void displayIntermediaryNodesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentForm is FrmAnalysis)
            {
                FrmAnalysis classAnalysisForm = currentForm as FrmAnalysis;
                ClassVisualizationController controller = new ClassVisualizationController(classAnalysisForm);
                controller.ShowIntermediaryNodes();
            }
        }

        private void displayOrphanNodesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentForm is FrmAnalysis)
            {
                FrmAnalysis classAnalysisForm = currentForm as FrmAnalysis;
                ClassVisualizationController controller = new ClassVisualizationController(classAnalysisForm);
                controller.ShowOrphanNodes();
            }
        }
    }
}
