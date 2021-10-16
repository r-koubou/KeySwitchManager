using Newtonsoft.Json;

namespace KeySwitchManager.Infrastructures.Storage.Json.Cakewalk.Models
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
        [JsonProperty( "b1" )]
        public int Byte1 { get; set; } = 0x00;
        [JsonProperty( "b2" )]
        public int Byte2 { get; set; } = 0x00;
        [JsonProperty( "b3" )]
        public int Byte3 { get; set; } = 0x00;
        [JsonProperty( "b4" )]
        public int Byte4 { get; set; } = 0x00;

        [JsonProperty( "allowTranspose" )]
        public int AllowTranspose { get; set; } = 0;

        [JsonProperty( "allowTransposeMidiCh" )]
        public int AllowTransposeMidiCh { get; set; } = 1;
        [JsonProperty( "triggerAt" )]
        public PlayAt TriggerAt { get; set; } = PlayAt.Default;
        [JsonProperty( "chaseMode" )]
        public ChaseMode ChaseMode { get; set; } = ChaseMode.Default;

        public MidiEvent()
        {}
    }
}
