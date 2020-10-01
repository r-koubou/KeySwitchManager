using System.Collections.Generic;

namespace Databases.LiteDB.KeySwitches.KeySwitches.Models
{
    public class ArticulationModel
    {
        public string ArticulationName { get; set; } = "Unknown";
        public ICollection<MidiMessageModel> NoteOn { get; set; } = new List<MidiMessageModel>();
        public ICollection<MidiMessageModel> ControlChange { get; set; } = new List<MidiMessageModel>();
        public ICollection<MidiMessageModel> ProgramChange { get; set; } = new List<MidiMessageModel>();
        public IDictionary<string, object> ExtraData { get; set; } = new Dictionary<string, object>();

        public ArticulationModel()
        {}

        public ArticulationModel(
            string articulationName,
            ICollection<MidiMessageModel> midiNoteOns,
            ICollection<MidiMessageModel> midiControlChanges,
            ICollection<MidiMessageModel> midiProgramChanges,
            IDictionary<string, object> extraData )
        {
            ArticulationName = articulationName;
            NoteOn           = midiNoteOns;
            ControlChange    = midiControlChanges;
            ProgramChange    = midiProgramChanges;
            ExtraData        = extraData;
        }
    }
}