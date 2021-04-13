using System.Text.Json.Serialization;

namespace KeySwitchManager.Infrastructure.Storage.Json.Cakewalk.Models
{
    public enum PlayAt
    {
        Start = 1,
        End,
        Both,
        Duration,
        Default = Duration
    }

    public enum ChaseMode
    {
        Note = 1,
        CCs,
        Full,
        Default = CCs
    }

    public class MidiEvent
    {
        [JsonPropertyName( "b1" )]
        public int Byte1 { get; set; } = 0x00;
        [JsonPropertyName( "b2" )]
        public int Byte2 { get; set; } = 0x00;
        [JsonPropertyName( "b3" )]
        public int Byte3 { get; set; } = 0x00;
        [JsonPropertyName( "b4" )]
        public int Byte4 { get; set; } = 0x00;

        [JsonPropertyName( "allowTranspose" )]
        public int AllowTranspose { get; set; } = 0;

        [JsonPropertyName( "allowTransposeMidiCh" )]
        public int AllowTransposeMidiCh { get; set; } = 1;
        [JsonPropertyName( "triggerAt" )]
        public PlayAt TriggerAt { get; set; } = PlayAt.Default;
        [JsonPropertyName( "chaseMode" )]
        public ChaseMode ChaseMode { get; set; } = ChaseMode.Default;

        public MidiEvent()
        {}
    }
}
