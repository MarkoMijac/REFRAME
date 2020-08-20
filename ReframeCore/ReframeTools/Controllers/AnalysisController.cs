﻿using IPCClient;
using ReframeAnalyzer;
using ReframeAnalyzer.Graph;
using ReframeClient;
using ReframeTools.Commands;
using ReframeTools.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReframeTools.Controllers
{
    public class AnalysisController
    {
        protected List<GuiCommand> GeneralGraphAnalyses { get; set; } = new List<GuiCommand>();

        protected List<GuiCommand> GeneralNodeAnalyses { get; set; } = new List<GuiCommand>();

        protected List<GuiCommand> CustomAnalyses { get; set; } = new List<GuiCommand>();

        private FrmAnalysisFilter FilterForm { get; set; }
        private FrmAnalysisView View { get; set; }
        public IAnalysisGraph AnalysisGraph { get; set; }
        public IEnumerable<IAnalysisNode> AnalysisNodes { get; set; }
        private Analyzer Analyzer { get; set; } = new Analyzer();

        public AnalysisController(FrmAnalysisView form, AnalysisGraphFactory graphFactory, FrmAnalysisFilter frmFilter = null)
        {
            View = form;
            FilterForm = frmFilter;

            CreateGeneralGraphCommands();
            CreateGeneralNodeCommands();
            CreateAnalysisGraph(View.ReactorIdentifier, graphFactory); 
        }

        private void CreateAnalysisGraph(string reactorIdentifier, AnalysisGraphFactory graphFactory)
        {
            var pipeClient = new ReframePipeClient();
            string xmlSource = pipeClient.GetReactor(reactorIdentifier);
            AnalysisGraph = graphFactory.CreateGraph(xmlSource);
        }

        private void DisplayGraph(List<IAnalysisNode> originalNodes)
        {
            try
            {
                AnalysisNodes = GetFilteredNodes(originalNodes);
                ShowGraph(AnalysisNodes);
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        private void ShowGraph(IEnumerable<IAnalysisNode> nodes)
        {
            if (View != null)
            {
                View.ShowAnalysis(nodes);

                SetGraphDetails();
                View.DisplayDetails();
            }
        }

        private void SetGraphDetails()
        {
            View.GraphIdentifier = AnalysisGraph.Identifier;
            View.GraphTotalNodeCount = GraphMetrics.GetNumberOfNodes(AnalysisGraph.Nodes).ToString();
            View.NumberOfAnalyzedNodes = GraphMetrics.GetNumberOfNodes(AnalysisNodes).ToString();
            View.NumberOfDependencies = GraphMetrics.GetNumberOfEdges(AnalysisNodes).ToString();
            View.MaxNumberOfDependencies = GraphMetrics.GetMaximumNumberOfEdges(AnalysisNodes).ToString();
            View.GraphDensity = GraphMetrics.GetGraphDensity(AnalysisNodes).ToString("N4");
        }

        private IEnumerable<IAnalysisNode> GetFilteredNodes(List<IAnalysisNode> originalNodes)
        {
            IEnumerable<IAnalysisNode> filteredNodes;
            if (FilterForm != null)
            {
                FilterForm.OriginalNodes = originalNodes;
                FilterForm.ShowDialog();

                var filter = FilterForm.Filter;
                filteredNodes = filter.Apply();
            }
            else
            {
                filteredNodes = originalNodes;
            }

            return filteredNodes;
        }

        private void CreateGeneralGraphCommands()
        {
            var showGraphCommand = new GuiCommand("showGraphCommand", "Show graph...", this, nameof(ShowEntireGraph));
            var showSourceNodesCommand = new GuiCommand("showSourceNodesCommand", "Show source nodes...", this, nameof(ShowSourceNodes));
            var showSinkNodesCommand = new GuiCommand("showSinkNodesCommand", "Show sink nodes...", this, nameof(ShowSinkNodes));
            var showLeafNodesCommand = new GuiCommand("showLeafNodesCommand", "Show leaf nodes...", this, nameof(ShowLeafNodes));
            var showOrphanNodesCommand = new GuiCommand("showOrphanNodesCommand", "Show orphan nodes...", this, nameof(ShowOrphanNodes));
            var showIntermediaryNodesCommand = new GuiCommand("showIntermediaryNodesCommand", "Show intermediary nodes...", this, nameof(ShowIntermediaryNodes));

            GeneralGraphAnalyses.Add(showGraphCommand);
            GeneralGraphAnalyses.Add(showSourceNodesCommand);
            GeneralGraphAnalyses.Add(showSinkNodesCommand);
            GeneralGraphAnalyses.Add(showLeafNodesCommand);
            GeneralGraphAnalyses.Add(showOrphanNodesCommand);
            GeneralGraphAnalyses.Add(showIntermediaryNodesCommand);
        }

        public IReadOnlyList<GuiCommand> GetGeneralGraphAnalyses()
        {
            return GeneralGraphAnalyses;
        }

        private void CreateGeneralNodeCommands()
        {
            var showPredecessorsForNode = new GuiCommand("showPredecessorsForNode", "Show node's predecessors...", this, nameof(ShowPredecessorNodes));
            var showSuccessorsForNode = new GuiCommand("showSuccessorsForNode", "Show node's successors...", this, nameof(ShowSuccessorNodes));
            var showNeighboursForNode = new GuiCommand("showNeighboursForNode", "Show node's neighbours...", this, nameof(ShowNeighbourNodes));
            var showSourceNodesForNode = new GuiCommand("showSourceNodesForNode", "Show node's source nodes...", this, nameof(ShowSourceNodesForNode));
            var showSinkNodesForNode = new GuiCommand("showSinkNodesForNode", "Show node's sink nodes...", this, nameof(ShowSinkNodesForNode));
            var showLeafNodesForNode = new GuiCommand("showLeafNodesForNode", "Show node's leaf nodes...", this, nameof(ShowLeafNodesForNode));
            var showIntermediaryPredecessors = new GuiCommand("showIntermediaryPredecessors", "Show intermediary predecessors...", this, nameof(ShowIntermediaryPredecessors));
            var showIntermediarySuccessors = new GuiCommand("showIntermediarySuccessors", "Show intermediary successors...", this, nameof(ShowIntermediarySuccessors));
            var showIntermediaryNodesForNode = new GuiCommand("showIntermediaryNodesForNode", "Show node's intermediary nodes...", this, nameof(ShowIntermediaryNodesForNode));

            GeneralNodeAnalyses.Add(showPredecessorsForNode);
            GeneralNodeAnalyses.Add(showSuccessorsForNode);
            GeneralNodeAnalyses.Add(showNeighboursForNode);
            GeneralNodeAnalyses.Add(showSourceNodesForNode);
            GeneralNodeAnalyses.Add(showSinkNodesForNode);
            GeneralNodeAnalyses.Add(showLeafNodesForNode);
            GeneralNodeAnalyses.Add(showIntermediaryPredecessors);
            GeneralNodeAnalyses.Add(showIntermediarySuccessors);
            GeneralNodeAnalyses.Add(showIntermediaryNodesForNode);
        }

        public IReadOnlyList<GuiCommand> GetGeneralNodeAnalyses()
        {
            return GeneralNodeAnalyses;
        }

        public void AddCustomAnalysisCommand(GuiCommand command)
        {
            if (command != null)
            {
                CustomAnalyses.Add(command);
            }
        }

        public void RemoveCustomAnalysisCommand(GuiCommand command)
        {
            if (command != null)
            {
                CustomAnalyses.Remove(command);
            }
        }

        public void RemoveCustomAnalysisCommand(string identifier)
        {
            var command = CustomAnalyses.FirstOrDefault(c => c.Identifier == identifier);
            RemoveCustomAnalysisCommand(command);
        }

        public IReadOnlyList<GuiCommand> GetCustomAnalyses()
        {
            return CustomAnalyses;
        }

        private void ShowEntireGraph()
        {
            DisplayGraph(AnalysisGraph.Nodes);
        }

        private void ShowSourceNodes()
        {
            DisplayGraph(Analyzer.GetSourceNodes(AnalysisGraph));
        }

        private void ShowSinkNodes()
        {
            DisplayGraph(Analyzer.GetSinkNodes(AnalysisGraph));
        }

        private void ShowLeafNodes()
        {
            DisplayGraph(Analyzer.GetLeafNodes(AnalysisGraph));
        }

        private void ShowOrphanNodes()
        {
            DisplayGraph(Analyzer.GetOrphanNodes(AnalysisGraph));
        }

        private void ShowIntermediaryNodes()
        {
            DisplayGraph(Analyzer.GetIntermediaryNodes(AnalysisGraph));
        }

        private void ShowPredecessorNodes()
        {
            uint nodeIdentifier = View.SelectedNodeId;
            if (nodeIdentifier != 0)
            {
                List<IAnalysisNode> predecessorNodes = Analyzer.GetNodeAndItsPredecessors(AnalysisGraph, nodeIdentifier, GetMaxDepth());
                DisplayGraph(predecessorNodes);
            }
        }

        private int GetMaxDepth()
        {
            var depthForm = new FrmMaxDepthLevel();
            depthForm.ShowDialog();

            return depthForm.MaxDepthLevel;
        }

        private void ShowSuccessorNodes()
        {
            uint nodeIdentifier = View.SelectedNodeId;
            if (nodeIdentifier != 0)
            {
                List<IAnalysisNode> successorNodes = Analyzer.GetNodeAndItsSuccessors(AnalysisGraph, nodeIdentifier, GetMaxDepth());
                DisplayGraph(successorNodes);
            }
        }

        private void ShowNeighbourNodes()
        {
            uint nodeIdentifier = View.SelectedNodeId;
            if (nodeIdentifier != 0)
            {
                List<IAnalysisNode> neighbourNodes = Analyzer.GetNodeAndItsNeighbours(AnalysisGraph, nodeIdentifier, GetMaxDepth());
                DisplayGraph(neighbourNodes);
            }
        }

        private void ShowSourceNodesForNode()
        {
            uint nodeIdentifier = View.SelectedNodeId;
            if (nodeIdentifier != 0)
            {
                DisplayGraph(Analyzer.GetSourceNodes(AnalysisGraph, nodeIdentifier));
            }
        }

        private void ShowSinkNodesForNode()
        {
            uint nodeIdentifier = View.SelectedNodeId;
            if (nodeIdentifier != 0)
            {
                DisplayGraph(Analyzer.GetSinkNodes(AnalysisGraph, nodeIdentifier));
            }
        }

        private void ShowLeafNodesForNode()
        {
            uint nodeIdentifier = View.SelectedNodeId;
            if (nodeIdentifier != 0)
            {
                DisplayGraph(Analyzer.GetLeafNodes(AnalysisGraph, nodeIdentifier));
            }
        }

        private void ShowIntermediaryPredecessors()
        {
            uint nodeIdentifier = View.SelectedNodeId;
            if (nodeIdentifier != 0)
            {
                DisplayGraph(Analyzer.GetIntermediaryPredecessors(AnalysisGraph, nodeIdentifier));
            }
        }

        private void ShowIntermediarySuccessors()
        {
            uint nodeIdentifier = View.SelectedNodeId;
            if (nodeIdentifier != 0)
            {
                DisplayGraph(Analyzer.GetIntermediarySuccessors(AnalysisGraph, nodeIdentifier));
            }
        }

        private void ShowIntermediaryNodesForNode()
        {
            uint nodeIdentifier = View.SelectedNodeId;
            if (nodeIdentifier != 0)
            {
                DisplayGraph(Analyzer.GetIntermediaryNodes(AnalysisGraph, nodeIdentifier));
            }
        }

        private void DisplayError(Exception e)
        {
            MessageBox.Show($"Unable to fetch and display data! Error:{e.Message}!");
        }
    }
}
