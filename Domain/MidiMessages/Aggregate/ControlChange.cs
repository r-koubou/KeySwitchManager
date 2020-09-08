using ArticulationManager.Domain.MidiMessages.Value;

namespace ArticulationManager.Domain.MidiMessages.Aggregate
{
    /// <summary>
    /// Represents a MIDI control change.
    /// </summary>
    public class ControlChange : IMessage
    {
        public IMessageData Status { get; } = StatusCode.ControlChange;
        public IMessageData Channel { get; } = MidiChannel.Zero;
        public IMessageData DataByte1 { get; }
        public IMessageData DataByte2 { get; }

        public ControlChange( ControlChangeNumber controlChangeNumber, ControlChangeValue controlChangeValue )
        {
            DataByte1 = controlChangeNumber;
            DataByte2 = controlChangeValue;
        }

        public ControlChange( MidiChannel midiChannel, ControlChangeNumber controlChangeNumber, ControlChangeValue controlChangeValue )
        {
            Status    = new StatusCode( StatusCode.ControlChange.Value );
            Channel   = midiChannel;
            DataByte1 = controlChangeNumber;
            DataByte2 = controlChangeValue;
        }
    }
}