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
        protected IEnumerable<IAnalysisNode> _analysisNodes;

        public VisualizationOptions VisualizationOptions { get; protected set; } = new VisualizationOptions();

        public VisualGraph(IEnumerable<IAnalysisNode> analysisNodes)
        {
            _analysisNodes = analysisNodes;
            Initialize();
        }

        protected virtual void Initialize()
        {

        }

        protected abstract void AddCustomProperties(Graph dgmlGraph);
        protected abstract void AddNodesToGraph(Graph dgmlGraph);
        protected abstract void AddDependenciesToGraph(Graph dgmlGraph);

        public Graph GetDGML()
        {
            Graph dgmlGraph = new Graph();

            AddCustomProperties(dgmlGraph);
            AddNodesToGraph(dgmlGraph);
            AddDependenciesToGraph(dgmlGraph);

            return dgmlGraph;
        }
    }
}
