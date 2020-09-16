using System;
using System.IO;

using KeySwitchManager.Common.Testing;
using KeySwitchManager.Databases.LiteDB.KeySwitches;
using KeySwitchManager.Interactors.KeySwitches.Removing;
using KeySwitchManager.Presenters.KeySwitches;
using KeySwitchManager.UseCases.KeySwitches.Removing;

using NUnit.Framework;

namespace KeySwitchManager.Interactors.Testing.KeySwitches
{
    [TestFixture]
    public class RemovingInteractorTest
    {
        [Test]
        public void AddingTest()
        {
            const string developerName = "Developer";
            const string productName = "Product";

            var inputData = new InputData(
                developerName,
                productName
            );

            var presenter = new IRemovingPresenter.Null();
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