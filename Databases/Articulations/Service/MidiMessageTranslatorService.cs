using ArticulationManager.Databases.Articulations.Model;
using ArticulationManager.Domain.MidiMessages.Aggregate;
using ArticulationManager.Domain.Services;

namespace ArticulationManager.Databases.Articulations.Service
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