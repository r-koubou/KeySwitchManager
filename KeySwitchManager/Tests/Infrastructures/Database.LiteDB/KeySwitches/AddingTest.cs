using System.Collections.Generic;
using System.IO;
using System.Linq;

using KeySwitchManager.Domain.KeySwitches.Midi.Models.Entities;
using KeySwitchManager.Domain.KeySwitches.Midi.Models.Helpers;
using KeySwitchManager.Domain.KeySwitches.Midi.Models.Values;
using KeySwitchManager.Infrastructure.Database.LiteDB.KeySwitches;
using KeySwitchManager.Testing.Commons.KeySwitches;

using NUnit.Framework;

namespace KeySwitchManager.Testing.Database.LiteDB.KeySwitches
{
    [TestFixture]
    public class AddingTest
    {
        [Test]
        public void InsertTest()
        {
            var repository = new LiteDbKeySwitchRepository( new MemoryStream() );
            var articulation = TestDataGenerator.CreateArticulation(
                new List<MidiNoteOn>()
                {
                    new MidiNoteOn( new MidiStatus( MidiStatusHelper.NoteOn ), new MidiNoteNumber( 1 ), new MidiVelocity( 100 ) )
                },
                new List<MidiControlChange>(),
                new List<MidiProgramChange>()
            );
            var record = TestDataGenerator.CreateKeySwitch( articulation );

            var result = repository.Save( record );
            Assert.AreEqual( 1, result.Inserted );

            var seq = repository.Find( record.ProductName );
            var cmp = seq.First();
            Assert.AreEqual( record, cmp );

            seq = repository.Find( record.DeveloperName );
            cmp = seq.First();
            Assert.AreEqual( record, cmp );

            seq = repository.Find( record.ProductName );
            cmp = seq.First();
            Assert.AreEqual( record, cmp );

            seq = repository.Find( record.DeveloperName, record.ProductName, record.InstrumentName );
            cmp = seq.First();
            Assert.AreEqual( record, cmp );

        }
    }
}