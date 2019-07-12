using ReframeCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeAnalyzer
{
    public static class Analyzer
    {
        public static string GetRegisteredGraphs()
        {
            string graphs = "";

            List<IDependencyGraph> registeredGraphs = GraphFactory.GetRegisteredGraphs();
            foreach (var graph in registeredGraphs)
            {
                graphs += $"Identifier={graph.Identifier}; Status={graph.Status}; NodeCount={graph.Nodes.Count}"+Environment.NewLine;
            }

            return graphs;
        }
    }
}
