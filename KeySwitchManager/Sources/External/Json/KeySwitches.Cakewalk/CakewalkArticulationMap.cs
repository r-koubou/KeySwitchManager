using System.Collections.Generic;
using System.Text.Json.Serialization;

using KeySwitchManager.Json.KeySwitches.Cakewalk.Models;

namespace KeySwitchManager.Json.KeySwitches.Cakewalk
{
    internal class CakewalkArticulationMap
    {
        [JsonPropertyName( "ArticulationMaps")]
        public IList<ArticulationMap> ArticulationMaps { get; set; } = new List<ArticulationMap>();
    }
}
