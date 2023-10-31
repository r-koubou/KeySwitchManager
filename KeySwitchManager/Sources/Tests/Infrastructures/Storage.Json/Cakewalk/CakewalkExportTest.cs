using System;
using System.Collections.Generic;
using System.IO;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Storage.Json.KeySwitches.Cakewalk;
using KeySwitchManager.Testing.Commons.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Export;

using NUnit.Framework;

namespace KeySwitchManager.Testing.Storage.Xml.Cakewalk
{
    [TestFixture]
    public class CakewalkExportTest
    {
        private static readonly string TestDirectory = TestContext.CurrentContext.TestDirectory;
        private static readonly string TestOutputDirectory = Path.Combine( TestDirectory, $"{nameof( CakewalkExportTest )}_Output" );


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

            var contentWriterFactory = new CakewalkExportContentFileWriterFactory( new DirectoryPath( outputDirectory ) );
            var exportContentFactory = new CakewalkExportContentFactory();
            var strategy = new SingleExportStrategy( contentWriterFactory, exportContentFactory );

            Assert.DoesNotThrowAsync( async () => await strategy.ExportAsync( new[] { keyswitch } ) );
        }

        [Test]
        public void ExportMultipleTest()
        {
            var keySwitches = new List<KeySwitch>
            {
                TestDataGenerator.CreateKeySwitch( productName: "Product1" ),
                TestDataGenerator.CreateKeySwitch( productName: "Product2" ),
            };

            var outputDirectory = Path.Combine( TestOutputDirectory, nameof( ExportMultipleTest ) );

            var contentWriterFactory = new CakewalkExportContentFileWriterFactory( new DirectoryPath( outputDirectory ) );
            var exportContentFactory = new CakewalkExportContentFactory();
            var strategy = new MultipleExportStrategy( contentWriterFactory, exportContentFactory);

            Assert.DoesNotThrowAsync( async () => await strategy.ExportAsync( keySwitches ) );
        }

        [Test]
        public void Export_Fail_ElementSizeOverThan_2_To_SingleFile_Test()
        {
            var outputDirectory = Path.Combine( TestOutputDirectory, nameof( Export_Fail_ElementSizeOverThan_2_To_SingleFile_Test ) );
            var keySwitches = new List<KeySwitch>
            {
                TestDataGenerator.CreateKeySwitch(),
                TestDataGenerator.CreateKeySwitch(),
            };

            var contentWriterFactory = new CakewalkExportContentFileWriterFactory( new DirectoryPath( outputDirectory ) );
            var exportContentFactory = new CakewalkExportContentFactory();
            var strategy = new SingleExportStrategy( contentWriterFactory, exportContentFactory );

            Assert.ThrowsAsync<ArgumentException>( async () => await strategy.ExportAsync( keySwitches ) );
        }
    }
}
