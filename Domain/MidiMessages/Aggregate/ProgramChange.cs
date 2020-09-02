using ArticulationManager.Domain.Commons;
using ArticulationManager.Domain.MidiMessages.Value;

namespace ArticulationManager.Domain.MidiMessages.Aggregate
{
    /// <summary>
    /// Represents a MIDI program change.
    /// </summary>
    public class ProgramChange : IMessage
    {
        public EntityId Id { get; }
        public IMessageData Status { get; } = StatusCode.ProgramChange;
        public IMessageData DataByte1 { get; }
        public IMessageData DataByte2 { get; }

        public ProgramChange( EntityId id, Channel channel, ProgramChangeNumber number )
        {
            Id        = id;
            DataByte1 = channel;
            DataByte2 = number;
        }
    }
}