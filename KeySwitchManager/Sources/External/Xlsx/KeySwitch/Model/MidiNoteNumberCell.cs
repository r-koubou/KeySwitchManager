using System;

using KeySwitchManager.Xlsx.KeySwitch.Helper;

namespace KeySwitchManager.Xlsx.KeySwitch.Model
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