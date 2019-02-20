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

        /// <summary>
        /// Gets update method name generated from default prefix and property name.
        /// </summary>
        /// <param name="propertyName">Property name represented by reactive node.</param>
        /// <returns>Update method name generated from default prefix and property name.</returns>
        public string GenerateDefaultUpdateMethodName(string propertyName)
        {
            return UpdateMethodNamePrefix + propertyName;
        }
    }
}
