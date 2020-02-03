using ReframeCore;
using ReframeCore.Factories;

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
