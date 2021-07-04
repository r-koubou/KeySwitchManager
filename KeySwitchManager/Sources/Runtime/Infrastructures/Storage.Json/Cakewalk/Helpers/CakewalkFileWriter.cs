using System.IO;
using System.Reactive.Subjects;
using System.Text;

using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Storage.Json.Cakewalk.Translators;

namespace KeySwitchManager.Infrastructures.Storage.Json.Cakewalk.Helpers
{
    public static class CakewalkFileWriter
    {
        public static void Write( Stream stream, KeySwitch keySwitch, Subject<string> loggingSubject, Encoding encoding )
        {
            if( loggingSubject.HasObservers )
            {
                loggingSubject.OnNext( keySwitch.ToString() );
            }

            using var writer = new StreamWriter( stream, encoding );
            // TODO すべての要素を束ねた 1 JSONファイルにしたい(Cakewalkは保持できる)
            var jsonText = new CakewalkExportTranslator( true ).Translate( keySwitch );

            writer.WriteLine( jsonText );
        }

        public static void Write( Stream stream, KeySwitch keySwitch, Subject<string> loggingSubject )
        {
            Write( stream, keySwitch, loggingSubject, Encoding.UTF8 );
        }
    }
}
