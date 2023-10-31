using System.Collections.Generic;
using System.IO;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.MidiMessages.Models.Aggregations;
using KeySwitchManager.Domain.MidiMessages.Models.Factory;
using KeySwitchManager.Infrastructures.Storage.Xml.KeySwitches.Cubase;
using KeySwitchManager.Testing.Commons.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Export;

using NUnit.Framework;

namespace KeySwitchManager.Testing.Storage.Xml.Cubase
{
    [TestFixture]
    public class CubaseExportTest
    {
        private static readonly string TestDirectory = TestContext.CurrentContext.TestDirectory;
        private static readonly string TestOutputDirectory = Path.Combine( TestDirectory, $"{nameof( CubaseExportTest )}_Output" );

        [Test]
        public void ExportTest()
        {
            var outputDirectory = Path.Combine( TestOutputDirectory, nameof( ExportTest ) );

            var articulation = TestDataGenerator.CreateArticulation(
                new List<MidiNoteOn>
                {
                    IMidiNoteOnFactory.Default.Create( 10, 20 )
                },
                new List<MidiControlChange>(),
                new List<MidiProgramChange>()
            );

            var keySwitch = TestDataGenerator.CreateKeySwitch( articulation );

            var contentWriterFactory = new CubaseExportContentFileWriterFactory( new DirectoryPath( outputDirectory ) );
            var exportContentFactory = new CubaseExportContentFactory();
            var strategy = new SingleExportStrategy( contentWriterFactory, exportContentFactory );

            Assert.DoesNotThrowAsync( async () => await strategy.ExportAsync( new[] { keySwitch } ) );

        }
    }
}
