using KeySwitchManager.Domain.MidiMessages.Entity;
using KeySwitchManager.Domain.MidiMessages.Values;
using KeySwitchManager.Domain.Translators;
using KeySwitchManager.Json.KeySwitches.Models;

namespace KeySwitchManager.Json.KeySwitches.Translators
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