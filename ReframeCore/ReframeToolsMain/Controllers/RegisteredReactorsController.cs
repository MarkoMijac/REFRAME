using ReframeBaseExceptions;
using ReframeClient;
using ReframeTools.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ReframeTools.Controllers
{
    public class RegisteredReactorsController
    {
        private FrmRegisteredReactors _view;

        public RegisteredReactorsController(FrmRegisteredReactors view)
        {
            _view = view;
        }

        public void ShowRegisteredReactors()
        {
            try
            {
                _view.dgvReactors.DataSource = null;
                string xmlReactors = FetchXmlSource();
                List<object> reactors = ParseReactors(xmlReactors);
                _view.dgvReactors.DataSource = reactors;
            }
            catch (ReframeException re)
            {
                System.Windows.Forms.MessageBox.Show(re.Message);
            }
        }

        private string FetchXmlSource()
        {
            try
            {
                var pipeClient = new ReframePipeClient();
                string xmlReactors = pipeClient.GetRegisteredReactors();
                return xmlReactors;
            }
            catch (Exception)
            {
                throw new ReframeException("Unable to fetch registered reactors from a server!");
            }
        }

        private List<object> ParseReactors(string xmlReactors)
        {
            try
            {
                List<object> list = new List<object>();

                XElement document = XElement.Parse(xmlReactors);
                IEnumerable<XElement> reactors = from r in document.Descendants("Reactor") select r;

                foreach (var reactor in reactors)
                {
                    string reactorIdentifier = reactor.Element("Identifier").Value;
                    XElement xeGraph = reactor.Element("Graph");
                    string graphIdentifier = xeGraph.Element("Identifier").Value;
                    string nodeCount = xeGraph.Element("TotalNodeCount").Value;
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
            catch (Exception)
            {
                throw new ReframeException("Unable to parse registered reactors!");
            }
        }
    }
}
