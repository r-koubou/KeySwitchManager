using System.Collections.Generic;
using System.IO;
using System.Reactive.Subjects;
using System.Text;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches.Translators;

namespace KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches.Helpers
{
    public static class KeySwitchFileReader
    {
        public static IReadOnlyCollection<KeySwitch> Read( Stream stream, Subject<string> loggingSubject, Encoding encoding )
        {
            using var reader = new StreamReader( stream, encoding );
            var jsonText = reader.ReadToEnd();

            var keySwitches = new YamlKeySwitchImportTranslator().Translate( new PlainText( jsonText ) );

            if( loggingSubject.HasObservers )
            {
                foreach( var k in keySwitches )
                {
                    loggingSubject.OnNext( k.ToString() );
                }
            }

            return keySwitches;
        }

        public static IReadOnlyCollection<KeySwitch> Read( Stream stream, Subject<string> loggingSubject )
        {
            return Read( stream, loggingSubject, Encoding.UTF8 );
        }
    }
}
