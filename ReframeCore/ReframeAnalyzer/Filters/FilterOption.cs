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
        public AnalysisLevel Level { get; private set; }
        private List<IAnalysisNode> SelectedNodes { get; set; } = new List<IAnalysisNode>();
        private List<IAnalysisNode> AllNodes { get; set; } = new List<IAnalysisNode>();

        public FilterOption(List<IAnalysisNode> allNodes, AnalysisLevel level)
        {
            AllNodes = allNodes;
            Level = level;
        }

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
    }
}