using System.Collections.Generic;

using KeySwitchManager.Infrastructures.Storage.Plist.KeySwitches.Logic.Models.Aggregations;

namespace KeySwitchManager.Infrastructures.Storage.Plist.KeySwitches.Logic.Models
{
    public class RootModel
    {
        public List<ArticulationModel> Articulations { get; } = new();
    }
}
