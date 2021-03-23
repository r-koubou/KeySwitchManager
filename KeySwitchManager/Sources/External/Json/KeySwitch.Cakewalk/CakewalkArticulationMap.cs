using System.Collections.Generic;
using System.Text.Json.Serialization;

using KeySwitchManager.Json.KeySwitch.Cakewalk.Model;

using CwArticulation = KeySwitchManager.Json.KeySwitch.Cakewalk.Model.Articulation;

namespace KeySwitchManager.Json.KeySwitch.Cakewalk
{
    internal class CakewalkArticulationMap
    {
        [JsonPropertyName( "ArticulationMaps")]
        public IList<ArticulationMap> ArticulationMaps { get; set; } = new List<ArticulationMap>();
    }
}
