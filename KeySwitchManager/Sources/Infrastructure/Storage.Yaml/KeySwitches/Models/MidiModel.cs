using System.Collections.Generic;

namespace KeySwitchManager.Storage.Yaml.KeySwitches.Models
{
    public class MidiModel
    {
        public IList<MidiMessageModel> NoteOn { get; set; } = new List<MidiMessageModel>();
        public IList<MidiMessageModel> ControlChange { get; set; } = new List<MidiMessageModel>();
        public IList<MidiMessageModel> ProgramChange { get; set; } = new List<MidiMessageModel>();

        public MidiModel()
        {}

        public MidiModel(
            IReadOnlyCollection<MidiMessageModel> noteOn,
            IReadOnlyCollection<MidiMessageModel> controlChange,
            IReadOnlyCollection<MidiMessageModel> programChange )
        {
            NoteOn        = new List<MidiMessageModel>( noteOn );
            ControlChange = new List<MidiMessageModel>( controlChange );
            ProgramChange = new List<MidiMessageModel>( programChange );
        }
    }
}