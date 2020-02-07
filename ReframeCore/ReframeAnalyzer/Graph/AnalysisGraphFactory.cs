namespace ReframeAnalyzer.Graph
{
    public enum AnalysisLevel { AssemblyLevel, NamespaceLevel, ClassLevel, ClassMemberLevel, ObjectLevel, ObjectMemberLevel, UpdateAnalysisLevel };

    public class AnalysisGraphFactory
    {
        public IAnalysisGraph CreateGraph(string xmlSource, AnalysisLevel analysisLevel)
        {
            IAnalysisGraph result;

            switch (analysisLevel)
            {
                case AnalysisLevel.ObjectMemberLevel:
                    {
                        result = new ObjectMemberAnalysisGraph(xmlSource);
                        break;
                    }
                case AnalysisLevel.ObjectLevel:
                    {
                        var objectMemberGraph = new ObjectMemberAnalysisGraph(xmlSource);
                        result = new ObjectAnalysisGraph(objectMemberGraph);
                        break;
                    }
                case AnalysisLevel.ClassMemberLevel:
                    {
                        var objectMemberGraph = new ObjectMemberAnalysisGraph(xmlSource);
                        result = new ClassMemberAnalysisGraph(objectMemberGraph);
                        break;
                    }
                case AnalysisLevel.ClassLevel:
                    {
                        var objectMemberGraph = new ObjectMemberAnalysisGraph(xmlSource);
                        var objectGraph = new ObjectAnalysisGraph(objectMemberGraph);
                        result = new ClassAnalysisGraph(objectGraph);
                        break;
                    }
                case AnalysisLevel.AssemblyLevel:
                    {
                        var objectMemberGraph = new ObjectMemberAnalysisGraph(xmlSource);
                        var objectGraph = new ObjectAnalysisGraph(objectMemberGraph);
                        var classAnalysisGraph = new ClassAnalysisGraph(objectGraph);
                        result = new AssemblyAnalysisGraph(classAnalysisGraph);
                        break;
                    }
                case AnalysisLevel.NamespaceLevel:
                    {
                        var objectMemberGraph = new ObjectMemberAnalysisGraph(xmlSource);
                        var objectGraph = new ObjectAnalysisGraph(objectMemberGraph);
                        var classAnalysisGraph = new ClassAnalysisGraph(objectGraph);
                        result = new NamespaceAnalysisGraph(classAnalysisGraph);
                        break;
                    }
                default:
                    result = null;
                    break;
            }

            return result;
        }

        public IAnalysisGraph CreateGraph(string xmlSource, IAnalysisGraph objectMemberGraph, AnalysisLevel analysisLevel)
        {
            IAnalysisGraph result;
            switch (analysisLevel)
            {
                case AnalysisLevel.UpdateAnalysisLevel:
                    result = new UpdateAnalysisGraph(xmlSource, objectMemberGraph as ObjectMemberAnalysisGraph);
                    break;
                default:
                    result = null;
                    break;
            }

            return result;
        }
    }
}
