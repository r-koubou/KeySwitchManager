using System.Collections.Generic;

using Newtonsoft.Json;

namespace KeySwitchManager.Infrastructures.Storage.Json.KeySwitches.Cakewalk.Models
{
    public class Articulation
    {
        [JsonProperty( "id" )]
        public int Id { get; }
        [JsonProperty( "name" )]
        public string Name { get; }
        [JsonProperty( "index" )]
        public int Index { get; }
        [JsonProperty( "groupId" )]
        public int GroupId { get; }
        [JsonProperty( "color" )]
        public string Color { get; } // = "ffff0000"; // ARGB
        [JsonProperty( "duration" )]
        public int Duration { get; }
        [JsonProperty( "events" )]
        public IList<MidiEvent> Events { get; }
        [JsonProperty( "transforms" )]
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
