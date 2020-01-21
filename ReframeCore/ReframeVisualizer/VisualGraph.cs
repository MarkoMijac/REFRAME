using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.GraphModel;
using System.Xml.Linq;
using ReframeAnalyzer.Graph;

namespace ReframeVisualizer
{
    public abstract class VisualGraph : IVisualGraph
    {
        protected Graph _dgmlGraph;
        protected IEnumerable<IAnalysisNode> _analysisNodes;

        public VisualGraph(IEnumerable<IAnalysisNode> analysisNodes)
        {
            _dgmlGraph = new Graph();
            _analysisNodes = analysisNodes;

            try
            {
                AddCustomProperties();
                AddNodesToGraph();
                AddDependenciesToGraph();
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        protected abstract void AddCustomProperties();
        protected abstract void AddNodesToGraph();
        protected abstract void AddDependenciesToGraph();

        public Graph GetDGML()
        {
            return _dgmlGraph;
        }
    }
}
