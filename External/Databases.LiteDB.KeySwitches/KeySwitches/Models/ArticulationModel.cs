using System.Collections.Generic;

namespace Databases.LiteDB.KeySwitches.KeySwitches.Models
{
    public class ArticulationModel
    {
        public string ArticulationName { get; set; } = "Unknown";
        public string ArticulationType { get; set; } = KeySwitchManager.Domain.KeySwitches.Value.ArticulationType.Default.ToString();
        public int ArticulationGroup { get; set; }
        public int ArticulationColor { get; set; }
        public List<MidiMessageModel> NoteOn { get; set; } = new List<MidiMessageModel>();
        public List<MidiMessageModel> ControlChange { get; set; } = new List<MidiMessageModel>();
        public List<MidiMessageModel> ProgramChange { get; set; } = new List<MidiMessageModel>();

        public ArticulationModel()
        {}

        public ArticulationModel(
            string articulationName,
            int articulationGroup,
            int articulationColor,
            IEnumerable<MidiMessageModel> midiNoteOns,
            IEnumerable<MidiMessageModel> midiControlChanges,
            IEnumerable<MidiMessageModel> midiProgramChanges )
        {
            ArticulationName  = articulationName;
            ArticulationGroup = articulationGroup;
            ArticulationColor = articulationColor;
            NoteOn            = new List<MidiMessageModel>( midiNoteOns );
            ControlChange     = new List<MidiMessageModel>( midiControlChanges );
            ProgramChange     = new List<MidiMessageModel>( midiProgramChanges );
        }
    }
}