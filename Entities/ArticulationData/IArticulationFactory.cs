using System.Collections.Generic;

using ArticulationManager.Entities.ArticulationData.Aggregate;
using ArticulationManager.Entities.ArticulationData.Value;
using ArticulationManager.Entities.MidiEventData.Aggregate;

namespace ArticulationManager.Entities.ArticulationData
{
    public interface IArticulationFactory
    {
        public Articulation Create(
            int id,
            string developerName,
            string productName,
            string articulationName,
            ArticulationType articulationType,
            int articulationGroup,
            int articulationColor );

        public Articulation Create(
            int id,
            string developerName,
            string productName,
            string articulationName,
            ArticulationType articulationType,
            int articulationGroup,
            int articulationColor,
            IEnumerable<MidiNoteOn> midiNoteOns,
            IEnumerable<MidiControlChange> midiControlChanges,
            IEnumerable<MidiProgramChange> midiProgramChanges );
    }

    public class SimpleArticulationFactory : IArticulationFactory
    {
        public Articulation Create(
            int id,
            string developerName,
            string productName,
            string articulationName,
            ArticulationType articulationType,
            int articulationGroup,
            int articulationColor )
        {
            return new Articulation(
                new ArticulationId( id ),
                new DeveloperName( developerName ),
                new ProductName( productName ),
                new ArticulationName( articulationName ),
                articulationType,
                new ArticulationGroup( articulationGroup ),
                new ArticulationColor( articulationColor ),
                new List<MidiNoteOn>(),
                new List<MidiControlChange>(),
                new List<MidiProgramChange>() );
        }

        public Articulation Create(
            int id,
            string developerName,
            string productName,
            string articulationName,
            ArticulationType articulationType,
            int articulationGroup,
            int articulationColor,
            IEnumerable<MidiNoteOn> midiNoteOns,
            IEnumerable<MidiControlChange> midiControlChanges,
            IEnumerable<MidiProgramChange> midiProgramChanges )
        {
            return new Articulation(
                new ArticulationId( id ),
                new DeveloperName( developerName ),
                new ProductName( productName ),
                new ArticulationName( articulationName ),
                articulationType,
                new ArticulationGroup( articulationGroup ),
                new ArticulationColor( articulationColor ),
                midiNoteOns,
                midiControlChanges,
                midiProgramChanges );
        }
    }
}