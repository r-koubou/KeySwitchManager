using System;
using System.Collections.Generic;

using ArticulationManager.Common.Utilities;
using ArticulationManager.Domain.Articulations.Aggregate;
using ArticulationManager.Domain.Articulations.Value;
using ArticulationManager.Domain.Commons;
using ArticulationManager.Domain.MidiMessages.Aggregate;
using ArticulationManager.Domain.Services;

namespace ArticulationManager.Databases.LiteDB.Testing
{
    public static class TestDataGenerator
    {
        public static Articulation Create(
            string developerName,
            string productName,
            string articulationName,
            ArticulationType articulationType = ArticulationType.Default,
            int articulationGroup = 0,
            int articulationColor = 0 )
        {
            DateTime now = DateTimeHelper.NowUtc();

            return new Articulation(
                new EntityGuid( Guid.NewGuid() ),
                EntityDateTimeService.FromDateTime( now ),
                EntityDateTimeService.FromDateTime( now ),
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

        public static Articulation CreateDummy()
        {
            return Create(
                "DeveloperName",
                "ProductName",
                "Power Chord"
            );
        }

    }
}