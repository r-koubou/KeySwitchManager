using System.Collections.Generic;

using Newtonsoft.Json;

namespace KeySwitchManager.Json.KeySwitches.Models
{
    [JsonObject("midi_message")]
    public class MidiModel
    {
        [JsonProperty( "note_on")]
        public IReadOnlyCollection<MidiMessageModel> NoteOn { get; set; } = new List<MidiMessageModel>();
        [JsonProperty( "control_change")]
        public IReadOnlyCollection<MidiMessageModel> ControlChange { get; set; } = new List<MidiMessageModel>();
        [JsonProperty( "program_change")]
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