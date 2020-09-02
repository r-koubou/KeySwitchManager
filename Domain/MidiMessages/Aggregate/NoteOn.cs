using ArticulationManager.Domain.Commons;
using ArticulationManager.Domain.MidiMessages.Value;

namespace ArticulationManager.Domain.MidiMessages.Aggregate
{
    /// <summary>
    /// Representing a MIDI note on.
    /// </summary>
    public class NoteOn : IMessage
    {
        public EntityId Id { get; }
        public IMessageData Status { get; } = StatusCode.NoteOn;
        public IMessageData DataByte1 { get; }
        public IMessageData DataByte2 { get; }

        public NoteOn( EntityId id, NoteNumber noteNumber, Velocity velocity )
        {
            Id        = id;
            DataByte1 = noteNumber;
            DataByte2 = velocity;
        }

        public NoteOn( EntityId id, Channel channel, NoteNumber noteNumber, Velocity velocity )
        {
            Id        = id;
            Status    = new StatusCode( channel, StatusCode.NoteOn.Value );
            DataByte1 = noteNumber;
            DataByte2 = velocity;
        }

    }
}