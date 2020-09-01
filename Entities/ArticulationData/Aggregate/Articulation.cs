using System.Collections.Generic;

using ArticulationManager.Entities.ArticulationData.Value;
using ArticulationManager.Entities.MidiEventData.Aggregate;

namespace ArticulationManager.Entities.ArticulationData.Aggregate
{
    public class Articulation
    {
        public ArticulationId Id { get; }
        public DeveloperName DeveloperName { get; }
        public ProductName ProductName { get; }
        public ArticulationName ArticulationName { get; }
        public ArticulationType ArticulationType { get; }
        public ArticulationGroup ArticulationGroup { get; }
        public ArticulationColor ArticulationColor { get; }
        public IReadOnlyList<MidiNoteOn> MidiNoteOns { get; }
        public IReadOnlyList<MidiControlChange> MidiControlChanges { get; }
        public IReadOnlyList<MidiProgramChange> MidiProgramChanges { get; }
        public Articulation(
            ArticulationId id,
            DeveloperName developerName,
            ProductName productName,
            ArticulationName articulationName,
            ArticulationType articulationType,
            ArticulationGroup articulationGroup,
            ArticulationColor articulationColor,
            IEnumerable<MidiNoteOn> midiNoteOns,
            IEnumerable<MidiControlChange> midiControlChanges,
            IEnumerable<MidiProgramChange> midiProgramChanges )
        {
            Id                 = id;
            DeveloperName      = developerName;
            ProductName        = productName;
            ArticulationName   = articulationName;
            ArticulationType   = articulationType;
            ArticulationGroup  = articulationGroup;
            ArticulationColor  = articulationColor;
            MidiNoteOns        = new List<MidiNoteOn>( midiNoteOns );
            MidiControlChanges = new List<MidiControlChange>( midiControlChanges );
            MidiProgramChanges = new List<MidiProgramChange>( midiProgramChanges );
        }
    }
}