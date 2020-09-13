using ArticulationManager.Domain.MidiMessages.Aggregate;
using ArticulationManager.Domain.MidiMessages.Value;
using ArticulationManager.Domain.Services;
using ArticulationManager.Json.Articulations.Model;

namespace ArticulationManager.Json.Articulations.Service
{
    public class MidiMessageModelTranslationService : IDataTranslationService<MidiMessageModel, IMessage>
    {
        public IMessage Translate( MidiMessageModel source )
        {
            return new GenericMessage(
                new StatusCode( source.Status ),
                new GenericData( source.Channel ),
                new GenericData( source.DataByte1 ),
                new GenericData( source.DataByte2 )
            );
        }
    }
}