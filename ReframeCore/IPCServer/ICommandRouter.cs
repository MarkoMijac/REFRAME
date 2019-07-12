using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPCServer
{
    public interface ICommandRouter
    {
        string Identifier { get; }
        string RouteCommand(string command);
    }
}
