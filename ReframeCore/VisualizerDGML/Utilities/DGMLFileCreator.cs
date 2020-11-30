using EnvDTE;
using ReframeVisualizer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace VisualizerDGML.Utilities
{
    public class DGMLFileCreator : IGraphFileCreator
    {
        const string _dgmlTemplatePath = @"C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\Common7\IDE\ItemTemplates\Modeling\Modeling.vstemplate";
        const string _extension = ".dgml";

        public Solution Solution { get; set; }

        public DGMLFileCreator(Solution solution)
        {
            Solution = solution;
        }

        public static List<ProjectItem> GetProjectItems(Project project)
        {
            List<ProjectItem> projectItems = new List<ProjectItem>();

            if (project != null)
            {
                foreach (ProjectItem item in project.ProjectItems)
                {
                    projectItems.Add(item);
                }
            }

            return projectItems;
        }

        private string GenerateName(IVisualGraph visualGraph)
        {
            return new Random().Next().ToString() + "_" + visualGraph.ReactorIdentifier;
        }

        public void CreateNewFile(IVisualGraph visualGraph)
        {
            Project project = Solution.Item(1);
            string fileName = GenerateName(visualGraph);
            string fileContent = visualGraph.SerializeGraph();

            ProjectItem newProjectItem = project.ProjectItems.AddFromTemplate(_dgmlTemplatePath, fileName);
            if (newProjectItem == null)
            {
                List<ProjectItem> allProjectItems = GetProjectItems(project);
                newProjectItem = allProjectItems.FirstOrDefault(p => p.Name == fileName + _extension);
            }

            TextSelection sel = newProjectItem.Document.Selection as TextSelection;
            sel.SelectAll();
            sel.Delete();
            sel.Insert(fileContent);

            newProjectItem.Document.Close(vsSaveChanges.vsSaveChangesYes);
            newProjectItem.Open();
            newProjectItem.Document.Activate();
        }
    }
}
