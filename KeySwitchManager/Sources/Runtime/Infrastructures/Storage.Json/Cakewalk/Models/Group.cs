using System.Text.Json.Serialization;

namespace KeySwitchManager.Infrastructures.Storage.Json.Cakewalk.Models
{
    public class Group
    {
        [JsonPropertyName( "id" )]
        public int Id { get; }
        [JsonPropertyName( "name" )]
        public string Name { get; }

        public Group( int id, string name )
        {
            Id   = id;
            Name = name;
        }
    }
}
