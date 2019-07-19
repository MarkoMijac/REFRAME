using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore.Nodes
{
    public class StaticNode
    {
        public int Identifier { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Assembly { get; set; }
        public string Namespace { get; set; }

        public IList<StaticNode> Predecessors { get; set; }
        public IList<StaticNode> Successors { get; set; }


    }
}
