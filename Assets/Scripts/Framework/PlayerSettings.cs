namespace Framework
{
    /// <summary>
    /// A class that hold data of the chosen settings.
    /// </summary>
    public sealed class PlayerSettings : Singleton<PlayerSettings>
    {
        public bool IsUsingController { get; set; }
    }
}