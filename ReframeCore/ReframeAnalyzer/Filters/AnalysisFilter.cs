using ReframeAnalyzer.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeAnalyzer.Filters
{
    public abstract class AnalysisFilter : IAnalysisFilter
    {
        protected Predicate<IAnalysisNode> Query { get; set; }

        protected List<IAnalysisNode> OriginalNodes { get; set; }

        protected List<IFilterOption> FilterOptions { get; set; } = new List<IFilterOption>();

        public AnalysisFilter(List<IAnalysisNode> originalNodes)
        {
            OriginalNodes = originalNodes;
            InitializeOptions();
            InitializeSelectedNodes();
        }

        protected virtual void InitializeOptions()
        {

        }

        private void InitializeSelectedNodes()
        {
            foreach (var option in FilterOptions)
            {
                option.SelectNodes();
            }
        }

        public virtual IEnumerable<IAnalysisNode> Apply()
        {
            List<IAnalysisNode> filteredNodes = new List<IAnalysisNode>();
            filteredNodes.AddRange(OriginalNodes.FindAll(Query));
            return filteredNodes;
        }
        protected IFilterOption GetFilterOption(AnalysisLevel level)
        {
            return FilterOptions.FirstOrDefault(o => o.Level == level);
        }
       

        #region Public methods
        public virtual void SelectNode(IAnalysisNode node)
        {
            if (node != null)
            {
                GetFilterOption(node.Level)?.SelectNode(node);
            }
        }

        public virtual void DeselectNode(IAnalysisNode node)
        {
            if (node != null)
            {
                GetFilterOption(node.Level)?.DeselectNode(node);
            }
        }

        public bool IsSelected(IAnalysisNode node)
        {
            if (node != null)
            {
                return GetFilterOption(node.Level).IsSelected(node);
            }
            return false;
        }

        #endregion
    }
}
