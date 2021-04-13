using System.IO;
using System.Text;

using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructure.Storage.Xml.Cubase.Translators;

namespace KeySwitchManager.Infrastructure.Storage.Xml.Cubase.Helpers
{
    public static class CubaseFileWriter
    {
        public static void Write( Stream stream, KeySwitch keySwitch, Encoding encoding )
        {
            using var writer = new StreamWriter( stream, encoding );
            var xmlText = new CubaseExportTranslator().Translate( keySwitch );

            writer.WriteLine( xmlText );
        }

        public static void Write( Stream stream, KeySwitch keySwitch )
        {
            Write( stream, keySwitch, Encoding.UTF8 );
        }
    }
}
