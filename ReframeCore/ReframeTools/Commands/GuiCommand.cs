using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReframeTools.Commands
{
    internal class GuiCommand
    {
        public string Identifier { get; private set; }
        public Action Command { get; set; }

        public GuiCommand(string identifier, object obj, string methodName)
        {
            Identifier = identifier;   
        }

        public void Invoke()
        {
            Command?.Invoke();
        }
    }
}
