namespace Framework
{
    public sealed class PlayerSettings : Singleton<PlayerSettings>
    {
        public bool IsUsingController { get; set; }
    }
}