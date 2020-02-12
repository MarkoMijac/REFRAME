using ReframeAnalyzer.Exceptions;
using System.Xml;
using System.Xml.Linq;

namespace ReframeAnalyzer.Graph
{
    public enum AnalysisLevel { AssemblyLevel, NamespaceLevel, ClassLevel, ClassMemberLevel, ObjectLevel, ObjectMemberLevel, UpdateAnalysisLevel };

    public class AnalysisGraphFactory
    {
        public IAnalysisGraph CreateGraph(string xmlSource, AnalysisLevel analysisLevel)
        {
            IAnalysisGraph result;

            ValidateXmlSource(xmlSource);

            switch (analysisLevel)
            {
                case AnalysisLevel.ObjectMemberLevel:
                    {
                        result = new ObjectMemberAnalysisGraph(xmlSource);
                        break;
                    }
                case AnalysisLevel.ObjectLevel:
                    {
                        var objectMemberGraph = CreateGraph(xmlSource, AnalysisLevel.ObjectMemberLevel);
                        result = new ObjectAnalysisGraph(objectMemberGraph);
                        break;
                    }
                case AnalysisLevel.ClassMemberLevel:
                    {
                        result = new ClassMemberAnalysisGraph(xmlSource);
                        break;
                    }
                case AnalysisLevel.ClassLevel:
                    {
                        var objectGraph = CreateGraph(xmlSource, AnalysisLevel.ObjectLevel);
                        result = new ClassAnalysisGraph(objectGraph);
                        break;
                    }
                case AnalysisLevel.AssemblyLevel:
                    {
                        var classAnalysisGraph = CreateGraph(xmlSource, AnalysisLevel.ClassLevel);
                        result = new AssemblyAnalysisGraph(classAnalysisGraph);
                        break;
                    }
                case AnalysisLevel.NamespaceLevel:
                    {
                        var classAnalysisGraph = CreateGraph(xmlSource, AnalysisLevel.ClassLevel);
                        result = new NamespaceAnalysisGraph(classAnalysisGraph);
                        break;
                    }
                default:
                    result = null;
                    break;
            }

            return result;
        }

        private void ValidateXmlSource(string xmlSource)
        {
            if (xmlSource == "")
            {
                throw new AnalysisException("XML source is empty!");
            }

            try
            {
                XElement.Parse(xmlSource);
            }
            catch (XmlException e)
            {
                throw new AnalysisException("XML Source is not valid! "+e.Message);
            }
        }

        public IUpdateGraph CreateGraph(string xmlSource, IAnalysisGraph objectMemberGraph, AnalysisLevel analysisLevel)
        {
            IUpdateGraph result;
            switch (analysisLevel)
            {
                case AnalysisLevel.UpdateAnalysisLevel:
                    result = new UpdateAnalysisGraph(xmlSource, objectMemberGraph);
                    break;
                default:
                    result = null;
                    break;
            }

            return result;
        }
    }
}
