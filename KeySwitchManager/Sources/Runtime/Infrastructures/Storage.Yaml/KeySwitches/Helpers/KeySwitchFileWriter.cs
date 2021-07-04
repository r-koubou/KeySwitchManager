using System.Collections.Generic;
using System.IO;
using System.Reactive.Subjects;
using System.Text;

using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches.Translators;

namespace KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches.Helpers
{
    public static class KeySwitchFileWriter
    {
        public static void Write( Stream stream, IReadOnlyCollection<KeySwitch> keySwitches, Subject<string> loggingSubject, Encoding encoding )
        {
            if( loggingSubject.HasObservers )
            {
                foreach( var k in keySwitches )
                {
                    loggingSubject.OnNext( k.ToString() );
                }
            }

            using var writer = new StreamWriter( stream, encoding );
            var yamlText = new YamlKeySwitchExportTranslator().Translate( keySwitches );

            writer.WriteLine( yamlText );
        }

        public static void Write( Stream stream, IReadOnlyCollection<KeySwitch> keySwitches, Subject<string> loggingSubject )
        {
            Write( stream, keySwitches, loggingSubject, Encoding.UTF8 );
        }
    }
}
