using System.Collections.Generic;

namespace KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches.Models.Aggregations
{
    public class ArticulationModel
    {
        public string Name { get; set; } = default!;

        public MidiModel MidiMessage { get; set; } = new MidiModel();

        public IDictionary<string, string> ExtraData { get; set; } = new Dictionary<string, string>();

        public ArticulationModel()
        {}

        public ArticulationModel(
            string name,
            MidiModel midiMessage,
            IReadOnlyDictionary<string, string> extraData )
        {
            Name        = name;
            MidiMessage = midiMessage;
            ExtraData   = new Dictionary<string, string>( extraData );
        }
    }
}