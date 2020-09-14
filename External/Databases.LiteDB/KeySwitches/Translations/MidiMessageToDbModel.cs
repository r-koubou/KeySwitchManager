using ArticulationManager.Databases.LiteDB.KeySwitches.Model;
using ArticulationManager.Domain.MidiMessages.Aggregate;
using ArticulationManager.Domain.Translations;

namespace ArticulationManager.Databases.LiteDB.KeySwitches.Translations
{
    public class MidiMessageToDbModel : IDataTranslator<IMessage, MidiMessageModel>
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