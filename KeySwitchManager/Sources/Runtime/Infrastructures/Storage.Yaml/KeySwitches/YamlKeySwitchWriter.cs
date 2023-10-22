using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches.Translators;

namespace KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches
{
    public sealed class YamlKeySwitchWriter : IKeySwitchWriter
    {
        private Encoding FileEncoding { get; }
        private Stream? Stream { get; set; }

        public bool LeaveOpen { get; }

        public YamlKeySwitchWriter( Stream stream ) : this( stream, Encoding.UTF8 ) {}

        public YamlKeySwitchWriter( Stream stream, Encoding filEncoding, bool leaveOpen = false )
        {
            FileEncoding = filEncoding ?? throw new ArgumentNullException( nameof( filEncoding ) );
            Stream      = stream ?? throw new ArgumentNullException( nameof( stream ) );
            LeaveOpen   = leaveOpen;
        }

        public void Dispose()
        {
            if( LeaveOpen || Stream == null )
            {
                return;
            }

            try
            {
                Stream.Flush();
                Stream.Close();
                Stream.Dispose();
            }
            catch
            {
                // ignored
            }

            Stream = null;
        }

        async Task IKeySwitchWriter.WriteAsync( IReadOnlyCollection<KeySwitch> keySwitches, IObserver<string>? loggingSubject )
        {
            if( Stream == null )
            {
                throw new NullReferenceException( nameof( Stream ) );
            }

            if( loggingSubject != null )
            {
                foreach( var k in keySwitches )
                {
                    loggingSubject.OnNext( k.ToString() );
                }
            }

            await using var writer = new StreamWriter( Stream, FileEncoding, IKeySwitchWriter.DefaultStreamWriterBufferSize, LeaveOpen );
            var yamlText = new YamlKeySwitchExportTranslator().Translate( keySwitches );

            await writer.WriteLineAsync( yamlText.Value );
        }
    }
}
