using System.Collections.Generic;
using System.IO;
using System.Linq;

using Databases.LiteDB.KeySwitches.KeySwitches;

using KeySwitchManager.Common.Testing.KeySwitches;
using KeySwitchManager.Domain.MidiMessages.Aggregate;
using KeySwitchManager.Domain.MidiMessages.Helpers;
using KeySwitchManager.Domain.MidiMessages.Value;
using KeySwitchManager.Interactors.Testing.KeySwitches;

using NUnit.Framework;

namespace Databases.LiteDB.KeySwitches.Testing.KeySwitches
{
    [TestFixture]
    public class AddingTest
    {
        [Test]
        public void AddTest()
        {
            var repository = new LiteDbKeySwitchRepository( new MemoryStream() );
            var articulation = TestDataGenerator.CreateArticulation(
                new List<MidiNoteOn>()
                {
                    new MidiNoteOn( IMidiStatusFactory.Default.Create( MidiStatusHelper.NoteOn ), IMidiNoteNumberFactory.Default.Create( 1 ), IMidiVelocityFactory.Default.Create( 100 ) )
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