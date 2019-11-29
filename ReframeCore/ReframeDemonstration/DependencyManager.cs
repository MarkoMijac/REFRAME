using ReframeCore;
using ReframeCore.Factories;
using ReframeCore.FluentAPI;
using ReframeCore.Helpers;
using ReframeDemonstration.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeDemonstration
{
    public static class DependencyManager
    {
        public static IReactor DefaultReactor
        {
            get
            {
                return ReactorRegistry.Instance.GetOrCreateReactor("DEFAULT_REACTOR");
            }
        }
    }
}
