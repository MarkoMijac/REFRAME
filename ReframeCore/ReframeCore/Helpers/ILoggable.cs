using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore.Helpers
{
    interface ILoggable
    {
        UpdateLogger NodeLog { get; }
    }
}
