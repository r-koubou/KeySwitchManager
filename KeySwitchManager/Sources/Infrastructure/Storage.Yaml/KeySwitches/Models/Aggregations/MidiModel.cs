using System.Collections.Generic;

namespace KeySwitchManager.Storage.Yaml.KeySwitches.Models.Aggregations
{
    public class MidiModel
    {
        public IList<MidiNoteOnModel> NoteOn { get; set; } = new List<MidiNoteOnModel>();
        public IList<MidiControlChangeModel> ControlChange { get; set; } = new List<MidiControlChangeModel>();
        public IList<MidiProgramChangeModel> ProgramChange { get; set; } = new List<MidiProgramChangeModel>();

        public MidiModel()
        {}

        public MidiModel(
            IReadOnlyCollection<MidiNoteOnModel> noteOn,
            IReadOnlyCollection<MidiControlChangeModel> controlChange,
            IReadOnlyCollection<MidiProgramChangeModel> programChange )
        {
            NoteOn        = new List<MidiNoteOnModel>( noteOn );
            ControlChange = new List<MidiControlChangeModel>( controlChange );
            ProgramChange = new List<MidiProgramChangeModel>( programChange );
        }
    }
}