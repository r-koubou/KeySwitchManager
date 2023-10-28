using System;
using System.Collections.Generic;
using System.IO;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Storage.Json.KeySwitches.Cakewalk;
using KeySwitchManager.Infrastructures.Storage.KeySwitches;
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
            var outputPath = Path.Combine( TestOutputDirectory, $"{nameof( ExportTest )}.json" );
            var keyswitch = TestDataGenerator.CreateKeySwitch();
            IExportContentWriter contentWriter = new FileExportContentWriter( new FilePath( outputPath ) );
            IExportContentFactory exportContentFactory = new CakewalkExportContentFactory();
            IExportStrategy strategy = new SingleExportStrategy( contentWriter, exportContentFactory );

            Assert.DoesNotThrowAsync( async () => await strategy.ExportAsync( new[] { keyswitch } ) );

            Console.WriteLine( File.ReadAllText( outputPath ) );
        }

        [Test]
        public void ExportMultipleTest()
        {
            var keySwitches = new List<KeySwitch>
            {
                TestDataGenerator.CreateKeySwitch( developerName: "TestDev1" ),
                TestDataGenerator.CreateKeySwitch( developerName: "TestDev2" ),
            };

            var outputDirectory = Path.Combine( TestOutputDirectory, nameof( ExportMultipleTest ) );

            IExportContentWriterFactory<KeySwitch> contentWriterFactory = new KeySwitchExportContentFileWriterFactory( ".json", new DirectoryPath( outputDirectory ) );
            IExportContentFactory exportContentFactory = new CakewalkExportContentFactory();
            IExportStrategy strategy = new MultipleExportStrategy( contentWriterFactory, exportContentFactory);

            Assert.DoesNotThrowAsync( async () => await strategy.ExportAsync( keySwitches ) );
        }

        [Test]
        public void Export_Fail_ElementSizeOverThan_2_To_SingleFile_Test()
        {
            var outputPath = Path.Combine( TestOutputDirectory, $"{nameof( ExportTest )}.json" );
            var keySwitches = new List<KeySwitch>
            {
                TestDataGenerator.CreateKeySwitch(),
                TestDataGenerator.CreateKeySwitch(),
            };

            IExportContentWriter contentWriter = new FileExportContentWriter( new FilePath( outputPath ) );
            IExportContentFactory exportContentFactory = new CakewalkExportContentFactory();
            IExportStrategy strategy = new SingleExportStrategy( contentWriter, exportContentFactory );

            Assert.ThrowsAsync<ArgumentException>( async () => await strategy.ExportAsync( keySwitches ) );
        }
    }
}
