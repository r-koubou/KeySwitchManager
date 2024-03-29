using System;

using KeySwitchManager.Infrastructures.Storage.Spreadsheet.KeySwitches.Helpers;

using ValueObjectGenerator;

namespace KeySwitchManager.Infrastructures.Storage.Spreadsheet.KeySwitches.Models.Values
{
    [ValueObject( typeof( string ) )]
    public partial class MidiNoteNumberCell
    {
        private static partial string Validate( string value )
        {
            if( !MidiNoteNameHelper.Contains( value ) )
            {
                throw new ArgumentException( nameof( value ) );
            }

            return value;
        }
    }
}