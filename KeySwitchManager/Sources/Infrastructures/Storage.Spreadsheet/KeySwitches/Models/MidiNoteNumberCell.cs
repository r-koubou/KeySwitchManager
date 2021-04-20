using System;

using KeySwitchManager.Infrastructures.Storage.Spreadsheet.KeySwitches.Helpers;

namespace KeySwitchManager.Infrastructures.Storage.Spreadsheet.KeySwitches.Models
{
    public class MidiNoteNumberCell
    {
        public string Value { get; }

        public MidiNoteNumberCell( string noteName )
        {
            if( !MidiNoteNameHelper.Contains( noteName ) )
            {
                throw new ArgumentException( nameof( noteName ) );
            }

            Value = noteName;
        }
    }
}