namespace ReframeCore
{
    /// <summary>
    /// Contains settings related to working with Reframe core.
    /// </summary>
    public class Settings
    {
        /// <summary>
        /// Determines if updates should be logged.
        /// </summary>
        public bool LogUpdates { get; set; }

        public Settings()
        {
            LogUpdates = true;
        }
    }
}