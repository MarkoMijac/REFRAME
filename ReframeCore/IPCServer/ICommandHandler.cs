using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPCServer
{
    public interface ICommandHandler
    {
        string Identifier { get; }
        string HandleCommand(string commandXml);
    }
}
