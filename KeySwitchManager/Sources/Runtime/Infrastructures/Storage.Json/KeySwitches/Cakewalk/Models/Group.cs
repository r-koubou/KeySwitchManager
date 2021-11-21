using Newtonsoft.Json;

namespace KeySwitchManager.Infrastructures.Storage.Json.Cakewalk.Models
{
    public class Group
    {
        [JsonProperty( "id" )]
        public int Id { get; }
        [JsonProperty( "name" )]
        public string Name { get; }

        public Group( int id, string name )
        {
            Id   = id;
            Name = name;
        }
    }
}
