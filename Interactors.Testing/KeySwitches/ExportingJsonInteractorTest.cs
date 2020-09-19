using System;
using System.IO;

using Databases.LiteDB.KeySwitches.KeySwitches;

using KeySwitchManager.Common.Testing;
using KeySwitchManager.Interactors.KeySwitches.Exporting;
using KeySwitchManager.Json.KeySwitches.Translations;
using KeySwitchManager.Presenters.KeySwitches;
using KeySwitchManager.UseCases.KeySwitches.Exporting;

using NUnit.Framework;

namespace KeySwitchManager.Interactors.Testing.KeySwitches
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

            var inputData = new ExportingTextRequest( entity.DeveloperName.Value, entity.ProductName.Value );
            var interactor = new ExportingJsonInteractor(
                dbRepository,
                new KeySwitchListListToJsonModelList{ Formatted = true },
                new IExportingTextPresenter.Console()
            );

            var response = interactor.Execute( inputData );
            Console.WriteLine( response );
        }
    }
}