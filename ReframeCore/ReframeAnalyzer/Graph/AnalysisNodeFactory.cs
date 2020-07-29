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
                    {
                        var factory = new AssemblyAnalysisNodeFactory();
                        analysisNode = factory.CreateNode(xNode);
                        break;
                    }
                case AnalysisLevel.NamespaceLevel:
                    {
                        var factory = new NamespaceAnalysisNodeFactory();
                        analysisNode = factory.CreateNode(xNode);
                        break;
                    }
                case AnalysisLevel.ClassLevel:
                    {
                        var factory = new ClassAnalysisNodeFactory();
                        analysisNode = factory.CreateNode(xNode);
                        break;
                    }
                case AnalysisLevel.ClassMemberLevel:
                    {
                        var factory = new ClassMemberAnalysisNodeFactory();
                        analysisNode = factory.CreateNode(xNode);
                        break;
                    }
                case AnalysisLevel.ObjectLevel:
                    {
                        var factory = new ObjectAnalysisNodeFactory();
                        analysisNode = factory.CreateNode(xNode);
                        break;
                    }
                case AnalysisLevel.ObjectMemberLevel:
                    {
                        var factory = new ObjectMemberAnalysisNodeFactory();
                        analysisNode = factory.CreateNode(xNode);
                        break;
                    }
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
