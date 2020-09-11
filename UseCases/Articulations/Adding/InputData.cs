using System.Collections.Generic;

using ArticulationManager.Domain.Articulations.Value;
using ArticulationManager.Domain.MidiMessages.Aggregate;

namespace ArticulationManager.UseCases.Articulations.Adding
{
    public class InputData
    {
        public string DeveloperName { get; }
        public string ProductName { get; }
        public string ArticulationName { get; }
        public ArticulationType ArticulationType { get; }
        public int ArticulationGroup { get; }
        public int ArticulationColor { get; }
        public IEnumerable<NoteOn> MidiNoteOns { get; }
        public IEnumerable<ControlChange> MidiControlChanges { get; }
        public IEnumerable<ProgramChange> MidiProgramChanges { get; }

        public InputData(
            string developerName,
            string productName,
            string articulationName,
            ArticulationType articulationType,
            int articulationGroup,
            int articulationColor,
            IEnumerable<NoteOn> midiNoteOns,
            IEnumerable<ControlChange> midiControlChanges,
            IEnumerable<ProgramChange> midiProgramChanges )
        {
            DeveloperName      = developerName;
            ProductName        = productName;
            ArticulationName   = articulationName;
            ArticulationType   = articulationType;
            ArticulationGroup  = articulationGroup;
            ArticulationColor  = articulationColor;
            MidiNoteOns        = midiNoteOns;
            MidiControlChanges = midiControlChanges;
            MidiProgramChanges = midiProgramChanges;
        }
    }
}