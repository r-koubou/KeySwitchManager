using System.Collections.Generic;

using LiteDB;

namespace Databases.LiteDB.KeySwitches.KeySwitches.Models
{
    public class ArticulationModel
    {
        public string ArticulationName { get; set; } = "Unknown";
        public IReadOnlyCollection<MidiMessageModel> NoteOn { get; set; } = new List<MidiMessageModel>();
        public IReadOnlyCollection<MidiMessageModel> ControlChange { get; set; } = new List<MidiMessageModel>();
        public IReadOnlyCollection<MidiMessageModel> ProgramChange { get; set; } = new List<MidiMessageModel>();
        public Dictionary<string, BsonValue> ExtraData { get; set; } = new Dictionary<string, BsonValue>();

        public ArticulationModel()
        {}

        public ArticulationModel(
            string articulationName,
            IReadOnlyCollection<MidiMessageModel> midiNoteOns,
            IReadOnlyCollection<MidiMessageModel> midiControlChanges,
            IReadOnlyCollection<MidiMessageModel> midiProgramChanges,
            Dictionary<string, BsonValue> extraData )

        {
            ArticulationName  = articulationName;
            NoteOn            = midiNoteOns;
            ControlChange     = midiControlChanges;
            ProgramChange     = midiProgramChanges;
            ExtraData         = extraData;
        }
    }
}