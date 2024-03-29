namespace KeySwitchManager.Infrastructures.Storage.Spreadsheet.KeySwitches.Helpers
{
    public static class SpreadsheetConstants
    {
        public static readonly string HeaderArticulationName = "Articulation Name";
        public static readonly string HeaderMidiNoteOnChannel = "NoteOn Ch";
        public static readonly string HeaderMidiNote = "Note";
        public static readonly string HeaderMidiVelocity = "Velocity";
        public static readonly string HeaderMidiCcChannel = "CC Ch";
        public static readonly string HeaderMidiCc = "CC No";
        public static readonly string HeaderMidiCcValue = "CC Data";
        public static readonly string HeaderPcChannel = "PC Ch";
        public static readonly string HeaderPcData = "PC Data";

        public static readonly string HeaderExtraPrefix = "Ext.";

        public static string MakeIndexedHeader( string header, int index )
            => $"{header}[{index}]";

        //
        // Row, Column index starts from 1.
        //

        #region Row, Column index

        // Position of Guid cell
        public static readonly int RowGuid = 2;
        public static readonly int ColumnGuid = 1;
        // Position of Developer name cell
        public static readonly int RowDeveloperName = 4;
        public static readonly int ColumnDeveloperName = 1;
        // Position of Product name cell
        public static readonly int RowProductName = 6;
        public static readonly int ColumnProductName = 1;
        // Position of Output name cell
        public static readonly int RowOutputName = 8;
        public static readonly int ColumnOutputName = 1;
        // Position of Author cell
        public static readonly int RowAuthor = 10;
        public static readonly int ColumnAuthor = 1;
        // Position of Description cell
        public static readonly int RowDescription = 12;
        public static readonly int ColumnDescription = 1;

        // row 13~24: Reserved for maintenance

        // Start of data entry row index
        public static readonly int RowDataHeader = 25;
        public static readonly int RowDataBegin = 26;

        // Start of data entry column index
        public static readonly int ColumnDataBegin = 1;
        public static readonly int ColumnMidiMessageBegin = 2;

        // DataValidations (Note)
        public static readonly int ColumnValidationMidiNoteList = 2;
        public static readonly int RowValidationMidiNoteListBegin = 2;
        public static readonly int RowValidationMidiNoteListEnd = 258;
        // DataValidations (Channel)
        public static readonly int ColumnValidationMidiChannelList = 8;
        public static readonly int RowValidationMidiChannelListBegin = 2;
        public static readonly int RowValidationMidiChannelListEnd = 18;

        #endregion

        #region Sheet names

        public static readonly string TemplateSheetName   = "__TEMPLATE__";
        public static readonly string DataListDefinitionSheetName = "DO NOT MODIFY!";
        public static readonly string IgnoreSheetNameRule = "Ignore";

        #endregion
    }
}