using ReframeAnalyzer.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeAnalyzer.Filters
{
    public class FilterOption : IFilterOption
    {
        public AnalysisLevel Level { get; protected set; }
        protected List<IAnalysisNode> SelectedNodes { get; set; } = new List<IAnalysisNode>();
        public List<IAnalysisNode> AllNodes { get; protected set; } = new List<IAnalysisNode>();

        public FilterOption(List<IAnalysisNode> originalNodes)
        {
            AllNodes = InitializeNodes(originalNodes);
        }

        public FilterOption(List<IAnalysisNode> allNodes, AnalysisLevel level)
        {
            AllNodes = allNodes;
            Level = level;
        }

        public bool IsSelected(IAnalysisNode node)
        {
            bool isSelected = false;

            if (node != null)
            {
                isSelected = SelectedNodes.Exists(n => n.Identifier == node.Identifier);
            }

            return isSelected;
        }

        public void SelectNode(IAnalysisNode node)
        {
            if (node != null && IsSelected(node) == false)
            {
                SelectedNodes.Add(node);
                OnNodeSelected(node);
            }
        }

        public void DeselectNode(IAnalysisNode node)
        {
            if (node != null && IsSelected(node) == true)
            {
                SelectedNodes.Remove(node);
                OnNodeDeselected(node);
            }
        }

        public void SelectNodes()
        {
            foreach (var node in AllNodes)
            {
                SelectNode(node);
            }
        }

        public void SelectNodes(Predicate<IAnalysisNode> condition)
        {
            foreach (var node in AllNodes)
            {
                if (condition(node) == true)
                {
                    SelectNode(node);
                }
            }
        }

        public void DeselectNodes()
        {
            SelectedNodes.Clear();
        }

        public void DeselectNodes(Predicate<IAnalysisNode> condition)
        {
            SelectedNodes.RemoveAll(condition);
        }

        private List<IAnalysisNode> InitializeNodes(List<IAnalysisNode> originalNodes)
        {
            List<IAnalysisNode> nodes = new List<IAnalysisNode>();

            foreach (var node in originalNodes)
            {
                nodes.Add(node);
            }

            return nodes;
        }

        public List<IAnalysisNode> GetNodes()
        {
            return AllNodes;
        }

        public List<IAnalysisNode> GetNodes(Predicate<IAnalysisNode> condition)
        {
            return GetNodes().FindAll(condition);
        }

        public List<IAnalysisNode> GetNodes(Predicate<IAnalysisNode> condition, bool distinct)
        {
            if (distinct == true)
            {
                List<IAnalysisNode> nodes = GetNodes().FindAll(condition);
                List<IAnalysisNode> distinctNodes = new List<IAnalysisNode>();
                foreach (var node in nodes)
                {
                    if (distinctNodes.Exists(n => n.Identifier == node.Identifier) == false)
                    {
                        distinctNodes.Add(node);
                    }
                }

                return distinctNodes;
            }
            else
            {
                return GetNodes(condition);
            }
            
        }

        public event EventHandler NodeSelected;
        public event EventHandler NodeDeselected;

        private void OnNodeSelected(IAnalysisNode node)
        {
            NodeSelected?.Invoke(node, null);
        }

        private void OnNodeDeselected(IAnalysisNode node)
        {
            NodeDeselected?.Invoke(node, null);
        }
    }
}
