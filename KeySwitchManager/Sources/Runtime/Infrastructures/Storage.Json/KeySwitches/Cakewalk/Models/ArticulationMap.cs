using System.Collections.Generic;

using Newtonsoft.Json;

namespace KeySwitchManager.Infrastructures.Storage.Json.KeySwitches.Cakewalk.Models
{
    public class ArticulationMap
    {
        [JsonProperty( "name", Required = Required.Always )]
        public string Name { get; }

        [JsonProperty( "groups" )]
        public IList<Group> Groups { get; }

        [JsonProperty( "articulations" )]
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
