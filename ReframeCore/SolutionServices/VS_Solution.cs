using EnvDTE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolutionServices
{
    public class VS_Solution
    {
        public Solution DTE_Solution { get; private set; }

        public VS_Solution(Solution solution)
        {
            DTE_Solution = solution;
        }

        public List<VS_Project> GetProjects()
        {
            List<VS_Project> projects = new List<VS_Project>();

            foreach (Project p in DTE_Solution.Projects)
            {
                projects.Add(new VS_Project(p));
            }

            return projects;
        }
    }
}
