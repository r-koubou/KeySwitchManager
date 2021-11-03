using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive.Subjects;
using System.Text;

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

            Stream.Flush();
            Stream.Close();
            Stream.Dispose();
            Stream = null;
        }

        public void Write( IReadOnlyCollection<KeySwitch> keySwitches, Subject<string>? loggingSubject = null )
        {
            if( Stream == null )
            {
                throw new NullReferenceException( nameof( Stream ) );
            }

            if( loggingSubject is { HasObservers: true } )
            {
                foreach( var k in keySwitches )
                {
                    loggingSubject.OnNext( k.ToString() );
                }
            }

            using var writer = new StreamWriter( Stream, FileEncoding, 512, false );
            var yamlText = new YamlKeySwitchExportTranslator().Translate( keySwitches );

            writer.WriteLine( yamlText );
        }
    }
}
