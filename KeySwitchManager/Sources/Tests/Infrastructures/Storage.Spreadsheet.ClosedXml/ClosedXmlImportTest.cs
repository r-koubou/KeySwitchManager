using System.IO;
using System.Linq;
using System.Threading.Tasks;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Infrastructures.Storage.KeySwitches.Import;
using KeySwitchManager.Infrastructures.Storage.Spreadsheet.ClosedXml.KeySwitches.Import;
using KeySwitchManager.UseCase.KeySwitches.Import;

using NUnit.Framework;

namespace KeySwitchManager.Testing.Storage.Spreadsheet.ClosedXml
{
    [TestFixture]
    public class ClosedXmlImportTest
    {
        private static readonly string TestDirectory = TestContext.CurrentContext.TestDirectory;
        private static readonly string TestInputDirectory = Path.Combine( TestDirectory, "TestData" );

        [Test]
        public async Task ImportTest()
        {
            var importPath = new FilePath( Path.Combine( TestInputDirectory, "ImportTestData.xlsx" ) );

            IContent content = new FileContent( importPath );
            IImportContentReader reader = new ClosedXmlImportContentReader();
            var keySwitches = await reader.ReadAsync( content );

            var x = keySwitches.FirstOrDefault();
            Assert.IsNotNull( x );
        }
    }
}
