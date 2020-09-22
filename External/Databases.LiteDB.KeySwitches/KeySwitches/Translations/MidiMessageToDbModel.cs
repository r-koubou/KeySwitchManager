using Databases.LiteDB.KeySwitches.KeySwitches.Models;

using KeySwitchManager.Domain.MidiMessages.Aggregate;
using KeySwitchManager.Domain.Translations;

namespace Databases.LiteDB.KeySwitches.KeySwitches.Translations
{
    public class MidiMessageToDbModel : IDataTranslator<IMidiMessage, MidiMessageModel>
    {
        public MidiMessageModel Translate( IMidiMessage source )
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