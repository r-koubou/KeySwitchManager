namespace KeySwitchManager.Domain.Commons
{
    public interface IPath
    {
        public string Path { get; }
        public bool Exists { get; }
        public bool IsFile { get; }
        public bool IsDirectory { get; }
    }
}