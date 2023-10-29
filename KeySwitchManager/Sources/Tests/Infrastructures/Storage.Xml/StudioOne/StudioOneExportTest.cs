using System.Collections.Generic;
using System.IO;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;
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

            IExportContentWriterFactory contentWriterFactory
                = new StudioOneExportContentFileWriterFactory(
                    new StudioOneGroupedExportPathBuilder( outputDirectory )
                );
            IExportContentFactory exportContentFactory = new StudioOneExportContentFactory();
            IExportStrategy strategy = new GroupedExportStrategy( contentWriterFactory, exportContentFactory);

            Assert.DoesNotThrowAsync( async () => await strategy.ExportAsync( keySwitches ) );
        }
    }
}
