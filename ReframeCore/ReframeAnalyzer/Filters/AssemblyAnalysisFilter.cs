using ReframeAnalyzer.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeAnalyzer.Filters
{
    public class AssemblyAnalysisFilter : AnalysisFilter
    {
        public AssemblyAnalysisFilter(List<IAnalysisNode> originalNodes) : base(originalNodes)
        {
            Query = new Predicate<IAnalysisNode>(n => IsSelected(n) == true);
        }

        protected override void InitializeOptions()
        {
            IFilterOption assemblyFilterOption = new FilterOption(GetAvailableAssemblyNodes(), AnalysisLevel.AssemblyLevel);
            FilterOptions.Add(assemblyFilterOption);
            assemblyFilterOption.SelectNodes();
        }
        public List<IAnalysisNode> GetAvailableAssemblyNodes()
        {
            List<IAnalysisNode> assemblyNodes = new List<IAnalysisNode>();

            foreach (var assemblyNode in OriginalNodes)
            {
                assemblyNodes.Add(assemblyNode);
            }

            return assemblyNodes;
        }

        public void SelectAllAssemblyNodes()
        {
            IFilterOption assemblyFilterOption = GetFilterOption(AnalysisLevel.AssemblyLevel);
            if (assemblyFilterOption != null)
            {
                assemblyFilterOption.SelectNodes();
            }
        }

        public void DeselectAllAssemblyNodes()
        {
            IFilterOption assemblyFilterOption = GetFilterOption(AnalysisLevel.AssemblyLevel);
            if (assemblyFilterOption != null)
            {
                assemblyFilterOption.DeselectNodes();
            }
        }
    }
}
