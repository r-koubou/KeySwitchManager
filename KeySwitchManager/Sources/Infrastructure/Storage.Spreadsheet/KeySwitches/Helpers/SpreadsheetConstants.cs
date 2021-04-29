namespace KeySwitchManager.Infrastructure.Storage.Spreadsheet.KeySwitches.Helpers
{
    public static class SpreadsheetConstants
    {
        public static readonly string HeaderArticulationName = "Articulation Name";
        public static readonly string HeaderMidiNote = "MIDI Note";
        public static readonly string HeaderMidiVelocity = "Velocity";
        public static readonly string HeaderMidiCc = "CC No";
        public static readonly string HeaderMidiCcValue = "CC Value";
        public static readonly string HeaderPcChannel = "PC Channel";
        public static readonly string HeaderPcData = "PC Data";

        public static readonly string HeaderExtraPrefix = "Ext.";

        //
        // Row, Column index starts from 1.
        //

        #region Row, Column index

        // Position of Output name cell
        public static readonly int RowOutputName = 2;
        public static readonly int ColumnOutputName = 1;
        // Position of Guid cell
        public static readonly int RowGuid = 4;
        public static readonly int ColumnGuid = 1;

        // row 5~24: Reserved for maintenance

        // Start of data entry row index
        public static readonly int RowDataHeader = 25;
        public static readonly int RowDataBegin = 26;

        // Start of data entry column index
        public static readonly int ColumnDataBegin = 1;
        public static readonly int ColumnMidiMessageBegin = 2;

        // DataValidations
        public static readonly int ColumnValidationMidiNoteList = 2;
        public static readonly int RowValidationMidiNoteListBegin = 2;
        public static readonly int RowValidationMidiNoteListEnd = 258;

        #endregion

        #region Sheet names

        public static readonly string TemplateSheetName   = "__TEMPLATE__";
        public static readonly string DataListDefinitionSheetName = "DO NOT MODIFY!";
        public static readonly string IgnoreSheetNameRule = "Ignore";

        #endregion
    }
}