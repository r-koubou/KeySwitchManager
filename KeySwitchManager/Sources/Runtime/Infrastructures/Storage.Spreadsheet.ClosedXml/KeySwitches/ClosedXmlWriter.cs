using System;
using System.Collections.Generic;
using System.IO;

using ClosedXML.Excel;

using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Storage.Spreadsheet.ClosedXml.KeySwitches.Translators;
using KeySwitchManager.Infrastructures.Storage.Spreadsheet.KeySwitches.Helpers;

using RkHelper.IO;

namespace KeySwitchManager.Infrastructures.Storage.Spreadsheet.ClosedXml.KeySwitches
{
    public class ClosedXmlWriter : IKeySwitchWriter
    {
        public bool LeaveOpen { get; }
        private Stream? Stream { get; set; }

        public ClosedXmlWriter( Stream target, bool leaveOpen = false )
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

        public void Write( IReadOnlyCollection<KeySwitch> keySwitches, IObserver<string>? loggingSubject = null )
        {
            using var template = new XLWorkbook(
                StreamHelper.GetAssemblyResourceStream<ClosedXmlWriter>( "Template.xlsx" )
            );

            var translator = new KeySwitchClosedXmlExportTranslator( template );

            using var workbook = translator.Translate( keySwitches );

            // Remove temporary worksheet
            if( workbook.TryGetWorksheet( SpreadsheetConstants.TemplateSheetName, out var removingSheet ) )
            {
                removingSheet.Delete();
            }

            if( loggingSubject != null )
            {
                foreach( var k in keySwitches )
                {
                    loggingSubject.OnNext( k.ToString() );
                }
            }

            workbook.SaveAs( Stream );
        }
    }
}
