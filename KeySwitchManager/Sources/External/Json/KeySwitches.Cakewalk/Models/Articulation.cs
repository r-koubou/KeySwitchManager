using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace KeySwitchManager.Json.KeySwitches.Cakewalk.Models
{
    internal class Articulation
    {
        [JsonPropertyName( "id" )]
        public int Id { get; }
        [JsonPropertyName( "name" )]
        public string Name { get; }
        [JsonPropertyName( "index" )]
        public int Index { get; }
        [JsonPropertyName( "groupId" )]
        public int GroupId { get; }
        [JsonPropertyName( "color" )]
        public string Color { get; } // = "ffff0000"; // ARGB
        [JsonPropertyName( "duration" )]
        public int Duration { get; }
        [JsonPropertyName( "events" )]
        public IList<MidiEvent> Events { get; }
        [JsonPropertyName( "transforms" )]
        public IList<Transform> Transforms { get; }

        public Articulation(
            int id,
            string name,
            int index,
            int groupId,
            string color,
            int duration,
            IEnumerable<MidiEvent> events,
            IEnumerable<Transform> transforms )
        {
            Id         = id;
            Name       = name;
            Index      = index;
            GroupId    = groupId;
            Color      = color;
            Duration   = duration;
            Events     = new List<MidiEvent>( events );
            Transforms = new List<Transform>( transforms );
        }
    }
}
