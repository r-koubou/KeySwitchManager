namespace KeySwitchManager.Xlsx.KeySwitches.Services
{
    public static class SpreadsheetConstants
    {
        public static readonly string ColumnArticulationName = "Articulation Name";
        public static readonly string ColumnMidiNote = "MIDI Note";
        public static readonly string ColumnMidiVelocity = "Velocity";
        public static readonly string ColumnMidiCc = "CC No";
        public static readonly string ColumnMidiCcValue = "CC Value";
        public static readonly string ColumnProgramChangeChannel = "PC Channel";
        public static readonly string ColumnProgramChangeData = "PC Data";

        public static readonly string ExtraColumnPrefix = "Ext.";

        //
        // Row, Column index starts from 1.
        //

        #region Row, Column index

        // Position of Output name cell
        public static readonly int RowOutputIndex = 2;
        public static readonly int ColumnOutputNameIndex = 1;
        // Position of Guid cell
        public static readonly int RowGuidIndex = 4;
        public static readonly int ColumnGuidIndex = 1;

        // row 5~24: Reserved for maintenance

        // Start of data entry row index (19==header)
        public static readonly int HeaderRowIndex = 25;
        public static readonly int StartRowIndex = 26;

        // Start of data entry column index
        public static readonly int StartColumnIndex = 1;
        public static readonly int StartMidiMessageColumnIndex = 2;

        // DataValidations
        public static readonly int ValidationMidiNoteListColumn = 1;
        public static readonly int ValidationMidiNoteListRowBegin = 2;
        public static readonly int ValidationMidiNoteListRowEnd = 258;

        #endregion

        #region Sheet names

        public static readonly string TemplateSheetName   = "__TEMPLATE__";
        public static readonly string DefinitionSheetName = "DO NOT MODIFY!";
        public static readonly string IgnoreSheetNameRule = "Ignore";

        #endregion
    }
}