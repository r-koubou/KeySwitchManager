using System;
using System.Collections.Generic;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Domain.KeySwitches.Models.Entities;
using KeySwitchManager.Domain.KeySwitches.Models.Values;
using KeySwitchManager.Domain.MidiMessages.Models.Entities;

using RkHelper.Time;

namespace KeySwitchManager.Testing.Commons.KeySwitches
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
                new KeySwitchId( Guid.NewGuid() ),
                new Author( "Author" ),
                new Description( "Description" ),
                new UtcDateTime( now ),
                new UtcDateTime( now ),
                new DeveloperName( developerName ),
                new ProductName( productName ),
                new InstrumentName( instrumentName ),
                new DataList<Articulation>( new[] { CreateArticulation() } ),
                new ExtraData( new Dictionary<ExtraDataKey, ExtraDataValue>
                {
                    {new ExtraDataKey( "extKey" ), new ExtraDataValue( "extValue" ) }
                })
            );
        }

        public static KeySwitch CreateKeySwitch( Articulation articulation )
        {
            return CreateKeySwitch( new[] { articulation } );
        }

        public static KeySwitch CreateKeySwitch( IReadOnlyCollection<Articulation> articulations )
        {
            var now = DateTimeHelper.NowUtc();

            return new KeySwitch(
                new KeySwitchId( Guid.NewGuid() ),
                new Author( "Author" ),
                new Description( "Description" ),
                new UtcDateTime( now ),
                new UtcDateTime( now ),
                new DeveloperName( "DeveloperName" ),
                new ProductName( "ProductName" ),
                new InstrumentName( "E.Guitar" ),
                new DataList<Articulation>( articulations ),
                new ExtraData( new Dictionary<ExtraDataKey, ExtraDataValue>
                {
                    { new ExtraDataKey( "extKey" ), new ExtraDataValue( "extValue" ) }
                })
            );
        }
        #endregion

        #region Articulation
        public static Articulation CreateArticulation( string articulationName = "Power Chord" )
        {
            return new Articulation(
                new ArticulationName( articulationName ),
                new DataList<MidiNoteOn>(),
                new DataList<MidiControlChange>(),
                new DataList<MidiProgramChange>(),
                new ExtraData( new Dictionary<ExtraDataKey, ExtraDataValue>
                {
                    { new ExtraDataKey( "extKey" ), new ExtraDataValue( "extValue" ) }
                })
            );
        }

        public static Articulation CreateArticulation(
            IReadOnlyCollection<MidiNoteOn> noteOns,
            IReadOnlyCollection<MidiControlChange> controlChanges,
            IReadOnlyCollection<MidiProgramChange> programChanges )
        {
            return new Articulation(
                new ArticulationName( "Power Chord" ),
                new DataList<IMidiChannelVoiceMessage>( noteOns ),
                new DataList<IMidiChannelVoiceMessage>( controlChanges ),
                new DataList<IMidiChannelVoiceMessage>( programChanges ),
                new ExtraData( new Dictionary<ExtraDataKey, ExtraDataValue>
                {
                    { new ExtraDataKey( "extKey" ), new ExtraDataValue( "extValue" ) }
                })
            );
        }
        #endregion
    }
}