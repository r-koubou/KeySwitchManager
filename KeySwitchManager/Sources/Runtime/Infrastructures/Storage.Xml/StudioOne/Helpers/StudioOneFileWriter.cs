using System.IO;
using System.Reactive.Subjects;
using System.Text;

using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Storage.Xml.StudioOne.Translators;

namespace KeySwitchManager.Infrastructures.Storage.Xml.StudioOne.Helpers
{
    public static class StudioOneFileWriter
    {
        public static void Write( Stream stream, KeySwitch keySwitch, Subject<string> loggingSubject, Encoding encoding )
        {
            loggingSubject.OnNext( keySwitch.ToString() );

            using var writer = new StreamWriter( stream, encoding );
            var xmlText = new StudioOneExportTranslator().Translate( keySwitch );

            writer.WriteLine( xmlText );
        }

        public static void Write( Stream stream, KeySwitch keySwitch, Subject<string> loggingSubject )
        {
            Write( stream, keySwitch, loggingSubject, Encoding.UTF8 );
        }
    }
}
