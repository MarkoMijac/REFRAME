using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.GraphModel;
using Microsoft.VisualStudio.GraphModel.Styles;

namespace VisualizerDGML.Utilities
{
    public class GraphPainter
    {
        public void Paint(Graph graph, GraphNode node, string colorCode)
        {
            GraphProperty background = GetOrCreateBackgroundProperty(graph);
            node.SetValue(background, colorCode);
        }

        private GraphProperty GetOrCreateBackgroundProperty(Graph graph)
        {
            GraphProperty background = graph.DocumentSchema.FindProperty("Background");
            if (background == null)
            {
                background = graph.DocumentSchema.Properties.AddNewProperty("Background", Type.GetType("System.String"));
            }

            return background;
        }
    }
}
