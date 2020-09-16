using System;
using System.Collections.Generic;

using KeySwitchManager.Common.Utilities;
using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.KeySwitches.Aggregate;
using KeySwitchManager.Domain.KeySwitches.Value;
using KeySwitchManager.Domain.MidiMessages.Aggregate;
using KeySwitchManager.Domain.Services;

namespace KeySwitchManager.Common.Testing
{
    public static class TestDataGenerator
    {
        #region KeySwitch
        public static KeySwitch CreateKeySwitch(
            string developerName = "DeveloperName",
            string productName = "ProductName",
            string instrumentName = "E.Guitar" )
        {
            var now = DateTimeHelper.NowUtc();

            return new KeySwitch(
                new EntityGuid( Guid.NewGuid() ),
                new Author( "Author" ),
                new Description( "Description" ),
                EntityDateTimeService.FromDateTime( now ),
                EntityDateTimeService.FromDateTime( now ),
                new DeveloperName( developerName ),
                new ProductName( productName ),
                new InstrumentName( instrumentName ),
                new [] { CreateArticulation() }
            );
        }

        public static KeySwitch CreateKeySwitch( Articulation articulation )
        {
            return CreateKeySwitch( new[] { articulation } );
        }

        public static KeySwitch CreateKeySwitch( IEnumerable<Articulation> articulations )
        {
            var now = DateTimeHelper.NowUtc();

            return new KeySwitch(
                new EntityGuid( Guid.NewGuid() ),
                new Author( "Author" ),
                new Description( "Description" ),
                EntityDateTimeService.FromDateTime( now ),
                EntityDateTimeService.FromDateTime( now ),
                new DeveloperName( "DeveloperName" ),
                new ProductName( "ProductName" ),
                new InstrumentName( "E.Guitar" ),
                articulations
            );
        }
        #endregion

        #region Articulation
        public static Articulation CreateArticulation(
            string articulationName = "Power Chord",
            ArticulationType articulationType = ArticulationType.Default,
            int articulationGroup = 0,
            int articulationColor = 0 )
        {
            var now = DateTimeHelper.NowUtc();

            return new Articulation(
                new ArticulationName( articulationName ),
                articulationType,
                new ArticulationGroup( articulationGroup ),
                new ArticulationColor( articulationColor ),
                new List<NoteOn>(),
                new List<ControlChange>(),
                new List<ProgramChange>()
            );
        }

        public static Articulation CreateArticulation(
            IEnumerable<NoteOn> noteOns,
            IEnumerable<ControlChange> controlChanges,
            IEnumerable<ProgramChange> programChanges )
        {
            var now = DateTimeHelper.NowUtc();

            return new Articulation(
                new ArticulationName( "Power Chord" ),
                ArticulationType.Default,
                new ArticulationGroup( 0 ),
                new ArticulationColor( 0 ),
                new List<NoteOn>( noteOns ),
                new List<ControlChange>( controlChanges ),
                new List<ProgramChange>( programChanges )
            );
        }
        #endregion
    }
}