using System.IO;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Infrastructure.Database.LiteDB.KeySwitches;
using KeySwitchManager.Testing.Commons.KeySwitches;

using NUnit.Framework;

namespace KeySwitchManager.Testing.Database.LiteDB.KeySwitches
{
    [TestFixture]
    public class LiteDbDeleteTest
    {
        [Test]
        public void DeleteTest()
        {
            using var repository = new LiteDbKeySwitchRepository( new FilePath( $"{Path.GetTempFileName()}.db" ) );
            var record = TestDataGenerator.CreateKeySwitch();

            #region Delete by Id
            repository.Save( record );
            Assert.AreEqual( 1, repository.Count() );

            repository.Delete( record.Id );
            Assert.AreEqual( 0, repository.Count() );
            #endregion

            #region Delete by DeveloperName and ProductName
            repository.Save( record );
            Assert.AreEqual( 1, repository.Count() );

            repository.Delete( record.DeveloperName, record.ProductName );
            Assert.AreEqual( 0, repository.Count() );
            #endregion

            #region Delete All
            repository.Save( record );
            Assert.AreEqual( 1, repository.Count() );

            repository.DeleteAll();
            Assert.AreEqual( 0, repository.Count() );
            #endregion

        }
    }
}