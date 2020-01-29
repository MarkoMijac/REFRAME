using ReframeAnalyzer.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeAnalyzer.Filters
{
    public interface IAnalysisFilter
    {
        IEnumerable<IAnalysisNode> Apply();
        bool IsSelected(IAnalysisNode node);
        void SelectNode(IAnalysisNode node);
        void DeselectNode(IAnalysisNode node);
        List<IAnalysisNode> GetAvailableAssemblyNodes();
        List<IAnalysisNode> GetAvailableNamespaceNodes();
        List<IAnalysisNode> GetAvailableClassNodes();
        List<IAnalysisNode> GetAvailableClassNodes(NamespaceAnalysisNode namespaceNode);
        void SelectAllClassNodes(NamespaceAnalysisNode namespaceNode);
        void DeselectAllClassNodes(NamespaceAnalysisNode namespaceNode);
        void SelectAllNamespaceNodes();
        void DeselectAllNamespaceNodes();
        void SelectAllAssemblyNodes();
        void DeselectAllAssemblyNodes();
        
    }
}
