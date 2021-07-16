namespace KeySwitchManager.Commons.Data
{
    public interface IPath
    {
        public string Path { get; }
        public bool Exists { get; }
        public bool IsFile { get; }
        public bool IsDirectory { get; }
        public void CreateNew() {}

        public class Null : IPath
        {
            public string Path { get; } = string.Empty;
            public bool Exists { get; } = false;
            public bool IsFile { get; } = false;
            public bool IsDirectory { get; } = false;
            public void CreateNew()
            {}
        }
    }
}