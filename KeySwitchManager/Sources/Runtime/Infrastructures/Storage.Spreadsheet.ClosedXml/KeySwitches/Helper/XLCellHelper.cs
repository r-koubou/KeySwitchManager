using System.Collections.Generic;
using System.Linq;

using ClosedXML.Excel;

using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Domain.KeySwitches.Models.Values;
using KeySwitchManager.Infrastructures.Storage.Spreadsheet.KeySwitches.Helpers;

namespace KeySwitchManager.Infrastructures.Storage.Spreadsheet.ClosedXml.KeySwitches.Helper
{
    // ReSharper disable once InconsistentNaming
    internal static class XLCellHelper
    {
        private const int MidiNoteOnHeaderCellColor = 0xFCE4D2;
        private const int MidiCcHeaderCellColor = 0xA9D7E1;
        private const int MidiPcHeaderCellColor = 0xBADBA5;
        private const int ExtraHeaderCellColor = 0x909090;

        public static void ActivateCellBorder( IXLStyle style )
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

        private static void SetValidationByBetween(
            IXLWorksheet sheet,
            int row,
            int column,
            string inputMessage,
            int minValue,
            int maxValue )
        {
            var cell = sheet.Cell( row, column );
            var d = cell.DataValidation;

            d.InputTitle   = $"{minValue}-{maxValue}";
            d.InputMessage = inputMessage;
            d.Decimal.Between( minValue, maxValue );
        }

        private static void SetValidationByList(
            IXLWorksheet sheet,
            int row,
            int column,
            string inputTitle,
            string inputMessage,
            IXLWorksheet listSheet,
            IXLCell begin,
            IXLCell end )
        {
            var cell = sheet.Cell( row, column );
            var d = cell.DataValidation;

            d.InputTitle   = inputTitle;
            d.InputMessage = inputMessage;
            d.List( listSheet.Range( begin, end ) );
        }

        public static void AdjustRowHeight( IXLWorksheet sheet, int row )
        {
            sheet.Row( row ).AdjustToContents();
        }

        public static void AdjustColumnWidth( IXLWorksheet sheet, int column )
        {
            // TODO: ClosedXml depends GDI+ (Windows).
            // Dont work on Linux, macOS without mono-libgdiplus
            // https://onihusube.hatenablog.com/entry/2020/03/11/013446

            // sheet.Column( column ).AdjustToContents();
        }

        public static void SetDefaultCellStyle( IXLWorksheet sheet, KeySwitch template, int rowCount = 100 )
        {
            var startRow = SpreadsheetConstants.RowDataBegin;
            var startColumn = SpreadsheetConstants.ColumnMidiMessageBegin;

            var row = startRow;

            var allExtraKeys = template.Articulations.SelectMany( x => x.ExtraData.Keys ).Distinct().ToArray();

            for( var r = 0; r < rowCount; r++ )
            {
                var noteMaxColumn = template.Articulations.Max( x => x.MidiNoteOns.Count );
                var ccMaxColumn = template.Articulations.Max( x => x.MidiControlChanges.Count );
                var pcMaxColumn = template.Articulations.Max( x => x.MidiProgramChanges.Count );

                var column = startColumn;
                column = SetDefaultMidiNoteCellStyle( sheet, row, column, noteMaxColumn );
                column = SetDefaultMidiCcCellStyle( sheet, row, column, ccMaxColumn );
                column = SetDefaultMidiPcCellStyle( sheet, row, column, pcMaxColumn );
                _      = SetDefaultExtraCellStyle( sheet, row, column, allExtraKeys );
                row++;
            }

            sheet.ShowGridLines = false;
        }

        #region Cell style for MIDI Channel
        private static void SetMidiChannelCellStyle( IXLWorksheet sheet, int row, int column )
        {
            var listSheet = sheet.Workbook.Worksheet( SpreadsheetConstants.DataListDefinitionSheetName );

            var begin = listSheet.Cell(
                SpreadsheetConstants.RowValidationMidiChannelListBegin,
                SpreadsheetConstants.ColumnValidationMidiChannelList );

            var end = listSheet.Cell(
                SpreadsheetConstants.RowValidationMidiChannelListEnd,
                SpreadsheetConstants.ColumnValidationMidiChannelList );

            SetValidationByList(
                sheet,
                row,
                column,
                "MIDI Channel",
                "Choose from Drop-down list or input number directly(1-16)\n" +
                "\n" +
                "If don’t use MIDI Channel, set Cell value empty. (=ch1)",
                listSheet,
                begin,
                end );

            sheet.Cell( row, column ).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        }
        #endregion


        #region Cell style for MIDI Note
        private static int SetDefaultMidiNoteCellStyle( IXLWorksheet sheet, int row, int column, int count = 1 )
        {
            for( var index = 1; index <= count; index++ )
            {
                SetHeaderCell( sheet, SpreadsheetConstants.RowDataHeader, column + 0, MidiNoteOnHeaderCellColor, SpreadsheetConstants.MakeIndexedHeader( SpreadsheetConstants.HeaderMidiNoteOnChannel, index ) );
                SetHeaderCell( sheet, SpreadsheetConstants.RowDataHeader, column + 1, MidiNoteOnHeaderCellColor, SpreadsheetConstants.MakeIndexedHeader( SpreadsheetConstants.HeaderMidiNote,          index ) );
                SetHeaderCell( sheet, SpreadsheetConstants.RowDataHeader, column + 2, MidiNoteOnHeaderCellColor, SpreadsheetConstants.MakeIndexedHeader( SpreadsheetConstants.HeaderMidiVelocity,      index ) );

                SetMidiChannelCellStyle( sheet, row, column + 0 );
                SetMidiNoteCellStyle( sheet, row, column + 1 );
                SetMidiVelocityCellStyle( sheet, row, column + 2 );
                column += 3;
            }

            return column;
        }

        private static void SetMidiNoteCellStyle( IXLWorksheet sheet, int row, int column )
        {
            var listSheet = sheet.Workbook.Worksheet( SpreadsheetConstants.DataListDefinitionSheetName );

            var begin = listSheet.Cell(
                SpreadsheetConstants.RowValidationMidiNoteListBegin,
                SpreadsheetConstants.ColumnValidationMidiNoteList );

            var end = listSheet.Cell(
                SpreadsheetConstants.RowValidationMidiNoteListEnd,
                SpreadsheetConstants.ColumnValidationMidiNoteList );

            SetValidationByList(
                sheet,
                row,
                column,
                "MIDI Note",
                "Choose from Drop-down list or input number directly(0-127)\n" +
                "\n" +
                "If don’t use MIDI Note, set Cell value empty.",
                listSheet,
                begin,
                end );

            sheet.Cell( row, column ).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        }

        private static void SetMidiVelocityCellStyle( IXLWorksheet sheet, int row, int column )
        {
            SetValidationByBetween(
                sheet,
                row,
                column,
                "If don't use MIDI Note on, set cell value empty.",
                0, 127 );

            sheet.Cell( row, column ).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        }
        #endregion

        #region Cell style for MIDI CC
        private static int SetDefaultMidiCcCellStyle( IXLWorksheet sheet, int row, int column, int count = 1 )
        {
            for( var index = 1; index <= count; index++ )
            {
                SetHeaderCell( sheet, SpreadsheetConstants.RowDataHeader, column + 0, MidiCcHeaderCellColor, SpreadsheetConstants.MakeIndexedHeader( SpreadsheetConstants.HeaderMidiCcChannel, index ) );
                SetHeaderCell( sheet, SpreadsheetConstants.RowDataHeader, column + 1, MidiCcHeaderCellColor, SpreadsheetConstants.MakeIndexedHeader( SpreadsheetConstants.HeaderMidiCc,        index ) );
                SetHeaderCell( sheet, SpreadsheetConstants.RowDataHeader, column + 2, MidiCcHeaderCellColor, SpreadsheetConstants.MakeIndexedHeader( SpreadsheetConstants.HeaderMidiCcValue,   index ) );

                SetMidiChannelCellStyle( sheet, row, column + 0 );
                SetMidiCcCellStyle( sheet, row, column + 1 );
                SetMidiCcCellStyle( sheet, row, column + 2 );
                column += 3;
            }

            return column;
        }

        private static void SetMidiCcCellStyle( IXLWorksheet sheet, int row, int column )
        {
            SetValidationByBetween(
                sheet,
                row,
                column,
                "If don't use CC set cell value empty",
                0, 127 );

            sheet.Cell( row, column ).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        }

        #endregion

        #region Cell style for MIDI PC
        private static int SetDefaultMidiPcCellStyle( IXLWorksheet sheet, int row, int column, int count = 1 )
        {
            for( var index = 1; index <= count; index++ )
            {
                SetHeaderCell( sheet, SpreadsheetConstants.RowDataHeader, column + 0, MidiPcHeaderCellColor, SpreadsheetConstants.MakeIndexedHeader( SpreadsheetConstants.HeaderPcChannel, index ) );
                SetHeaderCell( sheet, SpreadsheetConstants.RowDataHeader, column + 1, MidiPcHeaderCellColor, SpreadsheetConstants.MakeIndexedHeader( SpreadsheetConstants.HeaderPcData, index ) );

                SetMidiChannelCellStyle( sheet, row, column + 0 );
                SetMidiPcCellStyle( sheet, row, column + 1 );
                column += 2;
            }

            return column;
        }

        private static void SetMidiPcCellStyle( IXLWorksheet sheet, int row, int column )
        {
            SetValidationByBetween(
                sheet,
                row,
                column,
                "If don't use PC set cell value empty",
                0, 127 );

            sheet.Cell( row, column ).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        }

        #endregion

        #region Cell style for Extra data
        private static int SetDefaultExtraCellStyle( IXLWorksheet sheet, int row, int column, IEnumerable<ExtraDataKey> extraKeys )
        {
            foreach( var key in extraKeys )
            {
                SetHeaderCell(
                    sheet,
                    SpreadsheetConstants.RowDataHeader,
                    column,
                    ExtraHeaderCellColor,
                    SpreadsheetConstants.HeaderExtraPrefix + key.Value
                );
                SetExtraCellStyle( sheet, row, column );
                column++;
            }

            return column;
        }

        private static void SetExtraCellStyle( IXLWorksheet sheet, int row, int column )
        {
            sheet.Cell( row, column ).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        }

        #endregion

        private static void SetHeaderCell( IXLWorksheet sheet, int row, int column, int color, string text )
        {
            sheet.Cell( row, column ).Value                      = text;
            sheet.Cell( row, column ).Style.Fill.BackgroundColor = XLColor.FromArgb( color );
            sheet.Cell( row, column ).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        }

    }
}