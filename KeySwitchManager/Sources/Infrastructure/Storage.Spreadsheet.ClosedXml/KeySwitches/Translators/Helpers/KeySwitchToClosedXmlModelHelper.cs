using System;
using System.Collections.Generic;

using ClosedXML.Excel;

using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Domain.KeySwitches.Models.Entities;
using KeySwitchManager.Domain.MidiMessages.Models.Entities;
using KeySwitchManager.Infrastructure.Storage.Spreadsheet.ClosedXml.KeySwitches.Helper;
using KeySwitchManager.Infrastructure.Storage.Spreadsheet.KeySwitches.Helpers;

namespace KeySwitchManager.Infrastructure.Storage.Spreadsheet.ClosedXml.KeySwitches.Translators.Helpers
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

            #endregion
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