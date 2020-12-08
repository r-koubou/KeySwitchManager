using System.Collections.Generic;

using ClosedXML.Excel;

using KeySwitchManager.Domain.KeySwitches.Aggregate;
using KeySwitchManager.Domain.MidiMessages.Aggregate;
using KeySwitchManager.Domain.Translations;
using KeySwitchManager.Xlsx.KeySwitches.Services;

namespace KeySwitchManager.Xlsx.KeySwitches.Translators.FromKeySwitch
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

            var newWorksheet = book.Worksheet( SpreadsheetConstants.TemplateSheetName )
                                   .CopyTo( keySwitch.InstrumentName.Value, book.Worksheets.Count - 1 );

            XLCellHelper.SetDefaultCellStyle( newWorksheet, keySwitch );

            foreach( var articulation in keySwitch.Articulations )
            {
                _ = TranslateArticulation( keySwitch, articulation, newWorksheet, row );
                row++;
            }

            newWorksheet.Cell( SpreadsheetConstants.RowGuid, SpreadsheetConstants.ColumnGuid )
                        .Value = keySwitch.Id.Value;
            newWorksheet.Cell( SpreadsheetConstants.RowOutputName, SpreadsheetConstants.ColumnOutputName )
                        .Value = keySwitch.InstrumentName;
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