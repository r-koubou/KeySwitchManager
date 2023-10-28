using System.Collections.Generic;
using System.IO;
using System.Linq;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Storage.KeySwitches;
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

            IExportContentWriterFactory contentWriterFactory = new KeySwitchExportContentFileWriterFactory( ".xlsx", new DirectoryPath( outputDirectory ) );
            IExportContentFactory exportContentFactory = new ClosedXmlExportContentFactory();
            IExportStrategy strategy = new SingleExportStrategy( contentWriterFactory, exportContentFactory );

            Assert.DoesNotThrowAsync( async () => await strategy.ExportAsync( new[] { keyswitch } ) );
        }

        [Test]
        public void ExportGroupedTest()
        {
            var keySwitches = new List<KeySwitch>
            {
                TestDataGenerator.CreateKeySwitch( instrumentName: "instrument1" ),
                TestDataGenerator.CreateKeySwitch( instrumentName: "instrument2" ),
                TestDataGenerator.CreateKeySwitch( instrumentName: "instrument3" ),
            };

            var outputDirectory = Path.Combine( TestOutputDirectory, nameof( ExportGroupedTest ) );

            IExportContentWriterFactory contentWriterFactory
                = new KeySwitchExportContentFileWriterFactory(
                    ".xlsx",
                    new DirectoryPath( outputDirectory ),
                    ( items ) => items.First()
                );
            IExportContentFactory exportContentFactory = new ClosedXmlExportContentFactory();
            IExportStrategy strategy = new GroupedExportStrategy( contentWriterFactory, exportContentFactory);

            Assert.DoesNotThrowAsync( async () => await strategy.ExportAsync( keySwitches ) );
        }
        //
        // [Test]
        // public void Export_Fail_ElementSizeOverThan_2_To_SingleFile_Test()
        // {
        //     var outputDirectory = Path.Combine( TestOutputDirectory, nameof( Export_Fail_ElementSizeOverThan_2_To_SingleFile_Test ) );
        //     var keySwitches = new List<KeySwitch>
        //     {
        //         TestDataGenerator.CreateKeySwitch(),
        //         TestDataGenerator.CreateKeySwitch(),
        //     };
        //
        //     IExportContentWriterFactory contentWriterFactory = new KeySwitchExportContentFileWriterFactory( ".json", new DirectoryPath( outputDirectory ) );
        //     IExportContentFactory exportContentFactory = new CakewalkExportContentFactory();
        //     IExportStrategy strategy = new SingleExportStrategy( contentWriterFactory, exportContentFactory );
        //
        //     Assert.ThrowsAsync<ArgumentException>( async () => await strategy.ExportAsync( keySwitches ) );
        // }
    }
}
