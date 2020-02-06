using ReframeCore;
using ReframeCore.Factories;
using ReframeCore.Helpers;

namespace ReframeDemonstration
{
    public static class DependencyManager
    {
        public static IReactor DefaultReactor
        {
            get
            {
                var reactor = ReactorRegistry.Instance.GetOrCreateReactor("DEFAULT_REACTOR");
                (reactor.Updater as Updater).SkipUpdateIfInitialNodeNotChanged = true;
                return reactor;
            }
        }
    }
}
