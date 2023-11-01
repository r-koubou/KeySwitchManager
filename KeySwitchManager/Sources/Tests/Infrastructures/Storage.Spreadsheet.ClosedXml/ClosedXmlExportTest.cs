using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Storage.KeySwitches;
using KeySwitchManager.Infrastructures.Storage.Spreadsheet.ClosedXml.KeySwitches.Export;
using KeySwitchManager.Testing.Commons.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Export;

using NUnit.Framework;

namespace KeySwitchManager.Testing.Storage.Spreadsheet.ClosedXml
{
    [TestFixture]
    public class ClosedXmlExportTest
    {
        private static readonly string TestDirectory = TestContext.CurrentContext.TestDirectory;
        private static readonly string TestOutputDirectory = Path.Combine( TestDirectory, $"{nameof( ClosedXmlExportTest )}_Output" );

        [OneTimeSetUp]
        public void Setup()
        {
            var outputDirectory = new DirectoryPath( TestOutputDirectory );
            outputDirectory.CreateNew();
        }

        [Test]
        public void ExportTest()
        {
            var outputDirectory = Path.Combine( TestOutputDirectory, nameof( ExportTest ) );
            var keyswitch = TestDataGenerator.CreateKeySwitch();

            var contentWriterFactory = new ClosedXmlExportContentFileWriterFactory( new DirectoryPath( outputDirectory ) );
            var exportContentFactory = new ClosedXmlExportContentFactory();
            var strategy = new SingleExportStrategy( contentWriterFactory, exportContentFactory );

            Assert.DoesNotThrowAsync( async () => await strategy.ExportAsync( new[] { keyswitch } ) );
        }

        [Test]
        public void ExportGroupedTest()
        {
            var keySwitches = new List<KeySwitch>
            {
                TestDataGenerator.CreateKeySwitch( productName: "product", instrumentName: "instrument1" ),
                TestDataGenerator.CreateKeySwitch( productName: "product", instrumentName: "instrument2" ),
                TestDataGenerator.CreateKeySwitch( productName: "product", instrumentName: "instrument3" ),
            };

            var outputDirectory = new DirectoryPath( Path.Combine( TestOutputDirectory, nameof( ExportGroupedTest ) ) );

            var contentWriterFactory
                = new ClosedXmlExportContentFileWriterFactory(
                    new ClosedXmlGroupedExportPathBuilder( outputDirectory )
                );
            var exportContentFactory = new ClosedXmlExportContentFactory();
            var strategy = new GroupedExportStrategy( contentWriterFactory, exportContentFactory);

            Assert.DoesNotThrowAsync( async () => await strategy.ExportAsync( keySwitches ) );
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

            var contentWriterFactory = new ZipExportContentFileWriterFactory( zipArchive, new ClosedXmlGroupedExportPathBuilder() );
            var exportContentFactory = new ClosedXmlExportContentFactory();
            var strategy = new GroupedExportStrategy( contentWriterFactory, exportContentFactory );

            Assert.DoesNotThrowAsync( async () => await strategy.ExportAsync( keySwitches ) );
        }
    }
}
