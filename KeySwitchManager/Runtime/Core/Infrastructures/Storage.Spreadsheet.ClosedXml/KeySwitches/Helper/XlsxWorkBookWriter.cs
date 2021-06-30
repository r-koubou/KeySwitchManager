using System.Collections.Generic;

using ClosedXML.Excel;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Storage.KeySwitches;
using KeySwitchManager.Infrastructures.Storage.Spreadsheet.ClosedXml.KeySwitches.Translators;
using KeySwitchManager.Infrastructures.Storage.Spreadsheet.KeySwitches.Helpers;

using RkHelper.IO;

namespace KeySwitchManager.Infrastructures.Storage.Spreadsheet.ClosedXml.KeySwitches.Helper
{
    public static class XlsxWorkBookWriter
    {
        public static void Write( IReadOnlyCollection<KeySwitch> keySwitches, FilePath target, IStorageAccessListener listener )
        {
            using var template = new XLWorkbook(
                StreamHelper.GetAssemblyResourceStream<ClosedXmlFileSaveRepository>( "Template.xlsx" )
            );

            var translator = new KeySwitchClosedXmlExportTranslator( template );

            using var workbook = translator.Translate( keySwitches );

            // Remove temporary worksheet
            if( workbook.TryGetWorksheet( SpreadsheetConstants.TemplateSheetName, out var removingSheet ) )
            {
                removingSheet.Delete();
            }

            listener.OnWriteAccess( keySwitches, target );

            workbook.SaveAs( target.Path );
        }
    }
}