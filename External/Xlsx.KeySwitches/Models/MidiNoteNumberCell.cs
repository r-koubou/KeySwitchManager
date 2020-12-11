using System;

using KeySwitchManager.Xlsx.KeySwitches.Services;

namespace KeySwitchManager.Xlsx.KeySwitches.Models
{
    public class MidiNoteNumberCell
    {
        public string Value { get; }

        public MidiNoteNumberCell( string noteName )
        {
            if( MidiNoteNameHelper.Contains( noteName ) )
            {
                throw new ArgumentException( nameof( noteName ) );
            }

            Value = noteName;
        }
    }
}