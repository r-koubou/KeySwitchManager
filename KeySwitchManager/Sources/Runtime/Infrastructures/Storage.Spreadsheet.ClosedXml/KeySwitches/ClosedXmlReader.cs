using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Storage.Spreadsheet.ClosedXml.KeySwitches.Helper;
using KeySwitchManager.Infrastructures.Storage.Spreadsheet.KeySwitches.Translators;

using RkHelper.IO;

namespace KeySwitchManager.Infrastructures.Storage.Spreadsheet.ClosedXml.KeySwitches
{
    public class ClosedXmlReader : IKeySwitchReader
    {
        private const int InitialBufferSize = 1024 * 64;

        public bool LeaveOpen { get; }

        private Stream? Stream { get; set; }

        public ClosedXmlReader( Stream target, bool leaveOpen = false )
        {
            Stream    = target ?? throw new ArgumentNullException( nameof( target ) );
            LeaveOpen = leaveOpen;
        }

        public void Dispose()
        {
            if( LeaveOpen || Stream == null )
            {
                return;
            }

            Stream?.Flush();
            Stream?.Close();
            Stream?.Dispose();
            Stream = null;
        }

        async Task<IReadOnlyCollection<KeySwitch>> IKeySwitchReader.ReadAsync( IObserver<string>? loggingSubject )
        {
            if( Stream == null )
            {
                throw new InvalidOperationException( $"{nameof( Stream )} is null" );
            }

            using var memory = new MemoryStream( InitialBufferSize );

            await StreamHelper.ReadAllAndWriteAsync( Stream, memory );
            var xlsxBytes = memory.ToArray();

            var workBook = XlsxWorkBookParsingHelper.Parse( xlsxBytes );
            var translator = new SpreadsheetImportTranslator();

            var result = new List<KeySwitch>();
            result.AddRange( translator.Translate( workBook ) );

            if( loggingSubject == null )
            {
                return result;
            }

            foreach( var x in result )
            {
                loggingSubject.OnNext( x.ToString() );
            }

            return result;
        }
    }
}
