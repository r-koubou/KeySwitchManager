using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KeySwitchManager.Infrastructure.Storage.Json.Cakewalk.Models
{
    public class ArticulationMap
    {
        [JsonPropertyName( "name" )]
        [Required]
        public string Name { get; }

        [JsonPropertyName( "groups" )]
        public IList<Group> Groups { get; }

        [JsonPropertyName( "articulations" )]
        public IList<Articulation> Articulations { get; }

        public ArticulationMap(
            string name,
            IList<Group> groups,
            IList<Articulation> articulations )
        {
            Name          = name;
            Groups        = groups;
            Articulations = articulations;
        }
    }
}
