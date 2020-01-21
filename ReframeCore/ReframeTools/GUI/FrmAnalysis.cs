using IPCClient;
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
using System.Xml.Linq;

namespace ReframeTools.GUI
{
    public partial class FrmAnalysis : Form
    {
        public string ReactorIdentifier { get; set; }

        public FrmAnalysis()
        {
            InitializeComponent();
        }

        public FrmAnalysis(string reactorIdentifier)
            :this()
        {
            ReactorIdentifier = reactorIdentifier;
            SetFormTitle();
        }

        protected void FrmClassAnalysis_Load(object sender, EventArgs e)
        {
            
        }

        protected virtual void SetFormTitle()
        {
            Text = $"Graph analysis for reactor [{ReactorIdentifier}]";
            lblAnalysisDescription.Text = $"Graph analysis for reactor [{ReactorIdentifier}]";
        }

        public virtual void ShowXMLSource(string xmlSource)
        {
            rtxtXMLSource.Text = xmlSource;
        }

        public virtual void ShowTable(string xmlSource)
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

        public virtual void ShowTable(IEnumerable<IAnalysisNode> nodes)
        {
            dgvAnalysis.Rows.Clear();

            try
            {
                if (nodes != null)
                {
                    foreach (var node in nodes)
                    {
                        dgvAnalysis.Rows.Add(new string[]
                        {
                        node.Identifier.ToString(),
                        node.Name,
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

        public virtual void ShowDescription(string description)
        {
            lblAnalysisDescription.Text = description;
        }

        public string GetSelectedNodeIdentifier()
        {
            string identifier = "";

            if (dgvAnalysis.Rows.Count > 0)
            {
                identifier = dgvAnalysis.CurrentRow.Cells[0].Value.ToString();
            }

            return identifier;
        }
    }
}
