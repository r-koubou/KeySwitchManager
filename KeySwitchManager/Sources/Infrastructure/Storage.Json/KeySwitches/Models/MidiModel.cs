using System.Collections.Generic;

namespace KeySwitchManager.Infrastructure.Storage.Json.KeySwitches.Models
{
    public class MidiModel
    {
        public IReadOnlyCollection<MidiMessageModel> NoteOn { get; set; } = new List<MidiMessageModel>();
        public IReadOnlyCollection<MidiMessageModel> ControlChange { get; set; } = new List<MidiMessageModel>();
        public IReadOnlyCollection<MidiMessageModel> ProgramChange { get; set; } = new List<MidiMessageModel>();

        public MidiModel()
        {}

        public MidiModel(
            IReadOnlyCollection<MidiMessageModel> noteOn,
            IReadOnlyCollection<MidiMessageModel> controlChange,
            IReadOnlyCollection<MidiMessageModel> programChange )
        {
            NoteOn        = noteOn;
            ControlChange = controlChange;
            ProgramChange = programChange;
        }
    }
}