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
                IMidiStatusFactory.Default.Create( source.Status ),
                IGenericMidiDataFactory.Default.Create( source.Data1 ),
                IGenericMidiDataFactory.Default.Create( source.Data2 )
            );
        }
    }
}