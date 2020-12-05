using System.Collections.Generic;

using ClosedXML.Excel;

using KeySwitchManager.Domain.KeySwitches.Aggregate;
using KeySwitchManager.Domain.MidiMessages.Aggregate;
using KeySwitchManager.Domain.Translations;
using KeySwitchManager.Xlsx.KeySwitches.Services;

namespace KeySwitchManager.Xlsx.KeySwitches.Translators.Xlsx.FromKeySwitch
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

                TranslateArticulation( articulation, newWorksheet, row );
                row++;
            }

            TranslateExtra( keySwitch, book );

            newWorksheet.Cell( SpreadsheetConstants.RowGuid, SpreadsheetConstants.ColumnGuid )
                        .Value = keySwitch.Id.Value;
            newWorksheet.Cell( SpreadsheetConstants.RowOutputName, SpreadsheetConstants.ColumnOutputName )
                        .Value = keySwitch.InstrumentName;
        }

        private static void TranslateArticulation( Articulation articulation, IXLWorksheet sheet, int row )
        {
            var column = SpreadsheetConstants.ColumnMidiMessageBegin;

            column = TranslateMidiNoteMapping( articulation.MidiNoteOns, sheet, row, column );
            column = TranslateMidiControlChangeMapping( articulation.MidiControlChanges, sheet, row, column );
            _ = TranslateMidiProgramChangeMapping( articulation.MidiProgramChanges, sheet, row, column );
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

        private static void TranslateExtra( KeySwitch keySwitch, IXLWorkbook book )
        {
#warning TODO: Extra data in worksheet is reserved
        }

        #endregion

    }
}