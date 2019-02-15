using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore
{
    public class NodeFactorySettings
    {
        public string UpdateMethodNamePrefix { get; set; }

        public bool UseDefaultUpdateMethodNames { get; set; }

        public NodeFactorySettings()
        {
            UseDefaultUpdateMethodNames = true;
            UpdateMethodNamePrefix = "Update_";
        }
    }
}
