using System.Collections.Generic;

using ArticulationManager.Domain.Articulations.Value;
using ArticulationManager.Domain.Commons;
using ArticulationManager.Domain.MidiMessages.Aggregate;

namespace ArticulationManager.Domain.Articulations.Aggregate
{
    public class Articulation
    {
        public EntityGuid Guid { get; }
        public EntityDateTime Created { get; }
        public EntityDateTime LastUpdated { get; }
        public DeveloperName DeveloperName { get; }
        public ProductName ProductName { get; }
        public ArticulationName ArticulationName { get; }
        public ArticulationType ArticulationType { get; }
        public ArticulationGroup ArticulationGroup { get; }
        public ArticulationColor ArticulationColor { get; }
        public IReadOnlyList<NoteOn> MidiNoteOns { get; }
        public IReadOnlyList<ControlChange> MidiControlChanges { get; }
        public IReadOnlyList<ProgramChange> MidiProgramChanges { get; }

        public Articulation(
            EntityGuid guid,
            EntityDateTime created,
            EntityDateTime lastUpdated,
            DeveloperName developerName,
            ProductName productName,
            ArticulationName articulationName,
            ArticulationType articulationType,
            ArticulationGroup articulationGroup,
            ArticulationColor articulationColor,
            IEnumerable<NoteOn> midiNoteOns,
            IEnumerable<ControlChange> midiControlChanges,
            IEnumerable<ProgramChange> midiProgramChanges )
        {
            Guid               = guid;
            Created            = created;
            LastUpdated        = lastUpdated;
            DeveloperName      = developerName;
            ProductName        = productName;
            ArticulationName   = articulationName;
            ArticulationType   = articulationType;
            ArticulationGroup  = articulationGroup;
            ArticulationColor  = articulationColor;
            MidiNoteOns        = new List<NoteOn>( midiNoteOns );
            MidiControlChanges = new List<ControlChange>( midiControlChanges );
            MidiProgramChanges = new List<ProgramChange>( midiProgramChanges );
        }
    }
}