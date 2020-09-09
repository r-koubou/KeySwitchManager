using System.IO;

using ArticulationManager.Databases.LiteDB.Articulations;

using NUnit.Framework;

namespace ArticulationManager.Databases.LiteDB.Testing
{
    [TestFixture]
    public class RemovingTest
    {
        [Test]
        public void RemoveTest()
        {
            var repository = new LiteDbArticulationRepository( new MemoryStream() );
            var record = TestDataGenerator.CreateDummy();

            #region Delete by Id
            repository.Save( record );
            Assert.AreEqual( 1, repository.Count() );

            repository.Delete( record );
            Assert.AreEqual( 0, repository.Count() );
            #endregion

            #region Delete by DeveloperName
            repository.Save( record );
            Assert.AreEqual( 1, repository.Count() );

            repository.Delete( record.DeveloperName );
            Assert.AreEqual( 0, repository.Count() );
            #endregion

            #region Delete by ProductName
            repository.Save( record );
            Assert.AreEqual( 1, repository.Count() );

            repository.Delete( record.ProductName );
            Assert.AreEqual( 0, repository.Count() );
            #endregion

            #region Delete by ArticulationName
            repository.Save( record );
            Assert.AreEqual( 1, repository.Count() );

            repository.Delete( record.ArticulationName );
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