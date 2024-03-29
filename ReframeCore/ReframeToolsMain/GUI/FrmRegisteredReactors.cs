﻿using ReframeClient;
using ReframeTools.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ReframeTools.GUI
{
    public partial class FrmRegisteredReactors : Form
    {
        private Form _currentForm;
        private RegisteredReactorsController _registeredReactorsController;

        public FrmRegisteredReactors()
        {
            InitializeComponent();
            _registeredReactorsController = new RegisteredReactorsController(this);
        }

        private void FrmRegisteredReactors_Load(object sender, EventArgs e)
        {
            _registeredReactorsController.ShowRegisteredReactors();
        }

        public string GetSelectedReactorIdentifier()
        {
            if (dgvReactors.RowCount > 0)
            {
                return dgvReactors.CurrentRow.Cells[0].Value.ToString();
            }
            return "";
        }

        private void objectMemberlevelAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FrmObjectMemberAnalysisView(GetSelectedReactorIdentifier());
            DisplayForm(form);
        }

        private void objectlevelAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FrmObjectAnalysisView(GetSelectedReactorIdentifier());
            DisplayForm(form);
        }

        private void classMemberlevelAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FrmClassMemberAnalysisView(GetSelectedReactorIdentifier());
            DisplayForm(form);
        }

        private void classlevelAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FrmClassAnalysisView(GetSelectedReactorIdentifier());
            DisplayForm(form);
        }

        private void assemblylevelAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FrmAssemblyAnalysisView(GetSelectedReactorIdentifier());
            DisplayForm(form);
        }

        private void namespacelevelAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FrmNamespaceAnalysisView(GetSelectedReactorIdentifier());
            DisplayForm(form);
        }

        private void DisplayForm(Form form)
        {
            if (form != null)
            {
                if (form == _currentForm)
                {
                    form.Show();
                }
                else
                {
                    if (_currentForm != null)
                    {
                        _currentForm.Close();
                    }
                    _currentForm = form;
                    form.Show();
                }
            }
        }

        private void getLatestUpdateInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FrmUpdateProcessInfo(GetSelectedReactorIdentifier());
            DisplayForm(form);
        }

        private void btnShowDetails_Click(object sender, EventArgs e)
        {
            string reactorIdentifier = GetSelectedReactorIdentifier();
            if (reactorIdentifier != "")
            {
                var form = new FrmReactorDetails(reactorIdentifier);
                DisplayForm(form);
            }
        }

        private void btnFetchReactors_Click(object sender, EventArgs e)
        {
            _registeredReactorsController.ShowRegisteredReactors();
        }
    }
}
