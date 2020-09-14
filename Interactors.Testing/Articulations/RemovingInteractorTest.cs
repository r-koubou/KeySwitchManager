using System;
using System.Collections.Generic;
using System.IO;

using ArticulationManager.Common.Testing;
using ArticulationManager.Databases.LiteDB.Articulations;
using ArticulationManager.Domain.Articulations.Aggregate;
using ArticulationManager.Domain.Articulations.Value;
using ArticulationManager.Domain.Commons;
using ArticulationManager.Domain.MidiMessages.Aggregate;
using ArticulationManager.Domain.Services;
using ArticulationManager.Interactors.Articulations.Removing;
using ArticulationManager.Presenters.Articulations;
using ArticulationManager.UseCases.Articulations.Removing;

using NUnit.Framework;

namespace ArticulationManager.Interactors.Testing.Articulations
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