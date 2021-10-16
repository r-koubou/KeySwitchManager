using System.Collections.Generic;

using Newtonsoft.Json;

namespace KeySwitchManager.Infrastructures.Storage.Json.Cakewalk.Models
{
    public class CakewalkArticulationMap
    {
        [JsonProperty( "ArticulationMaps")]
        public IList<ArticulationMap> ArticulationMaps { get; set; } = new List<ArticulationMap>();
    }
}
