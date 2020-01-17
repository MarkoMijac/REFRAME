using Microsoft.VisualStudio.GraphModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeVisualizer
{
    public class Visualizer
    {
        private IVisualGraph _visualgraph;
        public Visualizer(IVisualGraph visualGraph)
        {
            _visualgraph = visualGraph;
        }

        public Graph GenerateDGMLGraph()
        {
            Graph graph = _visualgraph.GetDGML();
            PaintGraph(_visualgraph);

            return graph;
        }

        private void PaintGraph(IVisualGraph graph)
        {

        }
    }
}
