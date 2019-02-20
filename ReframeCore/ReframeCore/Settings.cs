namespace ReframeCore
{
    /// <summary>
    /// Contains settings related to working with Reframe core.
    /// </summary>
    public class Settings
    {
        public bool LogUpdates { get; set; }

        public Settings()
        {
            LogUpdates = true;
        }
    }
}