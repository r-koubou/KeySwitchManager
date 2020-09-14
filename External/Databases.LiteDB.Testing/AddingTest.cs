using System.Collections.Generic;
using System.IO;
using System.Linq;

using ArticulationManager.Common.Testing;
using ArticulationManager.Databases.LiteDB.Articulations;
using ArticulationManager.Domain.MidiMessages.Aggregate;
using ArticulationManager.Domain.MidiMessages.Value;

using NUnit.Framework;

namespace ArticulationManager.Databases.LiteDB.Testing
{
    [TestFixture]
    public class AddingTest
    {
        [Test]
        public void AddTest()
        {
            var repository = new LiteDbKeySwitchRepository( new MemoryStream() );
            var articulation = TestDataGenerator.CreateArticulation(
                new List<NoteOn>()
                {
                    new NoteOn( new NoteNumber( 1 ), new Velocity( 100 ) )
                },
                new List<ControlChange>(),
                new List<ProgramChange>()
            );
            var record = TestDataGenerator.CreateKeySwitch( articulation );

            repository.Save( record );

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