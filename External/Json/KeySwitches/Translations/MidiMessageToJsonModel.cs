using ArticulationManager.Domain.MidiMessages.Aggregate;
using ArticulationManager.Domain.Translations;
using ArticulationManager.Json.KeySwitches.Model;

namespace ArticulationManager.Json.KeySwitches.Translations
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