using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

using ClosedXML.Excel;

using KeySwitchManager.Common.IO;
using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.KeySwitches.Aggregate;
using KeySwitchManager.Gateways.KeySwitches;
using KeySwitchManager.Xlsx.KeySwitches.Translators;

namespace KeySwitchManager.Xlsx.KeySwitches
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

        public bool Save( IReadOnlyCollection<KeySwitch> keySwitch )
        {
            using var template = new XLWorkbook(
                StreamHelper.GetAssemblyResourceStream<XlsxExportingRepository>( "Template.xlsx" )
            );

            var translator = new KeySwitchToXlsx( template );

            using var workbook = translator.Translate( keySwitch );

            workbook.SaveAs( XlsxPath.Path );

            return true;
        }
    }
}