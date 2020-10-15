using ReframeBaseExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeVisualizer
{
    public class VisualizationException : ReframeException
    {
        public VisualizationException(string message) : base(message)
        {

        }
    }
}
