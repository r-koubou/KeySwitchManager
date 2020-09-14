using System.IO;

using ArticulationManager.Common.Testing;
using ArticulationManager.Databases.LiteDB.KeySwitches;
using ArticulationManager.Interactors.KeySwitches.Exporting.Text;
using ArticulationManager.Json.KeySwitches.Translations;
using ArticulationManager.Presenters.KeySwitches;
using ArticulationManager.UseCases.KeySwitches.Exporting.Text;

using NUnit.Framework;

namespace ArticulationManager.Interactors.Testing.KeySwitches
{
    [TestFixture]
    public class ExportingJsonInteractorTest
    {
        [Test]
        public void ExportTest()
        {
            #region Adding To DB
            var dbRepository = new LiteDbKeySwitchRepository( new MemoryStream() );
            var entity = TestDataGenerator.CreateKeySwitch();
            dbRepository.Save( entity );
            #endregion

            var inputData = new InputData( entity.DeveloperName.Value, entity.ProductName.Value );
            var interactor = new ExportingJsonInteractor(
                dbRepository,
                new IExportingTextPresenter.Console(),
                new EntityToJsonModel()
            );

            interactor.Execute( inputData );
        }
    }
}