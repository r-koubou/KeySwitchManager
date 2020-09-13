using System.IO;

using ArticulationManager.Common.Testing;
using ArticulationManager.Databases.LiteDB.Articulations;
using ArticulationManager.Interactors.Articulations.Exporting.Text;
using ArticulationManager.Json.Articulations.Translations;
using ArticulationManager.Presenters.Articulations;
using ArticulationManager.UseCases.Articulations.Exporting.Text;

using NUnit.Framework;

namespace ArticulationManager.Interactors.Testing.Articulations
{
    [TestFixture]
    public class ExportingJsonInteractorTest
    {
        [Test]
        public void ExportTest()
        {
            #region Adding To DB
            var dbRepository = new LiteDbArticulationRepository( new MemoryStream() );
            var entity = TestDataGenerator.CreateDummy();
            dbRepository.Save( entity );
            #endregion

            var inputData = new InputData( entity.DeveloperName.Value, entity.ProductName.Value );
            var interactor = new ExportingJsonInteractor(
                dbRepository,
                new IExportingTextPresenter.Console(),
                new EntityTranslator()
            );

            interactor.Execute( inputData );
        }
    }
}