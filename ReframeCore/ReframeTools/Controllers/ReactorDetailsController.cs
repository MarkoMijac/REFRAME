using ReframeAnalyzer;
using ReframeAnalyzer.Graph;
using ReframeAnalyzer.GraphFactories;
using ReframeClient;
using ReframeTools.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeTools.Controllers
{
    public class ReactorDetailsController
    {
        private string _reactorIdentifier;
        private FrmReactorDetails _view;
        public ReactorDetailsController(FrmReactorDetails view)
        {
            _view = view;
            _reactorIdentifier = view.ReactorIdentifier;
        }

        public void ShowReactorDetails()
        {
            var pipeClient = new ReframePipeClient();
            string xmlSource = pipeClient.GetReactor(_reactorIdentifier);
            
            var objectMemberGraphFactory = new ObjectMemberAnalysisGraphFactory();
            var objectMemberGraph = objectMemberGraphFactory.CreateGraph(xmlSource);

            var objectGraphFactory = new ObjectAnalysisGraphFactory();
            var objectGraph = objectGraphFactory.CreateGraph(xmlSource);

            var classMemberGraphFactory = new ClassMemberAnalysisGraphFactory();
            var classMemberGraph = classMemberGraphFactory.CreateGraph(xmlSource);

            var classGraphFactory = new ClassAnalysisGraphFactory();
            var classGraph = classGraphFactory.CreateGraph(xmlSource);

            var namespaceGraphFactory = new NamespaceAnalysisGraphFactory();
            var namespaceGraph = namespaceGraphFactory.CreateGraph(xmlSource);

            var assemblyGraphFactory = new AssemblyAnalysisGraphFactory();
            var assemblyGraph = assemblyGraphFactory.CreateGraph(xmlSource);

            SetBasicInfo(objectMemberGraph);
            SetStatistics(objectMemberGraph);
            SetObjectGraphStatistics(objectGraph);
            SetClassMemberGraphStatistics(classMemberGraph);
            SetClassGraphStatistics(classGraph);
            SetNamespaceGraphStatistics(namespaceGraph);
            SetAssemblyGraphStatistics(assemblyGraph);

            _view.DisplayDetails();
        }

        private void SetAssemblyGraphStatistics(IAnalysisGraph assemblyGraph)
        {
            _view.AssembliesCount = GraphMetrics.GetNumberOfNodes(assemblyGraph).ToString();
        }

        private void SetNamespaceGraphStatistics(IAnalysisGraph namespaceGraph)
        {
            _view.NamespacesCount = GraphMetrics.GetNumberOfNodes(namespaceGraph).ToString();
        }

        private void SetClassGraphStatistics(IAnalysisGraph classGraph)
        {
            _view.ClassesCount = GraphMetrics.GetNumberOfNodes(classGraph).ToString();
        }

        private void SetClassMemberGraphStatistics(IAnalysisGraph classMemberGraph)
        {
            _view.MembersCount = GraphMetrics.GetNumberOfNodes(classMemberGraph).ToString();
        }

        private void SetObjectGraphStatistics(IAnalysisGraph objectGraph)
        {
            _view.ObjectsCount = GraphMetrics.GetNumberOfNodes(objectGraph).ToString();
        }

        private void SetStatistics(IAnalysisGraph objectMemberGraph)
        {
            int numberOfEdges = GraphMetrics.GetNumberOfEdges(objectMemberGraph);
            int maxNumberOfEdges = GraphMetrics.GetMaximumNumberOfEdges(objectMemberGraph);
            float graphDensity = GraphMetrics.GetGraphDensity(objectMemberGraph);

            _view.GraphNumOfDependencies = numberOfEdges.ToString();
            _view.GraphMaxNumberOfDependencies = maxNumberOfEdges.ToString();
            _view.GraphDensity = graphDensity.ToString("N4");
        }

        private void SetBasicInfo(IAnalysisGraph objectMemberGraph)
        {
            var analyzer = new Analyzer();

            _view.GraphIdentifier = objectMemberGraph.Identifier;
            _view.GraphTotalNodeCount = GraphMetrics.GetNumberOfNodes(objectMemberGraph).ToString();

            int sourceNodesCount = analyzer.GetSourceNodes(objectMemberGraph).Count();
            _view.GraphSourceNodesCount = sourceNodesCount.ToString();

            int sinkNodesCount = analyzer.GetSinkNodes(objectMemberGraph).Count();
            _view.GraphSinkNodesCount = sinkNodesCount.ToString();

            int intermediateNodesCount = analyzer.GetIntermediaryNodes(objectMemberGraph).Count();
            _view.GraphIntermediateNodesCount = intermediateNodesCount.ToString();

            int orphanNodesCount = analyzer.GetOrphanNodes(objectMemberGraph).Count();
            _view.GraphOrphanNodesCount = orphanNodesCount.ToString();
        }
    }
}
