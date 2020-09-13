using ArticulationManager.Databases.LiteDB.Articulations.Model;
using ArticulationManager.Domain.MidiMessages.Aggregate;
using ArticulationManager.Domain.MidiMessages.Value;
using ArticulationManager.Domain.Translations;

namespace ArticulationManager.Databases.LiteDB.Translations
{
    public class DbModelToMidiMessage : IDataTranslator<MidiMessageModel, IMessage>
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