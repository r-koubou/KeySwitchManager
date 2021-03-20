using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace KeySwitchManager.Json.KeySwitch.Cakewalk.Model
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
        public IList<Event> Events { get; }
        [JsonPropertyName( "transforms" )]
        public IList<Transform> Transforms { get; }

        public Articulation(
            int id,
            string name,
            int index,
            int groupId,
            string color,
            int duration,
            IList<Event> events,
            IList<Transform> transforms )
        {
            Id         = id;
            Name       = name;
            Index      = index;
            GroupId    = groupId;
            Color      = color;
            Duration   = duration;
            Events     = new List<Event>( events );
            Transforms = new List<Transform>( transforms );
        }

        public class Event
        {
            [JsonPropertyName( "b1" )]
            public int Byte1 { get; }
            [JsonPropertyName( "b2" )]
            public int Byte2 { get; }
            [JsonPropertyName( "b3" )]
            public int Byte3 { get; }
            [JsonPropertyName( "b4" )]
            public int Byte4 { get; }
            [JsonPropertyName( "allowTranspose" )]
            public int AllowTranspose { get; }
            [JsonPropertyName( "allowTransposeMidiCh" )]
            public int AllowTransposeMidiCh { get; }
            [JsonPropertyName( "triggerAt" )]
            public int TriggerAt { get; }
            [JsonPropertyName( "chaseMode" )]
            public int ChaseMode { get; }

            public Event(
                int byte1,
                int byte2,
                int byte3,
                int byte4,
                int allowTranspose,
                int allowTransposeMidiCh,
                int triggerAt,
                int chaseMode )
            {
                Byte1                = byte1;
                Byte2                = byte2;
                Byte3                = byte3;
                Byte4                = byte4;
                AllowTranspose       = allowTranspose;
                AllowTransposeMidiCh = allowTransposeMidiCh;
                TriggerAt            = triggerAt;
                ChaseMode            = chaseMode;
            }
        }

        public class Transform
        {}
    }
}
