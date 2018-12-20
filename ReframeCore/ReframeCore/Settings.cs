namespace ReframeCore
{
    /// <summary>
    /// Contains settings related to working with Reframe core.
    /// </summary>
    public class Settings
    {
        public string UpdateMethodNamePrefix { get; set; }

        public bool LogUpdates { get; set; }

        public bool UseDefaultUpdateMethodNames { get; set; }

        public Settings()
        {
            UseDefaultUpdateMethodNames = true;
            UpdateMethodNamePrefix = "Update_";
            LogUpdates = true;
        }
    }
}