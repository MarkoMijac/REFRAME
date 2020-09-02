using Microsoft.VisualStudio.GraphModel;
using ReframeAnalyzer.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeVisualizer
{
    public interface IVisualGraph
    {
        Graph GetGraph();
        VisualizationOptions Options { get; }
        string SerializeGraph();
    }
}
