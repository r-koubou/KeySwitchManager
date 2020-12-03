using System.Collections.Generic;

using ClosedXML.Excel;

using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.KeySwitches.Aggregate;
using KeySwitchManager.Domain.MidiMessages.Aggregate;
using KeySwitchManager.Domain.Translations;
using KeySwitchManager.Xlsx.KeySwitches.Models;
using KeySwitchManager.Xlsx.KeySwitches.Services;

namespace KeySwitchManager.Xlsx.KeySwitches.Translators
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

        private static void TranslateWorkSheet( KeySwitch keySwitch, XLWorkbook book )
        {
            var row = SpreadsheetConstants.StartRowIndex;

            var newWorksheet = book.Worksheet( SpreadsheetConstants.TemplateSheetName )
                                     .CopyTo( keySwitch.InstrumentName.Value, book.Worksheets.Count - 1 );


            foreach( var articulation in keySwitch.Articulations )
            {

                TranslateArticulation( articulation, newWorksheet, row );
                row++;
            }

            TranslateExtra( keySwitch, book );
        }

        private static void TranslateArticulation( Articulation articulation, IXLWorksheet sheet, int row )
        {
            var column = SpreadsheetConstants.StartMidiMessageColumnIndex;

            column = TranslateMidiNoteMapping( articulation.MidiNoteOns, sheet, row, column );
            column = TranslateMidiControlChangeMapping( articulation.MidiControlChanges, sheet, row, column );
            column = TranslateMidiProgramChangeMapping( articulation.MidiProgramChanges, sheet, row, column );
        }

        private static int TranslateMidiNoteMapping(
            IEnumerable<IMidiMessage> midiMessages,
            IXLWorksheet sheet,
            int row,
            int startColumnIndex )
        {
            var noteNameList = MidiNoteNumberCell.GetNoteNameList();
            var column = startColumnIndex;
            var index = 1;

            foreach( var message in midiMessages )
            {
                var note = noteNameList[ message.DataByte1.Value ];
                var velocity = message.DataByte2.Value;

                SetupMidiNoteValidationData( sheet, row, column );

                sheet.Cell( row, column + 0 ).SetValue( note );
                sheet.Cell( row, column + 1 ).SetValue( velocity );
                column += 2;
                index++;
            }

            return column;
        }

        private static int TranslateMidiControlChangeMapping(
            IEnumerable<IMidiMessage> midiMessages,
            IXLWorksheet sheet,
            int row,
            int startColumnIndex )
        {
            var column = startColumnIndex;
            var index = 1;

            foreach( var message in midiMessages )
            {
                var ccNumber= message.DataByte1.Value;
                var ccValue = message.DataByte2.Value;

                sheet.Cell( row, column + 0 ).SetValue( ccNumber );
                sheet.Cell( row, column + 1 ).SetValue( ccValue );

                column += 2;
                index++;
            }

            return column;
        }

        private static int TranslateMidiProgramChangeMapping(
            IEnumerable<IMidiMessage> midiMessages,
            IXLWorksheet sheet,
            int row,
            int startColumnIndex )
        {
            var column = startColumnIndex;
            var index = 1;

            foreach( var message in midiMessages )
            {
                var pcChannel= message.DataByte1.Value;
                var pcNumber = message.DataByte2.Value;

                sheet.Cell( row, column + 0 ).SetValue( pcChannel );
                sheet.Cell( row, column + 1 ).SetValue( pcNumber );

                column += 2;
                index++;
            }

            return column;
        }

        private static void TranslateExtra( KeySwitch keySwitch, IXLWorkbook book )
        {
            #warning TODO: Extra data in worksheet is reserved
        }

        private static void SetupMidiNoteValidationData( IXLWorksheet sheet, int row, int column )
        {
            IXLWorkbook owner = sheet.Workbook;

        var listSheet = owner.Worksheet( SpreadsheetConstants.TemplateSheetName );

            var d = sheet.Cell( row, column ).DataValidation;
            d.InputTitle = "MIDI Note";
            d.InputMessage = "Choose from Drop-down list or input number directly(0-127)\n" +
                             "\n" +
                             "If don’t use MIDI Note, set Cell value empty.";

            var validateListBegin = listSheet.Cell(
                SpreadsheetConstants.ValidationMidiNoteListColumn,
                SpreadsheetConstants.ValidationMidiNoteListRowBegin );

            var validateListEnd = listSheet.Cell(
                SpreadsheetConstants.ValidationMidiNoteListColumn,
                SpreadsheetConstants.ValidationMidiNoteListRowEnd );

            d.List( listSheet.Range( validateListBegin, validateListEnd ) );
        }
    }
}