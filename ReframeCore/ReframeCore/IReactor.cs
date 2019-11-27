using ReframeCore.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore
{
    public interface IReactor
    {
        string Identifier { get; }
        IDependencyGraph Graph { get; }
        IUpdater Updater { get; }



    }
}
