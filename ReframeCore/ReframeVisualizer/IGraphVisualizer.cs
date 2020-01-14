using Microsoft.VisualStudio.GraphModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeVisualizer
{
    interface IGraphVisualizer
    {
        Graph GenerateDGMLGraph(string xmlData);
    }
}
