using System.Collections.Generic;
using System.IO;
using System.Text;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructure.Storage.Json.KeySwitches.Translators;

namespace KeySwitchManager.Infrastructure.Storage.Json.KeySwitches.Helpers
{
    public static class KeySwitchFileReader
    {
        public static IReadOnlyCollection<KeySwitch> Read( Stream stream, Encoding encoding )
        {
            using var reader = new StreamReader( stream, encoding );
            var jsonText = reader.ReadToEnd();

            return new KeySwitchImportTranslator().Translate( new PlainText( jsonText ) );
        }

        public static IReadOnlyCollection<KeySwitch> Read( Stream stream )
        {
            return Read( stream, Encoding.UTF8 );
        }
    }
}
