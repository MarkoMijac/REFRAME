﻿using IPCClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ReframeTools.GUI
{
    public partial class FrmRegisteredReactors : Form
    {
        private FrmAnalysisView _currentForm;

        public FrmRegisteredReactors()
        {
            InitializeComponent();
        }

        private void DisplayReactors()
        {
            dgvReactors.DataSource = null;
            try
            {
                string xmlReactors = ClientQueries.GetRegisteredReactors();
                dgvReactors.DataSource = ParseReactors(xmlReactors);
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to fetch reactors!");
            }

        }

        private List<object> ParseReactors(string xmlReactors)
        {
            List<object> list = new List<object>();

            XElement document = XElement.Parse(xmlReactors);
            IEnumerable<XElement> reactors = from r in document.Descendants("Reactor") select r;

            foreach (var reactor in reactors)
            {
                string reactorIdentifier = reactor.Element("Identifier").Value;
                XElement xeGraph = reactor.Element("Graph");
                string graphIdentifier = xeGraph.Element("Identifier").Value;
                string nodeCount = xeGraph.Element("NodeCount").Value;
                var g = new
                {
                    Identifier = reactorIdentifier,
                    GraphIdentifier = graphIdentifier,
                    GraphNodeCount = nodeCount
                };

                list.Add(g);
            }

            return list;
        }

        private void FrmRegisteredReactors_Load(object sender, EventArgs e)
        {
            DisplayReactors();
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

        private void DisplayForm(FrmAnalysisView form)
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
    }
}