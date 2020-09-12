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
            Guid id,
            DateTime created,
            DateTime lastUpdated,
            string developerName,
            string productName,
            string articulationName,
            ArticulationType articulationType,
            int articulationGroup,
            int articulationColor );

        public Articulation Create(
            Guid id,
            DateTime created,
            DateTime lastUpdated,
            string developerName,
            string productName,
            string articulationName,
            ArticulationType articulationType,
            int articulationGroup,
            int articulationColor,
            IEnumerable<IMessage> midiNoteOns,
            IEnumerable<IMessage> midiControlChanges,
            IEnumerable<IMessage> midiProgramChanges );

        public class Default : IArticulationFactory
        {
            public Articulation Create(
                Guid id,
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
                Guid id,
                DateTime created,
                DateTime lastUpdated,
                string developerName,
                string productName,
                string articulationName,
                ArticulationType articulationType,
                int articulationGroup,
                int articulationColor,
                IEnumerable<IMessage> midiNoteOns,
                IEnumerable<IMessage> midiControlChanges,
                IEnumerable<IMessage> midiProgramChanges )
            {
                created     = TimeZoneInfo.ConvertTimeToUtc( created );
                lastUpdated = TimeZoneInfo.ConvertTimeToUtc( lastUpdated );

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