using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using ClosedXML.Excel;

using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.KeySwitches;
using KeySwitchManager.Gateways.KeySwitches;
using KeySwitchManager.Xlsx.KeySwitches.ClosedXml.Translators;
using KeySwitchManager.Xlsx.KeySwitches.Helpers;

using RkHelper.IO;

namespace KeySwitchManager.Xlsx.KeySwitches.ClosedXml
{
    public class XlsxExportingRepository : IKeySwitchSpreadSheetRepository
    {
        public DirectoryPath XlsxDirectory { get; }

        public XlsxExportingRepository( DirectoryPath xlsxFileDirectory )
        {
            XlsxDirectory = xlsxFileDirectory;
        }

        public void Dispose()
        {}

        public IReadOnlyCollection<KeySwitch> Load()
        {
            throw new NotSupportedException();
        }

        public bool Save( IReadOnlyCollection<KeySwitch> keySwitches )
        {
            var productList = new Dictionary<string, List<KeySwitch>>();

            foreach( var keySwitch in keySwitches )
            {
                if( !productList.ContainsKey( keySwitch.ProductName.Value ) )
                {
                    productList[ keySwitch.ProductName.Value ] = new List<KeySwitch>();
                }
                productList[ keySwitch.ProductName.Value ].Add( keySwitch );
            }

            foreach( var product in productList.Keys )
            {
                using var template = new XLWorkbook(
                    StreamHelper.GetAssemblyResourceStream<KeySwitchInfo>( "Template.xlsx" )
                );

                var translator = new KeySwitchToXlsx( template );

                using var workbook = translator.Translate(
                    productList[ product ]
                       .OrderBy( x => x.InstrumentName.Value ).ToArray()
                );

                // Remove temporary worksheet
                if( workbook.TryGetWorksheet( SpreadsheetConstants.TemplateSheetName, out var removingSheet ) )
                {
                    removingSheet.Delete();
                }

                var outputFilePath = Path.Combine( XlsxDirectory.Path, $"{product}.xlsx" );

                if( !XlsxDirectory.Exists )
                {
                    Directory.CreateDirectory( XlsxDirectory.Path );
                }

                workbook.SaveAs( outputFilePath );
            }

            return true;
        }
    }
}