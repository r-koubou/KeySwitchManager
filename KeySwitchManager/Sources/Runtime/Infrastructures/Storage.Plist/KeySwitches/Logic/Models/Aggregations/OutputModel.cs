using System.Collections.Generic;

namespace KeySwitchManager.Infrastructures.Storage.Plist.KeySwitches.Logic.Models.Aggregations
{
    public class OutputModel
    {
        public List<IMidiMessageModel> MidiMessageModels { get; set; } = new();
    }
}
