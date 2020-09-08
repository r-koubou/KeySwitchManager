using System;
using System.IO;

using ArticulationManager.Common.Utilities;
using ArticulationManager.Databases.LiteDB.Articulations;
using ArticulationManager.Domain.Articulations;
using ArticulationManager.Domain.Articulations.Value;

using NUnit.Framework;

namespace ArticulationManager.Databases.LiteDB.Testing
{
    [TestFixture]
    public class AddingTest
    {
        [Test]
        public void AddTest()
        {
            var repository = new LiteDbArticulationRepository( new MemoryStream() );
            var record = TestDataGenerator.CreateDummy();

            repository.Save( record );
        }
    }
}