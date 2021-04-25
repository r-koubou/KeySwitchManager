using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace KeySwitchManager.Infrastructure.Storage.Json.Cakewalk.Models
{
    public class CakewalkArticulationMap
    {
        [JsonPropertyName( "ArticulationMaps")]
        public IList<ArticulationMap> ArticulationMaps { get; set; } = new List<ArticulationMap>();
    }
}
