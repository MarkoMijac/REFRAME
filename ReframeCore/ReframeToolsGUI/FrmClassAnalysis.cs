using IPCClient;
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

namespace ReframeToolsGUI
{
    public partial class FrmClassAnalysis : Form
    {
        private string _reactorIdentifier;
        public FrmClassAnalysis(string reactorIdentifier)
        {
            InitializeComponent();
            _reactorIdentifier = reactorIdentifier;
            SetFormTitle();
        }

        private void FrmClassAnalysis_Load(object sender, EventArgs e)
        {
            string xmlSource = ClientQueries.GetClassAnalysisGraph(_reactorIdentifier);
            ShowXMLSource(xmlSource);
            ShowAnalysis(xmlSource);
        }

        private void SetFormTitle()
        {
            Text = $"Class-level analysis for Reactor [{_reactorIdentifier}]";
        }

        private void ShowXMLSource(string xmlSource)
        {
            rtxtXMLSource.Text = xmlSource;
        }

        private void ShowAnalysis(string xmlSource)
        {
            dgvAnalysis.Rows.Clear();

            try
            {
                XElement document = XElement.Parse(xmlSource);
                IEnumerable<XElement> nodes = from n in document.Descendants("Node") select n;

                foreach (var node in nodes)
                {
                    string identifier = node.Element("Identifier").Value;
                    string name = node.Element("Name").Value;
                    string namespc = node.Element("Namespace").Value;
                    string assembly = node.Element("Assembly").Value;
                    string degree = node.Element("Degree").Value;
                    string inDegree = node.Element("InDegree").Value;
                    string outDegree = node.Element("OutDegree").Value;
                    string predecessorsCount = node.Element("PredecessorsCount").Value;
                    string successorsCount = node.Element("SuccessorsCount").Value;

                    dgvAnalysis.Rows.Add(new string[] 
                    {
                        identifier,
                        name,
                        namespc,
                        assembly,
                        degree,
                        inDegree,
                        outDegree
                    });
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
