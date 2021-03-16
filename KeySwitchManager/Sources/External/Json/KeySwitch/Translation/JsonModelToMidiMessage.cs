using KeySwitchManager.Domain.MidiMessages.Entity;
using KeySwitchManager.Domain.MidiMessages.Value;
using KeySwitchManager.Domain.Translations;
using KeySwitchManager.Json.KeySwitch.Model;

namespace KeySwitchManager.Json.KeySwitch.Translation
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