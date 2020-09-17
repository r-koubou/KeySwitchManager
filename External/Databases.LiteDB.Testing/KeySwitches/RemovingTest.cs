using System.IO;

using KeySwitchManager.Common.Testing;
using KeySwitchManager.Databases.LiteDB.KeySwitches;

using NUnit.Framework;

namespace KeySwitchManager.Databases.LiteDB.Testing.KeySwitches
{
    [TestFixture]
    public class RemovingTest
    {
        [Test]
        public void RemoveTest()
        {
            var repository = new LiteDbKeySwitchRepository( new MemoryStream() );
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