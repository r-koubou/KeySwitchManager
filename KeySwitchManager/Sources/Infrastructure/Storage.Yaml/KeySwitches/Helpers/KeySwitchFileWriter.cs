using System.Collections.Generic;
using System.IO;
using System.Text;

using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Storage.Yaml.KeySwitches.Translators;

namespace KeySwitchManager.Storage.Yaml.KeySwitches.Helpers
{
    public static class KeySwitchFileWriter
    {
        public static void Write( Stream stream, IReadOnlyCollection<KeySwitch> keySwitches, Encoding encoding )
        {
            using var writer = new StreamWriter( stream, encoding );
            var yamlText = new YamlKeySwitchExportTranslator().Translate( keySwitches );

            writer.WriteLine( yamlText );
        }

        public static void Write( Stream stream, IReadOnlyCollection<KeySwitch> keySwitches )
        {
            Write( stream, keySwitches, Encoding.UTF8 );
        }
    }
}
