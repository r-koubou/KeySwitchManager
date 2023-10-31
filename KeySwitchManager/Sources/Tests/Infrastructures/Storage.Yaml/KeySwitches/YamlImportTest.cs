using System.IO;
using System.Linq;
using System.Threading.Tasks;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Infrastructures.Storage.KeySwitches.Import;
using KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches.Import;
using KeySwitchManager.UseCase.KeySwitches.Import;

using NUnit.Framework;

namespace KeySwitchManager.Testing.Storage.Yaml.KeySwitches
{
    [TestFixture]
    public class YamlImportTest
    {
        private static readonly string TestDirectory = TestContext.CurrentContext.TestDirectory;
        private static readonly string TestInputDirectory = Path.Combine( TestDirectory, "KeySwitches", "TestData" );

        [Test]
        public async Task ImportTest()
        {
            var importPath = new FilePath( Path.Combine( TestInputDirectory, "ImportTestData.yaml" ) );

            IContent content = new FileContent( importPath );
            IImportContentReader reader = new YamlImportContentReader();
            var keySwitches = await reader.ReadAsync( content );

            var x = keySwitches.FirstOrDefault();
            Assert.IsNotNull( x );
        }
    }
}
