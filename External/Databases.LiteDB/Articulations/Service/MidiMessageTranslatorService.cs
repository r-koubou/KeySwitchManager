using ArticulationManager.Databases.LiteDB.Articulations.Model;
using ArticulationManager.Domain.MidiMessages.Aggregate;
using ArticulationManager.Domain.Services;

namespace ArticulationManager.Databases.LiteDB.Articulations.Service
{
    public class MidiMessageTranslatorService : IDataTranslationService<IMessage, MidiMessageModel>
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