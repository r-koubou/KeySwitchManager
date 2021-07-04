using System;

namespace KeySwitchManager.Infrastructures.Storage.Spreadsheet.ClosedXml.KeySwitches.Translators.Helpers
{
    public class NoMoreDuplicateSheetNameException : Exception
    {
        public NoMoreDuplicateSheetNameException( string message ) : base( message ) {}
        public NoMoreDuplicateSheetNameException( int duplicatedCount ) : base( $"{duplicatedCount}" ) {}
    }
}
