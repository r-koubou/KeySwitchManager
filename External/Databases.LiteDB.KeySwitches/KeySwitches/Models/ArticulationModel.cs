using System.Collections.Generic;

namespace Databases.LiteDB.KeySwitches.KeySwitches.Models
{
    public class ArticulationModel
    {
        public string ArticulationName { get; set; } = "Unknown";
        public IReadOnlyCollection<MidiMessageModel> NoteOn { get; set; } = new List<MidiMessageModel>();
        public IReadOnlyCollection<MidiMessageModel> ControlChange { get; set; } = new List<MidiMessageModel>();
        public IReadOnlyCollection<MidiMessageModel> ProgramChange { get; set; } = new List<MidiMessageModel>();
        public IReadOnlyDictionary<string, object> ExtraData { get; set; } = new Dictionary<string, object>();

        public ArticulationModel()
        {}

        public ArticulationModel(
            string articulationName,
            IReadOnlyCollection<MidiMessageModel> midiNoteOns,
            IReadOnlyCollection<MidiMessageModel> midiControlChanges,
            IReadOnlyCollection<MidiMessageModel> midiProgramChanges,
            IReadOnlyDictionary<string, object> extraData )
        {
            ArticulationName = articulationName;
            NoteOn           = midiNoteOns;
            ControlChange    = midiControlChanges;
            ProgramChange    = midiProgramChanges;
            ExtraData        = extraData;
        }
    }
}