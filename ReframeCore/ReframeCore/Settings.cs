namespace ReframeCore
{
    /// <summary>
    /// Contains settings related to working with Reframe core.
    /// </summary>
    public class Settings
    {
        public string UpdateMethodNamePrefix { get; set; }

        public Settings()
        {
            UpdateMethodNamePrefix = "Update_";
        }
    }
}