using ArticulationManager.Databases.LiteDB.Articulations.Model;
using ArticulationManager.Domain.MidiMessages.Aggregate;
using ArticulationManager.Domain.Translations;

namespace ArticulationManager.Databases.LiteDB.Translations
{
    public class MidiMessageToDbModel : IDataTranslator<IMessage, MidiMessageModel>
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