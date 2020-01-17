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

namespace ReframeTools.GUI
{
    public partial class FrmReactors : Form
    {
        public FrmReactors()
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
                var g = new
                {
                    Identifier = reactor.Element("Identifier").Value,
                    GraphIdentifier = reactor.Element("GraphIdentifier").Value,
                    GraphNodeCount = reactor.Element("GraphNodeCount").Value
                };

                list.Add(g);
            }

            return list;
        }

        private void FrmReactors_Load(object sender, EventArgs e)
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
    }
}
