using System;
using System.Collections.Generic;

using ArticulationManager.Domain.Articulations.Aggregate;
using ArticulationManager.Domain.Articulations.Value;
using ArticulationManager.Domain.Commons;
using ArticulationManager.Domain.MidiMessages.Aggregate;
using ArticulationManager.Domain.Services;

namespace ArticulationManager.Domain.Articulations
{
    public interface IArticulationFactory
    {
        public Articulation Create(
            string id,
            DateTime created,
            DateTime lastUpdated,
            string developerName,
            string productName,
            string articulationName,
            ArticulationType articulationType,
            int articulationGroup,
            int articulationColor );

        public Articulation Create(
            string id,
            DateTime created,
            DateTime lastUpdated,
            string developerName,
            string productName,
            string articulationName,
            ArticulationType articulationType,
            int articulationGroup,
            int articulationColor,
            IEnumerable<NoteOn> midiNoteOns,
            IEnumerable<ControlChange> midiControlChanges,
            IEnumerable<ProgramChange> midiProgramChanges );

        public class DefaultFactory : IArticulationFactory
        {
            public Articulation Create(
                string id,
                DateTime created,
                DateTime lastUpdated,
                string developerName,
                string productName,
                string articulationName,
                ArticulationType articulationType,
                int articulationGroup,
                int articulationColor )
            {
                return new Articulation(
                    new EntityGuid( id ),
                    EntityDateTimeService.FromDateTime( created ),
                    EntityDateTimeService.FromDateTime( lastUpdated ),
                    new DeveloperName( developerName ),
                    new ProductName( productName ),
                    new ArticulationName( articulationName ),
                    articulationType,
                    new ArticulationGroup( articulationGroup ),
                    new ArticulationColor( articulationColor ),
                    new List<NoteOn>(),
                    new List<ControlChange>(),
                    new List<ProgramChange>()
                );
            }

            public Articulation Create(
                string id,
                DateTime created,
                DateTime lastUpdated,
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
                return new Articulation(
                    new EntityGuid( id ),
                    EntityDateTimeService.FromDateTime( created ),
                    EntityDateTimeService.FromDateTime( lastUpdated ),
                    new DeveloperName( developerName ),
                    new ProductName( productName ),
                    new ArticulationName( articulationName ),
                    articulationType,
                    new ArticulationGroup( articulationGroup ),
                    new ArticulationColor( articulationColor ),
                    midiNoteOns,
                    midiControlChanges,
                    midiProgramChanges
                );
            }
        }
    }
}