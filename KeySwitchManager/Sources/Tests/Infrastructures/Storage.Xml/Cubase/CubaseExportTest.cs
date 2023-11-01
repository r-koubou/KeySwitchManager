using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Domain.MidiMessages.Models.Aggregations;
using KeySwitchManager.Domain.MidiMessages.Models.Factory;
using KeySwitchManager.Infrastructures.Storage.KeySwitches;
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

        [Test]
        public void ExportZipTest()
        {
            var outputDirectory = new DirectoryPath( Path.Combine( TestOutputDirectory, nameof( ExportZipTest ) ));
            outputDirectory.CreateNew();

            var zipFilePath = Path.Combine( outputDirectory.Path, "test.zip" );
            var info = new FileInfo( zipFilePath );
            if( info.Exists )
            {
                info.Delete();
            }

            var keySwitches = new List<KeySwitch>
            {
                TestDataGenerator.CreateKeySwitch( productName: "Product1" ),
                TestDataGenerator.CreateKeySwitch( productName: "Product2" ),
            };

            using var zipArchive = ZipFile.Open( zipFilePath, ZipArchiveMode.Create );

            var contentWriterFactory = new ZipExportContentFileWriterFactory( zipArchive, ".xml" );
            var exportContentFactory = new CubaseExportContentFactory();
            var strategy = new MultipleExportStrategy( contentWriterFactory, exportContentFactory );

            Assert.DoesNotThrowAsync( async () => await strategy.ExportAsync( keySwitches ) );
        }
    }
}
