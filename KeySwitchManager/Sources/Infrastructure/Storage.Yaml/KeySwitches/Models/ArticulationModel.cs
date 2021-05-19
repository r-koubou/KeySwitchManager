using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using YamlDotNet.Serialization;

namespace KeySwitchManager.Storage.Yaml.KeySwitches.Models
{
    public class ArticulationModel
    {
        [Required]
        public string Name { get; set; } = default!;

        public MidiModel MidiMessage { get; set; } = new MidiModel();

        [YamlIgnore]
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