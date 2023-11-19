using System.IO;

using RkHelper.System;

namespace KeySwitchManager.Infrastructures.Storage.KeySwitches
{
    public class StreamExportContentWriter : AbstractStreamExportContentWriter
    {
        private Stream TargetStream { get; }

        public StreamExportContentWriter( Stream targetStream )
        {
            TargetStream = targetStream;
        }

        protected override Stream OpenStream()
        {
            return TargetStream;
        }

        protected override void DisposeStream( Stream stream )
        {
            Disposer.Dispose( stream );
        }
    }
}
