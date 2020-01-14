using IPCClient;
using Microsoft.VisualStudio.GraphModel;
using ReframeTools.Helpers;
using ReframeVisualizer;
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

namespace ReframeTools
{
    public partial class FrmVisualizer : Form
    {
        public FrmVisualizer()
        {
            InitializeComponent();
        }

        private void btnDisplayGraph_Click(object sender, EventArgs e)
        {
            string identifier = dgvRegisteredGraphs.CurrentRow.Cells[0].Value.ToString();

            string xml = ClientQueries.GetGraphNodes(identifier);
            Graph dgmlGraph = Visualizer.GenerateDGMLGraph(xml);
            SolutionServices.CreateNewDgmlFile(identifier, dgmlGraph);
        }

        private void btnClassGraph_Click(object sender, EventArgs e)
        {
            string identifier = dgvRegisteredGraphs.CurrentRow.Cells[0].Value.ToString();

            string analysisGraphXML = ClientQueries.GetClassAnalysisGraph(identifier);
            ClassVisualGraph visualGraph = new ClassVisualGraph(analysisGraphXML);
            Graph dgmlGraph = Visualizer.GenerateDGMLGraph(visualGraph);
            SolutionServices.CreateNewDgmlFile(identifier, dgmlGraph);
        }

        private void btnGetRegisteredGraphs_Click(object sender, EventArgs e)
        {
            ShowRegisteredGraphs();
        }

        private void btnGetGraphNodes_Click(object sender, EventArgs e)
        {

        }

        private void FrmVisualizer_Load(object sender, EventArgs e)
        {

        }

        private void ShowRegisteredGraphs()
        {
            dgvRegisteredGraphs.DataSource = null;

            string xml = ClientQueries.GetRegisteredGraphs();

            XElement doc = XElement.Parse(xml);

            IEnumerable<XElement> graphs = from g in doc.Descendants("Graph") select g;

            List<object> list = new List<object>();

            foreach (var graph in graphs)
            {
                var g = new
                {
                    Identifier = graph.Element("Identifier").Value,
                    NodeCount = graph.Element("NodeCount").Value
                };

                list.Add(g);
            }

            dgvRegisteredGraphs.DataSource = list;
        }

        private void btnDisplayReactors_Click(object sender, EventArgs e)
        {
            ShowRegisteredReactors();
        }

        private void ShowRegisteredReactors()
        {
            dgvRegisteredGraphs.DataSource = null;

            string xml = ClientQueries.GetRegisteredReactors();

            XElement doc = XElement.Parse(xml);

            IEnumerable<XElement> reactors = from r in doc.Descendants("Reactor") select r;

            List<object> list = new List<object>();

            foreach (var reactor in reactors)
            {
                var g = new
                {
                    Identifier = reactor.Element("Identifier").Value,
                    GraphIdentifier = reactor.Element("GraphIdentifier").Value,
                    GraphNodeCount = reactor.Element("GraphNodeCount").Value
                };

                list.Add(g);
            }

            dgvRegisteredGraphs.DataSource = list;
        }
    }
}
