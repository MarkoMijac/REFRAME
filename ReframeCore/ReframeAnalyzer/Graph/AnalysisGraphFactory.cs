using ReframeAnalyzer.Exceptions;
using ReframeCore;
using ReframeExporter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeAnalyzer.Graph
{
    public enum AnalysisLevel { AssemblyLevel, ClassLevel };

    public class AnalysisGraphFactory
    {
        public IAnalysisGraph GetGraph(IReactor reactor, AnalysisLevel level)
        {
            ValidateGraph(reactor);

            IAnalysisGraph analysisGraph;
            switch (level)
            {
                case AnalysisLevel.ClassLevel:
                    {
                        var xmlExporter = new XmlExporter();
                        string xmlSource = xmlExporter.Export(reactor);
                        analysisGraph = new ClassAnalysisGraph(xmlSource);
                        break;
                    }
                case AnalysisLevel.AssemblyLevel:
                    {
                        analysisGraph = null;
                        break;
                    }
                default:
                    analysisGraph = null;
                    break;
            }

            return analysisGraph;
        }

        public IAnalysisGraph CreateGraph(string xmlSource, AnalysisLevel analysisLevel)
        {
            IAnalysisGraph analysisGraph;

            switch (analysisLevel)
            {
                case AnalysisLevel.ClassLevel:
                    {
                        analysisGraph = new ClassAnalysisGraph(xmlSource);
                        break;
                    }
                default:
                    analysisGraph = null;
                    break;
            }

            return analysisGraph;
        }

        private void ValidateGraph(IReactor reactor)
        {
            if (reactor == null)
            {
                throw new AnalyzerException("Provided dependency graph is null!");
            }
        }
    }
}
