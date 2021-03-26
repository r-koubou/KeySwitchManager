using System;
using System.IO;

using Database.LiteDB.KeySwitches;

using KeySwitchManager.Common.Testing.KeySwitches;
using KeySwitchManager.Interactors.KeySwitches.Searching;
using KeySwitchManager.Json.KeySwitches.Translators;
using KeySwitchManager.Presenters.KeySwitches;
using KeySwitchManager.UseCases.KeySwitches.Searching;

using NUnit.Framework;

namespace KeySwitchManager.Interactors.Testing.KeySwitches
{
    [TestFixture]
    public class SearchingInteractorTest
    {
        [Test]
        public void ExportTest()
        {
            #region Adding To DB
            var dbRepository = new LiteDbKeySwitchRepository( new MemoryStream() );
            var entity = TestDataGenerator.CreateKeySwitch();
            dbRepository.Save( entity );
            #endregion

            var inputData = new SearchingRequest( entity.DeveloperName.Value, entity.ProductName.Value );
            var interactor = new SearchingInteractor(
                dbRepository,
                new KeySwitchListListToJsonModelList{ Formatted = true },
                new ISearchingPresenter.Console()
            );

            var response = interactor.Execute( inputData );
            Console.WriteLine( response );
        }
    }
}