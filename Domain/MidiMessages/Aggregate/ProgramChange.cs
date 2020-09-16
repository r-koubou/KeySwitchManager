using KeySwitchManager.Domain.MidiMessages.Value;

namespace KeySwitchManager.Domain.MidiMessages.Aggregate
{
    /// <summary>
    /// Represents a MIDI program change.
    /// </summary>
    public class ProgramChange : IMessage
    {
        public IMessageData Status { get; }
        public IMessageData Channel { get; }

        public IMessageData DataByte1 { get; }
        public IMessageData DataByte2 { get; }

        public ProgramChange( MidiChannel midiChannel, ProgramChangeNumber number )
        {
            Status    = new StatusCode( StatusCode.ProgramChange.Value );
            Channel   = midiChannel;
            DataByte1 = number;
            DataByte2 = GenericData.Zero;
        }
    }
}