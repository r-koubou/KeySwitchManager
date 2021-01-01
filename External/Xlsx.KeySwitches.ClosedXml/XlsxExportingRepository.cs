using System;
using System.Collections.Generic;

using ClosedXML.Excel;

using KeySwitchManager.Common.IO;
using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.KeySwitches.Aggregate;
using KeySwitchManager.Gateways.KeySwitches;
using KeySwitchManager.Xlsx.KeySwitches.ClosedXml.Helpers;
using KeySwitchManager.Xlsx.KeySwitches.ClosedXml.Translators;
using KeySwitchManager.Xlsx.KeySwitches.Helpers;

namespace KeySwitchManager.Xlsx.KeySwitches.ClosedXml
{
    public class XlsxExportingRepository : IKeySwitchSpreadSheetRepository
    {
        public IPath XlsxPath { get; }

        public XlsxExportingRepository( IPath xlsxFilePath )
        {
            XlsxPath = xlsxFilePath;
        }

        public void Dispose()
        {}

        public IReadOnlyCollection<KeySwitch> Load()
        {
            throw new NotSupportedException();
        }

        public bool Save( IReadOnlyCollection<KeySwitch> keySwitches )
        {
            using var template = new XLWorkbook(
                StreamHelper.GetAssemblyResourceStream<XlsxExportingRepository>( "Template.xlsx" )
            );

            var translator = new KeySwitchToXlsx( template );

            using var workbook = translator.Translate( keySwitches );

            // Remove temporary worksheet
            if( workbook.TryGetWorksheet( SpreadsheetConstants.TemplateSheetName, out var removingSheet ) )
            {
                removingSheet.Delete();
            }

            workbook.SaveAs( XlsxPath.Path );

            return true;
        }
    }
}