using EnvDTE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolutionServices
{
    public class VS_ProjectItem
    {
        public ProjectItem DTE_ProjectItem { get; set; }

        public VS_ProjectItem(ProjectItem projectItem)
        {
            DTE_ProjectItem = projectItem;
        }
    }
}
