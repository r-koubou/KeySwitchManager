using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive.Subjects;
using System.Text;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches.Translators;

namespace KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches
{
    public class YamlKeySwitchReader : IKeySwitchReader
    {
        private Encoding FileEncoding { get; }
        private Stream? Stream { get; set; }

        public bool LeaveOpen { get; }

        public YamlKeySwitchReader( Stream stream ) : this( stream, Encoding.UTF8 ) {}

        public YamlKeySwitchReader( Stream stream, Encoding filEncoding, bool leaveOpen = false )
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

            Stream.Close();
            Stream.Dispose();
            Stream = null;
        }

        public IReadOnlyCollection<KeySwitch> Read( Subject<string>? loggingSubject = null )
        {
            if( Stream == null )
            {
                throw new NullReferenceException( nameof( Stream ) );
            }

            using var reader = new StreamReader( Stream, FileEncoding );
            var jsonText = reader.ReadToEnd();

            var keySwitches = new YamlKeySwitchImportTranslator().Translate( new PlainText( jsonText ) );

            if( loggingSubject is not { HasObservers: true } )
            {
                return keySwitches;
            }

            foreach( var k in keySwitches )
            {
                loggingSubject.OnNext( k.ToString() );
            }

            return keySwitches;
        }
    }
}
