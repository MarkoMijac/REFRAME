using ReframeCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeExporter
{
    interface IExporter
    {
        string Export(IReadOnlyList<IReactor> reactors);
        string Export(IReactor reactor);
    }
}
