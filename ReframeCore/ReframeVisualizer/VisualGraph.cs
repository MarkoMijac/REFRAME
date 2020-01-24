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

        public VisualizationOptions VisualizationOptions { get; protected set; } = new VisualizationOptions();

        public VisualGraph(IEnumerable<IAnalysisNode> analysisNodes)
        {
            _analysisNodes = analysisNodes;
            Initialize();
            
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

        protected virtual void Initialize()
        {
            _dgmlGraph = new Graph();
            
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
