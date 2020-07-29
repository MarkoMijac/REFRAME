using ReframeAnalyzer.Exceptions;
using System.Xml;
using System.Xml.Linq;

namespace ReframeAnalyzer.Graph
{
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
                        var factory = new ObjectMemberAnalysisGraphFactory();
                        result = factory.CreateGraph(xmlSource);
                        break;
                    }
                case AnalysisLevel.ObjectLevel:
                    {
                        var factory = new ObjectAnalysisGraphFactory();
                        result = factory.CreateGraph(xmlSource);
                        break;
                    }
                case AnalysisLevel.ClassMemberLevel:
                    {
                        var factory = new ClassMemberAnalysisGraphFactory();
                        result = factory.CreateGraph(xmlSource);
                        break;
                    }
                case AnalysisLevel.ClassLevel:
                    {
                        var factory = new ClassAnalysisGraphFactory();
                        result = factory.CreateGraph(xmlSource);
                        break;
                    }
                case AnalysisLevel.AssemblyLevel:
                    {
                        var factory = new AssemblyAnalysisGraphFactory();
                        result = factory.CreateGraph(xmlSource);
                        break;
                    }
                case AnalysisLevel.NamespaceLevel:
                    {
                        var factory = new NamespaceAnalysisGraphFactory();
                        result = factory.CreateGraph(xmlSource);
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

        public IAnalysisGraph CreateGraph(string xmlSource, string xmlUpdateInfo)
        {
            var factory = new ObjectMemberAnalysisGraphFactory();
            var objectMemberGraph = factory.CreateGraph(xmlSource);

            var updateGraphFactory = new UpdateAnalysisGraphFactory(objectMemberGraph);
            IUpdateGraph result = updateGraphFactory.CreateGraph(xmlUpdateInfo) as IUpdateGraph;
            return result;
        }
    }
}
