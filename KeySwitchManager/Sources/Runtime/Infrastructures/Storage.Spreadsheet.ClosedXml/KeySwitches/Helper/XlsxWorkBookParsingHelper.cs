using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using ClosedXML.Excel;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Infrastructures.Storage.Spreadsheet.KeySwitches.Helpers;
using KeySwitchManager.Infrastructures.Storage.Spreadsheet.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Storage.Spreadsheet.KeySwitches.Models.Aggregations;
using KeySwitchManager.Infrastructures.Storage.Spreadsheet.KeySwitches.Models.Values;

using RkHelper.Primitives;

namespace KeySwitchManager.Infrastructures.Storage.Spreadsheet.ClosedXml.KeySwitches.Helper
{
    internal static class XlsxWorkBookParsingHelper
    {
        private class ArticulationCellGroup
        {
            public ArticulationNameCell NameCell { get; set; } = ArticulationNameCell.Empty;
        }

        static XlsxWorkBookParsingHelper()
        {
            // Workaround
            // "System.NotSupportedException: No data is available for encoding 1252"
            // https://stackoverflow.com/questions/49215791/vs-code-c-sharp-system-notsupportedexception-no-data-is-available-for-encodin
            Encoding.RegisterProvider( CodePagesEncodingProvider.Instance );
        }

        public static Workbook Parse( Stream source )
        {
            var result = new Workbook();
            using var sourceBook = new XLWorkbook( source );

            foreach( var x in sourceBook.Worksheets )
            {
                // Ignore sheet
                if( x == null ||
                    x.Name == SpreadsheetConstants.DataListDefinitionSheetName ||
                    x.Name.Contains( SpreadsheetConstants.IgnoreSheetNameRule ) )
                {
                    continue;
                }

                Worksheet worksheet = ParseWorksheet( x );
                result.Worksheets.Add( worksheet );
            }

            return result;
        }

        public static Workbook Parse( byte[] xlsxBytes )
        {
            using var stream = new MemoryStream( xlsxBytes );
            return Parse( stream );
        }

        public static Workbook Parse( FilePath filePath )
        {
            using var stream = File.Open( filePath.Path, FileMode.Open, FileAccess.Read );
            return Parse( stream );
        }

        private static Worksheet ParseWorksheet( IXLWorksheet sourceSheet )
        {
            var rowCount = sourceSheet.Rows().Count();
            var worksheet = new Worksheet( sourceSheet.Name );

            var extraColumnNames = ParseExtraColumnNames( sourceSheet );

            for( var rowIndex = SpreadsheetConstants.RowDataBegin; rowIndex < rowCount; rowIndex++ )
            {
                if( IsEndOfRow( sourceSheet, rowIndex ) )
                {
                    break;
                }

                var context = new CellContext
                {
                    Sheet    = sourceSheet,
                    Row      = sourceSheet.Row( rowCount ),
                    RowIndex = rowIndex
                };

                // Standard Columns
                var row = ParseRow( context );

                // Extended Columns
                foreach( var extraColumnName in extraColumnNames )
                {
                    if( TryParseSheet( context, extraColumnName, out var extraValue ) )
                    {
                        row.Extra.Add(
                            extraColumnName.Substring( SpreadsheetConstants.HeaderExtraPrefix.Length ),
                            new ExtraDataCell( extraValue ) );
                    }
                }

                worksheet.Rows.Add( row );
            }

            #region GUID
            var guid = sourceSheet.Row( SpreadsheetConstants.RowGuid )
                                  .Cell( SpreadsheetConstants.ColumnGuid ).Value.ToString();

            worksheet.GuidCell = guid == null ?
                GuidCell.Empty : new GuidCell( new Guid( guid ) );
            #endregion

            #region Developer Name
            var developerName = sourceSheet.Row( SpreadsheetConstants.RowDeveloperName )
                                         .Cell( SpreadsheetConstants.ColumnDeveloperName ).Value.ToString();

            worksheet.DeveloperNameCell = developerName == null ?
                DeveloperNameCell.Empty : new DeveloperNameCell( developerName );
            #endregion

            #region Product Name
            var productName = sourceSheet.Row( SpreadsheetConstants.RowProductName )
                                        .Cell( SpreadsheetConstants.ColumnProductName ).Value.ToString();

            worksheet.ProductNameCell = productName == null ?
                ProductNameCell.Empty : new ProductNameCell( productName );
            #endregion

            #region Instrument Name
            var outputName = sourceSheet.Row( SpreadsheetConstants.RowOutputName )
                                        .Cell( SpreadsheetConstants.ColumnOutputName ).Value.ToString();

            worksheet.InstrumentNameCell = outputName == null ?
                InstrumentNameCell.Empty : new InstrumentNameCell( outputName );
            #endregion

            #region Author
            var author = sourceSheet.Row( SpreadsheetConstants.RowAuthor )
                                        .Cell( SpreadsheetConstants.ColumnAuthor ).Value.ToString();

            worksheet.AuthorCell = author == null ?
                AuthorCell.Empty : new AuthorCell( author );
            #endregion

            #region Description
            var description = sourceSheet.Row( SpreadsheetConstants.RowDescription )
                                    .Cell( SpreadsheetConstants.ColumnDescription ).Value.ToString();

            worksheet.DescriptionCell = description == null ?
                DescriptionCell.Empty : new DescriptionCell( description );
            #endregion

            return worksheet;
        }

        private static bool IsEndOfRow( IXLWorksheet sheet, int rowIndex )
        {
            if( rowIndex > sheet.Rows().Count() )
            {
                return true;
            }

            var value = sheet.Row( rowIndex )
                             .Cell( SpreadsheetConstants.ColumnDataBegin )
                             .Value;

            return StringHelper.IsEmpty( value );
        }

        private static Row ParseRow( CellContext context )
        {
            var articulationCellGroup = ParseArticulation( context );
            var row = new Row( articulationCellGroup.NameCell );

            row.MidiNoteList.AddRange( ParseMidiNotes( context ) );
            row.MidiControlChangeList.AddRange( ParseMidiControlChanges( context ) );
            row.MidiProgramChangeList.AddRange( ParseMidiProgramChanges( context ) );

            return row;
        }

        private static ArticulationCellGroup ParseArticulation( CellContext context )
        {
            var articulationCellGroup = new ArticulationCellGroup();

            ParseSheet( context, SpreadsheetConstants.HeaderArticulationName, out var cellValue );
            articulationCellGroup.NameCell = new ArticulationNameCell( cellValue );

            return articulationCellGroup;
        }

        private static IEnumerable<Row.MidiNote> ParseMidiNotes( CellContext context )
        {
            //----------------------------------------------------------------------
            // MIDI Notes
            // * Multiple MIDI Note Supported
            // * Column name format:
            // NoteOn Ch[1] ... NoteOn Ch[1+n]
            // Note[1] ... Note[1+n]
            // Velocity[1] ... Velocity[1+n]
            //----------------------------------------------------------------------
            var notes = new List<Row.MidiNote>();

            for( int i = 1; i < int.MaxValue; i++ )
            {
                #region Channel
                var midiChannel = ParseMidiChannelCell( context, SpreadsheetConstants.MakeIndexedHeader( SpreadsheetConstants.HeaderMidiNoteOnChannel, i ) );
                #endregion

                #region Note
                if( !TryParseSheet( context, SpreadsheetConstants.MakeIndexedHeader( SpreadsheetConstants.HeaderMidiNote, i ), out var noteNumberCell ) )
                {
                    break;
                }

                #endregion

                #region Velocity
                ParseSheet( context, SpreadsheetConstants.MakeIndexedHeader( SpreadsheetConstants.HeaderMidiVelocity, i ), out var velocityCell );

                if( !int.TryParse( velocityCell, out var velocityValue ) )
                {
                    velocityValue = 100;
                }
                #endregion

                var obj = new Row.MidiNote
                {
                    Channel  = new MidiChannelCell( midiChannel ),
                    Note     = new MidiNoteNumberCell( noteNumberCell ),
                    Velocity = new MidiNoteVelocityCell( velocityValue )
                };

                notes.Add( obj );
            }

            return notes;
        }

        private static IEnumerable<Row.MidiControlChange> ParseMidiControlChanges( CellContext context )
        {
            //----------------------------------------------------------------------
            // MIDI CC
            // * Multiple MIDI CC Supported
            // * Column name format:
            //   CC Ch[1] ... CC Ch[1+n]
            //   CC No[1] ... CC No[1+n]
            //   CC Value[1] ... CC Value[1+n]
            //----------------------------------------------------------------------
            var controlChanges = new List<Row.MidiControlChange>();

            for( int i = 1; i < int.MaxValue; i++ )
            {
                #region Channel
                var midiChannel = ParseMidiChannelCell( context, SpreadsheetConstants.MakeIndexedHeader( SpreadsheetConstants.HeaderPcChannel, i ) );
                #endregion

                #region CC No
                if( !TryParseSheet( context, SpreadsheetConstants.MakeIndexedHeader( SpreadsheetConstants.HeaderMidiCc, i ), out var ccNumberCell ) )
                {
                    break;
                }
                #endregion

                #region CC Value
                if( !TryParseSheet( context, SpreadsheetConstants.MakeIndexedHeader( SpreadsheetConstants.HeaderMidiCcValue, i ), out var ccValueCell ) )
                {
                    break;
                }
                #endregion

                var obj = new Row.MidiControlChange
                {
                    Channel  = new MidiChannelCell( midiChannel ),
                    CcNumber = new MidiControlChangeNumberCell( int.Parse( ccNumberCell ) ),
                    CcValue  = new MidiControlChangeValueCell( int.Parse( ccValueCell ) )
                };

                controlChanges.Add( obj );
            }

            return controlChanges;
        }

        private static IEnumerable<Row.MidiProgramChange> ParseMidiProgramChanges( CellContext context )
        {
            //----------------------------------------------------------------------
            // Program (MIDI Program Change?)
            // * Multiple value Supported
            // * Column name format:
            //   PC Channel[1] ... PC Channel[1+n]
            //   PC Data[1] ... PC Data[1+n]
            //----------------------------------------------------------------------
            var program = new List<Row.MidiProgramChange>();

            for( int i = 1; i < int.MaxValue; i++ )
            {
                #region Channel
                var midiChannel = ParseMidiChannelCell( context, SpreadsheetConstants.MakeIndexedHeader( SpreadsheetConstants.HeaderPcChannel, i ) );
                #endregion

                #region PC Value
                if( !TryParseSheet( context, SpreadsheetConstants.MakeIndexedHeader( SpreadsheetConstants.HeaderPcData, i ), out var pcDataCell ) )
                {
                    break;
                }
                #endregion

                var obj = new Row.MidiProgramChange
                {
                    Channel = new MidiChannelCell( midiChannel ),
                    Data    = new MidiProgramChangeCell( int.Parse( pcDataCell ) )
                };

                program.Add( obj );
            }

            return program;
        }

        private static void ParseSheet( CellContext context, string columnName, out string result )
        {
            if( !TryParseSheet( context.Sheet, context.RowIndex, columnName, out result ) )
            {
                throw new InvalidCellValueException( context.RowIndex, columnName );
            }
        }

        private static void ParseSheet( IXLWorksheet sheet, int rowIndex, string columnName, out string result )
        {
            if( !TryParseSheet( sheet, rowIndex, columnName, out result ) )
            {
                throw new InvalidCellValueException( rowIndex, columnName );
            }
        }

        private static bool TryParseSheet( CellContext context, string columnName, out string result )
        {
            return TryParseSheet( context.Sheet, context.RowIndex, columnName, out result );
        }

        private static bool TryParseSheet( IXLWorksheet sheet, int rowIndex, string columnName, out string result )
        {
            var i = 1;
            result = string.Empty;

            foreach( var columnCell in sheet.Row( SpreadsheetConstants.RowDataHeader ).Cells() )
            {
                if( columnCell != null && columnCell.Value.ToString() == columnName )
                {
                    var cell = sheet.Row( rowIndex ).Cell( i );

                    if( cell == null )
                    {
                        return false;
                    }

                    result = cell.Value?.ToString()!;

                    return !string.IsNullOrEmpty( result.Trim() );
                }
                i++;
            }
            return false;
        }

        private static int ParseMidiChannelCell( CellContext context, string columnName )
        {
            var midiChannel = 0x00;

            if( TryParseSheet( context, columnName, out var channelCell ) )
            {
                if( !int.TryParse( channelCell, out midiChannel ) )
                {
                    midiChannel = 0x00;
                }
                else
                {
                    // one-based index [1-16] to zero-based [0-15] index
                    midiChannel -= 1;

                    if( midiChannel < 0 )
                    {
                        midiChannel = 0;
                    }
                }
            }

            return midiChannel;
        }

        private static IReadOnlyCollection<string> ParseExtraColumnNames( IXLWorksheet sheet )
        {
            var result  = new List<string>();
            var headers = sheet.Row( SpreadsheetConstants.RowDataHeader ).Cells();
            var extraColumnNames = headers.Where( x =>
            {
                return x != null && ( x.Value.ToString() ?? string.Empty ).StartsWith( SpreadsheetConstants.HeaderExtraPrefix );
            });

            foreach( var i in extraColumnNames )
            {
                result.Add( i.Value.ToString()! );
            }

            return result;
        }

    }
}
