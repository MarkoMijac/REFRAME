﻿using IPCClient;
using ReframeAnalyzer;
using ReframeAnalyzer.Graph;
using ReframeTools.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReframeTools.Controllers
{
    public abstract class AnalysisController
    {
        protected FrmFilterAnalysis FilterView { get; set; }
        protected FrmAnalysisView View { get; set; }
        public IAnalysisGraph AnalysisGraph { get; set; }
        public IEnumerable<IAnalysisNode> AnalysisNodes { get; set; }
        protected Analyzer Analyzer { get; set; } = new Analyzer();
        protected AnalysisGraphFactory GraphFactory { get; set; } = new AnalysisGraphFactory();

        public AnalysisController(FrmAnalysisView form, FrmFilterAnalysis frmFilter = null)
        {
            View = form;
            FilterView = frmFilter;
        }

        protected void CreateAnalysisGraph(string reactorIdentifier, AnalysisLevel analysisLevel)
        {
            string xmlSource = ClientQueries.GetReactor(reactorIdentifier);
            AnalysisGraph = GraphFactory.CreateGraph(xmlSource, analysisLevel);
        }

        protected virtual void ShowGraph(IEnumerable<IAnalysisNode> nodes)
        {
            if (View != null)
            {
                View.ShowAnalysis(nodes);
            }
        }

        private IEnumerable<IAnalysisNode> GetFilteredNodes(IEnumerable<IAnalysisNode> originalNodes)
        {
            IEnumerable<IAnalysisNode> filteredNodes;
            if (FilterView != null)
            {
                FilterView.OriginalNodes = originalNodes;
                FilterView.ShowDialog();

                var filter = FilterView.Filter;
                filteredNodes = filter.Apply();
            }
            else
            {
                filteredNodes = originalNodes;
            }

            return filteredNodes;
        }

        public virtual void ShowEntireGraph()
        {
            try
            {
                var originalNodes = AnalysisGraph.Nodes;
                AnalysisNodes = GetFilteredNodes(originalNodes);
                ShowGraph(AnalysisNodes);
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        public virtual void ShowSourceNodes()
        {
            try
            {
                var originalNodes = Analyzer.GetSourceNodes(AnalysisGraph);
                AnalysisNodes = GetFilteredNodes(originalNodes);
                ShowGraph(AnalysisNodes);
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        public void ShowSinkNodes()
        {
            try
            {
                var originalNodes = Analyzer.GetSinkNodes(AnalysisGraph);
                AnalysisNodes = GetFilteredNodes(originalNodes);
                ShowGraph(AnalysisNodes);
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        public void ShowLeafNodes()
        {
            try
            {
                var originalNodes = Analyzer.GetLeafNodes(AnalysisGraph);
                AnalysisNodes = GetFilteredNodes(originalNodes);
                ShowGraph(AnalysisNodes);
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        public void ShowOrphanNodes()
        {
            try
            {
                var originalNodes = Analyzer.GetOrphanNodes(AnalysisGraph);
                AnalysisNodes = GetFilteredNodes(originalNodes);
                ShowGraph(AnalysisNodes);
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        public void ShowIntermediaryNodes()
        {
            try
            {
                var originalNodes = Analyzer.GetIntermediaryNodes(AnalysisGraph);
                AnalysisNodes = GetFilteredNodes(originalNodes);
                ShowGraph(AnalysisNodes);
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        public void ShowPredecessorNodes(string nodeIdentifier)
        {
            try
            {
                if (nodeIdentifier != "")
                {
                    IEnumerable<IAnalysisNode> originalNodes;

                    int maxDepth = GetMaxDepth();
                    if (maxDepth == 0)
                    {
                        originalNodes = Analyzer.GetPredecessors(AnalysisGraph, nodeIdentifier);
                    }
                    else
                    {
                        originalNodes = Analyzer.GetPredecessors(AnalysisGraph, nodeIdentifier, maxDepth);
                    }
                    AnalysisNodes = GetFilteredNodes(originalNodes);
                    ShowGraph(AnalysisNodes);
                }
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        private int GetMaxDepth()
        {
            var depthForm = new FrmMaxDepthLevel();
            depthForm.ShowDialog();

            return depthForm.MaxDepthLevel;
        }

        public void ShowSuccessorNodes(string nodeIdentifier)
        {
            try
            {
                if (nodeIdentifier != "")
                {
                    IEnumerable<IAnalysisNode> originalNodes;
                    int maxDepth = GetMaxDepth();
                    if (maxDepth == 0)
                    {
                        originalNodes = Analyzer.GetSuccessors(AnalysisGraph, nodeIdentifier);
                    }
                    else
                    {
                        originalNodes = Analyzer.GetSuccessors(AnalysisGraph, nodeIdentifier, maxDepth);
                    }
                    AnalysisNodes = GetFilteredNodes(originalNodes);
                    ShowGraph(AnalysisNodes);
                }
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        public void ShowNeighbourNodes(string nodeIdentifier)
        {
            try
            {
                if (nodeIdentifier != "")
                {
                    IEnumerable<IAnalysisNode> originalNodes;
                    int maxDepth = GetMaxDepth();
                    if (maxDepth == 0)
                    {
                        originalNodes = Analyzer.GetNeighbours(AnalysisGraph, nodeIdentifier);
                    }
                    else
                    {
                        originalNodes = Analyzer.GetNeighbours(AnalysisGraph, nodeIdentifier, maxDepth);
                    }
                    
                    AnalysisNodes = GetFilteredNodes(originalNodes);
                    ShowGraph(AnalysisNodes);
                }
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        protected void DisplayError(Exception e)
        {
            MessageBox.Show($"Unable to fetch and display data! Error:{e.Message}!");
        }
    }
}
