using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KeySwitchManager.Json.KeySwitches.Models
{
    public class ArticulationModel
    {
        [Required]
        public string Name { get; set; } = default!;

        public MidiModel MidiMessage { get; set; } = new MidiModel();

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