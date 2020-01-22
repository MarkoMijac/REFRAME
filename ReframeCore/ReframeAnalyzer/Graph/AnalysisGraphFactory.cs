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
    public enum AnalysisLevel { AssemblyLevel, NamespaceLevel, ClassLevel };

    public class AnalysisGraphFactory
    {
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
                case AnalysisLevel.AssemblyLevel:
                    {
                        var classAnalysisGraph = new ClassAnalysisGraph(xmlSource);
                        analysisGraph = new AssemblyAnalysisGraph(classAnalysisGraph);
                        break;
                    }
                case AnalysisLevel.NamespaceLevel:
                    {
                        var classAnalysisGraph = new ClassAnalysisGraph(xmlSource);
                        analysisGraph = new NamespaceAnalysisGraph(classAnalysisGraph);
                        break;
                    }
                default:
                    analysisGraph = null;
                    break;
            }

            return analysisGraph;
        }
    }
}
