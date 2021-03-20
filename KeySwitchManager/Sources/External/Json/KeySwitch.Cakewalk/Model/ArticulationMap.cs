using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KeySwitchManager.Json.KeySwitch.Cakewalk.Model
{
    internal class ArticulationMap
    {
        [JsonPropertyName( "name" )]
        [Required]
        public string Name { get; set; } = "New Articulation Map";

        [JsonPropertyName( "groups" )]
        public IList<Group> Groups { get; set; } = new List<Group>();

        [JsonPropertyName( "articulations" )]
        public IList<Articulation> Articulations { get; set; } = new List<Articulation>();
    }
}
