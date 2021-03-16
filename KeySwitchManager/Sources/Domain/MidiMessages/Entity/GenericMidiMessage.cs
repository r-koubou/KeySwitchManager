using KeySwitchManager.Domain.MidiMessages.Value;

namespace KeySwitchManager.Domain.MidiMessages.Entity
{
    /// <summary>
    /// Generic MIDI Event.
    /// </summary>
    public class GenericMidiMessage : IMidiMessage
    {
        public IMidiMessageData Status { get; }
        public IMidiMessageData DataByte1 { get; }
        public IMidiMessageData DataByte2 { get; }

        public GenericMidiMessage(
            IMidiMessageData status,
            IMidiMessageData data1,
            IMidiMessageData data2 )
        {
            Status    = status;
            DataByte1 = data1;
            DataByte2 = data2;
        }
    }
}