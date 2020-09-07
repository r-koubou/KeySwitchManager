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
        private string? WorkingDirectory;

        [SetUp]
        public void Setup()
        {
            WorkingDirectory = Path.GetDirectoryName( TestContext.CurrentContext.TestDirectory );
        }

        [Test]
        public void AddTest()
        {
            var dbPath = Path.Combine( WorkingDirectory, "test.db" );
            var repository = new LiteDatabaseRepository( dbPath );
            var factory = new IArticulationFactory.DefaultFactory();
            var date = DateTimeHelper.NowUtc();
            var record = factory.Create(
                Guid.NewGuid().ToString("N"),
                date,
                date,
                "DeloperName",
                "ProductName",
                "Power Chord",
                ArticulationType.Direction,
                0,
                0
            );

            repository.Save( record );
            System.Console.WriteLine( dbPath );
        }
    }
}