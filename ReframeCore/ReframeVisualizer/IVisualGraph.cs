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
        VisualizationOptions Options { get; }
        string SerializeGraph();
        string ReactorIdentifier { get; }
    }
}
