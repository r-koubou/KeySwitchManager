using KeySwitchManager.Databases.LiteDB.KeySwitches.Model;
using KeySwitchManager.Domain.MidiMessages.Aggregate;
using KeySwitchManager.Domain.MidiMessages.Value;
using KeySwitchManager.Domain.Translations;

namespace KeySwitchManager.Databases.LiteDB.KeySwitches.Translations
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