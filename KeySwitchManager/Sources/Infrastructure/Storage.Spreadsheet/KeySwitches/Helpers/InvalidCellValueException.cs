using System;

namespace KeySwitchManager.Infrastructure.Storage.Spreadsheet.KeySwitches.Helpers
{
    public class InvalidCellValueException : Exception
    {
        public InvalidCellValueException()
        {}

        public InvalidCellValueException( int rowIndex, string columnLabel )
            : base( $"Row:{rowIndex}, Column:{columnLabel} is invalid." )
        {
        }

        public InvalidCellValueException( string message ) : base( message )
        {
        }
    }
}