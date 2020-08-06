using ReframeAnalyzer.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeAnalyzer.Filters
{
    public abstract class FilterOption : IFilterOption
    {
        private List<IAnalysisNode> SelectedNodes { get; set; } = new List<IAnalysisNode>();

        public bool IsSelected(IAnalysisNode node)
        {
            return SelectedNodes.Exists(n => n.Identifier == node.Identifier);
        }

        public void SelectNode(IAnalysisNode node)
        {
            if (node != null && IsSelected(node) == false)
            {
                SelectedNodes.Add(node);
            }
        }

        public void DeselectNode(IAnalysisNode node)
        {
            if (node != null && IsSelected(node) == true)
            {
                SelectedNodes.Remove(node);
            }
        }

        public abstract List<IAnalysisNode> GetAvailableNodes();

        public void SelectAllNodes()
        {
            foreach (var node in GetAvailableNodes())
            {
                SelectNode(node);
            }
        }

        public void DeselectAllNodes()
        {
            SelectedNodes.Clear();
        }
    }
}
