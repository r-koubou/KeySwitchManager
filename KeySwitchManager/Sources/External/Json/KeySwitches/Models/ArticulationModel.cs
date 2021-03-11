using System.Collections.Generic;

using Newtonsoft.Json;

namespace KeySwitchManager.Json.KeySwitches.Models
{
    [JsonObject("articulation")]
    public class ArticulationModel
    {
        [JsonProperty( "name")]
        [JsonRequired]
        public string Name { get; set; } = default!;

        [JsonProperty("midi_message")]
        public MidiModel MidiMessage { get; set; } = new MidiModel();

        [JsonProperty( "extra")]
        public IReadOnlyDictionary<string, string> ExtraData { get; set; } = new Dictionary<string, string>();

        public ArticulationModel()
        {}

        public ArticulationModel(
            string name,
            MidiModel midiMessage,
            IReadOnlyDictionary<string, string> extraData )
        {
            Name        = name;
            MidiMessage = midiMessage;
            ExtraData   = extraData;
        }
    }
}