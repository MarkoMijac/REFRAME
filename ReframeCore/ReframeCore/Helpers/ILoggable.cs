using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore.Helpers
{
    public interface ILoggable
    {
        bool LoggingEnabled { get; set; }
        NodeLog NodeLog { get; }
    }
}
