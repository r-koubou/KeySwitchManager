using System.Collections.Generic;
using System.IO;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Storage.Spreadsheet.ClosedXml.KeySwitches;
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

            IExportContentWriterFactory contentWriterFactory = new ClosedXmlExportContentFileWriterFactory( new DirectoryPath( outputDirectory ) );
            IExportContentFactory exportContentFactory = new ClosedXmlExportContentFactory();
            IExportStrategy strategy = new SingleExportStrategy( contentWriterFactory, exportContentFactory );

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

            IExportContentWriterFactory contentWriterFactory
                = new ClosedXmlExportContentFileWriterFactory(
                    new ClosedXmlGroupedExportPathBuilder( ".xlsx", outputDirectory )
                );
            IExportContentFactory exportContentFactory = new ClosedXmlExportContentFactory();
            IExportStrategy strategy = new GroupedExportStrategy( contentWriterFactory, exportContentFactory);

            Assert.DoesNotThrowAsync( async () => await strategy.ExportAsync( keySwitches ) );
        }
    }
}
