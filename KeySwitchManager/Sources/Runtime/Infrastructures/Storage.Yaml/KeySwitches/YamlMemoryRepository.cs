using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Commons.Helpers;
using KeySwitchManager.Domain.KeySwitches;
using KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches.Translators;

using RkHelper.System;

namespace KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches
{
    public abstract class YamlMemoryRepository : OnMemoryKeySwitchRepository
    {
        protected YamlMemoryRepository( Stream stream, bool leaveOpen = false ) : base()
        {
            StreamReader? reader = null;

            try
            {
                reader = new StreamReader( stream, EncodingHelper.UTF8N );
                var yaml = reader.ReadToEnd();
                KeySwitches.AddRange( new YamlKeySwitchImportTranslator().Translate( new PlainText( yaml ) ) );
            }
            finally
            {
                if( !leaveOpen && reader is not null )
                {
                    Disposer.Dispose( reader );
                    Disposer.Dispose( stream );
                }
            }
        }

        public override async Task WriteBinaryToAsync( Stream stream, CancellationToken cancellationToken = default )
        {
            await using var writer = new StreamWriter( stream, EncodingHelper.UTF8N );

            var yaml = new ReadOnlyMemory<char>(
                new YamlKeySwitchExportTranslator().Translate( KeySwitches ).Value.ToCharArray()
            );

            await writer.WriteAsync( yaml, cancellationToken );
            await writer.FlushAsync();
        }
    }
}
