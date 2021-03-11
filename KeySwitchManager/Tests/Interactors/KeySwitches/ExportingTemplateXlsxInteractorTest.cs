using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Interactors.KeySwitches.Exporting;
using KeySwitchManager.UseCases.KeySwitches.Exporting;
using KeySwitchManager.Xlsx.KeySwitches.ClosedXml;

using NUnit.Framework;

namespace KeySwitchManager.Interactors.Testing.KeySwitches
{
    [TestFixture]
    public class ExportingTemplateXlsxInteractorTest
    {
        [Test]
        public void ExportTest()
        {
            var xlsxRepository = new XlsxExportingRepository( new DirectoryPath( "." ) );
            var interactor = new ExportingTemplateXlsxInteractor( xlsxRepository );
            var response = interactor.Execute( new ExportingTemplateXlsxRequest() );

            Assert.IsTrue( response.Result );

        }
    }
}