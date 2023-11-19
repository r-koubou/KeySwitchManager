using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Storage.KeySwitches;
using KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches.Export;
using KeySwitchManager.Testing.Commons.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Export;

using NUnit.Framework;

namespace KeySwitchManager.Testing.Storage.Yaml.KeySwitches
{
    [TestFixture]
    public class YamlExportTest
    {
        private static readonly string TestDirectory = TestContext.CurrentContext.TestDirectory;
        private static readonly string TestOutputDirectory = Path.Combine( TestDirectory, $"{nameof( YamlExportTest )}_Output" );

        [Test]
        public void ExportTest()
        {
            var outputDirectory = Path.Combine( TestOutputDirectory, nameof( ExportTest ) );

            var keySwitches = new List<KeySwitch>
            {
                TestDataGenerator.CreateKeySwitch( productName: "Product1" ),
                TestDataGenerator.CreateKeySwitch( productName: "Product2" ),
            };


            var contentWriterFactory = new YamlExportContentFileWriterFactory( new DirectoryPath( outputDirectory ) );
            var exportContentFactory = new YamlExportContentFactory();
            var strategy = new SingleExportStrategy( contentWriterFactory, exportContentFactory );

            Assert.DoesNotThrowAsync( async () => await strategy.ExportAsync( keySwitches ) );
        }

        [Test]
        public void ExportMultipleTest()
        {
            var outputDirectory = Path.Combine( TestOutputDirectory, nameof( ExportMultipleTest ) );

            var keySwitches = new List<KeySwitch>
            {
                TestDataGenerator.CreateKeySwitch( productName: "Product1" ),
                TestDataGenerator.CreateKeySwitch( productName: "Product2" ),
            };

            var contentWriterFactory = new YamlExportContentFileWriterFactory( new DirectoryPath( outputDirectory ) );
            var exportContentFactory = new YamlExportContentFactory();
            var strategy = new MultipleExportStrategy( contentWriterFactory, exportContentFactory );

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

            var contentWriterFactory = new ZipExportContentFileWriterFactory( zipArchive, ".yaml" );
            var exportContentFactory = new YamlExportContentFactory();
            var strategy = new MultipleExportStrategy( contentWriterFactory, exportContentFactory );

            Assert.DoesNotThrowAsync( async () => await strategy.ExportAsync( keySwitches ) );
        }
    }
}
