using System.Collections.Generic;

namespace Databases.LiteDB.KeySwitches.KeySwitches.Models
{
    public class ArticulationModel
    {
        public string ArticulationName { get; set; } = "Unknown";
        public string ArticulationType { get; set; } = KeySwitchManager.Domain.KeySwitches.Value.ArticulationType.Default.ToString();
        public int ArticulationGroup { get; set; }
        public int ArticulationColor { get; set; }
        public IReadOnlyCollection<MidiMessageModel> NoteOn { get; set; } = new List<MidiMessageModel>();
        public IReadOnlyCollection<MidiMessageModel> ControlChange { get; set; } = new List<MidiMessageModel>();
        public IReadOnlyCollection<MidiMessageModel> ProgramChange { get; set; } = new List<MidiMessageModel>();

        public ArticulationModel()
        {}

        public ArticulationModel(
            string articulationName,
            int articulationGroup,
            int articulationColor,
            IReadOnlyCollection<MidiMessageModel> midiNoteOns,
            IReadOnlyCollection<MidiMessageModel> midiControlChanges,
            IReadOnlyCollection<MidiMessageModel> midiProgramChanges )
        {
            ArticulationName  = articulationName;
            ArticulationGroup = articulationGroup;
            ArticulationColor = articulationColor;
            NoteOn            = midiNoteOns;
            ControlChange     = midiControlChanges;
            ProgramChange     = midiProgramChanges;
        }
    }
}