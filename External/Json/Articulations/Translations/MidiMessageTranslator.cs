using ArticulationManager.Domain.MidiMessages.Aggregate;
using ArticulationManager.Domain.Translations;
using ArticulationManager.Json.Articulations.Model;

namespace ArticulationManager.Json.Articulations.Translations
{
    public class MidiMessageTranslator : IDataTranslator<IMessage, MidiMessageModel>
    {
        public MidiMessageModel Translate( IMessage source )
        {
            return new MidiMessageModel(
                source.Status.Value,
                source.DataByte1.Value,
                source.DataByte2.Value
            );
        }
    }
}