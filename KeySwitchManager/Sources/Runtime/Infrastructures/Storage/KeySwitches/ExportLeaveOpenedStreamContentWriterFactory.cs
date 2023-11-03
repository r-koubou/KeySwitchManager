using System.IO;

namespace KeySwitchManager.Infrastructures.Storage.KeySwitches
{
    public sealed class ExportLeaveOpenedStreamContentWriterFactory : ExportStreamContentWriterFactory
    {
        public ExportLeaveOpenedStreamContentWriterFactory( Stream targetStream ) : base( targetStream ) {}

        protected override AbstractStreamExportContentWriter CreateStreamExportContentWriterImpl()
        {
            return new KeepOpenedStreamExportContentWriter( TargetStream );
        }
    }
}
