using KeySwitchManager.Domain.MidiMessages.Aggregate;
using KeySwitchManager.Domain.Translations;
using KeySwitchManager.Json.KeySwitches.Model;

namespace KeySwitchManager.Json.KeySwitches.Translations
{
    public class MidiMessageToJsonModel : IDataTranslator<IMessage, MidiMessageModel>
    {
        public MidiMessageModel Translate( IMessage source )
        {
            return new MidiMessageModel(
                source.Status.Value,
                source.Channel.Value,
                source.DataByte1.Value,
                source.DataByte2.Value
            );
        }
    }
}