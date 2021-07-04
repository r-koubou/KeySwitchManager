using System;
using System.Collections.Generic;
using System.Linq;

using ClosedXML.Excel;

using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Domain.KeySwitches.Models.Aggregations;
using KeySwitchManager.Domain.MidiMessages.Models.Aggregations;
using KeySwitchManager.Infrastructures.Storage.Spreadsheet.ClosedXml.KeySwitches.Helper;
using KeySwitchManager.Infrastructures.Storage.Spreadsheet.KeySwitches.Helpers;

namespace KeySwitchManager.Infrastructures.Storage.Spreadsheet.ClosedXml.KeySwitches.Translators.Helpers
{
    internal static class KeySwitchToClosedXmlModelHelper
    {
        /// <summary>
        /// Minimum number of blank cell rows to generate
        /// </summary>
        private const int MinimumBorderRowCount = 32;

        public static XLWorkbook Translate( IReadOnlyCollection<KeySwitch> keySwitches, XLWorkbook template, int minimumBorderRowCount = MinimumBorderRowCount )
        {
            foreach( var k in keySwitches )
            {
                TranslateWorkSheet( k, template, minimumBorderRowCount );
            }

            return template;
        }

        #region Translation
        private static void TranslateWorkSheet( KeySwitch keySwitch, XLWorkbook book, int minimumBorderRowCount )
        {
            var row = SpreadsheetConstants.RowDataBegin;
            var column = SpreadsheetConstants.ColumnDataBegin;

            var worksheetName = keySwitch.InstrumentName.Value;

            // if sheet name is already created, numbering to workSheetName
            worksheetName = FormatWorksheetName( book, worksheetName );

            var newWorksheet = book.Worksheet( SpreadsheetConstants.TemplateSheetName )
                                   .CopyTo( worksheetName, book.Worksheets.Count - 1 );

            XLCellHelper.SetDefaultCellStyle( newWorksheet, keySwitch );

            foreach( var articulation in keySwitch.Articulations )
            {
                var col = TranslateArticulation( keySwitch, articulation, newWorksheet, row );
                row++;
                column = Math.Max( column, col );
            }

            #region GUID
            newWorksheet.Cell( SpreadsheetConstants.RowGuid, SpreadsheetConstants.ColumnGuid )
                        .Value = keySwitch.Id.Value;
            #endregion

            #region Developer Name
            newWorksheet.Cell( SpreadsheetConstants.RowDeveloperName, SpreadsheetConstants.ColumnDeveloperName )
                        .Value = keySwitch.DeveloperName;
            #endregion

            #region Product Name
            newWorksheet.Cell( SpreadsheetConstants.RowProductName, SpreadsheetConstants.ColumnProductName )
                        .Value = keySwitch.ProductName;
            #endregion

            #region Instrument Name
            newWorksheet.Cell( SpreadsheetConstants.RowOutputName, SpreadsheetConstants.ColumnOutputName )
                        .Value = keySwitch.InstrumentName;
            #endregion

            #region Author
            newWorksheet.Cell( SpreadsheetConstants.RowAuthor, SpreadsheetConstants.ColumnAuthor )
                        .Value = keySwitch.Author;
            #endregion

            #region Description
            newWorksheet.Cell( SpreadsheetConstants.RowDescription, SpreadsheetConstants.ColumnDescription )
                        .Value = keySwitch.Description;
            #endregion

            // fill empty rows count (bordered only)
            var emptyRowCount = Math.Max(
                minimumBorderRowCount - ( row - SpreadsheetConstants.RowDataBegin ),
                0
            );

            #region Apply format to data cells

            var range = newWorksheet.Range(
                SpreadsheetConstants.RowDataHeader,
                SpreadsheetConstants.ColumnDataBegin,
                row - 1 + emptyRowCount,
                column - 1
            );

            // Draw cell border line
            XLCellHelper.ActivateCellBorder( range.Style );

            // Adjust column width
            // for( var i = SpreadsheetConstants.ColumnDataBegin; i < column; i++ )
            // {
            //     ** ClosedXml depends GDI+ **
            //     XLCellHelper.AdjustColumnWidth( newWorksheet, i );
            // }

            #endregion
        }

        private static string FormatWorksheetName( IXLWorkbook book, string worksheetName )
        {
            var originalSheetName = worksheetName;

            // xlsx worksheet name length must be <= 31 - 5 (5: '(###)'.Length for numbering if sheet name is duplicated
            if( worksheetName.Length > 26 )
            {
                worksheetName = worksheetName.Substring( 0, 26 );
            }

            // if sheet name is already created, numbering to workSheetName
            var duplicateIndex = 1;
            const int duplicationLimit = 999;

            while( true )
            {
                if( duplicateIndex > duplicationLimit )
                {
                    // Too many duplicated sheet names
                    throw new NoMoreDuplicateSheetNameException( worksheetName );
                }

                if( book.Worksheets.All( x => x.Name != worksheetName ) )
                {
                    break;
                }

                worksheetName = $"{originalSheetName}({duplicateIndex})";
                duplicateIndex++;

            }

            return worksheetName;
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

            column = extraDataTranslator.Translate(
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
                TranslateMidiMessageType.ChannelInStatus | TranslateMidiMessageType.Data1 | TranslateMidiMessageType.Data2
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
                TranslateMidiMessageType.ChannelInStatus | TranslateMidiMessageType.Data1 | TranslateMidiMessageType.Data2
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
                TranslateMidiMessageType.ChannelInStatus | TranslateMidiMessageType.Data1
            );
        }

        #endregion

        #region Updating Midi Message Cell
        public static int UpdateCellFromMidiMessage(
            TranslateMidiMessageType type,
            TranslateMidiMessageType mask,
            int midiValue,
            IXLWorksheet sheet,
            int row,
            int column,
            Action<IXLCell, int> updateAction )
        {
            if( !type.HasFlag( mask ) )
            {
                return column;
            }

            updateAction( sheet.Cell( row, column ), midiValue );
            column++;

            return column;
        }

        public static int UpdateCellFromMidiMessage(
            TranslateMidiMessageType type,
            TranslateMidiMessageType mask,
            int midiValue,
            IXLWorksheet sheet,
            int row,
            int column )
        {
            return UpdateCellFromMidiMessage(
                type,
                mask,
                midiValue,
                sheet,
                row,
                column,
                ( cell, value ) =>
                {
                    cell.Value = value;
                }
            );
        }
        #endregion

    }
}