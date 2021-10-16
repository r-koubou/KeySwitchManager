using Newtonsoft.Json;

namespace KeySwitchManager.Infrastructures.Storage.Json.Cakewalk.Models
{
    public enum EventKind
    {
        Note,
        KeyAft,
        Cc,
        Patch,
        ChAft,
        Wheel,
        Default = Note
    }

    public class Transform
    {
        #region Input
        [JsonProperty( "evtKndIn" )]
        public EventKind InputKind { get; set; } = EventKind.Default;
        [JsonProperty( "minInCh" )]
        public int InputMidiChannelMin { get; set; } = 0;
        [JsonProperty( "maxInCh" )]
        public int InputMidiChannelMax { get; set; } = 0;
        [JsonProperty( "minInKyCN" )]
        public int InputMidiNoteOrCcNoMin { get; set; } = 0;
        [JsonProperty( "maxInKyCN" )]
        public int InputMidiNoteOrCcNoMax { get; set; } = 0;
        [JsonProperty( "minInVlCV" )]
        public int InputMidiVelOrCcValMin { get; set; } = 0;
        [JsonProperty( "maxInVlCV" )]
        public int InputMidiVelOrCcValMax { get; set; } = 0;
        #endregion

        #region Output
        [JsonProperty( "evtKndOut" )]
        public EventKind OutputKind { get; set; } = EventKind.Default;
        [JsonProperty( "minOutCh" )]
        public int OutputMidiChannelMin { get; set; } = 0;
        [JsonProperty( "maxOutCh" )]
        public int OutputMidiChannelMax { get; set; } = 0;
        [JsonProperty( "minOutKyCN" )]
        public int OutputNoteOrCcNoMin { get; set; } = 0;
        [JsonProperty( "maxOutKyCN" )]
        public int OutputNoteOrCcNoMax { get; set; } = 0;
        [JsonProperty( "minOutVlCV" )]
        public int OutputVelOrCcValMin { get; set; } = 0;
        [JsonProperty( "maxOutVlCV" )]
        public int OutputVelOrCcValMax { get; set; } = 0;
        #endregion

        #region Offset
        [JsonProperty( "offsetCh" )]
        public int OffsetChannel { get; set; } = 0;
        [JsonProperty( "offsetKyCN" )]
        public int OffsetNoteOrCcNo { get; set; } = 0;
        [JsonProperty( "offsetVlCV" )]
        public int OffsetVelOrCcVal { get; set; } = 0;
        [JsonProperty( "scaleCh" )]
        public bool EnableScaleChannel { get; set; } = false;
        [JsonProperty( "scaleKyCN" )]
        public bool EnableScaleNoteOrCcNo { get; set; } = false;
        [JsonProperty( "scaleVlCV" )]
        public bool EnableScaleVelOrCcVal { get; set; } = false;
        #endregion

        public Transform()
        {}
    }
}
