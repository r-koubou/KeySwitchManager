using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using ClosedXML.Excel;

using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Storage.Spreadsheet.ClosedXml.KeySwitches.Translators;
using KeySwitchManager.Infrastructures.Storage.Spreadsheet.KeySwitches.Helpers;
using KeySwitchManager.UseCase.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Export;

using RkHelper.IO;

namespace KeySwitchManager.Infrastructures.Storage.Spreadsheet.ClosedXml.KeySwitches
{
    public class ClosedXmlExportContentFactory : IExportContentFactory
    {
        public Task<IContent> CreateAsync( IReadOnlyCollection<KeySwitch> keySwitches, CancellationToken cancellationToken = default )
        {
            using var template = new XLWorkbook(
                StreamHelper.GetAssemblyResourceStream<ClosedXmlExportContentFactory>( "Template.xlsx" )
            );

            var translator = new KeySwitchClosedXmlExportTranslator( template );

            using var workbook = translator.Translate( keySwitches );

            // Remove temporary worksheet
            if( workbook.TryGetWorksheet( SpreadsheetConstants.TemplateSheetName, out var removingSheet ) )
            {
                removingSheet.Delete();
            }

            var bufferStream = new MemoryStream();

            workbook.SaveAs( bufferStream );

            return Task.FromResult<IContent>( new BinaryContent( bufferStream.ToArray() ) );
        }
    }
}
