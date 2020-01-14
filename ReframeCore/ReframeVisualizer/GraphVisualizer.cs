using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.GraphModel;
using System.Xml.Linq;

namespace ReframeVisualizer
{
    public abstract class GraphVisualizer : IGraphVisualizer
    {
        private IGraphPainter _graphPainter;

        public Graph GenerateDGMLGraph(string xmlData)
        {
            Graph dgmlGraph = new Graph();

            XElement document = XElement.Parse(xmlData);
            IEnumerable<XElement> nodes = FetchNodes(document);

            AddCustomProperties(dgmlGraph);
            AddNodesToGraph(dgmlGraph);
            AddDependenciesToGraph(dgmlGraph, nodes);

            if (_graphPainter != null)
            {
                _graphPainter.Paint(dgmlGraph);
            }

            return dgmlGraph;
        }

        public GraphVisualizer()
        {
            _graphPainter = null;
        }

        public GraphVisualizer(IGraphPainter graphPainter)
        {
            _graphPainter = graphPainter;
        }

        protected abstract IEnumerable<XElement> FetchNodes(XElement document);
        protected abstract void AddCustomProperties(Graph graph);
        protected abstract void AddNodesToGraph(Graph graph);
        protected abstract void AddDependenciesToGraph(Graph graph, IEnumerable<XElement> nodes);
        

    }
}
