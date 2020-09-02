using Microsoft.VisualStudio.GraphModel;
using ReframeAnalyzer.Graph;
using ReframeVisualizer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeVisualizer.Graphs
{
    public interface IVisualGraph
    {
        VisualizationOptions Options { get; }
        string SerializeGraph();
    }
}
