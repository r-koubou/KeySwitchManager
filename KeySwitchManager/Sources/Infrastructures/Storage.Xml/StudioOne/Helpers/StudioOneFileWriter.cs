using System.IO;
using System.Text;

using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Storage.Xml.StudioOne.Translators;

namespace KeySwitchManager.Infrastructures.Storage.Xml.StudioOne.Helpers
{
    public static class StudioOneFileWriter
    {
        public static void Write( Stream stream, KeySwitch keySwitch, Encoding encoding )
        {
            using var writer = new StreamWriter( stream, encoding );
            var xmlText = new StudioOneExportTranslator().Translate( keySwitch );

            writer.WriteLine( xmlText );
        }

        public static void Write( Stream stream, KeySwitch keySwitch )
        {
            Write( stream, keySwitch, Encoding.UTF8 );
        }
    }
}
