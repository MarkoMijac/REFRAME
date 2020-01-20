using IPCClient;
using ReframeAnalyzer;
using ReframeTools.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReframeTools.Controllers
{
    public class ClassAnalysisController : GUIController
    {
        public ClassAnalysisController(FrmAnalysis form)
            :base(form)
        {
            
        }

        public override void ShowEntireGraph()
        {
            try
            {
                string xmlSource = ClientQueries.GetReactor(_form.ReactorIdentifier);
                
                string analysisDescription = $"Showing entire class-level graph [{_form.ReactorIdentifier}]";
                ShowGraph(xmlSource, analysisDescription);
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        public override void ShowSourceNodes()
        {
            try
            {
                string xmlGraph = ClientQueries.GetClassAnalysisGraphSourceNodes(_form.ReactorIdentifier);
                string analysisDescription = $"Showing only source nodes for class-level graph [{_form.ReactorIdentifier}]";
                ShowGraph(xmlGraph, analysisDescription);
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        public override void ShowSinkNodes()
        {
            try
            {
                string xmlGraph = ClientQueries.GetClassAnalysisGraphSinkNodes(_form.ReactorIdentifier);
                string analysisDescription = $"Showing only sink nodes for class-level graph [{_form.ReactorIdentifier}]";
                ShowGraph(xmlGraph, analysisDescription);
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        public override void ShowLeafNodes()
        {
            try
            {
                string xmlGraph = ClientQueries.GetClassAnalysisGraphLeafNodes(_form.ReactorIdentifier);
                string analysisDescription = $"Showing only leaf nodes for class-level graph [{_form.ReactorIdentifier}]";
                ShowGraph(xmlGraph, analysisDescription);
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        public override void ShowOrphanNodes()
        {
            try
            {
                string xmlGraph = ClientQueries.GetClassAnalysisGraphOrphanNodes(_form.ReactorIdentifier);
                string analysisDescription = $"Showing only orphan nodes for class-level graph [{_form.ReactorIdentifier}]";
                ShowGraph(xmlGraph, analysisDescription);
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        public override void ShowIntermediaryNodes()
        {
            try
            {
                string xmlGraph = ClientQueries.GetClassAnalysisGraphIntermediaryNodes(_form.ReactorIdentifier);
                string analysisDescription = $"Showing only intermediary nodes for class-level graph [{_form.ReactorIdentifier}]";
                ShowGraph(xmlGraph, analysisDescription);
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        public override void ShowPredecessorNodes()
        {
            try
            {
                string nodeIdentifier = _form.GetSelectedNodeIdentifier();
                if (nodeIdentifier != "")
                {
                    string xmlGraph = ClientQueries.GetClassAnalysisGraphPredecessorNodes(_form.ReactorIdentifier, nodeIdentifier);
                    string analysisDescription = $"Showing only predecessor nodes of node [{nodeIdentifier}] for class-level graph [{_form.ReactorIdentifier}]";
                    ShowGraph(xmlGraph, analysisDescription);
                }
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        public override void ShowSuccessorNodes()
        {
            try
            {
                string nodeIdentifier = _form.GetSelectedNodeIdentifier();
                if (nodeIdentifier != "")
                {
                    string xmlGraph = ClientQueries.GetClassAnalysisGraphSuccessorNodes(_form.ReactorIdentifier, nodeIdentifier);
                    string analysisDescription = $"Showing only successor nodes of node [{nodeIdentifier}] for class-level graph [{_form.ReactorIdentifier}]";
                    ShowGraph(xmlGraph, analysisDescription);
                }
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        public override void ShowNeighbourNodes()
        {
            try
            {
                string nodeIdentifier = _form.GetSelectedNodeIdentifier();
                if (nodeIdentifier != "")
                {
                    string xmlGraph = ClientQueries.GetClassAnalysisGraphNeighbourNodes(_form.ReactorIdentifier, nodeIdentifier);
                    string analysisDescription = $"Showing only neighbour nodes of node [{nodeIdentifier}] for class-level graph [{_form.ReactorIdentifier}]";
                    ShowGraph(xmlGraph, analysisDescription);
                }
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }
    }
}
