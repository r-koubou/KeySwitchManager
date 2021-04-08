using System.Text.Json.Serialization;

namespace KeySwitchManager.Json.KeySwitches.Cakewalk.Models
{
    internal enum EventKind
    {
        Note,
        KeyAft,
        Cc,
        Patch,
        ChAft,
        Wheel,
        Default = Note
    }

    internal class Transform
    {
        #region Input
        [JsonPropertyName( "evtKndIn" )]
        public EventKind InputKind { get; set; } = EventKind.Default;
        [JsonPropertyName( "minInCh" )]
        public int InputMidiChannelMin { get; set; } = 0;
        [JsonPropertyName( "maxInCh" )]
        public int InputMidiChannelMax { get; set; } = 0;
        [JsonPropertyName( "minInKyCN" )]
        public int InputMidiNoteOrCcNoMin { get; set; } = 0;
        [JsonPropertyName( "maxInKyCN" )]
        public int InputMidiNoteOrCcNoMax { get; set; } = 0;
        [JsonPropertyName( "minInVlCV" )]
        public int InputMidiVelOrCcValMin { get; set; } = 0;
        [JsonPropertyName( "maxInVlCV" )]
        public int InputMidiVelOrCcValMax { get; set; } = 0;
        #endregion

        #region Output
        [JsonPropertyName( "evtKndOut" )]
        public EventKind OutputKind { get; set; } = EventKind.Default;
        [JsonPropertyName( "minOutCh" )]
        public int OutputMidiChannelMin { get; set; } = 0;
        [JsonPropertyName( "maxOutCh" )]
        public int OutputMidiChannelMax { get; set; } = 0;
        [JsonPropertyName( "minOutKyCN" )]
        public int OutputNoteOrCcNoMin { get; set; } = 0;
        [JsonPropertyName( "maxOutKyCN" )]
        public int OutputNoteOrCcNoMax { get; set; } = 0;
        [JsonPropertyName( "minOutVlCV" )]
        public int OutputVelOrCcValMin { get; set; } = 0;
        [JsonPropertyName( "maxOutVlCV" )]
        public int OutputVelOrCcValMax { get; set; } = 0;
        #endregion

        #region Offset
        [JsonPropertyName( "offsetCh" )]
        public int OffsetChannel { get; set; } = 0;
        [JsonPropertyName( "offsetKyCN" )]
        public int OffsetNoteOrCcNo { get; set; } = 0;
        [JsonPropertyName( "offsetVlCV" )]
        public int OffsetVelOrCcVal { get; set; } = 0;
        [JsonPropertyName( "scaleCh" )]
        public bool EnableScaleChannel { get; set; } = false;
        [JsonPropertyName( "scaleKyCN" )]
        public bool EnableScaleNoteOrCcNo { get; set; } = false;
        [JsonPropertyName( "scaleVlCV" )]
        public bool EnableScaleVelOrCcVal { get; set; } = false;
        #endregion

        public Transform()
        {}
    }
}
