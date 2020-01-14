using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.GraphModel;
using System.Xml.Linq;

namespace ReframeVisualizer
{
    public abstract class VisualGraph : IVisualGraph
    {
        private string _xmlSource;

        public VisualGraph(string xml)
        {
            _xmlSource = xml;
        }

        public Graph GetDGML()
        {
            Graph dgmlGraph = new Graph();

            try
            {
                XElement document = XElement.Parse(_xmlSource);
                IEnumerable<XElement> nodes = FetchNodes(document);
                AddCustomProperties(dgmlGraph);
                AddNodesToGraph(dgmlGraph, nodes);
                AddDependenciesToGraph(dgmlGraph, nodes);
            }
            catch (Exception)
            {
                throw;
            }

            return dgmlGraph;
        }

        protected abstract IEnumerable<XElement> FetchNodes(XElement document);

        protected abstract void AddCustomProperties(Graph graph);
        protected abstract void AddNodesToGraph(Graph graph, IEnumerable<XElement> nodes);
        protected abstract void AddDependenciesToGraph(Graph graph, IEnumerable<XElement> nodes);
    }
}
