using System.IO;

namespace KeySwitchManager.Infrastructures.Storage.KeySwitches
{
    public class KeepOpenedStreamExportContentWriter : AbstractStreamExportContentWriter
    {
        private Stream TargetStream { get; }

        public KeepOpenedStreamExportContentWriter( Stream targetStream )
        {
            TargetStream = targetStream;
        }

        protected override Stream OpenStream()
        {
            return TargetStream;
        }

        protected override void DisposeStream( Stream stream ) {}
    }
}
