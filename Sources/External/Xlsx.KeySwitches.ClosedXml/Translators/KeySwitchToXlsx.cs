using System;
using System.Collections.Generic;

using ClosedXML.Excel;

using KeySwitchManager.Domain.KeySwitches.Aggregate;
using KeySwitchManager.Domain.MidiMessages.Aggregate;
using KeySwitchManager.Domain.Translations;
using KeySwitchManager.Xlsx.KeySwitches.ClosedXml.Helpers;
using KeySwitchManager.Xlsx.KeySwitches.Helpers;

namespace KeySwitchManager.Xlsx.KeySwitches.ClosedXml.Translators
{
    public class KeySwitchToXlsx : IDataTranslator<IReadOnlyCollection<KeySwitch>, XLWorkbook>
    {
        private XLWorkbook Template { get; }

        public KeySwitchToXlsx( XLWorkbook template )
        {
            Template = template;
        }

        public XLWorkbook Translate( IReadOnlyCollection<KeySwitch> keySwitches )
        {
            foreach( var k in keySwitches )
            {
                TranslateWorkSheet( k, Template );
            }

            return Template;
        }

        #region Translation
        private static void TranslateWorkSheet( KeySwitch keySwitch, XLWorkbook book )
        {
            var row = SpreadsheetConstants.RowDataBegin;
            var column = SpreadsheetConstants.ColumnDataBegin;

            var worksheetName = keySwitch.InstrumentName.Value;

            // xlsx worksheet name length must be <= 31
            if( worksheetName.Length > 31 )
            {
                worksheetName = worksheetName.Substring( 0, 31 );
            }

            var newWorksheet = book.Worksheet( SpreadsheetConstants.TemplateSheetName )
                                   .CopyTo( worksheetName, book.Worksheets.Count - 1 );

            XLCellHelper.SetDefaultCellStyle( newWorksheet, keySwitch );

            foreach( var articulation in keySwitch.Articulations )
            {
                var col = TranslateArticulation( keySwitch, articulation, newWorksheet, row );
                row++;
                column = Math.Max( column, col );
            }

            newWorksheet.Cell( SpreadsheetConstants.RowGuid, SpreadsheetConstants.ColumnGuid )
                        .Value = keySwitch.Id.Value;
            newWorksheet.Cell( SpreadsheetConstants.RowOutputName, SpreadsheetConstants.ColumnOutputName )
                        .Value = keySwitch.InstrumentName;

            // Draw cell border line
            var style = newWorksheet.Range(
                SpreadsheetConstants.RowDataHeader,
                SpreadsheetConstants.ColumnDataBegin,
                row - 1,
                column - 1
            ).Style;

            XLCellHelper.ActivateCellBorder( style );
        }

        private static int TranslateArticulation( KeySwitch keySwitch, Articulation articulation, IXLWorksheet sheet, int row )
        {
            // Articulation name
            var column = SpreadsheetConstants.ColumnDataBegin;
            sheet.Cell( row, column ).Value                      = articulation.ArticulationName.Value;
            sheet.Cell( row, column ).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            // MIDI data
            column = SpreadsheetConstants.ColumnMidiMessageBegin;

            column = TranslateMidiNoteMapping( articulation.MidiNoteOns, sheet, row, column );
            column = TranslateMidiControlChangeMapping( articulation.MidiControlChanges, sheet, row, column );
            column = TranslateMidiProgramChangeMapping( articulation.MidiProgramChanges, sheet, row, column );

            // Extra Data
            var extraDataTranslator = new ExtraDataTranslator( keySwitch );

            extraDataTranslator.Translate(
                sheet,
                SpreadsheetConstants.RowDataHeader,
                SpreadsheetConstants.RowDataBegin,
                column
            );

            return column;
        }

        private static int TranslateMidiNoteMapping(
            IEnumerable<IMidiMessage> midiMessages,
            IXLWorksheet sheet,
            int row,
            int startColumnIndex )
        {
            var t = new MidiNoteOnMessageTranslator( startColumnIndex );

            return t.Translate(
                midiMessages,
                sheet,
                SpreadsheetConstants.RowDataHeader,
                row,
                string.Empty,
                SpreadsheetConstants.HeaderMidiNote,
                SpreadsheetConstants.HeaderMidiVelocity,
                TranslateMidiMessageType.Data2 | TranslateMidiMessageType.Data3
            );
        }

        private static int TranslateMidiControlChangeMapping(
            IEnumerable<IMidiMessage> midiMessages,
            IXLWorksheet sheet,
            int row,
            int startColumnIndex )
        {
            var t = new MidiMessageTranslator( startColumnIndex );

            return t.Translate(
                midiMessages,
                sheet,
                SpreadsheetConstants.RowDataHeader,
                row,
                string.Empty,
                SpreadsheetConstants.HeaderMidiCc,
                SpreadsheetConstants.HeaderMidiCcValue,
                TranslateMidiMessageType.Data2 | TranslateMidiMessageType.Data3
            );
        }

        private static int TranslateMidiProgramChangeMapping(
            IEnumerable<IMidiMessage> midiMessages,
            IXLWorksheet sheet,
            int row,
            int startColumnIndex )
        {
            var t = new MidiMessageTranslator( startColumnIndex );

            return t.Translate(
                midiMessages,
                sheet,
                SpreadsheetConstants.RowDataHeader,
                row,
                SpreadsheetConstants.HeaderPcChannel,
                SpreadsheetConstants.HeaderPcData,
                string.Empty,
                TranslateMidiMessageType.Data1 | TranslateMidiMessageType.Data2
            );
        }

        #endregion
    }
}