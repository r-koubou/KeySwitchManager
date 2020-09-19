using System.Collections.Generic;

using Newtonsoft.Json;

namespace KeySwitchManager.Json.KeySwitches.Models
{
    [JsonObject("midi_message")]
    public class MidiModel
    {
        [JsonProperty( "note_on")]
        public List<MidiMessageModel> NoteOn { get; set; } = new List<MidiMessageModel>();
        [JsonProperty( "control_change")]
        public List<MidiMessageModel> ControlChange { get; set; } = new List<MidiMessageModel>();
        [JsonProperty( "program_change")]
        public List<MidiMessageModel> ProgramChange { get; set; } = new List<MidiMessageModel>();

        public MidiModel()
        {}

        public MidiModel(
            IEnumerable<MidiMessageModel> noteOn,
            IEnumerable<MidiMessageModel> controlChange,
            IEnumerable<MidiMessageModel> programChange )
        {
            NoteOn        = new List<MidiMessageModel>( noteOn );
            ControlChange = new List<MidiMessageModel>( controlChange );
            ProgramChange = new List<MidiMessageModel>( programChange );
        }
    }
}