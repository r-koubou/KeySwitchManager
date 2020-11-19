using KeySwitchManager.Domain.MidiMessages.Value;

namespace KeySwitchManager.Domain.MidiMessages.Aggregate
{
    /// <summary>
    /// Represents a MIDI control change.
    /// </summary>
    public class MidiControlChange : IMidiMessage
    {
        public IMidiMessageData Status { get; } = MidiStatusCode.ControlChange;
        public IMidiMessageData Channel { get; } = MidiChannel.Zero;
        public IMidiMessageData DataByte1 { get; }
        public IMidiMessageData DataByte2 { get; }

        public MidiControlChange( MidiControlChangeNumber midiControlChangeNumber, MidiControlChangeValue midiControlChangeValue )
        {
            DataByte1 = midiControlChangeNumber;
            DataByte2 = midiControlChangeValue;
        }

        public MidiControlChange( MidiChannel midiChannel, MidiControlChangeNumber midiControlChangeNumber, MidiControlChangeValue midiControlChangeValue )
        {
            Status    = new MidiStatusCode( MidiStatusCode.ControlChange.Value );
            Channel   = midiChannel;
            DataByte1 = midiControlChangeNumber;
            DataByte2 = midiControlChangeValue;
        }
    }
}