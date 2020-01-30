using EnvDTE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolutionServices
{
    public class VS_Project
    {
        public Project DTE_Project { get; private set; }

        public VS_Project(Project project)
        {
            DTE_Project = project;
        }

        public List<VS_ProjectItem> GetProjectItems()
        {
            List<VS_ProjectItem> projectItems = new List<VS_ProjectItem>();

            foreach (ProjectItem item in DTE_Project.ProjectItems)
            {
                projectItems.Add(new VS_ProjectItem(item));
            }

            return projectItems;
        }
    }
}
