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
        public bool EnableLogging { get; set; }

        public Settings()
        {
            EnableLogging = true;
        }
    }
}