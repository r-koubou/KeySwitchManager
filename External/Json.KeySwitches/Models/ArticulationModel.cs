using KeySwitchManager.Domain.KeySwitches.Value;

using Newtonsoft.Json;

namespace KeySwitchManager.Json.KeySwitches.Models
{
    [JsonObject("articulation")]
    public class ArticulationModel
    {
        [JsonProperty( "name")]
        [JsonRequired]
        public string Name { get; set; } = default!;

        [JsonProperty( "type" )]
        [JsonRequired]
        public ArticulationType Type { get; set; } = ArticulationType.Default;

        [JsonProperty( "group" )]
        public int Group { get; set; }

        [JsonProperty( "color")]
        public int Color { get; set; }

        [JsonProperty("midi_message")]
        public MidiModel MidiMessage { get; set; } = new MidiModel();

        public ArticulationModel()
        {}

        public ArticulationModel(
            string name,
            ArticulationType type,
            int group,
            int color,
            MidiModel midiMessage )
        {
            Name        = name;
            Type        = type;
            Group       = group;
            Color       = color;
            MidiMessage = midiMessage;
        }
    }
}