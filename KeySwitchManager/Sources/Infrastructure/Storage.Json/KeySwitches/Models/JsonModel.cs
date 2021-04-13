using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KeySwitchManager.Infrastructure.Storage.Json.KeySwitches.Models
{
    public class JsonModel
    {
        [Required]
        public string Name { get; set; } = default!;

        public MidiModel MidiMessage { get; set; } = new MidiModel();

        public IReadOnlyDictionary<string, string> ExtraData { get; set; } = new Dictionary<string, string>();

        public JsonModel()
        {}

        public JsonModel(
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