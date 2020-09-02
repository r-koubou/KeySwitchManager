using ArticulationManager.Domain.Commons;
using ArticulationManager.Domain.MidiMessages.Value;

namespace ArticulationManager.Domain.MidiMessages.Aggregate
{
    /// <summary>
    /// Generic MIDI Event.
    /// </summary>
    public class GenericMessage : IMessage
    {
        public EntityId Id { get; }
        public IMessageData Status { get; }
        public IMessageData DataByte1 { get; }
        public IMessageData DataByte2 { get; }

        public GenericMessage( EntityId id, IMessageData status, IMessageData data1, IMessageData data2 )
        {
            Id        = id;
            Status    = status;
            DataByte1 = data1;
            DataByte2 = data2;
        }

    }
}