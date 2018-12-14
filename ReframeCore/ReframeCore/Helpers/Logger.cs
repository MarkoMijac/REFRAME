using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore.Helpers
{
    public static class Logger
    {
        private static List<string> _nodesToUpdate = new List<string>();
        private static List<string> _updatedNodes = new List<string>();

        public static List<string> NodesToUpdate { get => _nodesToUpdate; }

        public static List<string> UpdatedNodes { get => _updatedNodes; }
    }
}
