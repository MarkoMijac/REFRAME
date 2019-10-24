using ReframeCore;
using ReframeCore.FluentAPI;
using ReframeDemonstration.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeDemonstration
{
    public static class DependencyManager
    {
        public static IDependencyGraph DefaultGraph
        {
            get
            {
                return GraphFactory.GetOrCreate("DEFAULT_GRAPH");
            }
        }
    }
}
