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
                IAuthorFactory.Default.Create( "Author" ),
                IDescriptionFactory.Default.Create( "Description" ),
                new EntityDateTime( now ),
                new EntityDateTime( now ),
                IDeveloperNameFactory.Default.Create( developerName ),
                IProductNameFactory.Default.Create( productName ),
                IInstrumentNameFactory.Default.Create( instrumentName ),
                new DataList<Articulation>( new[] { CreateArticulation() } ),
                new ExtraData( new Dictionary<ExtraDataKey, ExtraDataValue>
                {
                    {IExtraDataKeyFactory.Default.Create( "extKey" ), IExtraDataValueFactory.Default.Create( "extValue" ) }
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
                new EntityGuid( Guid.NewGuid() ),
                IAuthorFactory.Default.Create( "Author" ),
                IDescriptionFactory.Default.Create( "Description" ),
                new EntityDateTime( now ),
                new EntityDateTime( now ),
                IDeveloperNameFactory.Default.Create( "DeveloperName" ),
                IProductNameFactory.Default.Create( "ProductName" ),
                IInstrumentNameFactory.Default.Create( "E.Guitar" ),
                new DataList<Articulation>( articulations ),
                new ExtraData( new Dictionary<ExtraDataKey, ExtraDataValue>
                {
                    { IExtraDataKeyFactory.Default.Create( "extKey" ), IExtraDataValueFactory.Default.Create( "extValue" ) }
                })
            );
        }
        #endregion

        #region Articulation
        public static Articulation CreateArticulation( string articulationName = "Power Chord" )
        {
            return new Articulation(
                IArticulationNameFactory.Default.Create( articulationName ),
                new DataList<MidiNoteOn>(),
                new DataList<MidiControlChange>(),
                new DataList<MidiProgramChange>(),
                new ExtraData( new Dictionary<ExtraDataKey, ExtraDataValue>
                {
                    { IExtraDataKeyFactory.Default.Create( "extKey" ), IExtraDataValueFactory.Default.Create( "extValue" ) }
                })
            );
        }

        public static Articulation CreateArticulation(
            IReadOnlyCollection<MidiNoteOn> noteOns,
            IReadOnlyCollection<MidiControlChange> controlChanges,
            IReadOnlyCollection<MidiProgramChange> programChanges )
        {
            return new Articulation(
                IArticulationNameFactory.Default.Create( "Power Chord" ),
                new DataList<IMidiMessage>( noteOns ),
                new DataList<IMidiMessage>( controlChanges ),
                new DataList<IMidiMessage>( programChanges ),
                new ExtraData( new Dictionary<ExtraDataKey, ExtraDataValue>
                {
                    { IExtraDataKeyFactory.Default.Create( "extKey" ), IExtraDataValueFactory.Default.Create( "extValue" ) }
                })
            );
        }
        #endregion
    }
}