using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Storage.KeySwitches;
using KeySwitchManager.Infrastructures.Storage.Xml.KeySwitches.StudioOne;
using KeySwitchManager.Testing.Commons.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Export;

using NUnit.Framework;

namespace KeySwitchManager.Testing.Storage.Xml.StudioOne
{
    [TestFixture]
    public class StudioOneExportTest
    {
        private static readonly string TestDirectory = TestContext.CurrentContext.TestDirectory;
        private static readonly string TestOutputDirectory = Path.Combine( TestDirectory, $"{nameof( StudioOneExportTest )}_Output" );

        [Test]
        public void ExportTest()
        {
            var keySwitches = new List<KeySwitch>
            {
                TestDataGenerator.CreateKeySwitch( productName: "product", instrumentName: "instrument1" ),
                TestDataGenerator.CreateKeySwitch( productName: "product", instrumentName: "instrument2" ),
                TestDataGenerator.CreateKeySwitch( productName: "product", instrumentName: "instrument3" ),
            };

            var outputDirectory = new DirectoryPath( Path.Combine( TestOutputDirectory, nameof( ExportTest ) ) );

            var contentWriterFactory
                = new StudioOneExportContentFileWriterFactory(
                    new StudioOneGroupedExportPathBuilder( outputDirectory )
                );
            var exportContentFactory = new StudioOneExportContentFactory();
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

            var contentWriterFactory = new ZipExportContentFileWriterFactory( zipArchive, new StudioOneGroupedExportPathBuilder() );
            var exportContentFactory = new StudioOneExportContentFactory();
            var strategy = new GroupedExportStrategy( contentWriterFactory, exportContentFactory );

            Assert.DoesNotThrowAsync( async () => await strategy.ExportAsync( keySwitches ) );
        }
    }
}
