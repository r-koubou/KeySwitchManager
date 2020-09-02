using ArticulationManager.Domain.MidiMessages.Value;

namespace ArticulationManager.Domain.MidiMessages.Aggregate
{
    /// <summary>
    /// Representing a MIDI note on.
    /// </summary>
    public class NoteOn : IMessage
    {
        public IMessageData Status { get; } = StatusCode.NoteOn;
        public IMessageData DataByte1 { get; }
        public IMessageData DataByte2 { get; }

        public NoteOn( NoteNumber noteNumber, Velocity velocity )
        {
            DataByte1 = noteNumber;
            DataByte2 = velocity;
        }

        public NoteOn( Channel channel, NoteNumber noteNumber, Velocity velocity )
        {
            Status    = new StatusCode( channel, StatusCode.NoteOn.Value );
            DataByte1 = noteNumber;
            DataByte2 = velocity;
        }

    }
}