using KeySwitchManager.Domain.MidiMessages.Aggregate;
using KeySwitchManager.Domain.MidiMessages.Value;
using KeySwitchManager.Domain.Translations;
using KeySwitchManager.Json.KeySwitches.Models;

namespace KeySwitchManager.Json.KeySwitches.Translations
{
    public class JsonModelToMidiMessage : IDataTranslator<MidiMessageModel, IMidiMessage>
    {
        public IMidiMessage Translate( MidiMessageModel source )
        {
            return new GenericMidiMessage(
                new MidiStatus( source.Status ),
                new GenericMidiData( source.Data1 ),
                new GenericMidiData( source.Data2 )
            );
        }
    }
}