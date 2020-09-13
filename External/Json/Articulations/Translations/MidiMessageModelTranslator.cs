using ArticulationManager.Domain.MidiMessages.Aggregate;
using ArticulationManager.Domain.MidiMessages.Value;
using ArticulationManager.Domain.Translations;
using ArticulationManager.Json.Articulations.Model;

namespace ArticulationManager.Json.Articulations.Translations
{
    public class MidiMessageModelTranslator : IDataTranslator<MidiMessageModel, IMessage>
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