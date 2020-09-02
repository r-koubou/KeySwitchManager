using ArticulationManager.Domain.Commons;
using ArticulationManager.Domain.MidiMessages.Value;

namespace ArticulationManager.Domain.MidiMessages.Aggregate
{
    /// <summary>
    /// Represents a MIDI control change.
    /// </summary>
    public class ControlChange : IMessage
    {
        public EntityId Id { get; }
        public IMessageData Status { get; } = StatusCode.ControlChange;
        public IMessageData DataByte1 { get; }
        public IMessageData DataByte2 { get; }

        public ControlChange( EntityId id, ControlChangeNumber controlChangeNumber, ControlChangeValue controlChangeValue )
        {
            Id        = id;
            DataByte1 = controlChangeNumber;
            DataByte2 = controlChangeValue;
        }

        public ControlChange( EntityId id, Channel channel, ControlChangeNumber controlChangeNumber, ControlChangeValue controlChangeValue )
        {
            Id        = id;
            Status    = new StatusCode( channel, StatusCode.ControlChange.Value );
            DataByte1 = controlChangeNumber;
            DataByte2 = controlChangeValue;
        }

    }
}