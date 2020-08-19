﻿using ReframeAnalyzer.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeAnalyzer.Filters
{
    public class FilterOption : IFilterOption
    {
        private List<IAnalysisNode> SelectedNodes { get; set; } = new List<IAnalysisNode>();
        private List<IAnalysisNode> AllNodes { get; set; } = new List<IAnalysisNode>();

        public FilterOption(List<IAnalysisNode> allNodes)
        {
            AllNodes = allNodes;
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

        public List<IAnalysisNode> GetNodes()
        {
            return AllNodes;
        }

        public List<IAnalysisNode> GetNodes(Predicate<IAnalysisNode> condition)
        {
            return GetNodes().FindAll(condition);
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
