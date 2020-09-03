using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeVisualizer
{
    public interface IGraphFileCreator
    {
        void CreateNewFile(IVisualGraph visualGraph);
    }
}
