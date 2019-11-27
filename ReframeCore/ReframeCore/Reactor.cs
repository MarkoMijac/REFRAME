using ReframeCore.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore
{
    public class Reactor : IReactor
    {
        public string Identifier { get; private set; }
        public IDependencyGraph Graph { get; set; }
        public IUpdater Updater { get; set; }

        public Reactor(string identifier, IDependencyGraph graph, IUpdater updater)
        {
            Identifier = identifier;
            Graph = graph;
            Updater = updater;
        }
    }
}
