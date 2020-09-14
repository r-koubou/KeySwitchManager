using ArticulationManager.Domain.Articulations.Value;

using Newtonsoft.Json;

namespace ArticulationManager.Json.Articulations.Model
{
    [JsonObject("articulation")]
    public class ArticulationModel
    {
        [JsonProperty( "articulation_name")]
        [JsonRequired]
        public string ArticulationName { get; set; } = default!;

        [JsonProperty( "articulation_type" )]
        [JsonRequired]
        public ArticulationType ArticulationType { get; set; } = ArticulationType.Default;

        [JsonProperty( "articulation_group" )]
        public int ArticulationGroup { get; set; }

        [JsonProperty( "articulation_color")]
        public int ArticulationColor { get; set; }

        [JsonProperty("midi_message")]
        public MidiModel MidiMessage { get; set; } = new MidiModel();

        public ArticulationModel()
        {}

        public ArticulationModel(
            string articulationName,
            ArticulationType articulationType,
            int articulationGroup,
            int articulationColor,
            MidiModel midiMessage )
        {
            ArticulationName  = articulationName;
            ArticulationType  = articulationType;
            ArticulationGroup = articulationGroup;
            ArticulationColor = articulationColor;
            MidiMessage       = midiMessage;
        }
    }
}