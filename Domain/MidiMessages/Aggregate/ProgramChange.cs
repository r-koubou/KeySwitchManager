using ArticulationManager.Domain.MidiMessages.Value;

namespace ArticulationManager.Domain.MidiMessages.Aggregate
{
    /// <summary>
    /// Represents a MIDI program change.
    /// </summary>
    public class ProgramChange : IMessage
    {
        public IMessageData Status { get; } = StatusCode.ProgramChange;
        public IMessageData DataByte1 { get; }
        public IMessageData DataByte2 { get; }

        public ProgramChange( Channel channel, ProgramChangeNumber number )
        {
            DataByte1 = channel;
            DataByte2 = number;
        }
    }
}