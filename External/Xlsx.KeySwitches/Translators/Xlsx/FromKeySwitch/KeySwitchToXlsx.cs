using System;
using System.Collections.Generic;

using ClosedXML.Excel;

using KeySwitchManager.Domain.KeySwitches.Aggregate;
using KeySwitchManager.Domain.MidiMessages.Aggregate;
using KeySwitchManager.Domain.Translations;
using KeySwitchManager.Xlsx.KeySwitches.Models;
using KeySwitchManager.Xlsx.KeySwitches.Services;

namespace KeySwitchManager.Xlsx.KeySwitches.Translators.Xlsx.FromKeySwitch
{
    public class KeySwitchToXlsx : IDataTranslator<IReadOnlyCollection<KeySwitch>, XLWorkbook>
    {

        private const int DefaultMaxDataRowCount = 100;

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

            FillWorkSheetCellFormat( keySwitch, newWorksheet );
        }

        private static void TranslateArticulation( Articulation articulation, IXLWorksheet sheet, int row )
        {
            var column = SpreadsheetConstants.ColumnMidiMessageBegin;

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

                var headerRow = SpreadsheetConstants.RowDataHeader;
                var headerCol = column;

                sheet.Cell( headerRow, headerCol + 0 ).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                sheet.Cell( headerRow, headerCol + 0 ).Value                      = $"{SpreadsheetConstants.HeaderMidiNote}{index}";
                sheet.Cell( headerRow, headerCol + 0 ).Style.Fill.BackgroundColor = XLColor.FromArgb( 0xFCE4D2 );
                EnableRuledLineTo( sheet.Cell( headerRow, headerCol + 0 ).Style );

                sheet.Cell( headerRow, headerCol + 1 ).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                sheet.Cell( headerRow, headerCol + 1 ).Value                      = $"{SpreadsheetConstants.HeaderMidiVelocity}{index}";
                sheet.Cell( headerRow, headerCol + 1 ).Style.Fill.BackgroundColor = XLColor.FromArgb( 0xFCE4D2 );
                EnableRuledLineTo( sheet.Cell( headerRow, headerCol + 1 ).Style );

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

                var headerRow = SpreadsheetConstants.RowDataHeader;
                var headerCol = column;

                sheet.Cell( headerRow, headerCol + 0 ).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                sheet.Cell( headerRow, headerCol + 0 ).Value                      = $"{SpreadsheetConstants.HeaderMidiCc}{index}";
                sheet.Cell( headerRow, headerCol + 0 ).Style.Fill.BackgroundColor = XLColor.FromArgb( 0xA9D7E1 );
                EnableRuledLineTo( sheet.Cell( headerRow, headerCol + 0 ).Style );

                sheet.Cell( headerRow, headerCol + 1 ).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                sheet.Cell( headerRow, headerCol + 1 ).Value                      = $"{SpreadsheetConstants.HeaderMidiCcValue}{index}";
                sheet.Cell( headerRow, headerCol + 1 ).Style.Fill.BackgroundColor = XLColor.FromArgb( 0xA9D7E1 );
                EnableRuledLineTo( sheet.Cell( headerRow, headerCol + 1 ).Style );


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
                var pcChannel= message.Channel.Value;
                var pcNumber = message.DataByte1.Value;

                var headerRow = SpreadsheetConstants.RowDataHeader;
                var headerCol = column;

                sheet.Cell( headerRow, headerCol + 0 ).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                sheet.Cell( headerRow, headerCol + 0 ).Value                      = $"{SpreadsheetConstants.HeaderPcChannel}{index}";
                sheet.Cell( headerRow, headerCol + 0 ).Style.Fill.BackgroundColor = XLColor.FromArgb( 0xF28337 );
                EnableRuledLineTo( sheet.Cell( headerRow, headerCol + 0 ).Style );

                sheet.Cell( headerRow, headerCol + 1 ).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                sheet.Cell( headerRow, headerCol + 1 ).Value                      = $"{SpreadsheetConstants.HeaderPcData}{index}";
                sheet.Cell( headerRow, headerCol + 1 ).Style.Fill.BackgroundColor = XLColor.FromArgb( 0xF28337 );
                EnableRuledLineTo( sheet.Cell( headerRow, headerCol + 1 ).Style );


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

        #endregion

        #region Fill Excel cell style, data validate

        private static void FillWorkSheetCellFormat(
            KeySwitch keySwitch,
            IXLWorksheet sheet )
        {
            var column = SpreadsheetConstants.ColumnMidiMessageBegin;
            column = FillMidiNoteValidation( keySwitch, sheet, column );
            column = FillMidiCcValidation( keySwitch, sheet, column );
            column = FillMidiPcValidation( keySwitch, sheet, column );

            sheet.ShowGridLines = false;

        }

        private static int FillMidiNoteValidation(
            KeySwitch keySwitch,
            IXLWorksheet sheet,
            int startColumnIndex )
        {
            var row = SpreadsheetConstants.RowDataBegin;

            var maxCount = 0;

            foreach( var articulation in keySwitch.Articulations )
            {
                maxCount = Math.Max( maxCount, articulation.MidiNoteOns.Count );
            }

            for( var r = 0; r < DefaultMaxDataRowCount; r++ )
            {
                var column = startColumnIndex;

                for( var i = 0; i < maxCount; i++ )
                {
                    SetMidiNoteValidationData( sheet, row + r, column + 0 );
                    SetMidiVelocityValidationData( sheet, row + r, column + 1 );
                    column += 2;
                }
            }

            return startColumnIndex + ( maxCount * 2 );
        }

        private static int FillMidiCcValidation(
            KeySwitch keySwitch,
            IXLWorksheet sheet,
            int startColumnIndex )
        {
            var row = SpreadsheetConstants.RowDataBegin;

            var maxCount = 0;

            foreach( var articulation in keySwitch.Articulations )
            {
                maxCount = Math.Max( maxCount, articulation.MidiControlChanges.Count );
            }

            for( var r = 0; r < DefaultMaxDataRowCount; r++ )
            {
                var column = startColumnIndex;

                for( var i = 0; i < maxCount; i++ )
                {
                    SetMidiCcValidationData( sheet, row + r, column + 0 ); // cc number
                    SetMidiCcValidationData( sheet, row + r, column + 1 ); // cc value
                    column += 2;
                }
            }

            return startColumnIndex + ( maxCount * 2 );
        }

        private static int FillMidiPcValidation(
            KeySwitch keySwitch,
            IXLWorksheet sheet,
            int startColumnIndex )
        {
            var row = SpreadsheetConstants.RowDataBegin;

            var maxCount = 0;

            foreach( var articulation in keySwitch.Articulations )
            {
                maxCount = Math.Max( maxCount, articulation.MidiProgramChanges.Count );
            }

            for( var r = 0; r < DefaultMaxDataRowCount; r++ )
            {
                var column = startColumnIndex;

                for( var i = 0; i < maxCount; i++ )
                {
                    SetMidiPcValueValidationData( sheet, row + r, column + 0 ); // pc channel
                    SetMidiPcValueValidationData( sheet, row + r, column + 1 ); // pc data
                    column += 2;
                }
            }

            return startColumnIndex + ( maxCount * 2 );
        }

        #region Data validation
        private static void SetMidiNoteValidationData( IXLWorksheet sheet, int row, int column )
        {
            IXLWorkbook owner = sheet.Workbook;

            var listSheet = owner.Worksheet( SpreadsheetConstants.DataListDefinitionSheetName );

            var cell = sheet.Cell( row, column );

            var d = cell.DataValidation;
            d.InputTitle = "MIDI Note";
            d.InputMessage = "Choose from Drop-down list or input number directly(0-127)\n" +
                             "\n" +
                             "If don’t use MIDI Note, set Cell value empty.";

            var validateListBegin = listSheet.Cell(
                SpreadsheetConstants.RowValidationMidiNoteListBegin,
                SpreadsheetConstants.ColumnValidationMidiNoteList );

            var validateListEnd = listSheet.Cell(
                SpreadsheetConstants.RowValidationMidiNoteListEnd,
                SpreadsheetConstants.ColumnValidationMidiNoteList );

            d.List( listSheet.Range( validateListBegin, validateListEnd ) );

            EnableRuledLineTo( cell.Style );
        }

        private static void SetMidiVelocityValidationData( IXLWorksheet sheet, int row, int column )
        {
            IXLWorkbook owner = sheet.Workbook;

            var cell = sheet.Cell( row, column );

            var d = cell.DataValidation;
            d.InputTitle   = "0-127";
            d.InputMessage = "If don't use MIDI Note on, set cell value empty.";
            d.Decimal.Between( 0, 127 );

            EnableRuledLineTo( cell.Style );
        }

        private static void SetMidiCcValidationData( IXLWorksheet sheet, int row, int column )
        {
            IXLWorkbook owner = sheet.Workbook;

            var cell = sheet.Cell( row, column );

            var d = cell.DataValidation;
            d.InputTitle   = "0-127";
            d.InputMessage = "If don't use CC set cell value empty";
            d.Decimal.Between( 0, 127 );

            EnableRuledLineTo( cell.Style );
        }

        private static void SetMidiPcValueValidationData( IXLWorksheet sheet, int row, int column )
        {
            IXLWorkbook owner = sheet.Workbook;

            var cell = sheet.Cell( row, column );

            var d = cell.DataValidation;
            d.InputTitle   = "0-127";
            d.InputMessage = "If don't use Program Change, set cell value empty.";
            d.Decimal.Between( 0, 127 );

            EnableRuledLineTo( cell.Style );

        }
        #endregion

        private static void EnableRuledLineTo( IXLStyle style )
        {
            style.Border.TopBorder         = XLBorderStyleValues.Thin;
            style.Border.TopBorderColor    = XLColor.Black;
            style.Border.BottomBorder      = XLBorderStyleValues.Thin;
            style.Border.BottomBorderColor = XLColor.Black;
            style.Border.RightBorder       = XLBorderStyleValues.Thin;
            style.Border.RightBorderColor  = XLColor.Black;
            style.Border.LeftBorder        = XLBorderStyleValues.Thin;
            style.Border.LeftBorderColor   = XLColor.Black;
        }

        #endregion

    }
}