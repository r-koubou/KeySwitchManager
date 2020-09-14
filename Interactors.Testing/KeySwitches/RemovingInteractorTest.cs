using System;
using System.IO;

using ArticulationManager.Common.Testing;
using ArticulationManager.Databases.LiteDB.KeySwitches;
using ArticulationManager.Interactors.KeySwitches.Removing;
using ArticulationManager.Presenters.KeySwitches;
using ArticulationManager.UseCases.KeySwitches.Removing;

using NUnit.Framework;

namespace ArticulationManager.Interactors.Testing.KeySwitches
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