using ReframeAnalyzer.Exceptions;
using ReframeAnalyzer.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeAnalyzer.Filters
{
    public class AnalysisFilterFactory
    {
        public IAnalysisFilter CreateFilter(IEnumerable<IAnalysisNode> nodes, AnalysisLevel analysisLevel)
        {
            ValidateParameters(nodes, analysisLevel);

            IAnalysisFilter filter;

            switch (analysisLevel)
            {
                case AnalysisLevel.AssemblyLevel:
                    filter = new AssemblyAnalysisFilter(nodes);
                    break;
                case AnalysisLevel.NamespaceLevel:
                    filter = new NamespaceAnalysisFilter(nodes);
                    break;
                case AnalysisLevel.ClassLevel:
                    filter = new ClassAnalysisFilter(nodes);
                    break;
                case AnalysisLevel.ClassMemberLevel:
                    filter = new ClassMemberAnalysisFilter(nodes);
                    break;
                case AnalysisLevel.ObjectLevel:
                    filter = new ObjectAnalysisFilter(nodes);
                    break;
                case AnalysisLevel.ObjectMemberLevel:
                    filter = new ObjectMemberAnalysisFilter(nodes);
                    break;
                default:
                    filter = null;
                    break;
            }

            return filter;
        }

        private void ValidateParameters(IEnumerable<IAnalysisNode> nodes, AnalysisLevel level)
        {
            if (nodes == null)
            {
                throw new AnalysisException("List of nodes is null!");
            }

            if (nodes.Count() > 0)
            {
                if (IsMatch(nodes.ElementAt(0).GetType(), level) == false)
                {
                    throw new AnalysisException("Node list and requested analysis level do not match!");
                }
            }
        }

        private bool IsMatch(Type nodeType, AnalysisLevel level)
        {
            bool match = false;

            if (nodeType == typeof(AssemblyAnalysisNode) && level == AnalysisLevel.AssemblyLevel)
            {
                match = true;
            }
            else if (nodeType == typeof(NamespaceAnalysisNode) && level == AnalysisLevel.NamespaceLevel)
            {
                match = true;
            }
            else if (nodeType == typeof(ClassAnalysisNode) && level == AnalysisLevel.ClassLevel)
            {
                match = true;
            }
            else if (nodeType == typeof(ClassMemberAnalysisNode) && level == AnalysisLevel.ClassMemberLevel)
            {
                match = true;
            }
            else if (nodeType == typeof(ObjectAnalysisNode) && level == AnalysisLevel.ObjectLevel)
            {
                match = true;
            }
            else if (nodeType == typeof(ObjectMemberAnalysisNode) && level == AnalysisLevel.ObjectMemberLevel)
            {
                match = true;
            }

            return match;
        }
    }
}
