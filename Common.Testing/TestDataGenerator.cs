using System;
using System.Collections.Generic;

using KeySwitchManager.Common.Utilities;
using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.KeySwitches.Aggregate;
using KeySwitchManager.Domain.KeySwitches.Value;
using KeySwitchManager.Domain.MidiMessages.Aggregate;

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
                new EntityDateTime( now ),
                new EntityDateTime( now ),
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
                new EntityDateTime( now ),
                new EntityDateTime( now ),
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
                new List<MidiNoteOn>(),
                new List<MidiControlChange>(),
                new List<MidiProgramChange>()
            );
        }

        public static Articulation CreateArticulation(
            IEnumerable<MidiNoteOn> noteOns,
            IEnumerable<MidiControlChange> controlChanges,
            IEnumerable<MidiProgramChange> programChanges )
        {
            var now = DateTimeHelper.NowUtc();

            return new Articulation(
                new ArticulationName( "Power Chord" ),
                ArticulationType.Default,
                new ArticulationGroup( 0 ),
                new ArticulationColor( 0 ),
                new List<MidiNoteOn>( noteOns ),
                new List<MidiControlChange>( controlChanges ),
                new List<MidiProgramChange>( programChanges )
            );
        }
        #endregion
    }
}