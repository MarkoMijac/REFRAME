using EnvDTE;
using Microsoft.VisualStudio.GraphModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeTools
{
    public static class SolutionServices
    {
        private static string _dgmlTemplatePath = @"C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\Common7\IDE\ItemTemplates\Modeling\Modeling.vstemplate";

        public static Solution Solution { get; set; }

        /// <summary>
        /// Gets the list of projects found in a solution.
        /// </summary>
        /// <returns>List of projects found in a solution.</returns>
        public static List<Project> GetSolutionProjects()
        {
            List<Project> projects = new List<Project>();

            if (Solution != null)
            {
                foreach (Project project in Solution.Projects)
                {
                    projects.Add(project);
                }
            }

            return projects;
        }

        /// <summary>
        /// Gets the list of project items (files) for the provided project.
        /// </summary>
        /// <param name="project">The project whose items are being fetched.</param>
        /// <returns>List of project items.</returns>
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

        public static ProjectItem CreateNewDgmlFile(string dgmlFileName, Graph graph)
        {
            return CreateNewDgmlFile(dgmlFileName, graph.ToXml());
        }

        public static ProjectItem CreateNewDgmlFile(string dgmlFileName, string xmlSource)
        {
            Project project = Solution.Item(1);

            ProjectItem newProjectItem = project.ProjectItems.AddFromTemplate(_dgmlTemplatePath, dgmlFileName);
            if (newProjectItem == null)
            {
                List<ProjectItem> allProjectItems = GetProjectItems(project);
                newProjectItem = allProjectItems.FirstOrDefault(p => p.Name == dgmlFileName + ".dgml");
            }

            TextSelection sel = newProjectItem.Document.Selection as TextSelection;
            sel.SelectAll();
            sel.Delete();
            sel.Insert(xmlSource);

            newProjectItem.Document.Close(vsSaveChanges.vsSaveChangesYes);
            newProjectItem.Open();
            newProjectItem.Document.Activate();

            return newProjectItem;
        }
    }
}
