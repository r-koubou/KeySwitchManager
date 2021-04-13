using System.IO;
using System.Text;

using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructure.Storage.Json.Cakewalk.Translators;

namespace KeySwitchManager.Infrastructure.Storage.Json.Cakewalk.Helpers
{
    public static class CakewalkFileWriter
    {
        public static void Write( Stream stream, KeySwitch keySwitch, Encoding encoding )
        {
            using var writer = new StreamWriter( stream, encoding );
            // TODO すべての要素を束ねた 1 JSONファイルにしたい(Cakewalkは保持できる)
            var jsonText = new CakewalkExportTranslator( true ).Translate( keySwitch );

            writer.WriteLine( jsonText );
        }

        public static void Write( Stream stream, KeySwitch keySwitch )
        {
            Write( stream, keySwitch, Encoding.UTF8 );
        }
    }
}
