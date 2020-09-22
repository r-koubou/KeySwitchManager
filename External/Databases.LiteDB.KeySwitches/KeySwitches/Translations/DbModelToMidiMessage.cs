using Databases.LiteDB.KeySwitches.KeySwitches.Models;

using KeySwitchManager.Domain.MidiMessages.Aggregate;
using KeySwitchManager.Domain.MidiMessages.Value;
using KeySwitchManager.Domain.Translations;

namespace Databases.LiteDB.KeySwitches.KeySwitches.Translations
{
    public class DbModelToMidiMessage : IDataTranslator<MidiMessageModel, IMidiMessage>
    {
        public IMidiMessage Translate( MidiMessageModel source )
        {
            return new GenericMidiMessage(
                new MidiStatusCode( source.Status ),
                new GenericMidiData( source.Channel ),
                new GenericMidiData( source.DataByte1 ),
                new GenericMidiData( source.DataByte2 )
            );
        }
    }
}