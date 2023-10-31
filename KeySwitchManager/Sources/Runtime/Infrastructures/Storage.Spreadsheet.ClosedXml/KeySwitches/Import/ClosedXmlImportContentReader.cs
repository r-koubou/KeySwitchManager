using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Storage.Spreadsheet.ClosedXml.KeySwitches.Helper;
using KeySwitchManager.Infrastructures.Storage.Spreadsheet.KeySwitches.Translators;
using KeySwitchManager.UseCase.KeySwitches.Import;

namespace KeySwitchManager.Infrastructures.Storage.Spreadsheet.ClosedXml.KeySwitches.Import
{
    public class ClosedXmlImportContentReader : IImportContentReader
    {
        public async Task<IReadOnlyCollection<KeySwitch>> ReadAsync( IContent content, CancellationToken cancellationToken = default )
        {
            await using var stream = await content.GetContentStreamAsync( cancellationToken );
            using var memory = new MemoryStream();

            await stream.CopyToAsync( memory, cancellationToken );

            var workBook = XlsxWorkBookParsingHelper.Parse( stream );
            var translator = new SpreadsheetImportTranslator();

            var result = new List<KeySwitch>();
            result.AddRange( translator.Translate( workBook ) );

            return result;
        }
    }
}
