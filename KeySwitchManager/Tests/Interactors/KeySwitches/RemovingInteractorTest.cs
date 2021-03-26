using System;
using System.IO;

using Database.LiteDB.KeySwitch.KeySwitch;

using KeySwitchManager.Common.Testing.KeySwitches;
using KeySwitchManager.Interactors.KeySwitches.Removing;
using KeySwitchManager.Presenters.KeySwitch;
using KeySwitchManager.UseCases.KeySwitches.Removing;

using NUnit.Framework;

namespace KeySwitchManager.Interactors.Testing.KeySwitches
{
    [TestFixture]
    public class RemovingInteractorTest
    {
        [Test]
        public void RemovingTest()
        {
            const string developerName = "Developer";
            const string productName = "Product";
            const string instrumentName = "E.Guitar";

            var inputData = new RemovingRequest(
                developerName,
                productName,
                instrumentName
            );

            var presenter = new IRemovingPresenter.Console();
            var repository = new LiteDbKeySwitchRepository( new MemoryStream() );
            var interactor = new RemovingInteractor( repository, presenter );

            #region Adding Test data for removing
            var now = DateTime.Now;
            var entity = TestDataGenerator.CreateKeySwitch( inputData.DeveloperName, inputData.ProductName );

            repository.Save( entity );
            #endregion
            Assert.AreEqual( 1, repository.Count() );

            interactor.Execute( inputData );

            Assert.AreEqual( 0, repository.Count() );
        }
    }
}