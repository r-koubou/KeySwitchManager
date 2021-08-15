using System.Collections.Generic;
using System.IO;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Domain.KeySwitches.Models.Values;
using KeySwitchManager.Infrastructures.Database.LiteDB.KeySwitches;
using KeySwitchManager.Testing.Commons.KeySwitches;

using NUnit.Framework;

namespace KeySwitchManager.Testing.Database.LiteDB.KeySwitches
{
    [TestFixture]
    public class LiteDbDeleteTest
    {
        private static IKeySwitchRepository CreateRepository()
            => new LiteDbKeySwitchRepository( new FilePath( $"{Path.GetTempFileName()}.db" ) );

        [Test]
        public void DeleteByIdTest()
        {
            using var repository = CreateRepository();
            var record = TestDataGenerator.CreateKeySwitch();

            repository.Save( record );
            Assert.AreEqual( 1, repository.Count() );

            repository.Delete( record.Id );
            Assert.AreEqual( 0, repository.Count() );

        }

        [Test]
        public void DeleteByDeveloperAndProduct()
        {
            using var repository = CreateRepository();
            var record = TestDataGenerator.CreateKeySwitch();

            repository.Save( record );
            Assert.AreEqual( 1, repository.Count() );

            repository.Delete( record.DeveloperName, record.ProductName );
            Assert.AreEqual( 0, repository.Count() );
        }

        [Test]
        public void DeleteByWildcardTest()
        {
            using var repository = CreateRepository();

            var saveRecord = TestDataGenerator.CreateKeySwitch();
            var deleteRecord = TestDataGenerator.CreateKeySwitch(
                DeveloperName.Any.Value,
                ProductName.Any.Value,
                InstrumentName.Any.Value
            );

            repository.Save( saveRecord );
            Assert.AreEqual( 1, repository.Count() );

            var deleted = repository.Delete( deleteRecord.DeveloperName, deleteRecord.ProductName );
            Assert.AreEqual( 1, deleted );
            Assert.AreEqual( 0, repository.Count() );

        }

        [Test]
        public void DeleteAllTest()
        {
            using var repository = CreateRepository();
            var record = new List<KeySwitch>
            {
                TestDataGenerator.CreateKeySwitch(),
                TestDataGenerator.CreateKeySwitch()
            };

            repository.Save( record[ 0 ] );
            repository.Save( record[ 1 ] );
            Assert.AreEqual( record.Count, repository.Count() );

            repository.DeleteAll();
            Assert.AreEqual( 0, repository.Count() );
        }
    }
}