using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ReframeAnalyzer.Graph
{
    public class AnalysisNodeFactory
    {
        public IAnalysisNode CreateNode(XElement xNode, AnalysisLevel analysisLevel)
        {
            IAnalysisNode analysisNode;

            switch (analysisLevel)
            {
                case AnalysisLevel.AssemblyLevel:
                    analysisNode = new AssemblyAnalysisNode(xNode);
                    break;
                case AnalysisLevel.NamespaceLevel:
                    analysisNode = new NamespaceAnalysisNode(xNode);
                    break;
                case AnalysisLevel.ClassLevel:
                    analysisNode = new ClassAnalysisNode(xNode);
                    break;
                case AnalysisLevel.ClassMemberLevel:
                    analysisNode = new ClassMemberAnalysisNode(xNode);
                    break;
                case AnalysisLevel.ObjectLevel:
                    analysisNode = new ObjectAnalysisNode(xNode);
                    break;
                case AnalysisLevel.ObjectMemberLevel:
                    analysisNode = new ObjectMemberAnalysisNode(xNode);
                    break;
                case AnalysisLevel.UpdateAnalysisLevel:
                    analysisNode = null;
                    break;
                default:
                    analysisNode = null;
                    break;
            }

            return analysisNode;
        }
    }
}
