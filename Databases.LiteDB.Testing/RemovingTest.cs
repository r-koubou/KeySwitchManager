using System;
using System.IO;

using ArticulationManager.Common.Utilities;
using ArticulationManager.Databases.LiteDB.Articulations;
using ArticulationManager.Databases.LiteDB.Articulations.Model;
using ArticulationManager.Databases.LiteDB.Articulations.Service;
using ArticulationManager.Domain.Articulations;
using ArticulationManager.Domain.Articulations.Value;

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
            var toModelTranslator = new ArticulationModelTranslationService();
            var toTranslator = new ArticulationTranslationService();

            var date = DateTimeHelper.NowUtc();

            var articulation = TestDataGenerator.CreateDummy();

            repository.Save( articulation );
            Assert.AreEqual( 1,repository.Count() );

            foreach( var a in repository.Find<ArticulationModel>( x => true ) )
            {
                repository.Delete( toModelTranslator.Translate( a ) );
            }
            Assert.AreEqual( 0, repository.Count() );

        }
    }
}